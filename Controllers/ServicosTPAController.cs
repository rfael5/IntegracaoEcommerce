using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[ApiKey]
[Route("servicos-tpa")]
public class ServicosTPAController:ControllerBase
{
    private readonly ServicosTPA _servicos;
    public ServicosTPAController(ServicosTPA servicos)
    {
        _servicos = servicos;
    }

    [HttpGet("evento-tp")]
    public async Task<IActionResult> BuscarTipos()
    {
        try
        {
            var tipos = await _servicos.GetTipos();
            return Ok(tipos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok(new { message = e.Message });
        }
    }
    [HttpGet("categorias-evento")]
    public async Task<IActionResult> BuscarCategoriasEvento()
    {
        try
        {
            var categorias = await _servicos.GetCategoriasEventos();
            return Ok(categorias);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok(new { message = e.Message });
        }
    }

}