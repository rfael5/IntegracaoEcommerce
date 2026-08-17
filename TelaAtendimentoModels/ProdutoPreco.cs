using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public record ProdutoPreco
{
    [Key]
    [Column("ID")]
    public int id { get; set; } 
    [Column("PK_PRODUTO")]
    public string pkProduto { get; set; }
    [Column("CODPRODUTO")]
    public string codProduto { get; set; }
    [Column("DESCRICAO")]
    public string descricao { get; set; }
    [Column("REFERENCIA")]
    public string referencia { get; set; }
    [Column("UN1")]
    public string un { get; set; }
    [Column("CSTI")]
    public string csti { get; set; }
    [Column("IDX_NEGOCIO")]
    public string idxNegocio { get; set; }
    [Column("IDX_CLASSIFICACAO")]
    public string idxClassificacao { get; set; }
    [Column("PCCUSTO")]
    public decimal pcCusto { get; set; }
    [Column("LOCACAO")]
    public string locacao { get; set; }
    [Column("NCM")]
    public string ncm { get; set; }
    [Column("PK_TABELA")]
    public string pkTabela { get; set; }
    [Column("NOMETABELA")]
    public string nomeTabela { get; set; }
    [Column("PRECO1U1")]
    public decimal preco { get; set; }
    [Column("ENCOMENDA")]
    public string permiteEncomenda { get; set; }
}