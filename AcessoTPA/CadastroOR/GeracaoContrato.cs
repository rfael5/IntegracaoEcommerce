using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class GeracaoContrato
{
    private readonly AppDbContext _context;
    
    public GeracaoContrato(AppDbContext context)
    {
        _context = context;
    }


    private string CriarNomeDocumento(int? documento)
    {
        var sevenDigitsLength = documento?.ToString("D7");
        var documentName = $"OR{sevenDigitsLength}";
        return documentName;
    }

    private async Task<(int pkContrato, DateTime? dtInc)> CriarContrato(dynamic dadosContrato, int operador)
    {
        var novoContrato = new TpaContratoDTO
        {
            documento = CriarNomeDocumento(dadosContrato.documento),
            idxEntidade = dadosContrato.idxEntidade,
            opInc = operador,
            opAlt = operador,
            idxDoctoEst = dadosContrato.pkDoctoped,
            opAtivacao = operador 
        };

        _context.Contratos.Add(novoContrato);
        await _context.SaveChangesAsync();
        await _context.Entry(novoContrato).ReloadAsync();
        Console.WriteLine(novoContrato.GetType());
        Console.WriteLine(JsonSerializer.Serialize(novoContrato));
        return (novoContrato.pkContrato, novoContrato.dtInc);
    }

    private async Task<int> CriarContratoMov(
        dynamic dadosContratoMov, 
        int pkContrato, 
        DateTime dataContrato,
        int operador)
    {
        var horaCriacaoContrato = dataContrato.Hour;
        var hora = horaCriacaoContrato.ToString("D2");
        var textoHora = $"{hora}00";
        var novoContratoMov = new TpaContratoMovDTO
        {
            rdxContrato = pkContrato,
            dtInicio = dataContrato, 
            dtVencto = dataContrato, 
            idxTabela = dadosContratoMov.idxTabela,
            idxTabelaSub = dadosContratoMov.idxTabelaSub,
            totalDocto = dadosContratoMov.totalDocto,
            idxDoctoEst = dadosContratoMov.pkDoctoped,
            opInc = operador,
            opAlt = operador,
            horaSaida = textoHora
        };

        _context.ContratoMov.Add(novoContratoMov);
        await _context.SaveChangesAsync();
        await _context.Entry(novoContratoMov).ReloadAsync();
        
        return novoContratoMov.pkContratoMov;
    }
    
    public async Task CriarContratoItem(InfoMovtopedContratoItem movtoped, int pkContratoMov, int operador, int idxContratoAdendo = 0)
    {
        var novoItem = new TpaContratoItemDTO
        {
            rdxContratoMov = pkContratoMov,
            item = movtoped.item,
            idxMovtoEst = movtoped.pkMovtoped,
            idxProduto = movtoped.idxProduto,
            lQuantidade = movtoped.l_quantidade,
            lPrecoUni = movtoped.l_precouni,
            lPrecoTotal = movtoped.l_precototal,
            lValorBem = movtoped.l_valorbem,
            dtInc = BrazilTime.Now(),
            opInc = operador,
            dtAlt = BrazilTime.Now(),
            opAlt = operador,
            descricao = movtoped.referencia,
            referencia = movtoped.referencia,
            tipoProd = movtoped.tipoProd,
            locacao = movtoped.locacao,
            unidade = movtoped.unidade,
            locacaoBp = movtoped.locacaoBp,
            lP = movtoped.l_p,
            codProduto = movtoped.codProduto,
        };

        _context.ContratoItem.Add(novoItem);
    }


    private async Task SetarSituacaoMovtopedAutorizado(int pkDoctoped, int opAlt)
    {
        var now = BrazilTime.Now();
        const string _query = @$"
            UPDATE TPAMOVTOPED 
                SET SITUACAO = 'Z',
                OPALT = @opAlt,
                DTALT = @dtAlt 
            WHERE RDX_DOCTOPED=@pkDoctoped
        ";

        await _context.Database.ExecuteSqlRawAsync(_query, 
            new SqlParameter("pkDoctoped", pkDoctoped),
            new SqlParameter("opAlt", opAlt),
            new SqlParameter("dtAlt", now));
    }

    private async Task SetarSituacaoDoctopedVigente(
        int pkDoctoped, 
        int pkContratoMov, 
        int operador, 
        string temProfissional)
    {
        var now = BrazilTime.Now();
        const string _query = @$"
            UPDATE TPADOCTOPED
                SET SITUACAO = 'V',
                TEMPROFISSIONAL = @temProfissional,
                IDX_CONTRATOMOV = @pkContratoMov,
                DTALT = @dataAtual,
                OPALT = @opAlt
            WHERE PK_DOCTOPED = @pkDoctoped
        ";

        await _context.Database.ExecuteSqlRawAsync(_query,
            new SqlParameter("temProfissional", temProfissional),
            new SqlParameter("pkContratoMov", pkContratoMov),
            new SqlParameter("dataAtual", now),
            new SqlParameter("opAlt", operador),
            new SqlParameter("pkDoctoped", pkDoctoped));
    }

    private async Task InserirHistorico(int pkDoctoped, int operador)
    {
        var doctopedHistorico = new TpaDoctopedHistoricoDTO
        {
            rdxDoctoped = pkDoctoped,
            etapa = "T",
            parcial = "N",
            situacao = "I",
            texto = "CONTRATO;SITUACAO",
            dtInc = BrazilTime.Now(),
            opInc = operador,
        };

        _context.DoctopedHistorico.Add(doctopedHistorico);
    }

    public async Task<int> GerarContrato(int pkDoctoped, int operador, string temProfissional)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {

            var doctoped = await _context.Doctoped
                .Where(d => d.pkDoctoped == pkDoctoped)
                .Select((d) => new { d.pkDoctoped, d.documento, d.idxEntidade, d.idxTabela, d.idxTabelaSub, d.totalDocto })
                .SingleAsync();
            
            

            var contrato = await CriarContrato(doctoped, operador);
            int pkContrato = (int)contrato.pkContrato;
            DateTime dataCriacaoContrato = (DateTime)contrato.dtInc;

            var contratoMov = await CriarContratoMov(doctoped, pkContrato, dataCriacaoContrato, operador);

            var movtoped = await _context.Movtoped
                .Where(m => m.rdxDoctoped == pkDoctoped)
                .Select(m => new InfoMovtopedContratoItem
                {
                    pkMovtoped = m.pkMovtoped,
                    item = m.item,
                    idxProduto = m.idxProduto,
                    l_quantidade = m.l_quantidade,
                    l_precouni = m.l_precouni,
                    l_precototal = m.l_precototal,
                    l_valorbem = m.l_valorbem,
                    idxPatrimonio = m.idxPatrimonio,
                    idxPatrimonioMovto = m.idxPatrimonioMovto,
                    referencia = m.referencia,
                    tipoProd = m.tipoProd,
                    locacao = m.locacao,
                    unidade = m.unidade,
                    locacaoBp = m.locacaoBp,
                    l_p = m.l_p,
                    codProduto = m.codProduto
                })
                .ToListAsync();


            foreach (var item in movtoped)
            {
                await CriarContratoItem(item, contratoMov, operador);
            }

            await SetarSituacaoMovtopedAutorizado(pkDoctoped, operador);
            await SetarSituacaoDoctopedVigente(pkDoctoped, contratoMov, operador, temProfissional);
            await InserirHistorico(pkDoctoped, operador);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return pkContrato;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e);
            throw;
        }
    } 

    public async Task<int> GerarContratoAposCriacao(int pkDoctoped, int operador, string temProfissional)
    {
        try
        {

            var doctoped = await _context.Doctoped
                .Where(d => d.pkDoctoped == pkDoctoped)
                .Select((d) => new { d.pkDoctoped, d.documento, d.idxEntidade, d.idxTabela, d.idxTabelaSub, d.totalDocto })
                .SingleAsync();
            
            var contrato = await CriarContrato(doctoped, operador);
            int pkContrato = (int)contrato.pkContrato;
            DateTime dataCriacaoContrato = (DateTime)contrato.dtInc;

            var contratoMov = await CriarContratoMov(doctoped, pkContrato, dataCriacaoContrato, operador);

            var movtoped = await _context.Movtoped
                .Where(m => m.rdxDoctoped == pkDoctoped)
                .Select(m => new InfoMovtopedContratoItem
                {
                    pkMovtoped = m.pkMovtoped,
                    item = m.item,
                    idxProduto = m.idxProduto,
                    l_quantidade = m.l_quantidade,
                    l_precouni = m.l_precouni,
                    l_precototal = m.l_precototal,
                    l_valorbem = m.l_valorbem,
                    idxPatrimonio = m.idxPatrimonio,
                    idxPatrimonioMovto = m.idxPatrimonioMovto,
                    referencia = m.referencia,
                    tipoProd = m.tipoProd,
                    locacao = m.locacao,
                    unidade = m.unidade,
                    locacaoBp = m.locacaoBp,
                    l_p = m.l_p,
                    codProduto = m.codProduto
                })
                .ToListAsync();


            foreach (var item in movtoped)
            {
                await CriarContratoItem(item, contratoMov, operador);
            }

            await SetarSituacaoMovtopedAutorizado(pkDoctoped, operador);
            await SetarSituacaoDoctopedVigente(pkDoctoped, contratoMov, operador, temProfissional);
            await InserirHistorico(pkDoctoped, operador);

            await _context.SaveChangesAsync();
            return pkContrato;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    } 

}