public record AjustePedido
{
    public int rdxDoctoped { get; init; } //id do evento
    public int numero { get; init; } //numero do ajuste no evento
    public string descricao { get; init; } //nome do ajuste
    public string temProducao { get; init; } //se tem ajuste em produtos acabados
    public decimal totalValor { get; init; } //total valor ajuste
    public string? obs { get; init; } //observações sobre o ajuste - campo não obrigatório.
    public int operador { get; init; } //id do usuário no TPA
}