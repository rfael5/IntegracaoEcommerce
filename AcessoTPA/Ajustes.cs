using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class Ajustes
{
    private readonly AppDbContext _context;

    public Ajustes(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CriarPkAjustePed()
    {
        var ultimoId = await _context.Database.SqlQueryRaw<int>("SELECT MAX(ID) AS Value FROM TPAAJUSTEPED").FirstAsync();
        var novoId = ultimoId + 1;
        return novoId;
    }

    public async Task<TpaAjustePedDTO> CriarAjustePed(AjustePedido ajuste)
    {
        var novoAjuste = new TpaAjustePedDTO
        {
            rdxDoctoped = ajuste.rdxDoctoped,
            numero = ajuste.numero,
            descricao = ajuste.descricao,
            temProducao = ajuste.temProducao,
            totalValor = ajuste.totalValor,
            obs = ajuste.obs,
            opInc = ajuste.operador,
            opAlt = ajuste.operador
        };

        _context.AjustePed.Add(novoAjuste);
        await _context.SaveChangesAsync();
        await _context.Entry(novoAjuste).ReloadAsync();
        Console.WriteLine(JsonSerializer.Serialize(novoAjuste));

        return novoAjuste;
    }

    public async Task<TpaAjustePedItemDTO> CriarAjustePedItem(AjusteItem item, int rdxAjustePed)
    {
       var novoAjusteItem = new TpaAjustePedItemDTO
       {
           rdxAjustePed = rdxAjustePed,
           idxMovtoped = item.idxMovtoped,
           quantidade = item.quantidade,
           preco = item.preco,
           opInc = item.operador,
           opAlt = item.operador,
           tpAjusteProducao = item.tpAjusteProducao,
           tpAjusteSeparacao = item.tpAjusteSeparacao,
           qtOriginal = item.qtOriginal
       }; 

       return novoAjusteItem;
    }

    public async Task AtualizarTotalAjusteDoctoped(int pkDoctoped, decimal totalAjuste)
    {
        var totalAjusteAtual = await _context.Doctoped.Where(d => d.pkDoctoped == pkDoctoped).Select(d => d.totalAjuste).SingleAsync();
        Console.WriteLine(totalAjusteAtual);
        var valorAjusteAtualizado = totalAjusteAtual + totalAjuste;
        const string _query = @$"
            UPDATE TPADOCTOPED 
                SET TOTALAJUSTE = @valorAjuste
            WHERE PK_DOCTOPED = @pkDoctoped
        ";

        await _context.Database.ExecuteSqlRawAsync(_query,
            new SqlParameter("@valorAjuste", valorAjusteAtualizado),
            new SqlParameter("@pkDoctoped", pkDoctoped));
    }

    public async Task<int> CadastrarAjuste(InformacoesAjuste ajuste)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var ajustePed = await CriarAjustePed(ajuste.ajustePedido);
            foreach(var item in ajuste.itensAjuste)
            {
                var ajusteItem = await CriarAjustePedItem(item, ajustePed.pkAjustePed);
                _context.AjustePedItem.Add(ajusteItem);
            }

            var pkDoctoped = Convert.ToInt32(ajuste.ajustePedido.rdxDoctoped);
            await AtualizarTotalAjusteDoctoped(pkDoctoped, ajuste.ajustePedido.totalValor);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return ajustePed.pkAjustePed;
        }
        catch(Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e);
            Console.WriteLine(JsonSerializer.Serialize(ajuste));
            throw;
        }
    }

}