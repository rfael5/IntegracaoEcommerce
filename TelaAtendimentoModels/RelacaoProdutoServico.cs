using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public record ServicoProduto
{
    [Key]
    [Column("PK_PRODEVENTOSV")]
    public string pkProdEventoSv { get; init; }

    [Column("PK_EVENTOTPSV")]
    public string idTipoServico { get; init; }
    [Column("DESCRICAO")]
    public string tipoServico { get; init; }
    [Column("PK_PRODUTO")]
    public string idProduto { get; init; }
    [Column("NOME_PRODUTO")]
    public string produto { get; init; }
}