using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public record TipoServico
{
    [Key]
    [Column("ID")]
    public int id { get; init; }
    [Column("PK_EVENTOSV")]
    public string pkEventoSv { get; init; }
    [Column("DESCRICAO")]
    public string descricao { get; init; }
    [Column("TPIMPRESSAO")]
    public string tpImpressao { get; init; }
    [Column("TOTALIZADOR")]
    public string totalizador { get; init; }
    [Column("STATUS")]
    public string status { get; init; }
    [Column("CLASSES")]
    public string? classes { get; init; }
    [Column("QTPORCOES")]
    public int qtPorcoes { get; init; }
    [Column("CONVIDADOSPORCAO")]
    public int convidadosPorcao { get; init; }
    [Column("TPIMPRESSAOTOT")]
    public string tpImpressaoTot { get; init; }
    [Column("TPIMPRESSAOMSG")]
    public string tpImpressaoMsg { get; init; }
    [Column("TPREGISTROITENS")]
    public string tpRegistroItens { get; init; }
}