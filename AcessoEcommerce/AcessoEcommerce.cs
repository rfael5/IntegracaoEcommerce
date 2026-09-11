using WooCommerceNET;
using System.Text.Json;
using System.Globalization;
using Azure.Identity;
using WooCommerceNET.WooCommerce.v3;
using System.Runtime.Intrinsics.X86;
//using WooCommerceNET.WooCommerce.Legacy;


public enum ModoEntrega
{
    Entrega = 3,
    RetirarLoja = 1
}

public class AcessoEcommerce  
{
    private readonly AcessoTPA _acessoTpa;
   
    public AcessoEcommerce(AcessoTPA acessoTpa)
    {
        _acessoTpa = acessoTpa;
    }

    private List<DadosProduto> CriarObjetoProdutos(List<LineItems> produtosPedido)
    {
        var listaProdutos = new List<DadosProduto>();
        foreach(var produto in produtosPedido)
        {
            var novoProduto = new DadosProduto()
            {
                idProdutoEcommerce = produto.id,
                nomeProduto = produto.name,
                idProdutoTPA = 17403, //produto.product_id,
                quantidade = produto.quantity,
                preco = produto.price,
                subtotal = decimal.Parse(produto.subtotal, CultureInfo.InvariantCulture),
                total = decimal.Parse(produto.total, CultureInfo.InvariantCulture)
            };
            listaProdutos.Add(novoProduto);
        }

        return listaProdutos;
    }

    private async Task<DadosPedido> CriarPedido(JsonElement order)
    {
            var shipping = order.GetProperty("shipping");
            var billing = order.GetProperty("billing");
            var line_items = JsonSerializer.Deserialize<List<LineItems>>(order.GetProperty("line_items").GetRawText());
            var shipping_lines = order.GetProperty("shipping_lines");

            //var methodId = shipping_lines.GetArrayLength() > 0 ? shipping_lines[0].GetProperty("method_id").GetString;

            var pedido = new DadosPedido
            {
                idPedido = JsonExtensions.RequireUInt64(order, "id"), //order.GetProperty("id").GetUInt64(),
                status = JsonExtensions.RequireString(order, "status"), //order.GetProperty("status").GetString(),
                dtInc = DateTime.Now,
                dtAlt = DateTime.Now,
                totalPedido = JsonExtensions.RequireString(order, "total"), //order.GetProperty("total").GetString(),
                idClienteEcommerce = JsonExtensions.RequireUInt64(order, "customer_id"), //order.GetProperty("customer_id").GetUInt64(),
                modoEntregaDescricao = JsonExtensions.RequireModoEntrega(shipping_lines) == "3" ? "entrega" : "retirar-loja", //shipping_lines[0].GetProperty("method_id").GetString() == "pickup_location" ? ModoEntrega.RetirarLoja.ToString() : ModoEntrega.Entrega.ToString(),
                modoEntregaId = JsonExtensions.RequireModoEntrega(shipping_lines), //shipping_lines[0].GetProperty("method_id").GetString() == "pickup_location" ? ModoEntrega.RetirarLoja : ModoEntrega.Entrega,
                //modoEntrega = 0,

                dadosEntrega = new DadosEntrega
                {
                    rua = JsonExtensions.RequireString(shipping, "address_1"),  //shipping.GetProperty("address_1").GetString(),
                    numero = JsonExtensions.RequireString(shipping, "number"), //shipping.TryGetProperty("number", out var n) ? n.GetString() : null,
                    bairro = JsonExtensions.RequireString(shipping, "neighborhood"),  //shipping.TryGetProperty("neighborhood", out var b) ? b.GetString() : null,
                    cep = JsonExtensions.RequireString(shipping, "postcode"),  //shipping.GetProperty("postcode").GetString(),
                    cidade = JsonExtensions.RequireString(shipping, "city"),  //shipping.GetProperty("city").GetString(),
                    estado = JsonExtensions.RequireString(shipping, "state"),  //shipping.GetProperty("state").GetString(),
                    pais = JsonExtensions.RequireString(shipping, "country"),  //shipping.GetProperty("country").GetString(),
                    telefone = JsonExtensions.RequireString(shipping, "phone") //shipping.TryGetProperty("phone", out var p) ? p.GetString() : null
                },

                dadosCliente = new DadosCliente
                {
                    nomeCliente = $"{JsonExtensions.RequireString(billing, "first_name")} {JsonExtensions.RequireString(billing, "last_name")}",
                    cidade = JsonExtensions.RequireString(billing, "city" ), // billing.GetProperty("city").GetString(),
                    estado = JsonExtensions.RequireString(billing, "state" ), // billing.GetProperty("state").GetString(),
                    pais = JsonExtensions.RequireString(billing, "country" ), // billing.GetProperty("country").GetString(),
                    email = JsonExtensions.RequireString(billing, "email" ), // billing.GetProperty("email").GetString(),
                    telefone = JsonExtensions.RequireString(billing, "phone" ), // billing.GetProperty("phone").GetString(),
                    pessoaFj = JsonExtensions.RequireString(billing, "persontype" ), // billing.GetProperty("persontype").GetString(),
                    cnpjCpf = JsonExtensions.RequireString(billing, "persontype" ) == "F" ? JsonExtensions.RequireString(billing, "cpf" ) : JsonExtensions.RequireString(billing, "cnpj" ), 
                    dtNascimento = JsonExtensions.RequireString(billing, "birthdate" ), // billing.TryGetProperty("birthdate", out var d) ? d.GetString() : null,
                    sexo = JsonExtensions.RequireString(billing, "gender" ) // billing.TryGetProperty("gender", out var g) ? g.GetString() : null
                },

                produtos = CriarObjetoProdutos(line_items)
            };

            Console.WriteLine(JsonSerializer.Serialize(pedido));
            return pedido;
            
    }

    public async Task UpdateOrder(RestAPI rest, ulong orderId)
    {
        WCObject wc = new WCObject(rest, CultureInfo.GetCultureInfo("pt-BR"));
        await wc.Order.Update(orderId, new Order { status="completed" });
    }

    public async Task GetOrders()
    {
        var _rest = new RestAPI("https://loja.souttomayorevoce.com.br/wp-json/wc/v3/", "ck_5d7419e8d01368aeb391c6618790b90a55b5592a", "cs_ebc187544b75ec02a49eae5da9d5683ea45a4e21");
        var ordersJson = await _rest.GetRestful("orders?status=processing");
        using var doc = JsonDocument.Parse(ordersJson);
        var orders = doc.RootElement;
        var ordersQtd = orders.GetArrayLength();

        var listaPedidos = new List<DadosPedido>();

        foreach (var order in orders.EnumerateArray())
        {
            var orderId = JsonExtensions.RequireUInt64(order, "id");
            try
            {
                var pedido = await CriarPedido(order);
                await _acessoTpa.CadastrarPedido(pedido);

                try
                {
                    await UpdateOrder(_rest, orderId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Pedido {orderId} salvo no ERP, mas falhou ao atualizar no Wordpress.");
                    Console.WriteLine(ex);
                }
            }
            catch (InvalidOperationException invalidOp)
            {
                Console.WriteLine("Erro operação inválida");
                Console.WriteLine($"Pedido inválido. ID: {orderId}");
                Console.WriteLine(invalidOp.Message);
                Console.WriteLine(order.GetRawText());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }

}

