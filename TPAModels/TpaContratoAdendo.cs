using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPACONTRATOADENDO", Schema = "dbo")]
public record TpaContratoAdendoDTO
{
    [Key]
    [Column("PK_CONTRATOADENDO")]
    public int pkContratoAdendo { get; init; }
    [Column("RDX_CONTRATO")]
    public int? rdxContrato { get; init; }
    [Column("ADENDO")]
    public string? adendo { get; init; }
    [Column("DESCRICAO")]
    public string? descricao { get; init; }
    [Column("TEXTO")]
    public string? texto { get; init; } = " ";
    [Column("SITUACAO")]
    public string situacao { get; init; } = "A";
    [Column("DTINC")]
    public DateTime dtInc { get; init; } = BrazilTime.Now();
    [Column("OPINC")]
    public int opInc { get; init; }
    [Column("DTALT")] 
    public DateTime dtAlt { get; init; } = BrazilTime.Now();
    [Column("OPALT")]
    public int opAlt { get; init; } 
    [Column("TPTABELA")]
    public string tpTabela { get; init; } = "J";
    [Column("IDX_TABELA")]
    public string idxTabela { get; init; }
    [Column("DATAATIVACAO")]
    public DateTime? dataAtivacao { get; init; } = BrazilTime.Now();
    [Column("OPATIVACAO")]
    public int? opAtivacao { get; init; }
    [Column("DISTRATO")]
    public string? distrato { get; init; } = "N";
    [Column("IDX_TABELAAUX")]
    public string? idxTabelaAux { get; init; }
}