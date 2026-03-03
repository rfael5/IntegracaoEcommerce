using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public record EventoOrcPedAtendimento
{
    [Column("IDX_DOCTOPED")]
    public int? idxDoctoPed { get; init; } //Id do evento na tabela DOCTOPED

    [Column("SEQUENCIA")]
    public int sequencia { get; init; } //Id na lista de serviços

    [Column("TPIMPRESSAO")]
    public required string tpImpressao { get; init; } //Informação trazida da tabela de serviços do evento. Vou mandar na API.

    [Column("TOTITENS")]
    public int totItens { get; init; } //Número de itens no evento por serviço

    [Column("QUANTIDADE")]
    public decimal quantidade { get; init; } //Total de itens por serviço no evento.

    [Column("VALORITENS")]
    public decimal valorItens { get; init; } //Valor monetários dos itens por serviço

    [Column("DESCONTO")]
    public decimal desconto { get; init; }

    [Column("VALORTOTAL")]
    public decimal valorTotal { get; init; } // Total monetário por serviço

    [Column("OPINC")]
    public int opInc { get; init; } //Id do usuário no TPA

    [Column("OPALT")]
    public int opAlt { get; init; } //Id do usuário no TPA

    [Column("IDX_EVENTOTPSV")]
    public required string idxEventoTpSv { get; init; } //Id do serviço no evento. São selecionados na criação do evento.

    [Column("TOTALIZADOR")]
    public required string totalizador { get; init; } //Nome do grupo ao qual o serviço pertence

    [Column("DESCRICAO")]
    public required string descricao { get; init; } //Nome do serviço.

    [Column("TPIMPRESSAOTOT")]
    public required string tpImpressaoTot { get; init; } //Vai vir da tabela de tipo de serviços.

    [Column("TPIMPRESSAOMSG")]
    public required string tpImpressaoMsg { get; init; } //Vai vir da tabela de tipo de serviços.

    [Column("TPIMPRESSAOIMG")]
    public required string tpImpressaoImg { get; init; } //Vai vir da tabela de tipo de serviços.

    [Column("TPREGISTROITENS")]
    public required string tpRegistroItens { get; init; } //Vai vir da tabela de tipo de serviços.

    [Column("IDX_IMG")]
    public required string idxImg { get; init; } //Vai vir da tabela de tipo de serviços.
    public List<MovtopedAtendimento> produtos { get; init; }
}
