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
                    cep = JsonExtensions.RequireString(billing, "postcode" ), // billing.GetProperty("postcode").GetString(),
                    pais = JsonExtensions.RequireString(billing, "country" ), // billing.GetProperty("country").GetString(),
                    email = JsonExtensions.RequireString(billing, "email" ), // billing.GetProperty("email").GetString(),
                    telefone = JsonExtensions.RequireString(billing, "phone" ), // billing.GetProperty("phone").GetString(),
                    celular = JsonExtensions.RequireString(billing, "cellphone" ), // billing.TryGetProperty("cellphone", out var c) ? c.GetString() : null,
                    pessoa_fj = JsonExtensions.RequireString(billing, "persontype" ), // billing.GetProperty("persontype").GetString(),
                    //cpf_cnpj = JsonExtensions.RequireString(billing, "persontype" ) == "F" ? JsonExtensions.RequireString(billing, "cpf" ) : JsonExtensions.RequireString(billing, "cnpj" ), 
                    nascimento = JsonExtensions.RequireString(billing, "birthdate" ), // billing.TryGetProperty("birthdate", out var d) ? d.GetString() : null,
                    genero = JsonExtensions.RequireString(billing, "gender" ) // billing.TryGetProperty("gender", out var g) ? g.GetString() : null
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

public static class JsonExtensions
{
    public static string RequireString(JsonElement el, string name)
    {
        if(!el.TryGetProperty(name, out var prop))
        {
            throw new InvalidOperationException($"Dados faltando: '{name}'");
        }

        var value = prop.GetString();

        // if(string.IsNullOrWhiteSpace(value))
        // {
        //     throw new InvalidOperationException($"Propriedade '{name}' está vazia");
        // }

        if(value == null)
        {
            throw new InvalidOperationException($"Propriedade '{name}' está vazia");
        }
        return value;
    }

    public static decimal RequireDecimal(JsonElement el, string name)
    {
        if(!el.TryGetProperty(name, out var prop))
        {
            throw new InvalidOperationException($"Dados faltando: '{name}'");
        }

        if(prop.ValueKind == JsonValueKind.Number)
        {
            return prop.GetDecimal();
        }

        if(prop.ValueKind == JsonValueKind.String && decimal.TryParse(prop.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
            return d;
        }

        throw new InvalidOperationException($"Numero inválido '{name}'");
    }

    public static ulong RequireUInt64(JsonElement el, string name)
    {
        if(!el.TryGetProperty(name, out var prop))
        {
            throw new InvalidOperationException($"Dados faltando: '{name}'");
        }

        if(prop.ValueKind == JsonValueKind.Number)
        {
            return prop.GetUInt64();
        }

        throw new InvalidOperationException($"Número inválido '{name}'");
    }

    public static string RequireModoEntrega(JsonElement shippingLines)
    {
        if(shippingLines.ValueKind != JsonValueKind.Array || shippingLines.GetArrayLength() == 0)
        {
            throw new InvalidOperationException("Modo de entrega não enviado");
        }

        var methodId = shippingLines[0].GetProperty("method_id").GetString();

        if(methodId == "pickup_location")
        {
            return "1";
        }
        else if(methodId == "flat_rate" || methodId == "free_shipping")
        {
            //return ModoEntrega.Entrega;
            return "3";
        }
        else
        {
            throw new InvalidOperationException($"Modo de entrega inválido {methodId}");
        }
    }

    public static string? GetStringSafe(this JsonElement element, string name)
    {
        if(!element.TryGetProperty(name, out var prop))
        {
            return null;
        }

        return prop.ValueKind == JsonValueKind.String ? prop.GetString() : null;
    } 

    public static decimal? GetDecimalSafe(this JsonElement element, string name)
    {
        if(!element.TryGetProperty(name, out var prop))
        {
            return null;
        }

        if(prop.ValueKind == JsonValueKind.Number)
        {
            return prop.GetDecimal();
        }

        if(prop.ValueKind == JsonValueKind.String && decimal.TryParse(prop.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
            return d;
        }

        return null;
    }

    public static ulong? GetUInt64Safe(this JsonElement element, string name)
    {
        if(!element.TryGetProperty(name, out var prop))
        {
            return null;
        }

        return prop.ValueKind == JsonValueKind.Number ? prop.GetUInt64() : null;
    }
}