using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public record EventoTp
{
    [Key]
    [Column("ID")]
    public int id { get; init; }
    [Column("PK_EVENTOTP")] 
    public string pkEventoTp { get; init; }
    [Column("DESCRICAO")] 
    public string descricao  { get; init; }
    [Column("STATUS")]
    public string status { get; init; }
    [Column("OBS")]
    public string? obs { get; init; }
}

public record CategoriaEvento
{
    [Key]
    [Column("ID")]
    public int id { get; init; }
    [Column("PK_EVENTO")]
    public string pkEvento { get; init; }
    [Column("DESCRICAO")]
    public string descricao { get; init; }
    [Column("ESCALAALERTA")]
    public string? escalaAlerta { get; init; }
}