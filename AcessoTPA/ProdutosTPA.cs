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
            PRODUTO.CODPRODUTO as codProduto, PRODUTO.DESCRICAO AS nomeProduto, PRODUTO.REFERENCIA as referencia, PRODUTO.UN1 as un, 
            PRODUTO.IDX_NEGOCIO as idxNegocio, PRODUTO.IDX_CLASSIFICACAO as idxClassificacao, PRODUTO.CSTI as csti, PRODUTO.PCCUSTO as pcCusto, 
            PRODUTO.LOCACAO as locacao, PRODUTO.NCM as ncm, PRODUTO.ENCOMENDA AS permiteEncomenda
                FROM TPAPRODEVENTOSV as PRODSV
            INNER JOIN TPAEVENTOSV AS TPSV ON PRODSV.RDX_EVENTOSV = TPSV.PK_EVENTOSV
            INNER JOIN TPAPRODUTO AS PRODUTO ON PRODSV.IDX_PRODUTO = PRODUTO.PK_PRODUTO
                AND PRODUTO.IDX_NEGOCIO NOT IN ('Desativados')
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
                AND PRODUTO.IDX_NEGOCIO NOT IN ('Desativados')
                AND PRODUTO.STATUS = 'A'
                AND PRODUTO.VENDA = 'S'
        ";

        var totalRecords = await _dbPrincipal.Database.SqlQueryRaw<int>(count).SingleAsync(cancellationToken);

        var produtos = await _dbPrincipal.ProdutosServico.FromSqlRaw(_query, 
            new SqlParameter("@offset", offset),
            new SqlParameter("@pageSize", pageSize))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        foreach (var produto in produtos)
        {
            produto.itemServico = produto.itemServico?.Replace("\0", "");
            produto.codProduto = produto.codProduto?.Replace("\0", "");
            produto.nomeProduto = produto.nomeProduto?.Replace("\0", "");
            produto.referencia = produto.referencia?.Replace("\0", "");
            produto.un = produto.un?.Replace("\0", "");
            produto.idxNegocio = produto.idxNegocio?.Replace("\0", "");
            produto.idxClassificacao = produto.idxClassificacao?.Replace("\0", "");
            produto.locacao = produto.locacao?.Replace("\0", "");
            produto.ncm = produto.ncm?.Replace("\0", "");
        }

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
            PRODUTO.UN1, PRODUTO.IDX_NEGOCIO, PRODUTO.IDX_CLASSIFICACAO, PRODUTO.CSTI, PRODUTO.PCCUSTO, PRODUTO.LOCACAO, PRODUTO.NCM, 
            TABELA.PK_TABELA, TABELA.DESCRICAO NOMETABELA, TABELAPRECOS.PRECO1U1, PRODUTO.ENCOMENDA
                FROM TPAPRODUTO AS PRODUTO
            INNER JOIN TPATABELAPROD AS TABELAPRECOS ON PRODUTO.PK_PRODUTO = TABELAPRECOS.IDX_PRODUTO
            INNER JOIN TPATABELA AS TABELA ON TABELAPRECOS.RDX_TABELA = TABELA.PK_TABELA
                WHERE PRODUTO.IDX_NEGOCIO NOT IN ('Desativados') 
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
                WHERE PRODUTO.IDX_NEGOCIO NOT IN ('Desativados') 
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
        foreach (var produto in produtos)
        {
            produto.codProduto = produto.codProduto?.Replace("\0", "");
            produto.descricao= produto.descricao?.Replace("\0", "");
            produto.referencia = produto.referencia?.Replace("\0", "");
            produto.un = produto.un?.Replace("\0", "");
            produto.idxNegocio = produto.idxNegocio?.Replace("\0", "");
            produto.idxClassificacao = produto.idxClassificacao?.Replace("\0", "");
            produto.locacao = produto.locacao?.Replace("\0", "");
            produto.ncm = produto.ncm?.Replace("\0", "");
        }

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
            SELECT PRODUTO.ID, PRODUTO.PK_PRODUTO, PRODUTO.CODPRODUTO, PRODUTO.DESCRICAO, PRODUTO.REFERENCIA, PRODUTO.UN1, 
            PRODUTO.IDX_NEGOCIO, PRODUTO.IDX_CLASSIFICACAO, PRODUTO.CSTI, PRODUTO.PCCUSTO, PRODUTO.LOCACAO, PRODUTO.NCM,
            PRODUTO.ENCOMENDA
                FROM TPAPRODUTO AS PRODUTO
            WHERE PRODUTO.IDX_NEGOCIO NOT IN ('Desativados')
            AND PRODUTO.STATUS = 'A' 
            AND PRODUTO.VENDA = 'S'
                ORDER BY PRODUTO.ID
            OFFSET @offset ROWS 
            FETCH NEXT @pageSize ROWS ONLY
        ";

        const string count = @"
            SELECT COUNT(*) as Value
                FROM TPAPRODUTO AS PRODUTO
            WHERE PRODUTO.IDX_NEGOCIO NOT IN ('Desativados') 
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
        
        foreach (var produto in produtos)
        {
            produto.codProduto = produto.codProduto?.Replace("\0", "");
            produto.descricao= produto.descricao?.Replace("\0", "");
            produto.referencia = produto.referencia?.Replace("\0", "");
            produto.un = produto.un?.Replace("\0", "");
            produto.idxNegocio = produto.idxNegocio?.Replace("\0", "");
            produto.idxClassificacao = produto.idxClassificacao?.Replace("\0", "");
            produto.locacao = produto.locacao?.Replace("\0", "");
            produto.ncm = produto.ncm?.Replace("\0", "");
        }

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
            SELECT PRODUTO.ID, PRODUTO.PK_PRODUTO, PRODUTO.CODPRODUTO, PRODUTO.DESCRICAO, PRODUTO.REFERENCIA, PRODUTO.UN1, 
            PRODUTO.IDX_NEGOCIO, PRODUTO.IDX_CLASSIFICACAO, PRODUTO.CSTI, PRODUTO.PCCUSTO, PRODUTO.LOCACAO, PRODUTO.NCM,
            PRODUTO.ENCOMENDA
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

        foreach (var produto in materiais)
        {
            produto.codProduto = produto.codProduto?.Replace("\0", "");
            produto.descricao= produto.descricao?.Replace("\0", "");
            produto.referencia = produto.referencia?.Replace("\0", "");
            produto.un = produto.un?.Replace("\0", "");
            produto.idxNegocio = produto.idxNegocio?.Replace("\0", "");
            produto.idxClassificacao = produto.idxClassificacao?.Replace("\0", "");
            produto.locacao = produto.locacao?.Replace("\0", "");
            produto.ncm = produto.ncm?.Replace("\0", "");
        }

        return new PagedResponse<ProdutoEvento>
        {
            Data = materiais,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
        };
    }

    public async Task<PagedResponse<RelacaoServicoProduto>> GetRelacaoServicoProduto(
        QueryFilter filter,
        CancellationToken cancellationToken = default
    )
    {
        var pageNumber = Math.Max(1, filter.PageNumber);
        var pageSize = Math.Clamp(filter.PageSize, 1, 1000);
        var offset = (pageNumber - 1) * pageSize;

        const string _query = @$"
            
            SELECT RDX_EVENTOSV as idItemServico, IDX_PRODUTO as idProduto 
                FROM TPAPRODEVENTOSV
            ORDER BY RDX_EVENTOSV    
            OFFSET @offset ROWS 
            FETCH NEXT @pageSize ROWS ONLY
        ";

        const string count = @$"
        SELECT COUNT(*) AS Value FROM TPAPRODEVENTOSV
        ";

        var totalRecords = await _dbPrincipal.Database.SqlQueryRaw<int>(count).SingleAsync(cancellationToken);
        var relacaoServicoProduto = await _dbPrincipal.Database.SqlQueryRaw<RelacaoServicoProduto>(_query,
                new SqlParameter("@offset", offset),
                new SqlParameter("@pageSize", pageSize))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new PagedResponse<RelacaoServicoProduto>
        {
            Data = relacaoServicoProduto,
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

        foreach (var servico in tiposServico)
        {
            servico.descricao= servico.descricao?.Replace("\0", "");
            servico.totalizador = servico.totalizador?.Replace("\0", "");
            servico.classes = servico.classes?.Replace("\0", "");
        }

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

    public async Task<List<EventoTpSv>> GetTiposServicos()
    {
        const string _query = $@"
            SELECT 
                ID, 
                PK_EVENTOTPSV, 
                RDX_EVENTOTP,
                DESCRICAO, 
                SEQUENCIA, 
                TPIMPRESSAO, 
                TOTALIZADOR, 
                QUANTIDADE, 
                CONVIDADOS 
                STATUS, 
                IDX_EVENTOSV,
                TPIMPRESSAOTOT, 
                TPIMPRESSAOIMG, 
                TPREGISTROITENS, 
                VARIEDADESUGERIDA, 
                PERMISSAO, 
                CALCCONVIDADO,
                IDX_IMG 
            FROM TPAEVENTOTPSV
        ";

        var tiposServicos = await _dbPrincipal.EventoTpSv.FromSqlRaw(_query).ToListAsync();
        return tiposServicos;
    }

}