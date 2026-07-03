using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using RestSharp;
using System.Text.Json;

[ApiController]
[Route("ecommerce")]
public class EcommerceController : ControllerBase
{
    private readonly IntegracaoTray _integracao;
    private readonly EcommerceAuthService _auth;

    public EcommerceController(IntegracaoTray integracao, EcommerceAuthService auth)
    {
        _integracao = integracao;
        _auth = auth;
    }

    public record DadosAgendamento
    {
        public string session_id { get; init; }
        public string data_entrega { get; init; }
    }

    [HttpPost("agendar-entrega")]
    public async Task<IActionResult> AgendarEntrega([FromBody] DadosAgendamento dadosAgendamento)
    {
        Console.WriteLine(dadosAgendamento);
        return Ok(new {message = "ok"});
    }

    [HttpGet("teste-ecommerce")]
    public async Task<IActionResult> TesteFrontEcommerce()
    {
        await _integracao.TesteFrontEcommerce();
        return Ok(new {message = "ok"});
    }

    [EnableCors("All")]
    [HttpGet("auth")]
    public async Task<IActionResult> GetTokens()
    {
        var content = await _auth.Authorize();
        return Ok(new {message = content});
    }

    [HttpGet("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        var content = await _auth.Refresh();
        return Ok(content);
    }

    [HttpGet("carrinho")]
    public async Task<IActionResult> GetCarrinho()
    {
        var res = await _integracao.GetCarrinho();
        return Ok(res);
    }

    [HttpGet("todos-carrinhos")]
    public async Task<IActionResult> GetTodosCarrinho()
    {
        var res = await _integracao.TodosCarrinhos();
        return Ok(res);
    }
    
    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders()
    {
        await _integracao.GetOrders();
        return Ok(new {message = "ok"});
    }

    [HttpGet("get-all-orders")]
    public async Task<IActionResult> GetAllOrders()
    {
        var request = new RestClient($"{_auth.api_address}/orders?status=A enviar Vindi,A enviar");
        var orderRequests = new RestRequest()
            .AddParameter("access_token", _auth.access_token);
        
        var orderResponse = request.Get(orderRequests);
        
        return Ok(orderResponse.Content);
    }

    [HttpGet("get-statuses")]
    public async Task<IActionResult> GetStatuses()
    {
        var request = new RestClient($"{_auth.api_address}/orders/statuses");
        var orderRequests = new RestRequest()
            .AddParameter("access_token", _auth.access_token);
        
        var orderResponse = request.Get(orderRequests);
        
        return Ok(orderResponse.Content);
    }

    [HttpGet("complete-order")]
    public async Task<IActionResult> GetCompleteOrder()
    {
        var order = await _integracao.GetCompleteOrder(41);
        return Ok(order);
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _integracao.GetProducts();
        return Ok(products);
    }

    [HttpGet("customers")]
    public async Task<IActionResult> GetCustomers()
    {
        var customers = await _integracao.GetCustomers();
        return Ok(customers);
    }

}