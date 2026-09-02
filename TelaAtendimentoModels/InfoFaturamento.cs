using System.ComponentModel.DataAnnotations;

public record InfoFaturamento
{
    [Key]
    public int pkDoctoped { get; init; }
    public int documento { get; init; }
    public decimal totalDocto { get; init; }
    public decimal valorBaixado { get; init; }
}