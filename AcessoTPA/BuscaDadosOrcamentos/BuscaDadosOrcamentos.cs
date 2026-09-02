using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class BuscaDadosOrcamentos
{
    private readonly BancoPrincipal _bancoPrincipal;
    private readonly HashSet<string> statusPedidos = ["V", "N", "C"];
    private readonly DateTime dataInicio = new DateTime(2026, 9, 1);

    public BuscaDadosOrcamentos(BancoPrincipal bancoPrincipal)
    {
        _bancoPrincipal = bancoPrincipal;
    }

    public async Task<PagedResponse<TpaDoctopedDTO>> BuscarORs(QueryFilter filter, CancellationToken cancellationToken)
    {
        var pageNumber = Math.Max(1, filter.PageNumber);
        var pageSize = Math.Clamp(filter.PageSize, 1, 100);
        var offset = (pageNumber - 1) * pageSize;
        
        var query = _bancoPrincipal.Doctoped
            .Where(doc => doc.dtPrevisao >= dataInicio 
                && doc.tpDocto == "OR" 
                && (doc.situacao == "V" ||
                    doc.situacao == "N" ||
                    doc.situacao == "C"));

        var totalRecords = await query.CountAsync(cancellationToken);
        var documentos = await query
            .OrderBy(doc => doc.pkDoctoped)
            .Skip(offset)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResponse<TpaDoctopedDTO>
        {
            Data = documentos,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
        };
    }
    
    public async Task<List<TpaMovtopedDTO>> BuscarProdutosORs(int idDoctoped, CancellationToken cancellationToken)
    {
        var produtosEvento = await _bancoPrincipal.Movtoped
            .Where(mov => mov.rdxDoctoped == idDoctoped)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return produtosEvento;
    }
    
    public async Task<InfoFaturamento?> BuscarDadosFaturamento(int idDoctoped, CancellationToken cancellationToken)
    {
        const string _query = @$"
            WITH OR_MES AS (
            SELECT DISTINCT
                PED.PK_DOCTOPED,
                PED.TPDOCTO,
                PED.DOCUMENTO,
                PED.IDX_VENDEDOR1,
                PED.TOTALDOCTO,
                CTR.PK_CONTRATOMOV,
                CAST(CTR.DTINICIO AS DATE) AS DATA
            FROM SOUTTOMAYOR.dbo.TPADOCTOPED PED
            INNER JOIN SOUTTOMAYOR.dbo.TPACONTRATOMOV CTR
                ON CTR.IDX_DOCTOEST = PED.PK_DOCTOPED
                AND CTR.STATUS = 'A'
            WHERE PED.PK_DOCTOPED = @idDoctoped
        )
        SELECT
            O.PK_DOCTOPED as pkDoctoped,
        O.DOCUMENTO as documento,
        O.TOTALDOCTO as totalDocto,
            SUM(COALESCE(P.VALORPAGO, 0)) AS valorBaixado
        FROM OR_MES O
        INNER JOIN SOUTTOMAYOR.dbo.TPADESPESA D
            ON D.CONTRATOMOV = O.PK_CONTRATOMOV
        INNER JOIN SOUTTOMAYOR.dbo.TPADESPESAPARC P
            ON P.RDX_DESPESA = D.PK_DESPESA
            AND P.SITUACAO = 'P'
        GROUP BY
        O.DOCUMENTO,
        O.TOTALDOCTO,
        O.PK_DOCTOPED;";

        var faturamento = await _bancoPrincipal.Faturamento
            .FromSqlRaw(_query, new SqlParameter("@idDoctoped", idDoctoped))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
            //.SingleOrDefaultAsync(cancellationToken);

        return faturamento.SingleOrDefault();
    }
}
