using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class Ajustes
{
    private readonly AppDbContext _context;
    private readonly GeracaoContrato _contratos;

    public Ajustes(AppDbContext context, GeracaoContrato contratos)
    {
        _context = context;
        _contratos = contratos;
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

    public async Task<int> CriarAdendoContrato(DadosContratoAdendo dadosAdendo)
    {
        var adendo = new TpaContratoAdendoDTO
        {
            rdxContrato = dadosAdendo.idContrato,
            adendo = dadosAdendo.adendo,
            descricao = dadosAdendo.descricaoAjuste,
            opInc = dadosAdendo.operador,
            opAlt = dadosAdendo.operador,
            idxTabela = dadosAdendo.idAjuste,
            opAtivacao = dadosAdendo.operador
        };

        _context.ContratosAdendos.Add(adendo);
        await _context.SaveChangesAsync();
        await _context.Entry(adendo).ReloadAsync();
        Console.WriteLine(JsonSerializer.Serialize(adendo));
        return adendo.pkContratoAdendo;
    }

    public async Task AtualizarContratoMov(DadosContratoAdendo dadosAdendo, int idxContratoAdendo)
    {
        var totalDocto = await _context.ContratoMov
            .Where(d => d.pkContratoMov == dadosAdendo.pkContratoMov )
            .Select(d => d.totalDocto)
            .SingleAsync();
        
        var novoValorDocto = totalDocto + dadosAdendo.valorAjuste;
        
        var dataAtual = BrazilTime.Now();
        const string _query = $@"
            UPDATE TPACONTRATOMOV
                SET TOTALDOCTO = @totalDocto,
                DTALT = @dataAtual,
                OPALT = @operador,
                IDX_CONTRATOADENDO = @idxContratoAdendo
            WHERE PK_CONTRATOMOV = @pkContratoMov";
        
        await _context.Database.ExecuteSqlRawAsync(
            _query,
            new SqlParameter("totalDocto", novoValorDocto),
            new SqlParameter("dataAtual", dataAtual),
            new SqlParameter("operador", dadosAdendo.operador),
            new SqlParameter("idxContratoAdendo", idxContratoAdendo),
            new SqlParameter("pkContratoMov", dadosAdendo.pkContratoMov));
    }

    // public async Task AtualizarEventoOrc(int idEventoOrc)
    // {
    //     const string _query = @$"
    //         UPDATE TPAEVENTOORC
    //             SET OPCOMPRA = 'R',
    //             ASSOCIAMATERIAL='S'
    //         WHERE ID = @idEventoOrc
    //     ";

    //     await _context.Database.ExecuteSqlRawAsync(_query, new SqlParameter("idEventoOrc", idEventoOrc));
    // }

    public async Task InserirHistorico(DadosContratoAdendo dadosAdendo)
    {
         var doctopedHistorico = new TpaDoctopedHistoricoDTO
        {
            rdxDoctoped = dadosAdendo.idDoctoped,
            etapa = "T",
            parcial = "N",
            situacao = "A",
            texto = $"CONTRATO;{dadosAdendo.adendo}",
            dtInc = BrazilTime.Now(),
            opInc = dadosAdendo.operador,
        };

        _context.DoctopedHistorico.Add(doctopedHistorico);
    }

    public async Task TornarAjusteVigente(int idAjuste, int operador)
    {
        var now = BrazilTime.Now();
        const string _query = @$"
            UPDATE TPAAJUSTEPED
                SET SITUACAO = 'V',
                DTALT = @now,
                OPALT = @operador
            WHERE PK_AJUSTEPED = @idAjuste
        ";

        await _context.Database.ExecuteSqlRawAsync(
            _query,
            new SqlParameter("now", now),
            new SqlParameter("operador", operador),
            new SqlParameter("idAjuste", idAjuste));
    }

    public async Task<DadosContratoAdendo> BuscarDadosAdendo(AjustePedido request, int idAjuste)
    {
        var dadosAdendo = await _context.AjustePed
            .Where(ajuste => ajuste.pkAjustePed == idAjuste)
            .Join(_context.ContratoMov,
            ajuste => ajuste.rdxDoctoped,
            contratoMov => contratoMov.idxDoctoEst,
            (ajuste, contratoMov) => new DadosContratoAdendo
            {
                idContrato = (int)contratoMov.rdxContrato,
                adendo = request.descricaoAdendo,
                descricaoAjuste = ajuste.descricao,
                operador = request.operador,
                valorAjuste = (decimal)ajuste.totalValor,
                pkContratoMov = contratoMov.pkContratoMov,
                associaMaterial = request.associaMaterial,
                idAjuste = Convert.ToString(ajuste.pkAjustePed),
                idDoctoped = (int)ajuste.rdxDoctoped
            }).SingleAsync();   
        
        return dadosAdendo;
    }

    public async Task<List<InfoMovtopedContratoItem>> BuscarProdutosAjuste(int idAjuste, int rdxContratoMov)
    {
        const string _query = @$"
            SELECT M.PK_MOVTOPED AS pkMovtoped, M.IDX_PRODUTO AS idxProduto, A.QUANTIDADE AS l_quantidade, A.PRECO AS l_precouni, 
            M.L_VALORBEM AS l_valorbem, M.IDX_PATRIMONIO AS idxPatrimonio, M.IDX_PATRIMONIOMOVTO AS idxPatrimonioMovto, M.REFERENCIA AS referencia,
            M.TIPOPROD AS tipoProd, M.LOCACAO AS locacao, M.UNIDADE AS unidade, M.LOCACAOBP AS locacaoBp, M.L_P AS l_p, M.CODPRODUTO AS codProduto
            FROM TPAAJUSTEPEDITEM AS A
                INNER JOIN TPAMOVTOPED AS M ON A.IDX_MOVTOPED = M.PK_MOVTOPED
            WHERE RDX_AJUSTEPED = @idAjuste";
        
        var ultimoItem = await _context.ContratoItem.Where(produto => produto.rdxContratoMov == rdxContratoMov).MaxAsync(produto => produto.item);

        Console.WriteLine($"ULTIMO ITEM: {ultimoItem}");
        
        var produtos = await _context.AjustePedItem
        .Where(ajuste => ajuste.rdxAjustePed == idAjuste)
        .Join(
            _context.Movtoped, 
            ajuste => ajuste.idxMovtoped, 
            movtoped => movtoped.pkMovtoped,
            (ajuste, movtoped) => new InfoMovtopedContratoItem {
                pkMovtoped = movtoped.pkMovtoped, 
                idxProduto = movtoped.idxProduto,
                l_quantidade = (decimal)ajuste.quantidade, 
                l_precototal = (decimal)ajuste.preco, 
                item = ultimoItem + movtoped.item,
                l_valorbem = movtoped.l_valorbem,
                idxPatrimonio = movtoped.idxPatrimonio, 
                idxPatrimonioMovto = movtoped.idxPatrimonioMovto, 
                referencia = movtoped.referencia, 
                tipoProd = movtoped.tipoProd, 
                locacao = movtoped.locacao, 
                unidade = movtoped.unidade, 
                locacaoBp = movtoped.locacaoBp, 
                l_p = movtoped.l_p, 
                codProduto = movtoped.codProduto})
        .ToListAsync();

        return produtos;
                    
    }

    public async Task<int> AutorizarAjuste(AjustePedido requestAdendo, int idAjuste)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var dadosAdendo = await BuscarDadosAdendo(requestAdendo, idAjuste);
            var idAdendo = await CriarAdendoContrato(dadosAdendo);
            var produtos = await BuscarProdutosAjuste(Convert.ToInt32(dadosAdendo.idAjuste), dadosAdendo.pkContratoMov);
            foreach(var item in produtos)
            {
                await _contratos.CriarContratoItem(item, dadosAdendo.pkContratoMov, dadosAdendo.operador, idxContratoAdendo:idAdendo);
            }

            await AtualizarContratoMov(dadosAdendo, idAdendo);
            await TornarAjusteVigente(Convert.ToInt32(dadosAdendo.idAjuste), dadosAdendo.operador);
            await InserirHistorico(dadosAdendo);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return idAdendo;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e);
            throw;
        }
    }

    /// /////////////////////////////////////////////////////////////////////
    /// CADASTRAR E GERAR CONTRATO NA MESMA FUNÇÃO
    /// ////////////////////////////////////////////////////////////////////
    /// /// ////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////
    /// 
    
    public async Task<int> CadastrarAjusteGerarAdendoContrato(InformacoesAjuste ajuste)
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
            await AutorizarAdendoAposCriacaoAjuste(ajuste.ajustePedido, ajustePed.pkAjustePed);
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

    public async Task<int> AutorizarAdendoAposCriacaoAjuste(AjustePedido requestAdendo, int idAjuste)
    {
        try
        {
            var dadosAdendo = await BuscarDadosAdendo(requestAdendo, idAjuste);
            var idAdendo = await CriarAdendoContrato(dadosAdendo);
            var produtos = await BuscarProdutosAjuste(Convert.ToInt32(dadosAdendo.idAjuste), dadosAdendo.pkContratoMov);
            foreach(var item in produtos)
            {
                await _contratos.CriarContratoItem(item, dadosAdendo.pkContratoMov, dadosAdendo.operador, idxContratoAdendo:idAdendo);
            }

            await AtualizarContratoMov(dadosAdendo, idAdendo);
            await TornarAjusteVigente(Convert.ToInt32(dadosAdendo.idAjuste), dadosAdendo.operador);
            await InserirHistorico(dadosAdendo);

            await _context.SaveChangesAsync();
            return idAdendo;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}