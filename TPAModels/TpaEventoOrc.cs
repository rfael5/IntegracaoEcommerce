using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPACEVENTOORC", Schema = "dbo")]
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
    public required string codigo { get; init; }

    [Column("IDX_EVENTO")]
    public required string idxEvento { get; init; }

    [Column("CONTATO")]
    public required string contato { get; init; }

    [Column("CONTATOTEL")]
    public required string contatoTel { get; init; }

    [Column("CONTATOEMAIL1")]
    public required string contatoEmail1 { get; init; }

    [Column("CONTATOEMAIL2")]
    public required string contatoEmail2 { get; init; }

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
    public required string idxCasaEvento { get; init; }

    [Column("IDX_ENDERECO")]
    public required string idxEndereco { get; init; }

    [Column("LOCAL")]
    public string? local { get; init; }

    [Column("STATUS")]
    public required string status { get; init; }

    [Column("IDX_CLIENTE")]
    public required string idxCliente { get; init; }

    [Column("IDX_RESPONSAVEL")]
    public required string idxResponsavel { get; init; }

    [Column("IDX_PARCERIA")]
    public required string idxParceria { get; init; }

    [Column("MOTIVOSTATUS")]
    public required string motivoStatus { get; init; }

    [Column("DTINC")]
    public DateTime dtInc { get; init; }

    [Column("OPINC")]
    public int opInc { get; init; }

    [Column("DTALT")]
    public DateTime dtAlt { get; init; }

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
    public required string obsSaida { get; init; }

    [Column("OBSRETORNO")]
    public required string obsRetorno { get; init; }

    [Column("HSSAIDA")]
    public required string hsSaida { get; init; }

    [Column("HSRETORNO")]
    public required string hsRetorno { get; init; }

    [Column("ALT")]
    public int alt { get; init; }

    [Column("SIT1")]
    public required string sit1 { get; init; }

    [Column("SIT2")]
    public required string sit2 { get; init; }

    [Column("SEQUENCIA")]
    public int sequencia { get; init; }

    [Column("SIT3")]
    public required string sit3 { get; init; }

    [Column("ASSOCIAMATERIAL")]
    public required string associaMaterial { get; init; }

    [Column("OPCOMPRA")]
    public required string opCompra { get; init; }

    [Column("ATIVIDADE")]
    public required string atividade { get; init; }

    [Column("TPLOCAL")]
    public required string tpLocal { get; init; }

    [Column("CALCULOEXECUTADO")]
    public required string calculoExecutado { get; init; }

    [Column("IDX_DOCTORELOC")]
    public int? idxDoctorEloc { get; init; }

    [Column("OBSDESPESA")]
    public required string obsDespesa { get; init; }

    [Column("FINALIZAEXECUCAO")]
    public required string finalizaExecucao { get; init; }

    [Column("FINALIZAESCALA")]
    public required string finalizaEscala { get; init; }

    [Column("FINALIZACONSUMOADICIONAL")]
    public required string finalizaConsumoAdicional { get; init; }

    [Column("FINALIZARETORNO")]
    public required string finalizaRetorno { get; init; }

    [Column("FINALIZADESPESAEXTRA")]
    public required string finalizaDespesaExtra { get; init; }

    [Column("FINALIZAQUESTIONARIO")]
    public required string finalizaQuestionario { get; init; }

    [Column("IDX_QUESTIONARIOFINAL")]
    public required string idxQuestionarioFinal { get; init; }

    [Column("IDX_ESPACOFISICO")]
    public required string idxEspacoFisico { get; init; }

    [Column("TPCALCCORRECAO")]
    public required string tpCalcCorrecao { get; init; }

    [Column("CALCCORRECAO")]
    public decimal calcCorrecao { get; init; }

    [Column("FRETEDEVIDO")]
    public decimal freteDevido { get; init; }

    [Column("SITUACAOLOCACAO")]
    public required string situacaoLocacao { get; init; }

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
