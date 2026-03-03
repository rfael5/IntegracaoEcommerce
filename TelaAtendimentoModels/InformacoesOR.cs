public record InformacoesOR
{
    public DoctopedAtendimento doctopedAtendimento { get; set; }
    public EventoOrcAtendimento? eventoOrcAtendimento { get; set; }
    public List<EventoOrcPedAtendimento> eventoOrcPedAtendimento { get; set; }
    //public List<MovtopedAtendimento> movtopedAtendimento { get; set; }
    //public DoctopedFpAtendimento? doctopedFpAtendimento { get; set; }
}