using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPACONTRATOITEM", Schema = "dbo")]
public record TpaContratoItemDTO
{
    [Key]
    [Column("PK_CONTRATOITEM")]
    public int pkContratoItem { get; init; }

    [Column("RDX_CONTRATOMOV")]
    public int? rdxContratoMov { get; init; }

    [Column("ITEM")]
    public int? item { get; init; }

    [Column("IDX_MOVTOEST")]
    public int? idxMovtoEst { get; init; }

    [Column("IDX_PRODUTO")]
    public string? idxProduto { get; init; }

    [Column("L_QUANTIDADE")]
    public decimal? lQuantidade { get; init; }

    [Column("L_PRECOUNI")]
    public decimal? lPrecoUni { get; init; }

    [Column("L_PRECOTOTAL")]
    public decimal? lPrecoTotal { get; init; }

    [Column("L_VALORBEM")]
    public decimal? lValorBem { get; init; }

    [Column("IDX_PATRIMONIO")]
    public string? idxPatrimonio { get; init; }

    [Column("IDX_PATRIMONIOMOVTO")]
    public string? idxPatrimonioMovto { get; init; }

    [Column("DTINC")]
    public DateTime? dtInc { get; init; }

    [Column("OPINC")]
    public int? opInc { get; init; }

    [Column("DTALT")]
    public DateTime? dtAlt { get; init; }

    [Column("OPALT")]
    public int? opAlt { get; init; }

    [Column("DESCRICAO")]
    public string? descricao { get; init; }

    [Column("REFERENCIA")]
    public string? referencia { get; init; }

    [Column("TIPOPROD")]
    public string? tipoProd { get; init; }

    [Column("LOCACAO")]
    public string? locacao { get; init; }

    [Column("UNIDADE")]
    public string? unidade { get; init; }

    [Column("LOCACAOBP")]
    public string? locacaoBp { get; init; }

    [Column("L_P")]
    public string? lP { get; init; }

    [Column("DESCRICAOCADASTRO")]
    public string? descricaoCadastro { get; init; }

    [Column("REFERENCIACADASTRO")]
    public string? referenciaCadastro { get; init; }

    [Column("CODPRODUTO")]
    public string? codProduto { get; init; }

    [Column("RENOVAR")]
    public string? renovar { get; init; }

    [Column("STATUS")]
    public string? status { get; init; }

    [Column("IDX_DOCTOESTFAT")]
    public int? idxDoctoEstFat { get; init; }

    [Column("IDX_MOVTOESTFAT")]
    public int? idxMovtoEstFat { get; init; }

    [Column("STATUSFAT")]
    public string? statusFat { get; init; }

    [Column("DTDEVOLUCAO")]
    public DateTime? dtDevolucao { get; init; }

    [Column("PRECOAUTORIZADO")]
    public decimal? precoAutorizado { get; init; }

    [Column("QTDEVOLVIDO")]
    public decimal? qtDevolvido { get; init; }

    [Column("IDX_CONTRATOADENDO")]
    public int? idxContratoAdendo { get; init; }

    [Column("PEDIDOEXTERNO")]
    public string? pedidoExterno { get; init; }

    [Column("PEDIDOITEMEXTERNO")]
    public string? pedidoItemExterno { get; init; }
}