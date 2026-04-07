using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPAAJUSTEPEDITEM", Schema = "dbo")]
public record TpaAjustePedItemDTO
{
    [Key]
    [Column("PK_AJUSTEPEDITEM")]
    public int pkAjustePedItem { get; init; }

    [Column("RDX_AJUSTEPED")]
    public int? rdxAjustePed { get; init; }

    [Column("IDX_MOVTOPED")]
    public int? idxMovtoped { get; init; }

    [Column("QUANTIDADE")]
    public decimal? quantidade { get; init; }

    [Column("PRECO")]
    public decimal? preco { get; init; }

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

    [Column("TPAJUSTE")]
    public string? tpAjuste { get; init; } = "";

    [Column("TPAJUSTEPRODUCAO")]
    public string? tpAjusteProducao { get; init; }

    [Column("TPAJUSTESEPARACAO")]
    public string? tpAjusteSeparacao { get; init; }

    [Column("QTORIGINAL")]
    public decimal? qtOriginal { get; init; }

    [Column("QTITENSCARDAPIO")]
    public int? qtItensCardapio { get; init; } = 0;

    [Column("IDX_DOCTOPEDATIVIDADE")]
    public int? idxDoctopedAtividade { get; init; } = 0;
}