using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

[ApiController]
[ApiKey]
[Route("orcamentos")]
public class OrcamentosController:ControllerBase
{
    private readonly AcessoTPA _acessoTpa;
    private readonly GeracaoContrato _geracaoContrato;
    private readonly CadastroPedido _cadastroPedido;
    private readonly BuscaDadosOrcamentos _buscaOrcamentos;
    private readonly AppDbContext _context;

    public OrcamentosController(
        AcessoTPA acessoTpa, 
        GeracaoContrato geracaoContrato,
        CadastroPedido cadastroPedido,
        BuscaDadosOrcamentos buscaOrcamentos,
        AppDbContext context)
    {
        _acessoTpa = acessoTpa;
        _geracaoContrato = geracaoContrato;
        _cadastroPedido = cadastroPedido;
        _context = context;
        _buscaOrcamentos = buscaOrcamentos;
    }

    public record DadosFechamentoContrato
    {
        public int pkDoctoped { get; init; }
        public int operador { get; init; }
        public string temProfissional { get; init; }
    }

    public record RequestData
    {
        public int pkDoctoped { get; init; }
        public int operador { get; init; }
    }

    public record ResponseData
    {
        public int status { get; init; }
        public string message { get; init; }
        public object? data  { get; init; } = null;
    }

    public record RequestProdutoEvento
    {
        public string idDoctoped { get; init; }
    }

    public async Task<bool> ChecarDocumentoExiste(int pkDoctoped)
    {
        var documento = await _context.Doctoped.FirstOrDefaultAsync(doc => doc.pkDoctoped == pkDoctoped);
        if(documento == null)
        {
            return false;
        }
        return true;
    }

    //CRIA E GERA CONTRATO
    [HttpPost("criar-or")]
    public async Task<IActionResult> AutorizarOr([FromBody] InformacoesOR informacoesOr)
    {
        try
        {
            var result = await _cadastroPedido.CadastrarEGerarContrato(informacoesOr);

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
    

    //SOMENTE CRIA OR
    //  [HttpPost("criar-or")]
    // public async Task<IActionResult> CriarOr([FromBody] InformacoesOR informacoesOr)
    // {
    //     try
    //     {
    //         var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
    //         if (string.IsNullOrEmpty(ip))
    //         {
    //             ip = HttpContext.Connection.RemoteIpAddress?.ToString();
    //         }

    //         var result = await _cadastroPedido.CadastrarNovaOr(informacoesOr);

    //         return Ok(new ResponseData
    //         {
    //             status = 200,
    //             message = "OK",
    //             data = result
    //         });
    //     }
    //     catch (HttpRequestException e)
    //     {
    //         Console.WriteLine(e);

    //         return StatusCode(
    //             (int?)e.StatusCode ?? 500,
    //             new ResponseData
    //             {
    //                 status = (int?)e.StatusCode ?? 500,
    //                 message = e.Message
    //             });
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);

    //         return StatusCode(500, new ResponseData
    //         {
    //             status = 500,
    //             message = e.InnerException?.Message ?? e.Message
    //         });
    //     }
    // }

    [HttpPost("criar-ec")]
    public async Task<IActionResult> CriarEc([FromBody] InformacoesEC informacoesEc)
    {
        try
        {
            var result = await _cadastroPedido.CadastrarNovaEc(informacoesEc);
            Console.WriteLine(result);
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

            return StatusCode(
                500,
                new ResponseData
                {
                    status = 500,
                    message = e.InnerException?.Message ?? e.Message
                }
            );
        }
    }

    [HttpPost("gerar-contrato")]
    public async Task<IActionResult> FecharOrcamento([FromBody] DadosFechamentoContrato dadosFechamentoContrato)
    {
        try
        {
            var documentoExiste = await ChecarDocumentoExiste(dadosFechamentoContrato.pkDoctoped);
            if(!documentoExiste)
            {
                return StatusCode(

                    500,
                    new ResponseData
                    {
                        status = 500,
                        message = "OR/EC não encontrados no banco."
                    });
            }
            var result = await _geracaoContrato.GerarContrato(dadosFechamentoContrato.pkDoctoped, dadosFechamentoContrato.operador, dadosFechamentoContrato.temProfissional);
            Console.WriteLine(result);
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

            return StatusCode(
                500,
                new ResponseData
                {
                    status = 500,
                    message = e.InnerException?.Message ?? e.Message
                }
            );
        }
    }

    [HttpPost("cancelar-documento")]
    public async Task<IActionResult> CancelarDocumento([FromBody] RequestData requestData)
    {
        try
        {
            var documentoExiste = await ChecarDocumentoExiste(requestData.pkDoctoped);
            if(!documentoExiste)
            {
                return StatusCode(
                    500,
                    new ResponseData
                    {
                        status = 500,
                        message = "OR/EC não encontrados no banco."
                    });
            }
            await _acessoTpa.CancelarDocumento(requestData.pkDoctoped, requestData.operador);
            return Ok(new ResponseData
            {
                status = 200,
                message = "OK",
                data = requestData.pkDoctoped
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

            return StatusCode(
                500,
                new ResponseData
                {
                    status = 500,
                    message = e.InnerException?.Message ?? e.Message
                }
            );
        }
    }

    [HttpGet("buscar-ors")]
    public async Task<IActionResult> BuscarORs([FromQuery] QueryFilter filter, CancellationToken cancellationToken)
    {
        try
        {
            var ors = await _buscaOrcamentos.BuscarORs(filter, cancellationToken);
             return Ok(new PagedResponseData
            {
                status = 200,
                message = "OK",
                data = ors.Data,
                pageNumber = ors.PageNumber,
                pageSize = ors.PageSize,
                totalPages = ors.TotalPages,
                totalRecords = ors.TotalRecords,
                hasNextPage = ors.HasNextPage,
                hasPreviousPage = ors.HasPreviousPage
            });

        }catch(HttpRequestException e)
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

    [HttpGet("buscar-produtos-ors")]
    public async Task<IActionResult> BuscarProdutosORs(
        [FromQuery] RequestProdutoEvento request, 
        CancellationToken cancellationToken)
    {
        try
        {
            int idDoctoped = int.Parse(request.idDoctoped);
            var produtosOr = await _buscaOrcamentos.BuscarProdutosORs(idDoctoped, cancellationToken);
            return Ok(new ResponseData
            {
                status = 200,
                message="Ok",
                data = produtosOr
            });

        }catch(HttpRequestException e)
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
}