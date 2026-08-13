using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProdutoServico
{
    public string prodEventoSv { get; init; }
    public int idTipoServico { get; init; }
    public string itemServico { get; init; }
    public string pkProduto { get; init; }
    public string codProduto { get; init; }
    public string nomeProduto { get; init; }
    public string referencia { get; init; }
    public string un { get; init; }
    public string csti { get; init; }
    public string idxNegocio { get; init; }
    public string idxClassificacao { get; init; }
    public decimal pcCusto { get; init; }
    public string locacao { get; init; }
    public string ncm { get; init; }
    public string permiteEncomenda { get; init; }
}