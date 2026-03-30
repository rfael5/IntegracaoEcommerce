using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Net;

[ApiController]
[ApiKey]
[Route("produtos-tpa")]
public class ProdutosController:ControllerBase
{

    private readonly ProdutosTPA _produtos;
    public ProdutosController(ProdutosTPA produtos)
    {
       _produtos = produtos;
    }

    [EnableCors("All")]
    [HttpGet("produtos-servico")]
    public async Task<IActionResult> BuscarProdutosServico([FromQuery] QueryFilter filter, CancellationToken cancellationToken)
    {
        try
        {
            GetClientIpAddress();
            var produtosServico = await _produtos.GetProdutoPorServico(filter, cancellationToken);
            return Ok(new ResponseData
            {
                status = 200,
                message = "OK",
                data = produtosServico
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

    [HttpGet("produtos-preco")]
    public async Task<IActionResult> BuscarProdutosPreco([FromQuery] QueryFilter filter, CancellationToken cancellationToken)
    {
        try
        {
            GetClientIpAddress();
            var produtos = await _produtos.GetProdutosPreco(filter, cancellationToken);
            Console.WriteLine(produtos.Count());

            return Ok(new ResponseData
            {
                status = 200,
                message = "OK",
                data = produtos
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
    
    [HttpGet("produtos")]
    public async Task<IActionResult> BuscarTodosProdutos([FromQuery] QueryFilter filter, CancellationToken cancellationToken)
    {
        try
        {
            GetClientIpAddress();
            var produtos = await _produtos.GetProdutos(filter, cancellationToken);
            Console.WriteLine(produtos.Count());
            return Ok(new ResponseData
            {
                status = 200,
                message = "OK",
                data = produtos
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

    [HttpGet("materiais")]
    public async Task<IActionResult> BuscarMateriais([FromQuery] QueryFilter filter, CancellationToken cancellationToken)
    {
        try
        {
            GetClientIpAddress();
            var materiais = await _produtos.GetMateriais(filter, cancellationToken);
            return Ok(new ResponseData
            {
                status = 200,
                message = "OK",
                data = materiais
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

    [HttpGet("itens-servico")]
    public async Task<IActionResult> BuscarItensServico()
    {
        try
        {
            GetClientIpAddress();
            var itensServico = await _produtos.GetItensServico();
            return Ok(new ResponseData
            {
                status = 200,
                message = "OK",
                data = itensServico
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

    [HttpGet("tabelas-preco")]
    public async Task<IActionResult> BuscarTabelasPreco()
    {
        try
        {
            GetClientIpAddress();
            var tabelasPreco = await _produtos.GetTabelasPreco();
            return Ok(new ResponseData
            {
                status = 200,
                message = "OK",
                data = tabelasPreco
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

    public void GetClientIpAddress()
    {
        try
        {
            var forwarded = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
            if(!string.IsNullOrEmpty(forwarded))
            {
                var firstIp = forwarded.Split(',')[0].Trim();
                if (IPAddress.TryParse(firstIp, out _))
                {
                    Console.WriteLine("###################################");
                    Console.WriteLine(firstIp);
                    Console.WriteLine("###################################");
                }
            }

            Console.WriteLine("###################################");
            Console.WriteLine(HttpContext.Connection.RemoteIpAddress?.ToString());
            Console.WriteLine("###################################");
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }

}