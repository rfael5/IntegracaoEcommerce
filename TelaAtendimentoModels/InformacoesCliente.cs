using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public record InformacoesCliente
{
    [Key]
    [Column("ID")]
    public int id { get; init; }
    [Column("PK_CADASTRO")]
    public string pkCadastro { get; init; }
    [Column("NOME")]
    public string nome { get; init; }
    [Column("FANTASIA")]
    public string fantasia { get; init; }
    [Column("PESSOAFJ")]
    public string pessoafj { get; init; }
}