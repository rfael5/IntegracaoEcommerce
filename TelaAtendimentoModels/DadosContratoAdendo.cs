public record DadosContratoAdendo
{
    public int idContrato { get; init; }
    public string adendo { get; init; }
    public string descricaoAjuste { get; init; }
    public int operador { get; init; }
    public decimal valorAjuste { get; init; }
    public int pkContratoMov { get; init; }
    public string associaMaterial { get; init; }
    public string idAjuste { get; init; }
    public int idDoctoped { get; init; }
}

public record RequestAdendo
{
    public int idAjuste { get; init; }
    public string descricaoAdendo { get; init; }
    public string associaMaterial { get; init; }
    public int operador { get; init; }
}