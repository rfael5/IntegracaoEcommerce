using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPADOCTOPEDFP", Schema = "dbo")]
public record TpaDoctoPedFpDTO
{
    [Key]
    [Column("PK_DOCTOPEDFP")]
    public int pkDoctoPedFp { get; set; }

    [Column("RDX_DOCTOPED")]
    public int? rdxDoctoPed { get; init; }

    [Column("TPDOCTO")]
    public string tpDocto { get; init; } = "";

    [Column("DOCUMENTO")]
    public string documento { get; init; } = "";

    [Column("DTEMISSAO")]
    public DateTime dtEmissao { get; init; } = BrazilTime.Now();

    [Column("PRAZO")]
    public int prazo { get; init; } = 0;

    [Column("DTVENCTO")]
    public DateTime dtVencto { get; init; } = BrazilTime.Now();

    [Column("VALOR")]
    public decimal valor { get; init; }

    [Column("DESCRICAO")]
    public string descricao { get; init; } = "";

    [Column("DTINC")]
    public DateTime dtInc { get; init; } = BrazilTime.Now();

    [Column("OPINC")]
    public int opInc { get; init; }

    [Column("DTALT")]
    public DateTime dtAlt { get; init; } = BrazilTime.Now();

    [Column("OPALT")]
    public int opAlt { get; init; }

    [Column("MEIOPAGTO")]
    public string meioPagto { get; init; } = "EDH";

    [Column("IDX_OPFINANCEIRA")]
    public string idxOpFinanceira { get; init; } = "";

    [Column("IDX_CCUSTO")]
    public string idxCCusto { get; init; } = "         158";
}
