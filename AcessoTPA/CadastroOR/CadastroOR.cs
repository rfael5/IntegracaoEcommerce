using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class CadastroPedido
{
    private readonly AppDbContext _context;
    private UsuariosTPA _usuariosService;
    private AcessoTPA _acessoTpa;

    private GeracaoContrato _geracaoContrato;

    public CadastroPedido(
        AppDbContext context,
        UsuariosTPA usuariosService,
        AcessoTPA acessoTpa,
        GeracaoContrato geracaoContrato
        )
    {
        _context = context;
        _usuariosService = usuariosService;
        _acessoTpa = acessoTpa;
        _geracaoContrato = geracaoContrato;
    }

    public async Task<int?> CriarNumeroDocumentoOR(string tipoDocumento)
    {
        var maxDocumento = await _context.Doctoped.Where(doc => doc.tpDocto == tipoDocumento).MaxAsync(op => (int?)op.documento) ?? 0;
        Console.WriteLine(maxDocumento);
        Console.WriteLine(maxDocumento + 1);
        return maxDocumento + 1;
    }

    public async Task<string> CriarPkEventoOrc()
    {
        //var ultimoId = await _context.CadastroUsuarioTPA.OrderByDescending(u => u.id).Select(u => u.id).FirstAsync();
        var ultimoId = await _context.Database
        .SqlQueryRaw<int>("SELECT MAX(ID) AS Value FROM TPAEVENTOORC")
        .FirstAsync();
        var ultimoIdMaisUm = ultimoId + 1;
        //var teste = "       50526";
        //var novoPkEventoOrc = $"       {ultimoIdMaisUm}";
        return ultimoIdMaisUm.ToString().PadLeft(12);
    }

    public async Task<string> CriarPkOrcPed()
    {
        //var ultimoId = await _context.CadastroUsuarioTPA.OrderByDescending(u => u.id).Select(u => u.id).FirstAsync();
        var ultimoId = await _context.Database
        .SqlQueryRaw<int>("SELECT MAX(ID) AS Value FROM TPAEVENTOORCPED")
        .FirstAsync();
        var ultimoIdMaisUm = ultimoId + 1;
        //var teste = "       50526";
        //var novoPkOrcPed = $"       {ultimoIdMaisUm}";

        return ultimoIdMaisUm.ToString().PadLeft(12);
    }

    public async Task<InformacoesProdutoTPA> GetDadosProduto(int _pkProduto)
    {
        Console.WriteLine(_pkProduto);
        var result = await _context.Produto.Where(produto => produto.id == _pkProduto).SingleOrDefaultAsync();
        if (result == null)
        {
            throw new InvalidOperationException("Produto não encontrado no banco de dados");
        }
        return result;
    }

    public async Task CadastrarDoctopedFP(int rdxDoctoped, DoctopedAtendimento doctoped, DoctopedFpAtendimento doctopedFP)
    {
        var dataEmissao = BrazilTime.Now();
        var prazoTimespan = new TimeSpan(doctopedFP.prazo, 0, 0, 0);
        var dataVencimento = dataEmissao + prazoTimespan;
        await _context.Database.ExecuteSqlInterpolatedAsync(@$"INSERT INTO TPADOCTOPEDFP (RDX_DOCTOPED,DTEMISSAO,PRAZO,DTVENCTO,VALOR,DESCRICAO,MEIOPAGTO,IDX_OPFINANCEIRA,
            IDX_CCUSTO,DTINC,DTALT,OPINC,OPALT) VALUES ({rdxDoctoped}, {dataEmissao}, {doctopedFP.prazo}, {dataVencimento}, {doctoped.totalDocto},'', {doctopedFP.meioPagto}, 
            {doctopedFP.idxOpFinanceira}, {doctopedFP.idxCCusto},
            GETDATE(), GETDATE(), {doctoped.opInc}, {doctoped.opAlt})");

    }

    public async Task<TpaEventoOrcDTO> CadastrarEventoOrc(EventoOrcAtendimento eventoOrc)
    {
        var _pkEventoOrc = await CriarPkEventoOrc();
        var novoEventoOrc = new TpaEventoOrcDTO()
        {
            pkEventoOrc = _pkEventoOrc,
            descricao = eventoOrc.descricao,
            idxEvento = eventoOrc.idxEvento,
            convidados = eventoOrc.convidados,
            criancas = eventoOrc.criancas,
            dtInicio = eventoOrc.dtInicio,
            hsInicio = eventoOrc.hsInicio,
            dtTermino = eventoOrc.dtTermino,
            hsTermino = eventoOrc.hsTermino,
            obsInicio = eventoOrc.obsInicio,
            obsTermino = eventoOrc.obsTermino,
            idxEndereco = eventoOrc.idxEndereco,
            local = eventoOrc.local,
            opInc = eventoOrc.opInc,
            opAlt = eventoOrc.opAlt,
            idxEventoTp = eventoOrc.idxEventoTp,
            profissionalEscalado = eventoOrc.profissionalEscalado,
            custoOperacional = eventoOrc.custoOperacional,
            valorTabela = eventoOrc.valorTabela,
            prodValorOriginal = eventoOrc.prodValorOriginal,
            servValorOriginal = eventoOrc.servValorOriginal
        };
        _context.EventoOrc.Add(novoEventoOrc);
        await _context.SaveChangesAsync();
        await _context.Entry(novoEventoOrc).ReloadAsync();
        Console.WriteLine(JsonSerializer.Serialize(novoEventoOrc));

        return novoEventoOrc;
    }

    public async Task<TpaDoctopedDTO> CadastrarDoctoped(DoctopedAtendimento doctoped, string? _idxDoctoEvento = null)
    {
        var numeroDocumento = await CriarNumeroDocumentoOR(doctoped.tpDocto);
        var novaOR = new TpaDoctopedDTO()
        {
            operacao = doctoped.tpDocto == "OR" ? "OV" : "PV",
            tpDocto = doctoped.tpDocto,
            documento = numeroDocumento,
            idxEntidade = doctoped.idxEntidade,
            nome = doctoped.nome,
            cnpjCpf = doctoped.cnpjCpf,
            cidade = doctoped.cidade,
            uf = doctoped.uf,
            idxDepto = doctoped.idxDepto,
            idxTabela = doctoped.idxTabela,
            idxTabelaSub = doctoped.idxTabelaSub,
            idxFormaPag = doctoped.idxFormaPag,
            idxVendedor1 = doctoped.idxVendedor1,
            idxVendedor2 = doctoped.idxVendedor2,
            texto = doctoped.texto,
            totalDocto = doctoped.totalDocto,
            prodValor = doctoped.prodValor,
            prodDesc = doctoped.prodDesc,
            prodTotal = doctoped.prodTotal,
            freteValor = doctoped.freteValor,
            servValor = doctoped.servValor,
            servTotal = doctoped.servTotal,
            situacao = doctoped.situacao,
            temProduto = doctoped.temProduto,
            temServico = doctoped.temServico,
            temLocacao = doctoped.temLocacao,
            dtEvento = doctoped.dtEvento,
            totalItensProd = doctoped.totalItensProd,
            totalItensServ = doctoped.totalItensServ,
            totalItensLoc = doctoped.totalItensLoc,
            opInc = doctoped.opInc,
            opAlt = doctoped.opAlt,
            entregar = doctoped.entregar,
            idxDoctoEvento = _idxDoctoEvento == null ? "" : _idxDoctoEvento,
            contato = doctoped.contato,
            telefone = doctoped.telefone,
            email = doctoped.email,
            dtPrevisao = doctoped.dtPrevisao,
            horaPrevisao = doctoped.horaPrevisao,
            celular = doctoped.celular,
            temProducao = doctoped.temProducao,
            temProfissional = doctoped.temProfissional,
            totalAjuste = doctoped.totalAjuste,
            totalFinanceiro = doctoped.totalFinanceiro
        };

        _context.Doctoped.Add(novaOR);
        await _context.SaveChangesAsync();
        await _context.Entry(novaOR).ReloadAsync();
        Console.WriteLine(JsonSerializer.Serialize(novaOR));
        return novaOR;
    }

    public async Task<TpaMovtopedDTO> CadastrarMovtoped(MovtopedAtendimento movtoped, int _rdxDoctoped, TpaEventoOrcPedDTO? orcPed = null)
    {
        var dadosProduto = await GetDadosProduto(movtoped.idxProduto);
        Console.WriteLine(JsonSerializer.Serialize(dadosProduto));
        var novoMovtoped = new TpaMovtopedDTO
        {
            rdxDoctoped = _rdxDoctoped,
            idxDepto = movtoped.idxDepto,
            codProduto = movtoped.codProduto,
            descricao = movtoped.descricao,
            referencia = movtoped.referencia,
            tipoProd = movtoped.tipoProd,
            idxProduto = dadosProduto.pkProduto,
            unidade = movtoped.unidade,
            cst = movtoped.cst,
            l_quantidade = movtoped.l_quantidade,
            l_precouni = movtoped.l_precouni,
            l_precototal = movtoped.l_precototal,
            peso = dadosProduto.peso,
            situacao = movtoped.situacao,
            opInc = movtoped.opInc,
            opAlt = movtoped.opAlt,
            ncm = movtoped.ncm,
            idxEventoOrcGrupo = orcPed == null ? "" : orcPed.pkEventoOrcPed,
            grupo = orcPed == null ? "" : orcPed.descricao
        };

        return novoMovtoped;
    }

    public async Task<TpaEventoOrcPedDTO> CadastrarEventoOrcPed(EventoOrcPedAtendimento orcPed, int _idxDoctoped, int _sequencia)
    {
        var _pkOrcPed = await CriarPkOrcPed();
        var novoOrcPed = new TpaEventoOrcPedDTO
        {
            idxDoctoPed = _idxDoctoped,
            pkEventoOrcPed = _pkOrcPed,
            sequencia = _sequencia,
            tpImpressao = orcPed.tpImpressao,
            totItens = orcPed.totItens,
            quantidade = orcPed.quantidade,
            valorItens = orcPed.valorItens,
            desconto = orcPed.desconto,
            valorTotal = orcPed.valorTotal,
            opInc = orcPed.opInc,
            opAlt = orcPed.opAlt,
            idxEventoTpSv = orcPed.idxEventoTpSv,
            totalizador = orcPed.totalizador,
            descricao = orcPed.descricao,
            tpImpressaoTot = orcPed.tpImpressaoTot,
            tpImpressaoMsg = orcPed.tpImpressaoMsg,
            tpImpressaoImg = orcPed.tpImpressaoImg,
            tpRegistroItens = orcPed.tpRegistroItens,
            idxImg = orcPed.idxImg
        };

        _context.EventoOrcPed.Add(novoOrcPed);
        await _context.SaveChangesAsync();
        //await _context.Entry(novoOrcPed).ReloadAsync();
        return novoOrcPed;
    }

    public async Task<int> CadastrarNovaEc(InformacoesEC informacoesEc)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var doctoped = await CadastrarDoctoped(informacoesEc.doctopedAtendimento);
            foreach (var produto in informacoesEc.movtopedAtendimento)
            {
                var movtoped = await CadastrarMovtoped(produto, doctoped.pkDoctoped);
                _context.Movtoped.Add(movtoped);
            }

            await CadastrarDoctopedFP(doctoped.pkDoctoped, informacoesEc.doctopedAtendimento, informacoesEc.doctopedFpAtendimento);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return doctoped.pkDoctoped;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e);
            Console.WriteLine(JsonSerializer.Serialize(informacoesEc));
            throw;
        }
    }

    public async Task<int> CadastrarNovaOr(InformacoesOR informacoesOr)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var eventoOrc = await CadastrarEventoOrc(informacoesOr.eventoOrcAtendimento);
            var doctoped = await CadastrarDoctoped(informacoesOr.doctopedAtendimento, eventoOrc.pkEventoOrc);
            // foreach(var produto in informacoesOr.movtopedAtendimento)
            // {
            //     var movtoped = await CadastrarMovtopedOR(produto, doctoped.pkDoctoped);
            //     _context.Movtoped.Add(movtoped);
            // }
            int sequenciaOrcPed = 1;
            foreach (var orcPed in informacoesOr.eventoOrcPedAtendimento)
            {
                var _eventoOrcPed = await CadastrarEventoOrcPed(orcPed, doctoped.pkDoctoped, sequenciaOrcPed++);
                foreach (var produto in orcPed.produtos)
                {
                    var movtoped = await CadastrarMovtoped(produto, doctoped.pkDoctoped, _eventoOrcPed);
                    _context.Movtoped.Add(movtoped);
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return doctoped.pkDoctoped;

        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e);
            Console.WriteLine(JsonSerializer.Serialize(informacoesOr));
            throw;
        }
    }

    /// /////////////////////////////////////////////////////////////////////
    /// CADASTRAR E GERAR CONTRATO NA MESMA FUNÇÃO
    /// ////////////////////////////////////////////////////////////////////
    /// /// ////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////

    // private string CriarNomeDocumento(int? documento)
    // {
    //     var sevenDigitsLength = documento?.ToString("D7");
    //     var documentName = $"OR{sevenDigitsLength}";
    //     return documentName;
    // }

    // private async Task<(int pkContrato, DateTime? dtInc)> CriarContrato(dynamic dadosContrato, int operador)
    // {
    //     var novoContrato = new TpaContratoDTO
    //     {
    //         documento = CriarNomeDocumento(dadosContrato.documento),
    //         idxEntidade = dadosContrato.idxEntidade,
    //         opInc = operador,
    //         opAlt = operador,
    //         idxDoctoEst = dadosContrato.pkDoctoped,
    //         opAtivacao = operador
    //     };

    //     _context.Contratos.Add(novoContrato);
    //     await _context.SaveChangesAsync();
    //     await _context.Entry(novoContrato).ReloadAsync();
    //     Console.WriteLine(novoContrato.GetType());
    //     Console.WriteLine(JsonSerializer.Serialize(novoContrato));
    //     return (novoContrato.pkContrato, novoContrato.dtInc);
    // }

    // private async Task<int> CriarContratoMov(
    //     dynamic dadosContratoMov,
    //     int pkContrato,
    //     DateTime dataContrato,
    //     int operador)
    // {
    //     var horaCriacaoContrato = dataContrato.Hour;
    //     var hora = horaCriacaoContrato.ToString("D2");
    //     var textoHora = $"{hora}00";
    //     var novoContratoMov = new TpaContratoMovDTO
    //     {
    //         rdxContrato = pkContrato,
    //         dtInicio = dataContrato,
    //         dtVencto = dataContrato,
    //         idxTabela = dadosContratoMov.idxTabela,
    //         idxTabelaSub = dadosContratoMov.idxTabelaSub,
    //         totalDocto = dadosContratoMov.totalDocto,
    //         idxDoctoEst = dadosContratoMov.pkDoctoped,
    //         opInc = operador,
    //         opAlt = operador,
    //         horaSaida = textoHora
    //     };

    //     _context.ContratoMov.Add(novoContratoMov);
    //     await _context.SaveChangesAsync();
    //     await _context.Entry(novoContratoMov).ReloadAsync();

    //     return novoContratoMov.pkContratoMov;
    // }

    // public async Task CriarContratoItem(InfoMovtopedContratoItem movtoped, int pkContratoMov, int operador, int idxContratoAdendo = 0)
    // {
    //     var novoItem = new TpaContratoItemDTO
    //     {
    //         rdxContratoMov = pkContratoMov,
    //         item = movtoped.item,
    //         idxMovtoEst = movtoped.pkMovtoped,
    //         idxProduto = movtoped.idxProduto,
    //         lQuantidade = movtoped.l_quantidade,
    //         lPrecoUni = movtoped.l_precouni,
    //         lPrecoTotal = movtoped.l_precototal,
    //         lValorBem = movtoped.l_valorbem,
    //         dtInc = BrazilTime.Now(),
    //         opInc = operador,
    //         dtAlt = BrazilTime.Now(),
    //         opAlt = operador,
    //         descricao = movtoped.referencia,
    //         referencia = movtoped.referencia,
    //         tipoProd = movtoped.tipoProd,
    //         locacao = movtoped.locacao,
    //         unidade = movtoped.unidade,
    //         locacaoBp = movtoped.locacaoBp,
    //         lP = movtoped.l_p,
    //         codProduto = movtoped.codProduto,
    //     };

    //     _context.ContratoItem.Add(novoItem);
    // }


    // private async Task SetarSituacaoMovtopedAutorizado(int pkDoctoped, int opAlt)
    // {
    //     var now = BrazilTime.Now();
    //     const string _query = @$"
    //         UPDATE TPAMOVTOPED 
    //             SET SITUACAO = 'Z',
    //             OPALT = @opAlt,
    //             DTALT = @dtAlt 
    //         WHERE RDX_DOCTOPED=@pkDoctoped
    //     ";

    //     await _context.Database.ExecuteSqlRawAsync(_query,
    //         new SqlParameter("pkDoctoped", pkDoctoped),
    //         new SqlParameter("opAlt", opAlt),
    //         new SqlParameter("dtAlt", now));
    // }

    // private async Task SetarSituacaoDoctopedVigente(
    //     int pkDoctoped,
    //     int pkContratoMov,
    //     int operador,
    //     string temProfissional)
    // {
    //     var now = BrazilTime.Now();
    //     const string _query = @$"
    //         UPDATE TPADOCTOPED
    //             SET SITUACAO = 'V',
    //             TEMPROFISSIONAL = @temProfissional,
    //             IDX_CONTRATOMOV = @pkContratoMov,
    //             DTALT = @dataAtual,
    //             OPALT = @opAlt
    //         WHERE PK_DOCTOPED = @pkDoctoped
    //     ";

    //     await _context.Database.ExecuteSqlRawAsync(_query,
    //         new SqlParameter("temProfissional", temProfissional),
    //         new SqlParameter("pkContratoMov", pkContratoMov),
    //         new SqlParameter("dataAtual", now),
    //         new SqlParameter("opAlt", operador),
    //         new SqlParameter("pkDoctoped", pkDoctoped));
    // }

    // private async Task InserirHistorico(int pkDoctoped, int operador)
    // {
    //     var doctopedHistorico = new TpaDoctopedHistoricoDTO
    //     {
    //         rdxDoctoped = pkDoctoped,
    //         etapa = "T",
    //         parcial = "N",
    //         situacao = "I",
    //         texto = "CONTRATO;SITUACAO",
    //         dtInc = BrazilTime.Now(),
    //         opInc = operador,
    //     };

    //     _context.DoctopedHistorico.Add(doctopedHistorico);
    // }

    // public async Task<int> GerarContrato(int pkDoctoped, int operador, string temProfissional)
    // {
    //     using var transaction = await _context.Database.BeginTransactionAsync();
    //     try
    //     {

    //         var doctoped = await _context.Doctoped
    //             .Where(d => d.pkDoctoped == pkDoctoped)
    //             .Select((d) => new { d.pkDoctoped, d.documento, d.idxEntidade, d.idxTabela, d.idxTabelaSub, d.totalDocto })
    //             .SingleAsync();



    //         var contrato = await CriarContrato(doctoped, operador);
    //         int pkContrato = (int)contrato.pkContrato;
    //         DateTime dataCriacaoContrato = (DateTime)contrato.dtInc;

    //         var contratoMov = await CriarContratoMov(doctoped, pkContrato, dataCriacaoContrato, operador);

    //         var movtoped = await _context.Movtoped
    //             .Where(m => m.rdxDoctoped == pkDoctoped)
    //             .Select(m => new InfoMovtopedContratoItem
    //             {
    //                 pkMovtoped = m.pkMovtoped,
    //                 item = m.item,
    //                 idxProduto = m.idxProduto,
    //                 l_quantidade = m.l_quantidade,
    //                 l_precouni = m.l_precouni,
    //                 l_precototal = m.l_precototal,
    //                 l_valorbem = m.l_valorbem,
    //                 idxPatrimonio = m.idxPatrimonio,
    //                 idxPatrimonioMovto = m.idxPatrimonioMovto,
    //                 referencia = m.referencia,
    //                 tipoProd = m.tipoProd,
    //                 locacao = m.locacao,
    //                 unidade = m.unidade,
    //                 locacaoBp = m.locacaoBp,
    //                 l_p = m.l_p,
    //                 codProduto = m.codProduto
    //             })
    //             .ToListAsync();


    //         foreach (var item in movtoped)
    //         {
    //             await CriarContratoItem(item, contratoMov, operador);
    //         }

    //         await SetarSituacaoMovtopedAutorizado(pkDoctoped, operador);
    //         await SetarSituacaoDoctopedVigente(pkDoctoped, contratoMov, operador, temProfissional);
    //         await InserirHistorico(pkDoctoped, operador);

    //         await _context.SaveChangesAsync();
    //         await transaction.CommitAsync();
    //         return pkContrato;
    //     }
    //     catch (Exception e)
    //     {
    //         await transaction.RollbackAsync();
    //         Console.WriteLine(e);
    //         throw;
    //     }
    // }

    // public async Task<int> GerarContratoAposCriacao(int pkDoctoped, int operador, string temProfissional)
    // {
    //     try
    //     {

    //         var doctoped = await _context.Doctoped
    //             .Where(d => d.pkDoctoped == pkDoctoped)
    //             .Select((d) => new { d.pkDoctoped, d.documento, d.idxEntidade, d.idxTabela, d.idxTabelaSub, d.totalDocto })
    //             .SingleAsync();

    //         var contrato = await CriarContrato(doctoped, operador);
    //         int pkContrato = (int)contrato.pkContrato;
    //         DateTime dataCriacaoContrato = (DateTime)contrato.dtInc;

    //         var contratoMov = await CriarContratoMov(doctoped, pkContrato, dataCriacaoContrato, operador);

    //         var movtoped = await _context.Movtoped
    //             .Where(m => m.rdxDoctoped == pkDoctoped)
    //             .Select(m => new InfoMovtopedContratoItem
    //             {
    //                 pkMovtoped = m.pkMovtoped,
    //                 item = m.item,
    //                 idxProduto = m.idxProduto,
    //                 l_quantidade = m.l_quantidade,
    //                 l_precouni = m.l_precouni,
    //                 l_precototal = m.l_precototal,
    //                 l_valorbem = m.l_valorbem,
    //                 idxPatrimonio = m.idxPatrimonio,
    //                 idxPatrimonioMovto = m.idxPatrimonioMovto,
    //                 referencia = m.referencia,
    //                 tipoProd = m.tipoProd,
    //                 locacao = m.locacao,
    //                 unidade = m.unidade,
    //                 locacaoBp = m.locacaoBp,
    //                 l_p = m.l_p,
    //                 codProduto = m.codProduto
    //             })
    //             .ToListAsync();


    //         foreach (var item in movtoped)
    //         {
    //             await CriarContratoItem(item, contratoMov, operador);
    //         }

    //         await SetarSituacaoMovtopedAutorizado(pkDoctoped, operador);
    //         await SetarSituacaoDoctopedVigente(pkDoctoped, contratoMov, operador, temProfissional);
    //         await InserirHistorico(pkDoctoped, operador);

    //         await _context.SaveChangesAsync();
    //         return pkContrato;
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         throw;
    //     }
    // }

    public async Task<int> CadastrarEGerarContrato(InformacoesOR informacoesOr)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var eventoOrc = await CadastrarEventoOrc(informacoesOr.eventoOrcAtendimento);
            var doctoped = await CadastrarDoctoped(informacoesOr.doctopedAtendimento, eventoOrc.pkEventoOrc);
            // foreach(var produto in informacoesOr.movtopedAtendimento)
            // {
            //     var movtoped = await CadastrarMovtopedOR(produto, doctoped.pkDoctoped);
            //     _context.Movtoped.Add(movtoped);
            // }
            int sequenciaOrcPed = 1;
            foreach (var orcPed in informacoesOr.eventoOrcPedAtendimento)
            {
                var _eventoOrcPed = await CadastrarEventoOrcPed(orcPed, doctoped.pkDoctoped, sequenciaOrcPed++);
                foreach (var produto in orcPed.produtos)
                {
                    var movtoped = await CadastrarMovtoped(produto, doctoped.pkDoctoped, _eventoOrcPed);
                    _context.Movtoped.Add(movtoped);
                }
            }

            await _context.SaveChangesAsync();
            await _geracaoContrato.GerarContratoAposCriacao(doctoped.pkDoctoped, informacoesOr.doctopedAtendimento.opInc, doctoped.temProfissional);
            await transaction.CommitAsync();
            return doctoped.pkDoctoped;

        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e);
            Console.WriteLine(JsonSerializer.Serialize(informacoesOr));
            throw;
        }
    }

}





