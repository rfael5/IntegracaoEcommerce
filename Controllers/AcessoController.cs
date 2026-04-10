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

    public UserController(
        AcessoTPA acessoTpa,
        UsuariosTPA usuariosTpa,
        AcessoEcommerce acessoEcommerce)
    {
        _acessoTpa = acessoTpa;
        _usuariosTpa = usuariosTpa;
        _acessoEcommerce = acessoEcommerce;
    }

    public record DadosFechamentoContrato
    {
        public int pkDoctoped { get; init; }
        public int operador { get; init; }
        public string temProfissional { get; init; }
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
        return Ok(new { message = "ok" });
    }

    [HttpGet("get-vendedores")]
    public async Task<IActionResult> GetVendedores([FromQuery] QueryFilter filter, CancellationToken cancellationToken)
    {
        try
        {
            var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            }

            var result = await _acessoTpa.GetVendedores(filter, cancellationToken);
            return Ok(new PagedResponseData
            {
                status = 200,
                message = "OK",
                data = result.Data,
                pageNumber = result.PageNumber,
                pageSize = result.PageSize,
                totalPages = result.TotalPages,
                totalRecords = result.TotalRecords,
                hasNextPage = result.HasNextPage,
                hasPreviousPage = result.HasPreviousPage
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

    [HttpGet("relacao-produtos-servicos")]
    public async Task<IActionResult> GetServicosProdutos([FromQuery] QueryFilter filter, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _acessoTpa.GetTiposServico(filter, cancellationToken);
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

    [HttpGet("relacao-materiais-servicos")]
    public async Task<IActionResult> GetServicosMateriais([FromQuery] QueryFilter filter, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _acessoTpa.GetMateriais(filter, cancellationToken);
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

    [HttpGet("get-cadastros")]
    public async Task<IActionResult> GetCadastro([FromQuery] QueryFilter filter, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _usuariosTpa.BuscarCadastros(filter, cancellationToken);
            return Ok(new PagedResponseData
            {
                status = 200,
                message = "OK",
                data = result.Data,
                pageNumber = result.PageNumber,
                pageSize = result.PageSize,
                totalPages = result.TotalPages,
                totalRecords = result.TotalRecords,
                hasNextPage = result.HasNextPage,
                hasPreviousPage = result.HasPreviousPage
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

    [HttpGet("get-usuarios")]
    public async Task<IActionResult> VerUsuarios()
    {
        var result = await _usuariosTpa.VerUsuario();
        return Ok(result);
    }
}