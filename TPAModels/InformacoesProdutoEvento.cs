using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPAPRODUTO", Schema="dbo")]
public class InformacoesProdutoEvento
{
    [Key]
    [Column("ID")]
    public int id { get; set; }

    [Column("PK_PRODUTO")]
    public required string pkProduto { get; init; }

    [Column("CODPRODUTO")]
    public required string codProduto { get; init; }

    [Column("DESCRICAO")]
    public required string descricao { get; init; }

    [Column("REFERENCIA")]
    public required string referencia { get; init; }

    [Column("UN")]
    public required string un { get; init; }

    [Column("IDX_NEGOCIO")]
    public string? idxNegocio { get; init; }

    [Column("IDX_CLASSIFICACAO")]
    public required string idxClassificacao { get; init; }

    [Column("IDX_MARCA")]
    public required string idxMarca { get; init; }

    [Column("IDX_LINHA")]
    public required string idxLinha { get; init; }

    [Column("IDX_APLICACAO")]
    public required string idxAplicacao { get; init; }

    [Column("IDX_SEPARACAO")]
    public required string idxSeparacao { get; init; }

    [Column("CSTI")]
    public required string cstI { get; init; }

    [Column("PCCUSTO")]
    public decimal pcCusto { get; init; }

    [Column("LOCACAO")]
    public required string locacao { get; init; }

    [Column("NCM")]
    public required string ncm { get; init; }
}
