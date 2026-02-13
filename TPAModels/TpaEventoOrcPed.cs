using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPACEVENTOORCPED", Schema = "dbo")]
public record TpaEventoOrcPedDTO
{
    [Key]
    [Column("ID")]
    public int id { get; set; }

    [Column("IDX_DOCTOPED")]
    public int? idxDoctoPed { get; init; }

    [Column("PK_EVENTOORCPED")]
    public required string pkEventoOrcPed { get; init; }

    [Column("SEQUENCIA")]
    public int sequencia { get; init; }

    [Column("TPIMPRESSAO")]
    public required string tpImpressao { get; init; }

    [Column("TOTITENS")]
    public int totItens { get; init; }

    [Column("QUANTIDADE")]
    public decimal quantidade { get; init; }

    [Column("VALORITENS")]
    public decimal valorItens { get; init; }

    [Column("TPDESCONTO")]
    public required string tpDesconto { get; init; }

    [Column("DESCONTO")]
    public decimal desconto { get; init; }

    [Column("VALORTOTAL")]
    public decimal valorTotal { get; init; }

    [Column("CUSTOTOTAL")]
    public decimal custoTotal { get; init; }

    [Column("DESPESAS")]
    public decimal despesas { get; init; }

    [Column("DTINC")]
    public DateTime dtInc { get; init; }

    [Column("OPINC")]
    public int opInc { get; init; }

    [Column("DTALT")]
    public DateTime dtAlt { get; init; }

    [Column("OPALT")]
    public int opAlt { get; init; }

    [Column("QTPROP")]
    public decimal qtProp { get; init; }

    [Column("QTPROPCONVIDADOS")]
    public int qtPropConvidados { get; init; }

    [Column("QTPROPARREDONDAR")]
    public required string qtPropArredondar { get; init; }

    [Column("IDX_EVENTOTPSV")]
    public required string idxEventoTpSv { get; init; }

    [Column("TOTALIZADOR")]
    public required string totalizador { get; init; }

    [Column("QUANTIDADEESTOQUE")]
    public decimal quantidadeEstoque { get; init; }

    [Column("TEXTO")]
    public string? texto { get; init; }

    [Column("DESCRICAO")]
    public required string descricao { get; init; }

    [Column("OPCAO")]
    public int opcao { get; init; }

    [Column("IDX_DOCTOPEDATIVIDADE")]
    public int? idxDoctoPedAtividade { get; init; }

    [Column("TPIMPRESSAOTOT")]
    public required string tpImpressaoTot { get; init; }

    [Column("TPIMPRESSAOMSG")]
    public required string tpImpressaoMsg { get; init; }

    [Column("TPIMPRESSAOIMG")]
    public required string tpImpressaoImg { get; init; }

    [Column("TPREGISTROITENS")]
    public required string tpRegistroItens { get; init; }

    [Column("QTUNIDADE")]
    public decimal qtUnidade { get; init; }

    [Column("VALORCONVIDADO")]
    public decimal valorConvidado { get; init; }

    [Column("OPVALOR")]
    public required string opValor { get; init; }

    [Column("IDX_IMG")]
    public required string idxImg { get; init; }

    [Column("CORTESIA")]
    public required string cortesia { get; init; }

    [Column("TPIMPRESSAOTAB")]
    public required string tpImpressaoTab { get; init; }

    [Column("INCIDEIMPOSTOS")]
    public string? incideImpostos { get; init; }

    [Column("PERMITEINCLUSAODIRETA")]
    public string? permiteInclusaoDireta { get; init; }

    [Column("FORMATURA")]
    public string? formatura { get; init; }

    [Column("NFORMANDOS")]
    public int? nFormandos { get; init; }

    [Column("NCORTESIAS")]
    public int? nCortesias { get; init; }

    [Column("OPCIONAL")]
    public string? opcional { get; init; }

    [Column("COMISSIONADO")]
    public string? comissionado { get; init; }

    [Column("COMISSAO")]
    public decimal? comissao { get; init; }
}
