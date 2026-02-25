using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting.Server;
using System.Text.Json;
using System.Net;
using System.Net.WebSockets;
using System.Text;


[ApiController]
[Route("user")]
public class UserController:ControllerBase
{
    private readonly AcessoTPA _acessoTpa;
    private readonly UsuariosTPA _usuariosTpa;
    private readonly AcessoEcommerce _acessoEcommerce;
    private readonly CadastroPedido _cadastroOr;

    public UserController(
        AcessoTPA acessoTpa, 
        UsuariosTPA usuariosTpa, 
        AcessoEcommerce acessoEcommerce,
        CadastroPedido cadastroOr)
    {
        _acessoTpa = acessoTpa;
        _usuariosTpa = usuariosTpa;
        _acessoEcommerce = acessoEcommerce;
        _cadastroOr = cadastroOr;
    }

    [EnableCors("All")]
    [HttpGet("codigo-usuario")]
    public async Task<IActionResult> Teste()
    {
       await _acessoEcommerce.GetOrders();
       return Ok(new {message = "ok"});
    }

    [HttpGet("get-usuarios")]
    public async Task<IActionResult> VerUsuarios()
    {
        var result = await _usuariosTpa.VerUsuario();
        return Ok(result);
    }

    [HttpGet("get-movtoped")]
    public async Task<IActionResult> GetMovtoped()
    {
        var result = await _acessoTpa.GetMovtoped();
        return Ok(result);
    }

    [HttpPost("criar-or")]
    public async Task<IActionResult> CriarOr([FromBody] InformacoesOR informacoesOr)
    {
        var result = await _cadastroOr.CadastrarNovaOr(informacoesOr);
        return Ok(new {message = result});
    }

    [HttpPost("criar-ec")]
    public async Task<IActionResult> CriarEc([FromBody] InformacoesOR informacoesOr)
    {
        var result = await _cadastroOr.CadastrarNovaEC(informacoesOr);
        return Ok(new {message = result});
    }
    
}