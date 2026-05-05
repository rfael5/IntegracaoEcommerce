using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

[ApiController]
[ApiKey]
[Route("ajustes")]
public class AjustesController:ControllerBase
{
    private readonly Ajustes _ajustesService;

    public AjustesController(Ajustes ajustesService)
    {
        _ajustesService = ajustesService;
    }

    public record RequestCancelamento
    {
        public int pkAjustePed { get; init; }
        public int operador { get; init; }
        public int pkDoctoped { get; init; }
    }

 

    [EnableCors("All")]
    [HttpPost("criar-ajuste")]
    public async Task<IActionResult> CriarAjuste([FromBody] InformacoesAjuste ajuste)
    {
        try
        {
            var result = await _ajustesService.CadastrarAjuste(ajuste);
            return Ok(new ResponseData {
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

    [HttpPost("autorizar-ajuste")]
    public async Task<IActionResult> AutorizarAjuste([FromBody] InformacoesAjuste requestAdendo)
    {
        try
        {
            //var result = await _ajustesService.AutorizarAjuste(requestAdendo);
            var result = await _ajustesService.CadastrarAjusteGerarAdendoContrato(requestAdendo);
            return Ok(new ResponseData {
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

    [HttpPost("cancelar-ajuste")]
    public async Task<IActionResult> CancelarAjuste([FromBody] RequestCancelamento requestCancelamento)
    {
         try
        {
            //var result = await _ajustesService.AutorizarAjuste(requestAdendo);
            await _ajustesService.CancelarAjuste(requestCancelamento.pkAjustePed, requestCancelamento.operador, requestCancelamento.pkDoctoped);
            return Ok(new ResponseData {
                status = 200,
                message = "OK",
                data = requestCancelamento.pkAjustePed
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
}