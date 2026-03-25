using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class TiposServicoTPA
{
    private readonly PrincipalDbContext _dbPrincipal;
    public List<TipoServico> servicos; 

    public TiposServicoTPA(PrincipalDbContext dbPrincipal)
    {
        _dbPrincipal = dbPrincipal;   
        GetTiposServico();
        foreach(var teste in servicos)
        {
            Console.WriteLine(teste.id);
        }
    }

    public void GetTiposServico()
    {
        var _query = @$"
             SELECT 
                ID, 
                PK_EVENTOSV, 
                DESCRICAO, 
                TPIMPRESSAO, 
                TOTALIZADOR, 
                STATUS, 
                CLASSES, 
                QTPORCOES, 
                CONVIDADOSPORCAO, 
                TPIMPRESSAOTOT,
                TPIMPRESSAOMSG, 
                TPREGISTROITENS 
            FROM TPAEVENTOSV
        ";

        servicos = _dbPrincipal.Servicos.FromSqlRaw(_query).ToList();
    }
}