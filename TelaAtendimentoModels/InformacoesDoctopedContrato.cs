using System.Collections;

public record InformacoesDoctopedContrato
{
    public int pkDoctoped { get; init; }
    public int? documento { get; init; }
    public string? idxEntidade { get; init; }
    public int idxTabela { get; init; }
    public int idxTabelaSub { get; init; }
    public decimal totalDocto { get; init; }

}