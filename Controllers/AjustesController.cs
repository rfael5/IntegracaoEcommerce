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
    public async Task<IActionResult> AutorizarAjuste([FromBody] RequestAdendo requestAdendo)
    {
        try
        {
            var result = await _ajustesService.AutorizarAjuste(requestAdendo);
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
}