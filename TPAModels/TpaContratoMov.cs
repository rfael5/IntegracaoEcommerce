using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPACONTRATOMOV", Schema = "dbo")]
public record TpaContratoMovDTO
{
    [Key]
    [Column("PK_CONTRATOMOV")]
    public int pkContratoMov { get; init; }

    [Column("RDX_CONTRATO")]
    public int? rdxContrato { get; init; }

    [Column("SITUACAO")]
    public string? situacao { get; init; }

    [Column("STATUS")]
    public string? status { get; init; }

    [Column("DTSTATUS")]
    public DateTime? dtStatus { get; init; }

    [Column("TPVALIDADE")]
    public string? tpValidade { get; init; }

    [Column("NRENOVACAO")]
    public int? nrRenovacao { get; init; }

    [Column("DTINICIO")]
    public DateTime? dtInicio { get; init; }

    [Column("DTVENCTO")]
    public DateTime? dtVencto { get; init; }

    [Column("IDX_TABELA")]
    public string? idxTabela { get; init; }

    [Column("IDX_TABELASUB")]
    public string? idxTabelaSub { get; init; }

    [Column("IDX_RESPONSAVEL")]
    public string? idxResponsavel { get; init; }

    [Column("TEXTO")]
    public string? texto { get; init; }

    [Column("NFMODELO")]
    public string? nfModelo { get; init; }

    [Column("IDX_MOEDA")]
    public string? idxMoeda { get; init; }

    [Column("TOTALDOCTO")]
    public decimal? totalDocto { get; init; }

    [Column("TPCOBRANCA")]
    public string? tpCobranca { get; init; }

    [Column("DIACOBRANCA")]
    public int? diaCobranca { get; init; }

    [Column("IDX_FORMAPAG")]
    public string? idxFormaPag { get; init; }

    [Column("IDX_DOCTOEST")]
    public int? idxDoctoEst { get; init; }

    [Column("DTINC")]
    public DateTime? dtInc { get; init; }

    [Column("OPINC")]
    public int? opInc { get; init; }

    [Column("DTALT")]
    public DateTime? dtAlt { get; init; }

    [Column("OPALT")]
    public int? opAlt { get; init; }

    [Column("MOTIVOSTATUS")]
    public string? motivoStatus { get; init; }

    [Column("TPRENOVACAO")]
    public string? tpRenovacao { get; init; }

    [Column("PERIODORENOVACAO")]
    public int? periodoRenovacao { get; init; }

    [Column("PERIODOVALIDADE")]
    public int? periodoValidade { get; init; }

    [Column("IDX_INDEXADOR")]
    public string? idxIndexador { get; init; }

    [Column("DESCONTOCONCEDIDO")]
    public decimal? descontoConcedido { get; init; }

    [Column("IDX_DOCTOESTFAT")]
    public int? idxDoctoEstFat { get; init; }

    [Column("STATUSFAT")]
    public string? statusFat { get; init; }

    [Column("TPPERIODOFATUR")]
    public string? tpPeriodoFatur { get; init; }

    [Column("PERIODOFATUR")]
    public int? periodoFatur { get; init; }

    [Column("DEVOLUCAO")]
    public string? devolucao { get; init; }

    [Column("AUTORIZACAOFATUR")]
    public string? autorizacaoFatur { get; init; }

    [Column("IDX_FILIALFATUR")]
    public string? idxFilialFatur { get; init; }

    [Column("HORASAIDA")]
    public string? horaSaida { get; init; }

    [Column("IDX_CONTRATOADENDO")]
    public int? idxContratoAdendo { get; init; }

    [Column("ADITIVO")]
    public int? aditivo { get; init; }

    [Column("DIASCREDITO")]
    public int? diasCredito { get; init; }

    [Column("FATURASEPARADA")]
    public string? faturaSeparada { get; init; }

    [Column("PERIODOFATURADO")]
    public int? periodoFaturado { get; init; }

    [Column("DTVENCTOFATURADO")]
    public DateTime? dtVenctoFaturado { get; init; }

    [Column("DTEVENTO")]
    public DateTime? dtEvento { get; init; }
}