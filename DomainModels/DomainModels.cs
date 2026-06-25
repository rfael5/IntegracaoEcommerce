public record DadosPedido
{
    public ulong idPedido { get; init; }
    public required string status { get; init; }
    public DateTime dtInc { get; init; }
    public DateTime dtAlt { get; init; }
    public DateTime dataEntrega { get; init; }
    public string horaEntrega { get; init; }
    public required string totalPedido { get; init; }
    public decimal frete { get; set; }
    public ulong idClienteEcommerce { get; init; }
    public required string modoEntregaDescricao { get; init; }
    public required string modoEntregaId { get; init; }
    public required DadosEntrega dadosEntrega { get; init; }
    public required DadosCliente dadosCliente { get; init; }
    public required List<DadosProduto> produtos { get; init; }
}

public record LineItems
{
    public ulong id { get; init; }
    public required string name { get; init; }
    public ulong product_id { get; init; }
    public decimal quantity { get; init; }
    public required string tax_class { get; init; }
    public required string subtotal { get; init; }
    public required string subtotal_tax { get; init; }
    public required string total { get; init; }
    public required string total_tax { get; init; }
    public List<object>? taxes { get; init; }
    public List<object>? meta_data { get; init; }
    public required string sku { get; init; }
    public decimal price { get; init; }
    public object? image { get; init; }
    public string? parent_name { get; init; }
}

public record DadosProduto
{
    public ulong idProdutoEcommerce { get; init; }
    public required string nomeProduto { get; init; }
    public ulong idProdutoTPA { get; init; }
    public decimal quantidade { get; init; }
    public decimal preco { get; init; }
    public decimal subtotal { get; init; }
    public decimal total { get; init; }
}

public record DadosEntrega
{
    public required string rua { get; init; }
    public required string numero { get; init; }
    public required string bairro { get; init; }
    public required string cep { get; init; }
    public required string cidade { get; init; }
    public required string estado { get; init; }
    public required string pais { get; init; }
    public required string telefone { get; init; }
}

public record DadosCliente
{
    public required string nomeCliente { get; init; }
    public required string cidade { get; init; }
    public required string estado { get; init; }
    public required string cep { get; init; }
    public required string pais { get; init; }
    public required string email { get; init; }
    public required string telefone { get; init; }
    public required string celular { get; init; }
    public required string pessoa_fj { get; init; }
    public string cpf_cnpj { get; init; } = "14726676032";
    public required string nascimento { get; init; }
    public required string genero { get; init; }
}

public record UserKeys
{
    public string pkCadastro { get; set; }       
    public string pkEndereco { get; set; }       
}

public record ResponseData
{
    public int status { get; set; }
    public string message { get; set; }
    public object? data { get; set; } = null;
}

public record PagedResponseData
{
    public int status { get; set; }
    public string message { get; set; }
    public object? data { get; set; } = null;
    public int? pageNumber { get; set; }
    public int? pageSize { get; set; }
    public int? totalPages { get; set; }
    public int? totalRecords { get; set; }
    public bool? hasNextPage { get; set; }
    public bool? hasPreviousPage { get; set; }
}