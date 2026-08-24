using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public record DoctopedAtendimento 
{
    [Column("TPDOCTO")]
    public string? tpDocto { get; init; } //Tipo de documento: EC ou OR
    [Column("IDX_ENTIDADE")]
    public string? idxEntidade { get; init; } // ID do cliente no TPA
    [Column("NOME")]
    public required string nome { get; init; } // Nome do cliente
    [Column("CNPJCPF")]
    public required string cnpjCpf { get; init; }
    [Column("CIDADE")]
    public string cidade { get; init; } = ""; //Endereços do evento
    [Column("UF")]
    public string uf { get; init; } = " "; //Endereços do evento
    [Column("IDX_DEPTO")]
    public string idxDepto { get; init; } // Se for OR tem que ser 9, se for EC tem que ser 10
    [Column("IDX_TABELA")]
    public string? idxTabela { get; init; } // id da tabela de preços selecionada na tela de criação do evento
    [Column("IDX_TABELASUB")]
    public string? idxTabelaSub { get; init; } // Depende do id acima. Vou passar as relações.
    [Column("IDX_FORMAPAG")]
    public string? idxFormaPag { get; init; } // ID forma de pagamento. Vou passar as relações
    [Column("IDX_VENDEDOR1")]
    public string idxVendedor1 { get; init; } // ID do vendedor no TPA
    [Column("IDX_VENDEDOR2")]
    public string? idxVendedor2 { get; init; } // ID do assistente
    [Column("TEXTO")]
    public string? texto { get; init; } // Observações externas
    [Column("TXTHISTORICO")]
    public string? txtHistorico { get; init; } // Observações internas
    [Column("TXTCONCLUSAO")]
    public string? txtConclusao { get; init; } // Observações faturamento
    [Column("TOTALDOCTO")]
    public decimal totalDocto { get; init; } // Valor total do evento
    [Column("PRODVALOR")]
    public decimal prodValor { get; init; } // Valor total dos produtos receitas
    [Column("PRODDESC")]
    public decimal prodDesc { get; init; } // Valor desconto
    [Column("PRODTOTAL")]
    public decimal prodTotal { get; init; } // Valor total + desconto
    [Column("FRETEVALOR")]
    public decimal freteValor { get; init; } // Valor frete
    [Column("SERVVALOR")] 
    public decimal servValor { get; init; } //Valor total profissionais (profissionais)
    [Column("SERVDESC")] 
    public decimal servDesc { get; init; } // Desconto valor profissionais
    [Column("SERVTOTAL")] 
    public decimal servTotal { get; init; } // Valor total profissionais + desconto
    [Column("SITUACAO")] 
    public string situacao { get; init; } // Status do evento. De início será 'N' (Não confirmado)
    [Column("TEMPRODUTO")] 
    public string temProduto { get; init; } // Se possui produto, serviço ou materiais no evento. Valor S ou N.
    [Column("TEMSERVICO")] 
    public string temServico { get; init; } // Se possui produto, serviço ou materiais no evento. Valor S ou N.
    [Column("TEMLOCACAO")] 
    public string temLocacao { get; init; } // Se possui produto, serviço ou materiais no evento. Valor S ou N.
    [Column("DTEVENTO")] 
    public DateTime dtEvento { get; init; } // Data do evento
    [Column("TOTALITENSPROD")] 
    public int totalItensProd { get; init; } // Quantidade de produtos receita
    [Column("TOTALITENSSERV")] 
    public int totalItensServ { get; init; } // Total de profissionais
    [Column("TOTALITENSLOC")] 
    public int totalItensLoc { get; init; } // Total materiais
    [Column("OPINC")] 
    public int opInc { get; init; } // Id do usuário no TPA.
    [Column("OPALT")] 
    public int opAlt { get; init; } // Id do usuário no TPA.
    [Column("ENTREGAR")] 
    public required string entregar { get; init; } //OR sempre letra E. EC, se for entrega = 3, se for retirar na loja = 1
    [Column("IDX_DOCTOEVENTO")]
    public string idxDoctoEvento { get; init; } // ID da tabela TPAEVENTOORC com informações do evento. Ela também tem que ser criada antes dessa.
    [Column("CONTATO")]
    public string? contato { get; init; } // Dados de contato do cliente - nome
    [Column("TELEFONE")]
    public string telefone { get; init; } // Dados de contato do cliente - telefone
    [Column("EMAIL")]
    public string email { get; init; } // Dados de contato do cliente - email
    [Column("DTPREVISAO")]
    public DateTime dtPrevisao { get; init; } // Data entrega encomenda
    [Column("HORAPREVISAO")]
    public required string horaPrevisao { get; init; } // Horario entrega encomenda
    [Column("CELULAR")]
    public string? celular { get; init; } // celular de contato
    [Column("TEMPRODUCAO")]
    public string temProducao { get; init; } // Se passa por produção na cozinha. S ou N.
    [Column("TEMPROFISSIONAL")] 
    public string temProfissional { get; init; } // Se tem profissionais. Isso é pedido em outra coluna acima. Tenho que encontrar a diferença dos dois.
    [Column("TOTALAJUSTE")] 
    public decimal totalAjuste { get; init; } // Valor dos ajustes
    [Column("TOTALFINANCEIRO")] 
    public decimal totalFinanceiro { get; init; } // Total do evento com descontos e ajustes
}