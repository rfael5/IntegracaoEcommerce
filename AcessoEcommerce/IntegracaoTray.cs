using System.Globalization;
using System.Text.Json;
using RestSharp;

public class IntegracaoTray
{
    public readonly AcessoTPA _acessoTpa;
    public EcommerceAuthService _authService;

    public IntegracaoTray(AcessoTPA acessoTpa, EcommerceAuthService authService)
    {
        _acessoTpa = acessoTpa;
        _authService = authService;
    }

    public record InfoDataEntrega
    {
        public DateTime dataEntrega { get; init; }
        public string horaEntrega { get; init; }        
    }

    //     public async Task<string?> Authorize()
    //     {
    //     var client = new RestClient($"{api_address}/auth");
    //     var request = new RestRequest()
    //         .AddParameter("consumer_key", consumer_key)
    //         .AddParameter("consumer_secret", consumer_secret)
    //         .AddParameter("code", code);
    //     try
    //     {
    //         var response = client.Post(request);
    //         Console.WriteLine(response.Content);
    //         using var doc = JsonDocument.Parse(response.Content);
    //         var content = doc.RootElement;
    //         access_token = JsonExtensions.RequireString(content, "access_token");
    //         refresh_token = JsonExtensions.RequireString(content, "refresh_token");

    //         return response.Content;
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         throw;
    //     }
    // }

    // public async Task<AuthResponse> Refresh()
    // {
    //     try
    //     {
    //         var refresh = new RestClient($"{api_address}/auth");
    //         var request = new RestRequest()
    //             .AddParameter("refresh_token", refresh_token);

    //         var response = refresh.Get(request);

    //         var refreshResponse = JsonSerializer.Deserialize<AuthResponse>(response.Content);
    //         access_token = refreshResponse.access_token;
    //         refresh_token = refreshResponse.refresh_token;

    //         return refreshResponse;
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e.Message);
    //         throw;
    //     }
    // }

    public async Task<string> GetCarrinho()
    {
        //mgbphccu72k56v9jmvrph8am6q
        //qkgnh42gloahabam2iqn8nsgse
        //d5njuo2bep2qe6j7eebjnhsci0
        //uvopc6o74u9g6ukaegfone3ni7
        var session_id = "d5njuo2bep2qe6j7eebjnhsci0";
        var request = new RestClient($"{_authService.api_address}/carts/{session_id}/complete");
        var cartRequest = new RestRequest()
            .AddParameter("access_token", _authService.access_token);
        var response = request.Get(cartRequest);
        return response.Content;
    }

    public async Task<string> TodosCarrinhos()
    {
        var request = new RestClient($"{_authService.api_address}/carts");
        var cartRequest = new RestRequest()
            .AddParameter("access_token", _authService.access_token);
        var response = request.Get(cartRequest);
        return response.Content;
    }

    public async Task RequestOrders()
    {
        Console.WriteLine("orders");
        try
        {
            //var request = new RestClient($"{_authService.api_address}/orders?status=Producao");
            var request = new RestClient($"{_authService.api_address}/orders?status=A enviar Vindi,A enviar");
            var orderRequests = new RestRequest()
                .AddParameter("access_token", _authService.access_token);
            var orderResponse = request.Get(orderRequests);

            using var doc = JsonDocument.Parse(orderResponse.Content);
            var ordersDoc = doc.RootElement;
            var orders = ordersDoc.GetProperty("Orders");
            Console.WriteLine(orders);

            foreach (var teste in orders.EnumerateArray())
            {
                var id = teste.GetProperty("Order").GetProperty("id").GetString();
                try
                {
                    var completeOrder = await GetCompleteOrder(Convert.ToInt32(id));
                    await CriarPedido(completeOrder);
                    try
                    {
                        await AtualizarStatusProntoEnvio(id);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Pedido {id} salvo no ERP, mas falhou ao atualizar na Tray.");
                        Console.WriteLine(e);
                    }
                }
                catch (InvalidOperationException invalidOp)
                {
                    Console.WriteLine("Erro operação inválida");
                    Console.WriteLine($"Pedido inválido. ID: {id}");
                    Console.WriteLine(invalidOp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public async Task GetOrders()
    {
        while (true)
        {
            if (_authService.access_token == null)
            {
                Console.WriteLine("null");
                await _authService.Authorize();
                await RequestOrders();
            }
            else if (DateTime.Now >= _authService.date_expiration_access_token)
            {
                Console.WriteLine("old");
                await _authService.Refresh();
                await RequestOrders();
            }
            else
            {
                Console.WriteLine("##########################");
                Console.WriteLine(_authService.access_token);
                Console.WriteLine("##########################");
                await RequestOrders();
            }

            await Task.Delay(600000);
        }
    }

    public async Task<string> GetCompleteOrder(int orderId)
    {
        var order = new RestClient($"{_authService.api_address}/orders/{orderId}/complete");
        var orderRequests = new RestRequest()
            .AddParameter("access_token", _authService.access_token);
        var orderResponse = order.Get(orderRequests);

        return orderResponse.Content;
    }

    public async Task<string> GetProducts()
    {
        var products = new RestClient($"{_authService.api_address}/products");
        var productsRequest = new RestRequest()
            .AddParameter("access_token", _authService.access_token);
        var productsResponse = products.Get(productsRequest);
        return productsResponse.Content;
    }

    public async Task<string> GetCustomers()
    {
        var customers = new RestClient($"{_authService.api_address}/customers");
        var customersRequest = new RestRequest()
            .AddParameter("access_token", _authService.access_token);
        var customersResponse = customers.Get(customersRequest);
        return customersResponse.Content;
    }

    private InfoDataEntrega GetDataEntrega(JsonElement extensions, ulong numeroPedido)
    {
        var additionalInfo = extensions.GetProperty("AdditionalProductInfo")[0];
        var dataInformation = JsonExtensions.RequireString(additionalInfo , "information");
        var splitted = dataInformation.Split(" ");
        Console.WriteLine("??????????????????????????????");
        foreach(var teste in splitted)
        {
            Console.WriteLine(teste);
        }
        var horaString = splitted[7][..5];
        Console.WriteLine(horaString);
        Console.WriteLine("??????????????????????????????");
        var horaFormatada = $"{horaString[0..2]}{horaString[3..5]}";
        //DateTime dataFormatada;

        if(DateTime.TryParseExact(splitted[6], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataFormatada))
        {
            Console.WriteLine($"Data formatada com sucesso: {dataFormatada}");
        }
        else
        {
            throw new Exception($"Erro ao tentar formatar data. Pedido: {numeroPedido}");
        }

        return new InfoDataEntrega()
        {
            dataEntrega = dataFormatada,
            horaEntrega = horaFormatada
        };

    }

    private async Task CriarPedido(string orderContent)
    {
        using var doc = JsonDocument.Parse(orderContent);
        var root = doc.RootElement;
        var order = root.GetProperty("Order");
        var extensions = root.GetProperty("Extensions");
        var _idPedido = ulong.Parse(JsonExtensions.RequireString(order, "id"));
        var infoDataEntrega = GetDataEntrega(extensions, _idPedido);
        Console.WriteLine("?????????????");
        Console.WriteLine(JsonSerializer.Serialize(infoDataEntrega));
        Console.WriteLine("?????????????");

        var pedido = new DadosPedido
        {
            idPedido = ulong.Parse(JsonExtensions.RequireString(order, "id")), //order.GetProperty("id").GetUInt64(),
            status = JsonExtensions.RequireString(order, "status"), //order.GetProperty("status").GetString(),
            dtInc = DateTime.Now,
            dtAlt = DateTime.Now,
            dataEntrega = infoDataEntrega.dataEntrega,
            horaEntrega = infoDataEntrega.horaEntrega,
            frete = decimal.Parse(JsonExtensions.RequireString(order, "shipment_value")),
            totalPedido = JsonExtensions.RequireString(order, "total"), //order.GetProperty("total").GetString(),
            idClienteEcommerce = ulong.Parse(JsonExtensions.RequireString(order, "customer_id")), //order.GetProperty("customer_id").GetUInt64(),
            modoEntregaDescricao = JsonExtensions.RequireString(order, "shipment") == "Retirar no local" ? "retirar-loja" : "entrega", //JsonExtensions.RequireModoEntrega(dadosEntrega_lines) == "3" ? "entrega" : "retirar-loja", //entrega
            modoEntregaId = JsonExtensions.RequireString(order, "shipment") == "Retirar no local" ? "1" : "3",//JsonExtensions.RequireModoEntrega(dadosEntrega_lines), 
            dadosEntrega = GetDadosEntrega(order),
            dadosCliente = GetDadosCliente(order),
            produtos = GetDadosProduto(order)
        };

        //Console.WriteLine(JsonSerializer.Serialize(pedido));

       await _acessoTpa.CadastrarPedidoTray(pedido);        
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
        var enderecoCliente = enderecos[0].GetProperty("CustomerAddress");
        return new DadosCliente
        {
            nomeCliente = JsonExtensions.RequireString(dadosEntrega, "name"),
            cidade = JsonExtensions.RequireString(enderecoCliente, "city"), 
            estado = JsonExtensions.RequireString(enderecoCliente, "state"), 
            pais = JsonExtensions.RequireString(enderecoCliente, "country"), 
            email = JsonExtensions.RequireString(dadosEntrega, "email"), 
            telefone = JsonExtensions.RequireString(dadosEntrega, "phone"), 
            pessoaFj =  JsonExtensions.RequireString(dadosEntrega, "cpf"), 
            dtNascimento = BrazilTime.Now().ToString(), 
            sexo = "M"
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
                idProdutoTPA = ulong.Parse(JsonExtensions.RequireString(prod, "product_id")),
                quantidade = decimal.Parse(JsonExtensions.RequireString(prod, "quantity")),
                preco = decimal.Parse(JsonExtensions.RequireString(prod, "price")),
                subtotal = decimal.Parse(JsonExtensions.RequireString(prod, "price")),
                total = decimal.Parse(JsonExtensions.RequireString(prod, "price"))
            };

            listaProdutos.Add(novoProduto);
        }

        return listaProdutos;
    }

    private async Task AtualizarStatusProntoEnvio(string orderId)
    {
        try
        {
            var request = new RestClient($"{_authService.api_address}/orders/{orderId}?access_token={_authService.access_token}");
            var requestParameters = new RestRequest()
                //.AddParameter("Order[status_id]", "1");
                .AddParameter("Order[status_id]", "353");

            var orderResponse = request.Put(requestParameters);

            Console.WriteLine(orderResponse.Content);

            // var client = new RestClient(api_address);
            // var request = new RestRequest($"/orders/{orderId}", Method.Put);

            // request.AddQueryParameter("access_token", access_token);
            // request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            // request.AddParameter("Order[status_id]", "1");

            // var response = client.Execute(request);

            // Console.WriteLine(response.Content);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task TesteFrontEcommerce()
    {
        Console.WriteLine("Comunicação ecommerce");
    }
}