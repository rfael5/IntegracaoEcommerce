// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Cors;

// [ApiController]
// [ApiKey]
// [Route("produtos-tpa")]
// public class ProdutosController:ControllerBase
// {

//     private readonly ProdutosTpaService _produtos;
//     public ProdutosController(ProdutosTpaService produtos)
//     {
//        _produtos = produtos;
//     }

//      [EnableCors("All")]
//     [HttpGet("produtos-acabados")]
//     public async Task<IActionResult> BuscarProdutosAcabados([FromQuery] QueryFilter filter, CancellationToken cancellationToken)
//     {
//         try
//         {
//             var produtos = await _produtos.GetProdutosAcabados(filter, cancellationToken);
//             Console.WriteLine(produtos.Count());

//             return Ok(new ResponseData
//             {
//                 status = 200,
//                 message = "OK",
//                 data = produtos
//             });
//         }
//         catch(HttpRequestException e)
//         {
//             Console.WriteLine(e);
//             return StatusCode(
//                 (int?)e.StatusCode ?? 500,
//                 new ResponseData
//                 {
//                     status = (int?)e.StatusCode ?? 500,
//                     message = e.Message
//                 });
//         }
//         catch(Exception e)
//         {
//             Console.WriteLine(e);
//             return StatusCode(500, new ResponseData
//             {
//                 status = 500,
//                 message = e.InnerException?.Message ?? e.Message
//             });
//         }
//     }
    
//     [HttpGet("todos-produtos")]
//     public async Task<IActionResult> BuscarTodosProdutos()
//     {
//         try
//         {
//             var produtos = await _produtos.GetTodosProdutos();
//             Console.WriteLine(produtos.Count());
//             return Ok(new ResponseData
//             {
//                 status = 200,
//                 message = "OK",
//                 data = produtos
//             });
//         }
//         catch(HttpRequestException e)
//         {
//             Console.WriteLine(e);
//             return StatusCode(
//                 (int?)e.StatusCode ?? 500,
//                 new ResponseData
//                 {
//                     status = (int?)e.StatusCode ?? 500,
//                     message = e.Message
//                 });
//         }
//         catch(Exception e)
//         {
//             Console.WriteLine(e);
//             return StatusCode(500, new ResponseData
//             {
//                 status = 500,
//                 message = e.InnerException?.Message ?? e.Message
//             });
//         }
//     }

// }