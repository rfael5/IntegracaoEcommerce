public record AjusteItem
{
    public int rdxAjustePed { get; init; }
    public int idxMovtoped { get; init; } // id do item no evento
    public decimal quantidade { get; init; } //quantidade modificado
    public decimal preco { get; init; } //valor da modificação
    public int operador { get; init; } //id do usuário no TPA
    public string tpAjusteProducao { get; init; } //se o ajuste foi em receitas
    public string tpAjusteSeparacao { get; init; } //se o ajuste foi em materiais
    public decimal qtOriginal { get; init; } //quantidade antes do ajuste
}