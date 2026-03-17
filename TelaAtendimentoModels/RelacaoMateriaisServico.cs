using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public record ServicoMateriais
{
    [Key]
   
    [Column("PK_PRODUTO")]
    public string idProduto { get; init; }
    [Column("CODPRODUTO")]
    public string codProduto{ get; init; }
    [Column("NOME_PRODUTO")]
    public string produto { get; init; }



}