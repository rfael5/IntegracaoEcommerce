using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public record InformacoesCliente
{
    [Key]
    [Column("ID")]
    public int id { get; init; }
    [Column("PK_CADASTRO")]
    public string? pkCadastro { get; init; }
    [Column("NOME")]
    public required string nome { get; init; }
    [Column("FANTASIA")]
    public string? fantasia { get; init; }
    [Column("PESSOAFJ")]
    public required string pessoafj { get; init; }
    [Column("CNPJCPF")]
    public required string cnpjCpf { get; init; }
    [Column("NATURALCIDADE")]
    public string? naturalCidade { get; init; }
    [Column("NATURALUF")]
    public string? naturalUf { get; init; }
    [Column("PAIS")]
    public string? pais { get; init; }
    [Column("TELEFONE1")]
    public string? telefone { get; init; }
    [Column("EMAIL")]
    public string? email { get; init; }
    [Column("DTNASCIMENTO")]
    public DateTime? dtNascimento { get; init; }
    [Column("SEXO")]
    public string? sexo { get; init; }

}