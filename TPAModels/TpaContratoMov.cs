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
    public string? situacao { get; init; } = "A";

    [Column("STATUS")]
    public string? status { get; init; } = "A";

    [Column("DTSTATUS")]
    public DateTime? dtStatus { get; init; } = BrazilTime.Now();

    [Column("TPVALIDADE")]
    public string? tpValidade { get; init; } = "D";

    [Column("NRENOVACAO")]
    public int? nrRenovacao { get; init; } = 0;

    [Column("DTINICIO")]
    public DateTime? dtInicio { get; init; }

    [Column("DTVENCTO")]
    public DateTime? dtVencto { get; init; }

    [Column("IDX_TABELA")]
    public string? idxTabela { get; init; }

    [Column("IDX_TABELASUB")]
    public string? idxTabelaSub { get; init; }

    [Column("IDX_RESPONSAVEL")]
    public string? idxResponsavel { get; init; } = " ";

    [Column("TEXTO")]
    public string? texto { get; init; } 

    [Column("NFMODELO")]
    public string? nfModelo { get; init; } = "00";

    [Column("IDX_MOEDA")]
    public string? idxMoeda { get; init; } = "R$";

    [Column("TOTALDOCTO")]
    public decimal? totalDocto { get; init; }

    [Column("TPCOBRANCA")]
    public string? tpCobranca { get; init; } = "M";

    [Column("DIACOBRANCA")]
    public int? diaCobranca { get; init; } = 0;

    [Column("IDX_FORMAPAG")]
    public string? idxFormaPag { get; init; } = " ";

    [Column("IDX_DOCTOEST")]
    public int? idxDoctoEst { get; init; }

    [Column("DTINC")]
    public DateTime? dtInc { get; init; } = BrazilTime.Now();

    [Column("OPINC")]
    public int? opInc { get; init; }

    [Column("DTALT")]
    public DateTime? dtAlt { get; init; } = BrazilTime.Now();

    [Column("OPALT")]
    public int? opAlt { get; init; }

    [Column("MOTIVOSTATUS")]
    public string? motivoStatus { get; init; } = " ";

    [Column("TPRENOVACAO")]
    public string? tpRenovacao { get; init; } = " ";

    [Column("PERIODORENOVACAO")]
    public int? periodoRenovacao { get; init; } = 1;

    [Column("PERIODOVALIDADE")]
    public int? periodoValidade { get; init; } = 1;

    [Column("IDX_INDEXADOR")]
    public string? idxIndexador { get; init; } = " ";

    [Column("DESCONTOCONCEDIDO")]
    public decimal? descontoConcedido { get; init; } = 0;

    [Column("IDX_DOCTOESTFAT")]
    public int? idxDoctoEstFat { get; init; } = 0;

    [Column("STATUSFAT")]
    public string? statusFat { get; init; } = "N";

    [Column("TPPERIODOFATUR")]
    public string? tpPeriodoFatur { get; init; } = "U";

    [Column("PERIODOFATUR")]
    public int? periodoFatur { get; init; } = 1;

    [Column("DEVOLUCAO")]
    public string? devolucao { get; init; } = "P";

    [Column("AUTORIZACAOFATUR")]
    public string? autorizacaoFatur { get; init; } = "P";

    [Column("IDX_FILIALFATUR")]
    public string? idxFilialFatur { get; init; } = "          1";

    [Column("HORASAIDA")]
    public string? horaSaida { get; init; }

    [Column("IDX_CONTRATOADENDO")]
    public int? idxContratoAdendo { get; init; } = 0;

    [Column("ADITIVO")]
    public int? aditivo { get; init; } = 0;

    [Column("DIASCREDITO")]
    public int? diasCredito { get; init; } = 0;

    [Column("FATURASEPARADA")]
    public string? faturaSeparada { get; init; } = "N";

    [Column("PERIODOFATURADO")]
    public int? periodoFaturado { get; init; } = 1;

    [Column("DTVENCTOFATURADO")]
    public DateTime? dtVenctoFaturado { get; init; }

    [Column("DTEVENTO")]
    public DateTime? dtEvento { get; init; }
}