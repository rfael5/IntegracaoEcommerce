using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPAEVENTOORC", Schema = "dbo")]
public record TpaEventoOrcDTO
{
    [Key]
    [Column("ID")]
    public int id { get; set; }

    [Column("PK_EVENTOORC")]
    public required string pkEventoOrc { get; init; }

    [Column("DESCRICAO")]
    public string? descricao { get; init; }

    [Column("CODIGO")]
    public string? codigo { get; init; } = "";

    [Column("IDX_EVENTO")]
    public required string idxEvento { get; init; }

    [Column("CONTATO")]
    public string? contato { get; init; } = "";

    [Column("CONTATOTEL")]
    public string contatoTel { get; init; } = "";

    [Column("CONTATOEMAIL1")]
    public string contatoEmail1 { get; init; } = "";

    [Column("CONTATOEMAIL2")]
    public string contatoEmail2 { get; init; } = "";

    [Column("CONVIDADOS")]
    public int convidados { get; init; }

    [Column("CRIANCAS")]
    public int criancas { get; init; }

    [Column("DTINICIO")]
    public DateTime dtInicio { get; init; }

    [Column("HSINICIO")]
    public required string hsInicio { get; init; }

    [Column("DTTERMINO")]
    public DateTime dtTermino { get; init; }

    [Column("HSTERMINO")]
    public required string hsTermino { get; init; }

    [Column("OBSINICIO")]
    public required string obsInicio { get; init; }

    [Column("OBSTERMINO")]
    public required string obsTermino { get; init; }

    [Column("TEXTOEVENTO")]
    public string? textoEvento { get; init; }

    [Column("TEXTOSEPARACAO")]
    public string? textoSeparacao { get; init; }

    [Column("TEXTOPRODUCAO")]
    public string? textoProducao { get; init; }

    [Column("IDX_CASAEVENTO")]
    public string idxCasaEvento { get; init; } = "";

    [Column("IDX_ENDERECO")]
    public required string idxEndereco { get; init; }

    [Column("LOCAL")]
    public string? local { get; init; }

    [Column("STATUS")]
    public string status { get; init; } = "A";

    [Column("IDX_CLIENTE")]
    public string idxCliente { get; init; } = "";

    [Column("IDX_RESPONSAVEL")]
    public string idxResponsavel { get; init; } = "";

    [Column("IDX_PARCERIA")]
    public string idxParceria { get; init; } = "";

    [Column("MOTIVOSTATUS")]
    public string motivoStatus { get; init; } = "";

    [Column("DTINC")]
    public DateTime dtInc { get; init; } = BrazilTime.Now();

    [Column("OPINC")]
    public int opInc { get; init; }

    [Column("DTALT")]
    public DateTime dtAlt { get; init; } = BrazilTime.Now();

    [Column("OPALT")]
    public int opAlt { get; init; }

    [Column("IDX_EVENTOTP")]
    public required string idxEventoTp { get; init; }

    [Column("NUMERO")]
    public int numero { get; init; }

    [Column("PROFISSIONALESCALADO")]
    public required string profissionalEscalado { get; init; }

    [Column("TEXTOEXECUCAO")]
    public string? textoExecucao { get; init; }

    [Column("OBSSAIDA")]
    public string obsSaida { get; init; } = "";

    [Column("OBSRETORNO")]
    public string obsRetorno { get; init; } = "";

    [Column("HSSAIDA")]
    public string hsSaida { get; init; } = "";

    [Column("HSRETORNO")]
    public string hsRetorno { get; init; } = "";

    [Column("ALT")]
    public int alt { get; init; } = 0;

    [Column("SIT1")]
    public string sit1 { get; init; } = "";

    [Column("SIT2")]
    public string sit2 { get; init; } = "";

    [Column("SEQUENCIA")]
    public int sequencia { get; init; } = 0;

    [Column("SIT3")]
    public string sit3 { get; init; } = "";

    [Column("ASSOCIAMATERIAL")]
    public string associaMaterial { get; init; } = "";

    [Column("OPCOMPRA")]
    public string opCompra { get; init; } = "";

    [Column("ATIVIDADE")]
    public string atividade { get; init; } = "N";

    [Column("TPLOCAL")]
    public string tpLocal { get; init; } = "";

    [Column("CALCULOEXECUTADO")]
    public string calculoExecutado { get; init; } = "";

    [Column("IDX_DOCTORELOC")]
    public int? idxDoctorEloc { get; init; } = 0;

    [Column("OBSDESPESA")]
    public string obsDespesa { get; init; } = "";

    [Column("FINALIZAEXECUCAO")]
    public string finalizaExecucao { get; init; } = "P";

    [Column("FINALIZAESCALA")]
    public string finalizaEscala { get; init; } = "P";

    [Column("FINALIZACONSUMOADICIONAL")]
    public string finalizaConsumoAdicional { get; init; } = "P";

    [Column("FINALIZARETORNO")]
    public string finalizaRetorno { get; init; } = "P";

    [Column("FINALIZADESPESAEXTRA")]
    public string finalizaDespesaExtra { get; init; } = "P";

    [Column("FINALIZAQUESTIONARIO")]
    public string finalizaQuestionario { get; init; } = "P";

    [Column("IDX_QUESTIONARIOFINAL")]
    public string idxQuestionarioFinal { get; init; } = "";

    [Column("IDX_ESPACOFISICO")]
    public string idxEspacoFisico { get; init; } = "";

    [Column("TPCALCCORRECAO")]
    public string tpCalcCorrecao { get; init; } = "N";

    [Column("CALCCORRECAO")]
    public decimal calcCorrecao { get; init; } = 0;

    [Column("FRETEDEVIDO")]
    public decimal freteDevido { get; init; } = 0;

    [Column("SITUACAOLOCACAO")]
    public string situacaoLocacao { get; init; } = "";

    [Column("CUSTOOPERACIONAL")]
    public decimal custoOperacional { get; init; }

    [Column("VALORTABELA")]
    public decimal valorTabela { get; init; }

    [Column("PRODVALORORIGINAL")]
    public decimal prodValorOriginal { get; init; }

    [Column("SERVVALORORIGINAL")]
    public decimal servValorOriginal { get; init; }

    [Column("VERSAO")]
    public int? versao { get; init; }

    [Column("PERMITEALTERACAO")]
    public string? permiteAlteracao { get; init; }

    [Column("SITUACAOANTES")]
    public string? situacaoAntes { get; init; }

    [Column("CONVIDADOSCOMPARECIDOS")]
    public int? convidadosComparecidos { get; init; }

    [Column("IDX_ENDERECORECOLHIMENTO")]
    public string? idxEnderecoRecolhimento { get; init; }

    [Column("CODIBGE")]
    public string? codIbge { get; init; }

    [Column("EDITAVEL")]
    public string? editavel { get; init; }
}
