using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public record EventoOrcAtendimento
{
    [Column("DESCRICAO")]
    public string? descricao { get; init; } //Descrição do que é o evento. Ex: aniversário 80 anos Jose, batizado matheus.

    [Column("IDX_EVENTO")]
    public required string idxEvento { get; init; } //ID do tipo de evento no TPA.

    [Column("CONVIDADOS")]
    public int convidados { get; init; } // Numero de convidados

    [Column("CRIANCAS")]
    public int criancas { get; init; } // Numero de crianças entre os convidados

    [Column("DTINICIO")]
    public DateTime dtInicio { get; init; } // Data inicio do evento

    [Column("HSINICIO")]
    public required string hsInicio { get; init; } // Hora inicio do evento

    [Column("DTTERMINO")]
    public DateTime dtTermino { get; init; } // Data termino do evento

    [Column("HSTERMINO")]
    public required string hsTermino { get; init; } // Hora termino do evento

    [Column("OBSINICIO")]
    public required string obsInicio { get; init; } // Observações adicionadas pelo vendedor

    [Column("OBSTERMINO")]
    public required string obsTermino { get; init; } // Observações adicionadas pelo vendedor

    [Column("IDX_ENDERECO")]
    public required string idxEndereco { get; init; } // ID do endereço na tabela TPAENDERECO

    [Column("LOCAL")]
    public string? local { get; init; } // Endereço completo

    [Column("OPINC")]
    public int opInc { get; init; } // ID usuário TPA

    [Column("OPALT")]
    public int opAlt { get; init; } // ID usuário TPA

    [Column("IDX_EVENTOTP")]
    public required string idxEventoTp { get; init; } // Outro ID de classificação de evento selecionado na criação. Vou enviar da API.

    [Column("PROFISSIONALESCALADO")]
    public required string profissionalEscalado { get; init; } // Se escala de profissionais foi feita

    [Column("CUSTOOPERACIONAL")]
    public decimal custoOperacional { get; init; } //Soma do preço de custo de produção. Está na tabela de produtos e vou enviar pela API.

    [Column("VALORTABELA")]
    public decimal valorTabela { get; init; } //Valor total preço de venda dos produtos

    [Column("PRODVALORORIGINAL")]
    public decimal prodValorOriginal { get; init; } //Valor total preço de venda dos produtos

    [Column("SERVVALORORIGINAL")]
    public decimal servValorOriginal { get; init; } //Valor total dos serviços.
}
