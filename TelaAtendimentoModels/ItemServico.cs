using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ItemServico
{
    public int id { get; init; }
    public string pkEventoSv { get; init; }
    public string descricao { get; init; }
    public string tpImpressao { get; init; }
    public string totalizador { get; init; }
    public string status { get; init; }
    public string? classes { get; init; }
    public int qtPorcoes { get; init; }
    public int convidadosPorcao { get; init; }
    public string tpImpressaoTot { get; init; }
    public string tpImpressaoMsg { get; init; }
    public string tpRegistroItens { get; init; }
}