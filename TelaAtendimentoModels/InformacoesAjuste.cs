public record InformacoesAjuste
{
    public AjustePedido ajustePedido { get; init; }
    public List<AjusteItem> itensAjuste { get; init; }
}