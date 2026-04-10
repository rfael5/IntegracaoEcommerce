using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

[ApiController]
[ApiKey]
[Route("orcamentos")]
public class OrcamentosController:ControllerBase
{
    private readonly AcessoTPA _acessoTpa;
    private readonly GeracaoContrato _geracaoContrato;
    private readonly CadastroPedido _cadastroPedido;

    public OrcamentosController(
        AcessoTPA acessoTpa, 
        GeracaoContrato geracaoContrato,
        CadastroPedido cadastroPedido)
    {
        _acessoTpa = acessoTpa;
        _geracaoContrato = geracaoContrato;
        _cadastroPedido = cadastroPedido;
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

     [HttpPost("criar-or")]
    public async Task<IActionResult> CriarOr([FromBody] InformacoesOR informacoesOr)
    {
        try
        {
            var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            }

            var result = await _cadastroPedido.CadastrarNovaOr(informacoesOr);

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
}