// using System.Text.Json;
// using System.Threading.Tasks;
// using Microsoft.Data.SqlClient;
// using Microsoft.EntityFrameworkCore;

// public class ProdutosTPA
// {
//     private readonly AppDbContext _context;

//     public ProdutosTPA(AppDbContext context)
//     {
//         _context = context;
//     }

//      public async Task<List<ProdutoTpa>> GetTodosProdutos()
//     {
//         var _query = @$"
//             SELECT PRODUTO.ID, PRODUTO.PK_PRODUTO, PRODUTO.CODPRODUTO, PRODUTO.DESCRICAO, PRODUTO.REFERENCIA, 
//                 PRODUTO.UN, PRODUTO.IDX_NEGOCIO, PRODUTO.IDX_CLASSIFICACAO, PRODUTO.CSTI, PRODUTO.PCCUSTO, PRODUTO.LOCACAO, PRODUTO.NCM, 
//                 TABELA.PK_TABELA, TABELA.DESCRICAO NOMETABELA, TABELAPRECOS.PRECO1U1
//                     FROM TPAPRODUTO AS PRODUTO
//                 INNER JOIN TPATABELAPROD AS TABELAPRECOS ON PRODUTO.PK_PRODUTO = TABELAPRECOS.IDX_PRODUTO
//                 INNER JOIN TPATABELA AS TABELA ON TABELAPRECOS.RDX_TABELA = TABELA.PK_TABELA
//                 WHERE IDX_NEGOCIO NOT IN ('Manutenção', 'Desativados', 'Locação de Materiais') AND PRODUTO.STATUS = 'A' AND TABELAPRECOS.RDX_TABELA IN (1, 22, 24, 3) 
//             ORDER BY PRODUTO.ID      
//         ";
//         var produtos = await _context.ProdutosTpa.FromSqlRaw(_query).ToListAsync();
//         return produtos;
//     }

//     public async Task<List<ProdutoTpa>> GetProdutosAcabados(
//         QueryFilter filter,
//         CancellationToken cancellationToken = default)
//     {
//         var pageNumber = Math.Max(1, filter.PageNumber);
//         var pageSize = Math.Clamp(filter.PageSize, 1, 1000);
//         var offset = (pageNumber - 1) * pageSize;

//         const string sql = @"
//         SELECT PRODUTO.ID, PRODUTO.PK_PRODUTO, PRODUTO.CODPRODUTO, PRODUTO.DESCRICAO, PRODUTO.REFERENCIA, 
//                 PRODUTO.UN, PRODUTO.IDX_NEGOCIO, PRODUTO.IDX_CLASSIFICACAO, PRODUTO.CSTI, PRODUTO.PCCUSTO, PRODUTO.LOCACAO, PRODUTO.NCM, 
//                 TABELA.PK_TABELA, TABELA.DESCRICAO NOMETABELA, TABELAPRECOS.PRECO1U1
//                     FROM TPAPRODUTO AS PRODUTO
//                 INNER JOIN TPATABELAPROD AS TABELAPRECOS ON PRODUTO.PK_PRODUTO = TABELAPRECOS.IDX_PRODUTO
//                 INNER JOIN TPATABELA AS TABELA ON TABELAPRECOS.RDX_TABELA = TABELA.PK_TABELA
//                 WHERE IDX_NEGOCIO NOT IN ('Manutenção', 'Desativados', 'Locação de Materiais') AND PRODUTO.STATUS = 'A' AND TABELAPRECOS.RDX_TABELA IN (1, 22, 24, 3) 
//             ORDER BY PRODUTO.ID
//             OFFSET @offset ROWS 
//             FETCH NEXT @pageSize ROWS ONLY
//         ";

//         const string countSql = @"
//             SELECT COUNT(*) as Value
//             FROM TPAPRODUTO AS PRODUTO
//             INNER JOIN TPATABELAPROD AS TABELAPRECOS 
//                 ON PRODUTO.PK_PRODUTO = TABELAPRECOS.IDX_PRODUTO
//             INNER JOIN TPATABELA AS TABELA 
//                 ON TABELAPRECOS.RDX_TABELA = TABELA.PK_TABELA
//             WHERE PRODUTO.IDX_NEGOCIO NOT IN ('Manutenção', 'Desativados', 'Locação de Materiais') 
//             AND PRODUTO.STATUS = 'A' 
//             AND TABELAPRECOS.RDX_TABELA IN (1, 22, 24, 3)
//         ";

//         var totalRecords = await _context.Database
//             .SqlQueryRaw<int>(countSql)
//             .SingleAsync(cancellationToken);

//         var produtos = await _context.ProdutosTpa
//             .FromSqlRaw(sql,
//                 new SqlParameter("@offset", offset),
//                 new SqlParameter("@pageSize", pageSize))
//             .AsNoTracking()
//             .ToListAsync(cancellationToken);

//         return produtos;
//     }
// }