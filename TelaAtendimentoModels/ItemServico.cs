using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ItemServico
{
    public int id { get; set; }
    public string pkEventoSv { get; set; }
    public string descricao { get; set; }
    public string tpImpressao { get; set; }
    public string totalizador { get; set; }
    public string status { get; set; }
    public string? classes { get; set; }
    public int qtPorcoes { get; set; }
    public int convidadosPorcao { get; set; }
    public string tpImpressaoTot { get; set; }
    public string tpImpressaoMsg { get; set; }
    public string tpRegistroItens { get; set; }
}