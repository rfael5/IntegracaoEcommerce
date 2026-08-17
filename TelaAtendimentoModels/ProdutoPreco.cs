using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public record ProdutoPreco
{
    [Key]
    [Column("ID")]
    public int id { get; init; } 
    [Column("PK_PRODUTO")]
    public string pkProduto { get; init; }
    [Column("CODPRODUTO")]
    public string codProduto { get; init; }
    [Column("DESCRICAO")]
    public string descricao { get; init; }
    [Column("REFERENCIA")]
    public string referencia { get; init; }
    [Column("UN1")]
    public string un { get; init; }
    [Column("CSTI")]
    public string csti { get; init; }
    [Column("IDX_NEGOCIO")]
    public string idxNegocio { get; init; }
    [Column("IDX_CLASSIFICACAO")]
    public string idxClassificacao { get; init; }
    [Column("PCCUSTO")]
    public decimal pcCusto { get; init; }
    [Column("LOCACAO")]
    public string locacao { get; init; }
    [Column("NCM")]
    public string ncm { get; init; }
    [Column("PK_TABELA")]
    public string pkTabela { get; init; }
    [Column("NOMETABELA")]
    public string nomeTabela { get; init; }
    [Column("PRECO1U1")]
    public decimal preco { get; init; }
    [Column("ENCOMENDA")]
    public string permiteEncomenda { get; init; }
}