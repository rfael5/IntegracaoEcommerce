using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public record MovtopedAtendimento
{
    [Column("RDX_DOCTOPED")]
    public int? rdxDoctoped { get; init; } // ID da tabela doctoped
    [Column("IDX_DEPTO")]
    public string? idxDepto { get; init; } // 9 se for OR, 10 se for EC
    [Column("CODPRODUTO")]
    public required string codProduto { get; init; } //código do produto no TPA
    [Column("DESCRICAO")]
    public string? descricao { get; init; } //nome do produto
    [Column("REFERENCIA")]
    public string? referencia { get; init; } //nome referencia do produto
    [Column("TIPOPROD")]
    public string tipoProd { get; init; } // P se for receita, S se não for
    [Column("IDX_PRODUTO")] 
    public string? idxProduto { get; init; } //id do produto na tabela do tpa
    [Column("UNIDADE")] 
    public string unidade { get; init; } //unidade de venda do produto
    [Column("CST")] 
    public string cst { get; init; } //numero relacionado ao produto no tpa. irei enviar
    [Column("L_QUANTIDADE")] 
    public decimal l_quantidade { get; init; } //quantidade do produto no evento
    [Column("L_PRECOUNI")] 
    public decimal l_precouni { get; init; } // preço unitário do produto
    [Column("L_PRECOTOTAL")] 
    public decimal l_precototal { get; init; } // preço unitário x quantidade
    [Column("PESO")] 
    public decimal peso { get; init; } // peso do produto
    [Column("SITUACAO")] 
    public string situacao { get; init; } //Status do evento. De início será N.
    [Column("OPINC")] 
    public int opInc { get; init; } //ID do usuário no TPA
    [Column("OPALT")] 
    public int opAlt { get; init; } //ID do usuário no TPA
    [Column("NCM")] 
    public string ncm { get; init; } //Código ncm do produto. Também vou enviar do banco.
}
