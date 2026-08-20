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

    public record ProdutosItemServicoRequest
    {
        public int idItemServico { get; init; }
    }

    [EnableCors("All")]
    [HttpGet("produtos-servico")]
    public async Task<IActionResult> BuscarProdutosServico([FromQuery] QueryFilter filter, CancellationToken cancellationToken)
    {
        try
        {
            GetClientIpAddress();
            var produtosServico = await _produtos.GetProdutoPorServico(filter, cancellationToken);
            return Ok(new PagedResponseData
            {
                status = 200,
                message = "OK",
                data = produtosServico.Data,
                pageNumber = produtosServico.PageNumber,
                pageSize = produtosServico.PageSize,
                totalPages = produtosServico.TotalPages,
                totalRecords = produtosServico.TotalRecords,
                hasNextPage = produtosServico.HasNextPage,
                hasPreviousPage = produtosServico.HasPreviousPage
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

    [HttpPost("produtos-por-servico")]
    public async Task<IActionResult> BuscarProdutosPorItemServico(
            [FromQuery] QueryFilter filter, 
            CancellationToken cancellationToken,
            [FromBody] ProdutosItemServicoRequest request)
    {
        try
        {
            var produtosServico = await _produtos.GetProdutosPorItemServico(request.idItemServico, filter, cancellationToken);
            return Ok(new PagedResponseData
            {
                status = 200,
                message = "OK",
                data = produtosServico.Data,
                pageNumber = produtosServico.PageNumber,
                pageSize = produtosServico.PageSize,
                totalPages = produtosServico.TotalPages,
                totalRecords = produtosServico.TotalRecords,
                hasNextPage = produtosServico.HasNextPage,
                hasPreviousPage = produtosServico.HasPreviousPage
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

            return Ok(new PagedResponseData
            {
                status = 200,
                message = "OK",
                data = produtos.Data,
                pageNumber = produtos.PageNumber,
                pageSize = produtos.PageSize,
                totalPages = produtos.TotalPages,
                totalRecords = produtos.TotalRecords,
                hasNextPage = produtos.HasNextPage,
                hasPreviousPage = produtos.HasPreviousPage   
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
            return Ok(new PagedResponseData
            {
                status = 200,
                message = "OK",
                data = produtos.Data,
                pageNumber = produtos.PageNumber,
                pageSize = produtos.PageSize,
                totalPages = produtos.TotalPages,
                totalRecords = produtos.TotalRecords,
                hasNextPage = produtos.HasNextPage,
                hasPreviousPage = produtos.HasPreviousPage           
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
            return Ok(new PagedResponseData
            {
                status = 200,
                message = "OK",
                data = materiais.Data,
                pageNumber = materiais.PageNumber,
                pageSize = materiais.PageSize,
                totalPages = materiais.TotalPages,
                totalRecords = materiais.TotalRecords,
                hasNextPage = materiais.HasNextPage,
                hasPreviousPage = materiais.HasPreviousPage  
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

    [HttpGet("relacao-servico-produto")]
    public async Task<IActionResult> BuscarRelacaoServicoProduto([FromQuery] QueryFilter filter, CancellationToken cancellationToken)
    {
        try
        {
            var relacaoServicosProdutos = await _produtos.GetRelacaoServicoProduto(filter, cancellationToken);
            return Ok(new PagedResponseData
            {
                status = 200,
                message = "OK",
                data = relacaoServicosProdutos.Data,
                pageNumber = relacaoServicosProdutos.PageNumber,
                pageSize = relacaoServicosProdutos.PageSize,
                totalPages = relacaoServicosProdutos.TotalPages,
                totalRecords = relacaoServicosProdutos.TotalRecords,
                hasNextPage = relacaoServicosProdutos.HasNextPage,
                hasPreviousPage = relacaoServicosProdutos.HasPreviousPage  
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
            return Ok(new PagedResponseData
            {
                status = 200,
                message = "OK",
                data = itensServico.Data,
                pageNumber = itensServico.PageNumber,
                pageSize = itensServico.PageSize,
                totalPages = itensServico.TotalPages,
                totalRecords = itensServico.TotalRecords,
                hasNextPage = itensServico.HasNextPage,
                hasPreviousPage = itensServico.HasPreviousPage  
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
            return Ok(new PagedResponseData
            {
                status = 200,
                message = "OK",
                data = tabelasPreco.Data,
                pageNumber = tabelasPreco.PageNumber,
                pageSize = tabelasPreco.PageSize,
                totalPages = tabelasPreco.TotalPages,
                totalRecords = tabelasPreco.TotalRecords,
                hasNextPage = tabelasPreco.HasNextPage,
                hasPreviousPage = tabelasPreco.HasPreviousPage
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