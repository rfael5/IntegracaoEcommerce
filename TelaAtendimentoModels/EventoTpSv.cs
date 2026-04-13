using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public record EventoTpSv
{
    [Key]
    [Column("ID")]
    public int id { get; init; }

    [Column("PK_EVENTOTPSV")]
    public string pkEventotpsv { get; init; }

    [Column("RDX_EVENTOTP")]
    public string rdxEventotp { get; init; }

    [Column("DESCRICAO")]
    public string descricao { get; init; } 

    [Column("SEQUENCIA")]
    public int sequencia { get; init; }

    [Column("TPIMPRESSAO")]
    public string tpImpressao { get; init; } 

    [Column("TOTALIZADOR")]
    public string totalizador { get; init; } 

    [Column("QUANTIDADE")]
    public decimal quantidade { get; init; }

    [Column("STATUS")]
    public int status { get; init; }

    [Column("IDX_EVENTOSV")]
    public string idxEventosv { get; init; }

    [Column("TPIMPRESSAOTOT")]
    public string tpImpressaoTot { get; init; } 

    [Column("TPIMPRESSAOIMG")]
    public string tpImpressaoImg { get; init; } 

    [Column("TPREGISTROITENS")]
    public string tpRegistroItens { get; init; } 

    [Column("VARIEDADESUGERIDA")]
    public int variedadeSugerida { get; init; } 

    [Column("PERMISSAO")]
    public string? permissao { get; init; } 

    [Column("CALCCONVIDADO")]
    public string calcConvidado { get; init; } 

    [Column("IDX_IMG")]
    public string idxImg { get; init; }
}