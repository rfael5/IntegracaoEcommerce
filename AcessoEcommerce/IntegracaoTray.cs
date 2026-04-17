using System.Text.Json;
using RestSharp;

public class IntegracaoTray
{
        private readonly string api_address = "https://celiasouttomayor.commercesuite.com.br/web_api";
        private readonly string consumer_key = "a75943d6601451a79a1d80b8b6eb3ccd32fcf9d3e7fa2c39ad32010180e9a0ac";
        private readonly string consumer_secret = "5706705aba4d2cf32d6024adf9878926799f6ad4e777d5ebc5eafb41850db83a";
        private readonly string code = "7690ae9e20502a84af649f187a54a8203daa140aab2491eda027a79f3d8504b9";
        private string access_token = "APP_ID-8289-STORE_ID-1471881-67b15acdbdcbdc319b0d4fbab1dee9a8974897a723fecaff3200c6f26f7f224b";
        private string refresh_token = "cdc76ad6c0e79c66d6621ed9b433ac535d7b082961a1ab03a6411a512f4cf28b";

        public async Task<string?> Authorize()
        {
        var client = new RestClient($"{api_address}/auth");
        var request = new RestRequest()
            .AddParameter("consumer_key", consumer_key)
            .AddParameter("consumer_secret", consumer_secret)
            .AddParameter("code", code);
        try
        {
            var response = client.Post(request);
            Console.WriteLine(response.Content);
            using var doc = JsonDocument.Parse(response.Content);
            var content = doc.RootElement;
            access_token = JsonExtensions.RequireString(content, "access_token");
            refresh_token = JsonExtensions.RequireString(content, "refresh_token");

            return response.Content;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<AuthResponse> Refresh()
    {
        try
        {
            var refresh = new RestClient($"{api_address}/auth");
            var request = new RestRequest()
                .AddParameter("refresh_token", refresh_token);

            var response = refresh.Get(request);

            var refreshResponse = JsonSerializer.Deserialize<AuthResponse>(response.Content);
            access_token = refreshResponse.access_token;
            refresh_token = refreshResponse.refresh_token;

            return refreshResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<string> GetOrders()
    {
        var request = new RestClient($"{api_address}/orders?status=PENDENTE");
        var orderRequests = new RestRequest()
            .AddParameter("access_token", access_token);
        var orderResponse = request.Get(orderRequests);

        using var doc = JsonDocument.Parse(orderResponse.Content);
        var ordersDoc = doc.RootElement;
        var orders = ordersDoc.GetProperty("Orders");
        //Console.WriteLine(ordersList);

        foreach(var teste in orders.EnumerateArray())
        {
            var id = teste.GetProperty("Order").GetProperty("id").GetString();
            var completeOrder = await GetCompleteOrder(Convert.ToInt32(id));
            Console.WriteLine(completeOrder);
            Console.WriteLine("###############");
            //await CriarPedido(completeOrder);
        }

        return orderResponse.Content;
    }

    public async Task<string> GetCompleteOrder(int orderId)
    {
        var order = new RestClient($"{api_address}/orders/{orderId}/complete");
        var orderRequests = new RestRequest()
            .AddParameter("access_token", access_token);
        var orderResponse = order.Get(orderRequests);

        return orderResponse.Content;
    }

    public async Task<string> GetProducts()
    {
        var products = new RestClient($"{api_address}/products");
        var productsRequest = new RestRequest()
            .AddParameter("access_token", access_token);
        var productsResponse = products.Get(productsRequest);
        return productsResponse.Content;
    }

    public async Task<string> GetCustomers()
    {
        var customers = new RestClient($"{api_address}/customers");
        var customersRequest = new RestRequest()
            .AddParameter("access_token", access_token);
        var customersResponse = customers.Get(customersRequest);
        return customersResponse.Content;
    }

    private async Task CriarPedido(string orderContent)
    {
        using var doc = JsonDocument.Parse(orderContent);
        var root = doc.RootElement;
        var order = root.GetProperty("Order");

        var pedido = new DadosPedido
        {
            idPedido = JsonExtensions.RequireUInt64(order, "id"), //order.GetProperty("id").GetUInt64(),
            status = JsonExtensions.RequireString(order, "status"), //order.GetProperty("status").GetString(),
            dtInc = DateTime.Now,
            dtAlt = DateTime.Now,
            totalPedido = JsonExtensions.RequireString(order, "total"), //order.GetProperty("total").GetString(),
            idClienteEcommerce = JsonExtensions.RequireUInt64(order, "customer_id"), //order.GetProperty("customer_id").GetUInt64(),
            modoEntregaDescricao = "entrega",//JsonExtensions.RequireModoEntrega(dadosEntrega_lines) == "3" ? "entrega" : "retirar-loja", 
            modoEntregaId = "3",//JsonExtensions.RequireModoEntrega(dadosEntrega_lines), 
            dadosEntrega = GetDadosEntrega(order),
            dadosCliente = GetDadosCliente(order),
            produtos = GetDadosProduto(order)
        };
    }

    private DadosEntrega GetDadosEntrega(JsonElement order)
    {
        var dadosEntrega = order.GetProperty("Customer");

        return new DadosEntrega
        {
            rua = JsonExtensions.RequireString(dadosEntrega, "address"),  //shipping.GetProperty("address_1").GetString(),
            numero = JsonExtensions.RequireString(dadosEntrega, "number"), //shipping.TryGetProperty("number", out var n) ? n.GetString() : null,
            bairro = JsonExtensions.RequireString(dadosEntrega, "neighborhood"),  //shipping.TryGetProperty("neighborhood", out var b) ? b.GetString() : null,
            cep = JsonExtensions.RequireString(dadosEntrega, "zip_code"),  //shipping.GetProperty("postcode").GetString(),
            cidade = JsonExtensions.RequireString(dadosEntrega, "city"),  //shipping.GetProperty("city").GetString(),
            estado = JsonExtensions.RequireString(dadosEntrega, "state"),  //shipping.GetProperty("state").GetString(),
            pais = JsonExtensions.RequireString(dadosEntrega, "country"),  //shipping.GetProperty("country").GetString(),
            telefone = JsonExtensions.RequireString(dadosEntrega, "phone") //shipping.TryGetProperty("phone", out var p) ? p.GetString() : null
        };
    }

    private DadosCliente GetDadosCliente(JsonElement order)
    {
        var dadosEntrega = order.GetProperty("Customer");
        var enderecos = dadosEntrega.GetProperty("CustomerAddresses");
        var enderecoCliente = enderecos[0];
        return new DadosCliente
        {
            nomeCliente = JsonExtensions.RequireString(dadosEntrega, "name"),
            cidade = JsonExtensions.RequireString(enderecoCliente, "city"), 
            estado = JsonExtensions.RequireString(enderecoCliente, "state"), 
            cep = JsonExtensions.RequireString(enderecoCliente, "zip_code"), 
            pais = JsonExtensions.RequireString(enderecoCliente, "country"), 
            email = JsonExtensions.RequireString(dadosEntrega, "email"), 
            telefone = JsonExtensions.RequireString(dadosEntrega, "phone"), 
            celular = JsonExtensions.RequireString(dadosEntrega, "cellphone"), 
            pessoa_fj =  JsonExtensions.RequireString(dadosEntrega, "cpf"), 
            nascimento = BrazilTime.Now().ToString(), 
            genero = "N" 
        };
    }

    private List<DadosProduto> GetDadosProduto(JsonElement order)
    {
        var dadosProduto = order.GetProperty("ProductsSold");
        var listaProdutos = new List<DadosProduto>();
        foreach(var produto in dadosProduto.EnumerateArray())
        {
            var prod = produto.GetProperty("ProductsSold");
            var novoProduto = new DadosProduto
            {
                idProdutoEcommerce = ulong.Parse(JsonExtensions.RequireString(prod, "product_id")),
                nomeProduto = JsonExtensions.RequireString(prod, "name"),
                idProdutoTPA = ulong.Parse(JsonExtensions.RequireString(prod, "reference")),
                quantidade = decimal.Parse(JsonExtensions.RequireString(prod, "quantity")),
                preco = decimal.Parse(JsonExtensions.RequireString(prod, "price")),
                subtotal = decimal.Parse(JsonExtensions.RequireString(prod, "price")),
                total = decimal.Parse(JsonExtensions.RequireString(prod, "price"))
            };

            listaProdutos.Add(novoProduto);
        }

        return listaProdutos;
    }
}