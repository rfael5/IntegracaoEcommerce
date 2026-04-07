using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPAFUNCIONARIO", Schema = "dbo")]
public record VendedoresDTO
{
   [Key]
   [Column("PK_FUNCIONARIO")]
   public string pkFuncionario { get; init; }
   [Column("NOME")]
   public string nome { get; init; }
   [Column("NOMEINTERNO")]
   public string nomeInterno { get; init; } 
   [Column("IDX_OPSETOR")]
   public string setor { get; init; }
   [Column("PK_OPERADOR")]
   public string operador { get; init; }
}