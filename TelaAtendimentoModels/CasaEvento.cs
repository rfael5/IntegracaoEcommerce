using System.ComponentModel.DataAnnotations;

public record CasaEvento
{
    [Key]
    public string pkCadastro { get; init; }
    public string? nome { get; init; }
    public string? endTp { get; init; }
    public string? rua { get; init; }
    public string? numero { get; init; }
    public string? bairro { get; init; }
    public string? cidade { get; init; }
    public string? uf { get; init; }
    public string? cep { get; init; }
}