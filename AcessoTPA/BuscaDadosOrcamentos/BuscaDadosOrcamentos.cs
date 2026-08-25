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
        //var queryMovtoped = _bancoPrincipal.Movtoped.FromSqlRaw($"SELECT * FROM dbo.TPAMOVTOPED WHERE RDX_DOCTOPED IN ({(idDoctopeds.Count > 0 ? string.Join(',', idDoctopeds) : "NULL")})").AsNoTracking();
        var produtosEvento = await _bancoPrincipal.Movtoped
            .Where(mov => mov.rdxDoctoped == idDoctoped)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return produtosEvento;
    }
}
