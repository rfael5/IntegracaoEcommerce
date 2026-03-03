using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting.Server;
using System.Text.Json;
using System.Net;
using System.Net.WebSockets;
using System.Text;


[ApiController]
[ApiKey]
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

    public record ResponseData
    {
        public int status { get; set; }
        public string message { get; set; }
        public object? data { get; set; } = null;
    }

    [EnableCors("All")]
    [HttpGet("codigo-usuario")]
    public async Task<IActionResult> Teste()
    {
       await _acessoEcommerce.GetOrders();
       return Ok(new {message = "ok"});
    }

    //&pageNumber=1&pageSize=5
    [HttpGet("get-cadastros")]
    public async Task<IActionResult> GetCadastro([FromQuery] QueryFilter filter, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _usuariosTpa.BuscarCadastros(filter, cancellationToken);
            return Ok(new ResponseData
            {
                status = 200,
                message = "OK",
                data = result
            });
        }
        catch(HttpRequestException e)
        {
            Console.WriteLine(e);
            return StatusCode(
                (int?)e.StatusCode ?? 500,
                new ResponseData
                {
                    status = (int?)e.StatusCode ?? 500,
                    message = e.Message
                });
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ResponseData
            {
                status = 500,
                message = e.InnerException?.Message ?? e.Message
            });
        }
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
        try
        {
            var result = await _cadastroOr.CadastrarNovaOr(informacoesOr);

            return Ok(new ResponseData
            {
                status = 200,
                message = "OK",
                data = result
            });
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);

            return StatusCode(
                (int?)e.StatusCode ?? 500,
                new ResponseData
                {
                    status = (int?)e.StatusCode ?? 500,
                    message = e.Message
                });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            return StatusCode(500, new ResponseData
            {
                status = 500,
                message = e.InnerException?.Message ?? e.Message
            });
        }
    }

    // [HttpPost("criar-ec")]
    // public async Task<ResponseData> CriarEc([FromBody] InformacoesOR informacoesOr)
    // {
    //     try
    //     {
    //         var result = await _cadastroOr.CadastrarNovaEC(informacoesOr);
    //         Console.WriteLine(result);
    //         var message = new ResponseData
    //         {
    //             status = 200,
    //             message = "OK",
    //             data = result
    //         };
    //         return message;
    //     }
    //     catch(HttpRequestException e)
    //     {
    //         Console.WriteLine(e);
    //         var message = new ResponseData
    //         {
    //             status = Convert.ToInt32(e.StatusCode),
    //             message = e.Message
    //         };
    //         return message;
    //     }
    //     catch(Exception e)
    //     {
    //         Console.WriteLine(e);
    //         var message = new ResponseData
    //         {
    //             status = 500,
    //             message = e.Message               
    //         };
    //         return message;
    //     }
    // }
    
}