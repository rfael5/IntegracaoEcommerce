using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPADOCTOPEDHISTORICO", Schema = "dbo")]
public record TpaDoctopedHistoricoDTO
{
    [Key]
    [Column("PK_DOCTOPEDHISTORICO")]
    public int pkDoctopedHistorico { get; init; }

    [Column("RDX_DOCTOPED")]
    public int? rdxDoctoped { get; init; }

    [Column("SITUACAO")]
    public string? situacao { get; init; }

    [Column("TEXTO")]
    public string? texto { get; init; }

    [Column("DTINC")]
    public DateTime? dtInc { get; init; }

    [Column("OPINC")]
    public int? opInc { get; init; }

    [Column("ETAPA")]
    public string? etapa { get; init; }

    [Column("PARCIAL")]
    public string? parcial { get; init; }
}