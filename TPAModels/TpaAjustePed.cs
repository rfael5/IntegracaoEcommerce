using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPAAJUSTEPED", Schema="dbo")]
public record TpaAjustePedDTO
{
    [Key]
    [Column("PK_AJUSTEPED")]
    public int pkAjustePed { get; init; }

    [Column("RDX_DOCTOPED")]
    public int? rdxDoctoped { get; init; }

    [Column("NUMERO")]
    public int? numero { get; init; }

    [Column("DESCRICAO")]
    public string? descricao { get; init; }

    [Column("CONVIDADOS")]
    public int? convidados { get; init; } = 0;

    [Column("TPMODIFICACAO")]
    public string? tpModificacao { get; init; } = "N";

    [Column("DATA")]
    public DateTime? data { get; init; } = BrazilTime.Now();

    [Column("SITUACAO")]
    public string? situacao { get; init; } = "N";

    [Column("DTINC")]
    public DateTime? dtInc { get; init; } = BrazilTime.Now();

    [Column("OPINC")]
    public int? opInc { get; init; }

    [Column("DTALT")]
    public DateTime? dtAlt { get; init; } = BrazilTime.Now();

    [Column("OPALT")]
    public int? opAlt { get; init; }

    [Column("SITUACAOOP")]
    public string? situacaoOp { get; init; } = "";

    [Column("SITUACAOSP")]
    public string? situacaoSp { get; init; } = "";

    [Column("TEMPRODUCAO")]
    public string? temProducao { get; init; }

    [Column("DTEVENTOMODIFICADO")]
    public string? dtEventoModificado { get; init; } = "N";

    [Column("DTINICIOANTES")]
    public DateTime? dtInicioAntes { get; init; }

    [Column("DTTERMINOANTES")]
    public DateTime? dtTerminoAntes { get; init; }

    [Column("OBSVALORSOBRETOTAL")]
    public string? obsValorSobreTotal { get; init; } = "";

    [Column("VALORSOBRETOTAL")]
    public decimal? valorSobreTotal { get; init; } = 0;

    [Column("IDX_VENDEDOR1")]
    public string? idxVendedor1 { get; init; } = "";

    [Column("IDX_VENDEDOR2")]
    public string? idxVendedor2 { get; init; } = "";

    [Column("TPLOCALANTES")]
    public string? tpLocalAntes { get; init; } = "";

    [Column("IDX_ESPACOFISICOANTES")]
    public string? idxEspacoFisicoAntes { get; init; } = "";

    [Column("IDX_CASAEVENTOANTES")]
    public string? idxCasaEventoAntes { get; init; } = "";

    [Column("IDX_ENDERECOANTES")]
    public string? idxEnderecoAntes { get; init; } = "";

    [Column("LOCALANTES")]
    public string? localAntes { get; init; } = "";

    [Column("ENDERECOMODIFICADO")]
    public string? enderecoModificado { get; init; } = "N";

    [Column("TOTALVALOR")]
    public decimal? totalValor { get; init; }

    [Column("OBS")]
    public string? obs { get; init; }

    [Column("TEXTOANTES")]
    public string? textoAntes { get; init; }

    [Column("TXTHISTORICOANTES")]
    public string? txtHistoricoAntes { get; init; }

    [Column("TXTCONCLUSAOANTES")]
    public string? txtConclusaoAntes { get; init; }

    [Column("IDX_DOCTOPEDATIVIDADE")]
    public int? idxDoctopedAtividade { get; init; } = 0;

    [Column("VALORDISTRATO")]
    public decimal? valorDistrato { get; init; }

    [Column("OBSAJUSTE")]
    public string? obsAjuste { get; init; }

    [Column("DISTRATO")]
    public string? distrato { get; init; }

    [Column("DTINICIO")]
    public DateTime? dtInicio { get; init; }

    [Column("DTTERMINO")]
    public DateTime? dtTermino { get; init; }

    [Column("VENDEDOR1ANTES")]
    public string? vendedor1Antes { get; init; }

    [Column("VENDEDOR2ANTES")]
    public string? vendedor2Antes { get; init; }

    [Column("TPLOCAL")]
    public string? tpLocal { get; init; }

    [Column("LOCAL")]
    public string? local { get; init; }

    [Column("TEXTO")]
    public string? texto { get; init; }

    [Column("TXTHISTORICO")]
    public string? txtHistorico { get; init; }

    [Column("TXTCONCLUSAO")]
    public string? txtConclusao { get; init; }

    [Column("FINANCEIRO")]
    public string? financeiro { get; init; }

    [Column("TIPOAJUSTE")]
    public string? tipoAjuste { get; init; } = "C";

    [Column("IDX_FUNCIONARIO")]
    public string? idxFuncionario { get; init; } = "";
} 