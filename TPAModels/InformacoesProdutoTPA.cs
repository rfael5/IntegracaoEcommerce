using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPAPRODUTO", Schema="dbo")]
public class InformacoesProdutoTPA
{
    [Key]
    [Column("ID")]
    public int id { get; init; }
    [Column("PK_PRODUTO")]
    public string pkProduto { get; init; }
    [Column("DESCRICAO")]
    public string descricao { get; init; }
    [Column("CODPRODUTO")]
    public string codProduto { get; init; }
    [Column("UN")]
    public string un_ecommerce { get; init; }
    [Column("UN1")]
    public string unidade { get; init; }
    [Column("CSTI")]
    public string csti { get; init; }
    [Column("PCCUSTO")]
    public decimal pcCusto { get; init; }
    [Column("PCMEDIO")]
    public decimal pcMedio { get; init; }
    [Column("NCM")]
    public string ncm { get; init; }
    [Column("PESOKG")]
    public decimal peso { get; init; }
}
