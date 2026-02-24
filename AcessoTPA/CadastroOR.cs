using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class CadastroOR
{
    private readonly AppDbContext _context;
    private UsuariosTPA _usuariosService;
    private AcessoTPA _acessoTpa;

    public CadastroOR(AppDbContext context, UsuariosTPA usuariosService, AcessoTPA acessoTpa)
    {
        _context = context;
        _usuariosService = usuariosService;
        _acessoTpa = acessoTpa;
    }

    public async Task<int?> CriarNumeroDocumentoOR()
    { 
        var maxDocumento = await _context.Doctoped.Where(doc => doc.tpDocto == "OR").MaxAsync(op => (int?)op.documento) ?? 0;
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
        var novoPkEventoOrc = $"       {ultimoIdMaisUm}";
        return novoPkEventoOrc;
    }

    public async Task<string> CriarPkOrcPed()
    {
        //var ultimoId = await _context.CadastroUsuarioTPA.OrderByDescending(u => u.id).Select(u => u.id).FirstAsync();
        var ultimoId = await _context.Database
        .SqlQueryRaw<int>("SELECT MAX(ID) AS Value FROM TPAEVENTOORCPED")
        .FirstAsync();
        var ultimoIdMaisUm = ultimoId + 1;
        //var teste = "       50526";
        var novoPkOrcPed = $"       {ultimoIdMaisUm}";
        return novoPkOrcPed;
    }

    public async Task<InformacoesProdutoTPA> GetDadosProduto(string _pkProduto)
    {
        var result = await _context.Produto.Where(produto => produto.pkProduto.Trim() == _pkProduto).SingleOrDefaultAsync();
        if(result == null)
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
        var numeroDocumento = await CriarNumeroDocumentoOR();
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
            situacao = "N",
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
            idxDoctoEvento = _idxDoctoEvento,
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

    public async Task<TpaMovtopedDTO> CadastrarMovtopedOR(MovtopedAtendimento movtoped, int _rdxDoctoped)
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
            idxProduto = movtoped.idxProduto,
            unidade = movtoped.unidade,
            cst = movtoped.cst,
            l_quantidade = movtoped.l_quantidade,
            l_precouni = movtoped.l_precouni,
            l_precototal = movtoped.l_precototal,
            peso = dadosProduto.peso,
            situacao = movtoped.situacao,
            opInc = movtoped.opInc,
            opAlt = movtoped.opAlt,
            ncm = movtoped.ncm
        };

        return novoMovtoped;
    }

    public async Task<TpaEventoOrcPedDTO> CadastrarEventoOrcPed(EventoOrcPedAtendimento orcPed, int _idxDoctoped)
    {
        var _pkOrcPed = await CriarPkOrcPed();
        var novoOrcPed = new TpaEventoOrcPedDTO
        {
            idxDoctoPed = _idxDoctoped,
            pkEventoOrcPed = _pkOrcPed,
            sequencia = orcPed.sequencia,
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

        return novoOrcPed;
    }

    public async Task<string> CadastrarNovaOr(InformacoesOR informacoesOr)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var eventoOrc = await CadastrarEventoOrc(informacoesOr.eventoOrcAtendimento);
            var doctoped = await CadastrarDoctoped(informacoesOr.doctopedAtendimento, eventoOrc.pkEventoOrc);
            foreach(var produto in informacoesOr.movtopedAtendimento)
            {
                var movtoped = await CadastrarMovtopedOR(produto, doctoped.pkDoctoped);
                _context.Movtoped.Add(movtoped);
            }
            foreach(var orcPed in informacoesOr.eventoOrcPedAtendimento)
            {
                var _eventoOrcPed = await CadastrarEventoOrcPed(orcPed, doctoped.pkDoctoped);
                _context.EventoOrcPed.Add(_eventoOrcPed);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return $"OR {doctoped.documento} criada";

        }
        catch(Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e);
            Console.WriteLine(JsonSerializer.Serialize(informacoesOr));
            throw;
        }
    }

    public async Task<string> CadastrarNovaEC(InformacoesOR informacoesOr, DoctopedFpAtendimento doctopedFP)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var doctoped = await CadastrarDoctoped(informacoesOr.doctopedAtendimento);
            foreach(var produto in informacoesOr.movtopedAtendimento)
            {
                var movtoped = await CadastrarMovtopedOR(produto, doctoped.pkDoctoped);
                _context.Movtoped.Add(movtoped);
            }
            await CadastrarDoctopedFP(doctoped.pkDoctoped, informacoesOr.doctopedAtendimento, doctopedFP);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return $"EC {doctoped.documento} criada";
        }
        catch(Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e);
            Console.WriteLine(JsonSerializer.Serialize(informacoesOr));
            throw;
        }
    }


}