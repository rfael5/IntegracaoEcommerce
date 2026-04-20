using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class ServicosTPA 
{
    private readonly PrincipalDbContext _dbPrincipal;

    public ServicosTPA(PrincipalDbContext dbPrincipal)
    {
        _dbPrincipal = dbPrincipal;
    }

    public async Task<List<CategoriaEvento>> GetCategoriasEventos()
    {
        const string _query = $@"
            SELECT ID, PK_EVENTO, DESCRICAO, ESCALAALERTA FROM TPAEVENTO
        ";

        var categorias = await _dbPrincipal.CategoriasEvento.FromSqlRaw(_query).ToListAsync();
        return categorias;
    }

    public async Task<List<EventoTp>> GetTiposServico()
    {
        const string _query = $@"SELECT ID, PK_EVENTOTP, DESCRICAO, STATUS, OBS FROM TPAEVENTOTP";
        var tipos = await _dbPrincipal.EventoTps.FromSqlRaw(_query).ToListAsync();
        return tipos;
    }

    
}