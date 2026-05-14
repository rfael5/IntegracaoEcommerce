using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

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

    public record Teste
    {
        public string value { get; set; }
    }

    [HttpPost("teste-envio")]
    public async Task<IActionResult> TesteEnvio([FromBody] Teste teste)
    {
        Console.WriteLine(teste.value);
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

    [HttpGet("complete-order")]
    public async Task<IActionResult> GetCompleteOrder()
    {
        var order = await _integracao.GetCompleteOrder(21);
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