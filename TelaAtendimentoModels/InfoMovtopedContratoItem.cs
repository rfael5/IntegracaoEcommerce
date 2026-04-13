public record InfoMovtopedContratoItem
{
    public int pkMovtoped { get; init; }
    public int? item { get; init; }
    public string idxProduto { get; init; }
    public decimal l_quantidade { get; init; }
    public decimal l_precouni { get; init; }
    public decimal l_precototal { get; init; }
    public decimal l_valorbem { get; init; }
    public string? idxPatrimonio { get; init; }
    public string? idxPatrimonioMovto { get; init; }
    public string referencia { get; init; }
    public string? tipoProd { get; init; }
    public string? locacao { get; init; }
    public string unidade { get; init; }
    public string? locacaoBp { get; init; }
    public string l_p { get; init; }
    public string codProduto { get; init; }
}