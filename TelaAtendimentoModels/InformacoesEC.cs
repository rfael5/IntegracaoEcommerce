public record InformacoesEC
{
    public DoctopedAtendimento doctopedAtendimento { get; set; }
    public List<MovtopedAtendimento> movtopedAtendimento { get; set; }
    public DoctopedFpAtendimento doctopedFpAtendimento { get; set; }
}