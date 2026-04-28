using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

[ApiController]
[Route("ecommerce")]
public class EcommerceController : ControllerBase
{
    private readonly IntegracaoTray _integracao;

    public EcommerceController(IntegracaoTray integracao)
    {
        _integracao = integracao;
    }

    [HttpGet("teste-ecommerce")]
    public async Task<IActionResult> TesteFrontEcommerce()
    {
        await _integracao.TesteFrontEcommerce();
        return Ok(new {message = "ok"});
    }

    [EnableCors("All")]
    // [HttpGet("auth")]
    // public async Task<IActionResult> GetTokens()
    // {
    //     var content = await _integracao.Authorize();
    //     return Ok(new {message = content});
    // }

    // [HttpGet("refresh")]
    // public async Task<IActionResult> RefreshToken()
    // {
    //     var content = await _integracao.Refresh();
    //     return Ok(content);
    // }

    [HttpGet("carrinho")]
    public async Task<IActionResult> GetCarrinho()
    {
        var res = await _integracao.GetCarrinho();
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
        var order = await _integracao.GetCompleteOrder(3);
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