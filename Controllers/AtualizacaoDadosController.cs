using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch.Operations;

[ApiController]
[ApiKey]
[Route("atualizacao")]
public class AtualizacaoController:ControllerBase
{
    private readonly BancoTesteLocal _context;
    private readonly HashSet<string> alteracoesPermitidas = ["/texto", "/txtHistorico", "/txtConclusao"];

    public AtualizacaoController(BancoTesteLocal context)
    {
        _context = context;
    }

    public record AtualizacaoRequest
    {
        public int pkDoctoped { get; init; }
        public required JsonPatchDocument<TpaDoctopedDTO> patch { get; init; }
    }

    [EnableCors("All")]
    [HttpPatch("atualizar-documento/{pkDoctoped:int}")]
    public IActionResult AtualizacaoDocumento(
    int pkDoctoped,
    [FromBody] JsonPatchDocument<TpaDoctopedDTO> patch)
    {
        try
        {
            if(patch == null)
            {
                return BadRequest(new ResponseData
                {
                    status = 400,
                    message = "Patch inválido."
                });
            }
            if(patch.Operations.Count == 0)
            {
                return BadRequest(new ResponseData
                {
                    status = 400,
                    message = "Nenhuma alteração informada."
                });
            }

            foreach(var op in patch.Operations)
            {
                if(op.OperationType != OperationType.Replace)
                {
                    return BadRequest(new ResponseData
                    {
                        status = 400,
                        message = "Operação de atualização inválida."
                    });
                }

                if(!alteracoesPermitidas.Contains(op.path))
                {
                    return BadRequest(new ResponseData
                    {
                       status = 400,
                       message = $"Não é permitido modificar o parâmetro {op.path}"
                    });
                }
            };
            var documento = _context.Doctoped.FirstOrDefault(doc => doc.pkDoctoped == pkDoctoped);
            if(documento == null)
            {
                return NotFound(new ResponseData
                {
                    status = 404,
                    message = "Documento não encontrado."
                });
            }

            patch.ApplyTo(documento, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(
                new ResponseData
                {
                   status = 400,
                   message = "Há parâmetros inválidos na requisição", 
                });
            } 

            _context.Update(documento);
            _context.SaveChanges();
            
            return Ok(new ResponseData
            {
                status = 200,
                message = $"{documento.tpDocto} {documento.documento} atualizado" 
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
}