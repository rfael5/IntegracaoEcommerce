using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProdutoServico
{
    public string prodEventoSv { get; set; }
    public int idTipoServico { get; set; }
    public string itemServico { get; set; }
    public string pkProduto { get; set; }
    public string codProduto { get; set; }
    public string nomeProduto { get; set; }
    public string referencia { get; set; }
    public string un { get; set; }
    public string csti { get; set; }
    public string idxNegocio { get; set; }
    public string idxClassificacao { get; set; }
    public decimal pcCusto { get; set; }
    public string locacao { get; set; }
    public string ncm { get; set; }
    public string permiteEncomenda { get; set; }
}