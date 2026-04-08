using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class GeracaoContrato
{
    private readonly AppDbContext _context;
    
    public GeracaoContrato(AppDbContext context)
    {
        _context = context;
    }

    public async Task SetarSituacaoMovtopedAutorizado(int pkDoctoped)
    {
        const string _query = @$"
            UPDATE TPAMOVTOPED SET SITUACAO = 'Z' WHERE RDX_DOCTOPED=@pkDoctoped
        ";

        await _context.Database.ExecuteSqlRawAsync(_query, new SqlParameter("pkDoctoped", pkDoctoped));
    }
}