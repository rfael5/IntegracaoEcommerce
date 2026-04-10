using System.Diagnostics;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class ProdutosTPA  
{
    private readonly PrincipalDbContext _dbPrincipal;

    public ProdutosTPA(PrincipalDbContext dbPrincipal )
    {
        _dbPrincipal = dbPrincipal;
    }

    public async Task<PagedResponse<ProdutoServico>> GetProdutoPorServico(QueryFilter filter, CancellationToken cancellationToken = default)
    {
        var pageNumber = Math.Max(1, filter.PageNumber);
        var pageSize = Math.Clamp(filter.PageSize, 1, 100);
        var offset = (pageNumber - 1) * pageSize;

        const string _query = @"
            SELECT PRODSV.PK_PRODEVENTOSV as prodEventoSv, TPSV.ID as idTipoServico, TPSV.DESCRICAO as itemServico, PRODUTO.PK_PRODUTO as pkProduto, 
            PRODUTO.CODPRODUTO as codProduto, PRODUTO.DESCRICAO AS nomeProduto, PRODUTO.REFERENCIA as referencia, PRODUTO.UN as un, 
            PRODUTO.IDX_NEGOCIO as idxNegocio, PRODUTO.IDX_CLASSIFICACAO as idxClassificacao, PRODUTO.CSTI as csti, PRODUTO.PCCUSTO as pcCusto, 
            PRODUTO.LOCACAO as locacao, PRODUTO.NCM as ncm
                FROM TPAPRODEVENTOSV as PRODSV
            INNER JOIN TPAEVENTOSV AS TPSV ON PRODSV.RDX_EVENTOSV = TPSV.PK_EVENTOSV
            INNER JOIN TPAPRODUTO AS PRODUTO ON PRODSV.IDX_PRODUTO = PRODUTO.PK_PRODUTO 
                AND PRODUTO.IDX_NEGOCIO NOT IN ('Manutenção', 'Desativados', 'Locação de Materiais') 
                AND PRODUTO.STATUS = 'A'
                AND PRODUTO.VENDA = 'S'
            ORDER BY PRODSV.PK_PRODEVENTOSV 
            OFFSET @offset ROWS 
            FETCH NEXT @pageSize ROWS ONLY
        ";

        const string count = @"
            SELECT COUNT(*) AS Value
                FROM TPAPRODEVENTOSV as PRODSV
            INNER JOIN TPAEVENTOSV AS TPSV ON PRODSV.RDX_EVENTOSV = TPSV.PK_EVENTOSV
            INNER JOIN TPAPRODUTO AS PRODUTO ON PRODSV.IDX_PRODUTO = PRODUTO.PK_PRODUTO 
                AND PRODUTO.IDX_NEGOCIO NOT IN ('Manutenção', 'Desativados', 'Locação de Materiais') 
                AND PRODUTO.STATUS = 'A'
                AND PRODUTO.VENDA = 'S'
        ";

        var totalRecords = await _dbPrincipal.Database.SqlQueryRaw<int>(count).SingleAsync(cancellationToken);

        var produtos = await _dbPrincipal.ProdutosServico.FromSqlRaw(_query, 
            new SqlParameter("@offset", offset),
            new SqlParameter("@pageSize", pageSize))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        //return produtos;

        return new PagedResponse<ProdutoServico>
        {
            Data = produtos,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
        };
    }


    public async Task<PagedResponse<ProdutoPreco>> GetProdutosPreco(
        QueryFilter filter,
        CancellationToken cancellationToken = default)
    {
        var pageNumber = Math.Max(1, filter.PageNumber);
        var pageSize = Math.Clamp(filter.PageSize, 1, 100);
        var offset = (pageNumber - 1) * pageSize;

        const string sql = @"
            SELECT PRODUTO.ID, PRODUTO.PK_PRODUTO, PRODUTO.CODPRODUTO, PRODUTO.DESCRICAO, PRODUTO.REFERENCIA, 
            PRODUTO.UN, PRODUTO.IDX_NEGOCIO, PRODUTO.IDX_CLASSIFICACAO, PRODUTO.CSTI, PRODUTO.PCCUSTO, PRODUTO.LOCACAO, PRODUTO.NCM, 
            TABELA.PK_TABELA, TABELA.DESCRICAO NOMETABELA, TABELAPRECOS.PRECO1U1
                FROM TPAPRODUTO AS PRODUTO
            INNER JOIN TPATABELAPROD AS TABELAPRECOS ON PRODUTO.PK_PRODUTO = TABELAPRECOS.IDX_PRODUTO
            INNER JOIN TPATABELA AS TABELA ON TABELAPRECOS.RDX_TABELA = TABELA.PK_TABELA
                WHERE IDX_NEGOCIO NOT IN ('Manutenção', 'Desativados', 'Locação de Materiais') 
            AND PRODUTO.STATUS = 'A' 
            AND PRODUTO.VENDA = 'S'
            ORDER BY PRODUTO.ID
            OFFSET @offset ROWS 
            FETCH NEXT @pageSize ROWS ONLY
        ";

        const string count = @"
             SELECT COUNT(*) AS Value
                FROM TPAPRODUTO AS PRODUTO
            INNER JOIN TPATABELAPROD AS TABELAPRECOS ON PRODUTO.PK_PRODUTO = TABELAPRECOS.IDX_PRODUTO
            INNER JOIN TPATABELA AS TABELA ON TABELAPRECOS.RDX_TABELA = TABELA.PK_TABELA
                WHERE IDX_NEGOCIO NOT IN ('Manutenção', 'Desativados', 'Locação de Materiais') 
            AND PRODUTO.STATUS = 'A' 
            AND PRODUTO.VENDA = 'S'
        ";

        var totalRecords = await _dbPrincipal.Database.SqlQueryRaw<int>(count).SingleAsync(cancellationToken);

        var produtos = await _dbPrincipal.ProdutosPreco
            .FromSqlRaw(sql,
                new SqlParameter("@offset", offset),
                new SqlParameter("@pageSize", pageSize))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        //return produtos;

        return new PagedResponse<ProdutoPreco>
        {
            Data = produtos,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
        };
    }

    public async Task<PagedResponse<ProdutoEvento>> GetProdutos(
        QueryFilter filter,
        CancellationToken cancellationToken = default
    )
    {
        var pageNumber = Math.Max(1, filter.PageNumber);
        var pageSize = Math.Clamp(filter.PageSize, 1, 100);
        var offset = (pageNumber - 1) * pageSize;

        const string _query = @$"
            SELECT PRODUTO.ID, PRODUTO.PK_PRODUTO, PRODUTO.CODPRODUTO, PRODUTO.DESCRICAO, PRODUTO.REFERENCIA, 
            PRODUTO.UN, PRODUTO.IDX_NEGOCIO, PRODUTO.IDX_CLASSIFICACAO, PRODUTO.CSTI, PRODUTO.PCCUSTO, PRODUTO.LOCACAO, PRODUTO.NCM
                FROM TPAPRODUTO AS PRODUTO
            WHERE IDX_NEGOCIO NOT IN ('Manutenção', 'Desativados', 'Locação de Materiais') 
            AND PRODUTO.STATUS = 'A' 
            AND PRODUTO.VENDA = 'S'
                ORDER BY PRODUTO.ID
            OFFSET @offset ROWS 
            FETCH NEXT @pageSize ROWS ONLY
        ";

        const string count = @"
            SELECT COUNT(*) as Value
                FROM TPAPRODUTO AS PRODUTO
            WHERE IDX_NEGOCIO NOT IN ('Manutenção', 'Desativados', 'Locação de Materiais') 
            AND PRODUTO.STATUS = 'A' 
            AND PRODUTO.VENDA = 'S'
        ";

        var totalRecords = await _dbPrincipal.Database.SqlQueryRaw<int>(count).SingleAsync(cancellationToken);

        var produtos = await _dbPrincipal.ProdutosEvento
            .FromSqlRaw(_query,
                new SqlParameter("@offset", offset),
                new SqlParameter("@pageSize", pageSize))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        //return produtos
        return new PagedResponse<ProdutoEvento>
        {
            Data = produtos,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
        };
    }

    public async Task<PagedResponse<ProdutoEvento>> GetMateriais(
        QueryFilter filter,
        CancellationToken cancellationToken = default
    )
    {
        var pageNumber = Math.Max(1, filter.PageNumber);
        var pageSize = Math.Clamp(filter.PageSize, 1, 100);
        var offset = (pageNumber - 1) * pageSize;

        const string _query = @$"
            SELECT PRODUTO.ID, PRODUTO.PK_PRODUTO, PRODUTO.CODPRODUTO, PRODUTO.DESCRICAO, PRODUTO.REFERENCIA, 
            PRODUTO.UN, PRODUTO.IDX_NEGOCIO, PRODUTO.IDX_CLASSIFICACAO, PRODUTO.CSTI, PRODUTO.PCCUSTO, PRODUTO.LOCACAO, PRODUTO.NCM
                FROM TPAPRODUTO AS PRODUTO
            WHERE IDX_NEGOCIO = 'Locação de Materiais'
            AND PRODUTO.STATUS = 'A' 
            AND PRODUTO.LOCACAO = 'S'
                ORDER BY PRODUTO.ID
            OFFSET @offset ROWS 
            FETCH NEXT @pageSize ROWS ONLY
        ";

        const string count = @$"
        SELECT COUNT(*) AS Value
            FROM TPAPRODUTO AS PRODUTO
        WHERE IDX_NEGOCIO = 'Locação de Materiais'
        AND PRODUTO.STATUS = 'A' 
        AND PRODUTO.LOCACAO = 'S'
        ";

        var totalRecords = await _dbPrincipal.Database.SqlQueryRaw<int>(count).SingleAsync(cancellationToken);

        var materiais = await _dbPrincipal.ProdutosEvento
            .FromSqlRaw(_query,
                new SqlParameter("@offset", offset),
                new SqlParameter("@pageSize", pageSize))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new PagedResponse<ProdutoEvento>
        {
            Data = materiais,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
        };
    }

    public async Task<PagedResponse<ItemServico>> GetItensServico()
    {
        const string _query = @$"
            SELECT 
                ID as id, 
                PK_EVENTOSV as pkEventoSv, 
                DESCRICAO as descricao, 
                TPIMPRESSAO as tpImpressao, 
                TOTALIZADOR as totalizador, 
                STATUS as status, 
                CLASSES as classes, 
                QTPORCOES as qtPorcoes, 
                CONVIDADOSPORCAO as convidadosPorcao, 
                TPIMPRESSAOTOT as tpImpressaoTot,
                TPIMPRESSAOMSG as tpImpressaoMsg, 
                TPREGISTROITENS as tpRegistroItens
            FROM TPAEVENTOSV
        ";

        const string count = "SELECT COUNT(*) AS Value FROM TPAEVENTOSV";

        var totalRecords = await _dbPrincipal.Database.SqlQueryRaw<int>(count).SingleAsync();

        var tiposServico = await _dbPrincipal.ItensServico.FromSqlRaw(_query).ToListAsync();
        return new PagedResponse<ItemServico>
        {
            Data = tiposServico,
            PageNumber = 1,
            PageSize = 100,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)100)
        };
    }

    public async Task<PagedResponse<TabelaPreco>> GetTabelasPreco()
    {
        const string _query = @$"
            SELECT ID as id, PK_TABELA as pkTabela, DESCRICAO as descricao FROM TPATABELA
        ";

        const string count = "SELECT COUNT(*) AS Value FROM TPATABELA";

        var totalRecords = await _dbPrincipal.Database.SqlQueryRaw<int>(count).SingleAsync();

        var tabelasPreco = await _dbPrincipal.TabelasPreco.FromSqlRaw(_query).ToListAsync();
        return new PagedResponse<TabelaPreco>
        {
            Data = tabelasPreco,
            PageNumber = 1,
            PageSize = 100,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)100)
        };
    }

}