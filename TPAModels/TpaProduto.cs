using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPAPRODUTO", Schema = "dbo")]
public record TpaProdutoDTO
{
    [Key]
    [Column("ID")]
    public int id { get; set; }

    [Column("PK_PRODUTO")]
    public required string pkProduto { get; init; }

    [Column("RDX_EMPRESA")]
    public string? rdxEmpresa { get; init; }

    [Column("CODPRODUTO")]
    public required string codProduto { get; init; }

    [Column("DESCRICAO")]
    public required string descricao { get; init; }

    [Column("REFERENCIA")]
    public required string referencia { get; init; }

    [Column("CODBARRA")]
    public required string codBarra { get; init; }

    [Column("CODFAB")]
    public required string codFab { get; init; }

    [Column("CODORI")]
    public required string codOri { get; init; }

    [Column("CODRED")]
    public int codRed { get; init; }

    [Column("TIPOPROD")]
    public required string tipoProd { get; init; }

    [Column("ESTOQUE")]
    public required string estoque { get; init; }

    [Column("IDX_MOEDA")]
    public required string idxMoeda { get; init; }

    [Column("LOCACAO")]
    public required string locacao { get; init; }

    [Column("IDX_NEGOCIO")]
    public string? idxNegocio { get; init; }

    [Column("IDX_CLASSIFICACAO")]
    public required string idxClassificacao { get; init; }

    [Column("IDX_MARCA")]
    public required string idxMarca { get; init; }

    [Column("IDX_LINHA")]
    public required string idxLinha { get; init; }

    [Column("IDX_APLICACAO")]
    public required string idxAplicacao { get; init; }

    [Column("IDX_SEPARACAO")]
    public required string idxSeparacao { get; init; }

    [Column("UNSEP")]
    public required string unSep { get; init; }

    [Column("UN")]
    public required string un { get; init; }

    [Column("UN1")]
    public required string un1 { get; init; }

    [Column("PROP1")]
    public decimal prop1 { get; init; }

    [Column("UN2")]
    public required string un2 { get; init; }

    [Column("PROP2")]
    public decimal prop2 { get; init; }

    [Column("UNREP")]
    public required string unRep { get; init; }

    [Column("PROPREP")]
    public decimal propRep { get; init; }

    [Column("PCREP")]
    public decimal pcRep { get; init; }

    [Column("TEXTO")]
    public string? texto { get; init; }

    [Column("ARQUIVOIMAGEM")]
    public required string arquivoImagem { get; init; }

    [Column("STATUS")]
    public required string status { get; init; }

    [Column("DTSTATUS")]
    public DateTime? dtStatus { get; init; }

    [Column("MOTIVOSTATUS")]
    public required string motivoStatus { get; init; }

    [Column("PESOKG")]
    public decimal pesoKg { get; init; }

    [Column("VOLUME")]
    public decimal volume { get; init; }

    [Column("CF")]
    public required string cf { get; init; }

    [Column("NCM")]
    public required string ncm { get; init; }

    [Column("GERAICMS")]
    public required string geraIcms { get; init; }

    [Column("ICMSBASEI")]
    public decimal icmsBaseI { get; init; }

    [Column("ICMSBASEE")]
    public decimal icmsBaseE { get; init; }

    [Column("ICMSALIQ")]
    public decimal icmsAliq { get; init; }

    [Column("MVA")]
    public decimal mva { get; init; }

    [Column("CSTI")]
    public required string cstI { get; init; }

    [Column("CSTE")]
    public required string cstE { get; init; }

    [Column("GERAIPI")]
    public required string geraIpi { get; init; }

    [Column("IPIALIQ")]
    public decimal ipiAliq { get; init; }

    [Column("GERAISSQN")]
    public required string geraIssqn { get; init; }

    [Column("ISSQNALIQ")]
    public decimal issqnAliq { get; init; }

    [Column("PCCOMPRA")]
    public decimal pcCompra { get; init; }

    [Column("PCCUSTO")]
    public decimal pcCusto { get; init; }

    [Column("DTULTIMACOMPRA")]
    public DateTime? dtUltimaCompra { get; init; }

    [Column("DTINC")]
    public DateTime dtInc { get; init; }

    [Column("OPINC")]
    public int opInc { get; init; }

    [Column("DTALT")]
    public DateTime dtAlt { get; init; }

    [Column("OPALT")]
    public int opAlt { get; init; }
  [Column("MANUTENCAO")]
    public required string manutencao { get; init; }

    [Column("PRODUTOACABADO")]
    public required string produtoAcabado { get; init; }

    [Column("TPLOCACAO")]
    public required string tpLocacao { get; init; }

    [Column("VENDA")]
    public required string venda { get; init; }

    [Column("ESPECIFICACAO")]
    public string? especificacao { get; init; }

    [Column("OBJETOCONTRATO")]
    public string? objetoContrato { get; init; }

    [Column("ESTMIN")]
    public decimal estMin { get; init; }

    [Column("ESTMAX")]
    public decimal estMax { get; init; }

    [Column("PTORES")]
    public decimal ptORes { get; init; }

    [Column("DIASCONSUMO")]
    public int diasConsumo { get; init; }

    [Column("DIASENTREGA")]
    public int diasEntrega { get; init; }

    [Column("ENDERECO")]
    public required string endereco { get; init; }

    [Column("QTMIN")]
    public decimal qtMin { get; init; }

    [Column("ALTURA")]
    public decimal altura { get; init; }

    [Column("LARGURA")]
    public decimal largura { get; init; }

    [Column("COMPRIMENTO")]
    public decimal comprimento { get; init; }

    [Column("ACESSORIO")]
    public required string acessorio { get; init; }

    [Column("TAMANHO")]
    public decimal tamanho { get; init; }

    [Column("TEXTOCOMPOSICAO")]
    public string? textoComposicao { get; init; }

    [Column("PARAMCOMPOSICAO")]
    public string? paramComposicao { get; init; }

    [Column("VERSAOCOMPOSICAO")]
    public int versaoComposicao { get; init; }

    [Column("TOT1NOME")]
    public required string tot1Nome { get; init; }

    [Column("TOT2NOME")]
    public required string tot2Nome { get; init; }

    [Column("TOT3NOME")]
    public required string tot3Nome { get; init; }

    [Column("TOT1PERC")]
    public decimal tot1Perc { get; init; }

    [Column("TOT2PERC")]
    public decimal tot2Perc { get; init; }

    [Column("TOT3PERC")]
    public decimal tot3Perc { get; init; }

    [Column("ENCOMENDA")]
    public required string encomenda { get; init; }

    [Column("RESSUPAUTOMATICO")]
    public required string resSupAutomatico { get; init; }

    [Column("PERMITEVDAZERO")]
    public required string permiteVdaZero { get; init; }

    [Column("EXIGEPROFISSIONAL")]
    public required string exigeProfissional { get; init; }

    [Column("EXIGEVISTORIA")]
    public required string exigeVistoria { get; init; }

    [Column("PARTICIPACOMISSAO")]
    public required string participacaoComissao { get; init; }

    [Column("QTMINIMO1")]
    public decimal qtMinimo1 { get; init; }

    [Column("QTMINIMO2")]
    public decimal qtMinimo2 { get; init; }

    [Column("ESTMAXAUTOMATICO")]
    public required string estMaxAutomatico { get; init; }

    [Column("CALCAUTOMATICO")]
    public required string calcAutomatico { get; init; }

    [Column("NETAPAPRODUCAO")]
    public int nEtapaProducao { get; init; }

    [Column("MSGVENDA")]
    public string? msgVenda { get; init; }

    [Column("MSGCOMPRA")]
    public string? msgCompra { get; init; }

    [Column("DIASDISPONIBILIDADE")]
    public int diasDisponibilidade { get; init; }

    [Column("TRIBUTACAO")]
    public required string tributacao { get; init; }

    [Column("OPCOMPOSICAO")]
    public required string opComposicao { get; init; }

    [Column("QUANTIDADE1")]
    public decimal quantidade1 { get; init; }

    [Column("NCONV1")]
    public int nConv1 { get; init; }

    [Column("QUANTIDADE2")]
    public decimal quantidade2 { get; init; }

    [Column("NCONV2")]
    public int nConv2 { get; init; }

    [Column("DECIMAL1")]
    public int decimal1 { get; init; }

    [Column("DECIMAL2")]
    public int decimal2 { get; init; }

    [Column("RENDIMENTO")]
    public decimal rendimento { get; init; }

    [Column("RENDIMENTO1")]
    public decimal rendimento1 { get; init; }

    [Column("RENDIMENTO2")]
    public decimal rendimento2 { get; init; }

    [Column("CONTROLELOTE")]
    public required string controleLote { get; init; }

    [Column("REFAZCCUSTO")]
    public required string refazCCusto { get; init; }

    [Column("UNPRODUCAO")]
    public required string unProducao { get; init; }

    [Column("PROPPRODUCAO")]
    public decimal propProducao { get; init; }

    [Column("PCMEDIO")]
    public decimal pcMedio { get; init; }

    [Column("ORIGEM")]
    public required string origem { get; init; }

    [Column("CODANP")]
    public required string codAnp { get; init; }

    [Column("CEST")]
    public required string cest { get; init; }

    [Column("CUSTOOPERACIONAL")]
    public decimal custoOperacional { get; init; }

    [Column("NEGOCIO")]
    public required string negocio { get; init; }

    [Column("PRODUCAOEXTERNA")]
    public required string producaoExterna { get; init; }

    [Column("OPSUPRIMENTOPRODUCAO")]
    public required string opSuprimentoProducao { get; init; }

    [Column("OPSUPRIMENTOMP")]
    public required string opSuprimentoMp { get; init; }

    [Column("DESCANP")]
    public string? descAnp { get; init; }

    [Column("ARREDONDAMENTOUN1")]
    public string? arredondamentoUn1 { get; init; }

    [Column("ARREDONDAMENTOUN2")]
    public string? arredondamentoUn2 { get; init; }

    [Column("VALORPMPF")]
    public decimal? valorPmpf { get; init; }

    [Column("SERVICOCFOP")]
    public string? servicoCfop { get; init; }

    [Column("RETORNAVEL")]
    public string? retornavel { get; init; }

    [Column("HORASDISPONIBILIDADE")]
    public int? horasDisponibilidade { get; init; }

    [Column("MINUTOSDISPONIBILIDADE")]
    public int? minutosDisponibilidade { get; init; }

    [Column("PCCUSTOPRODUCAO")]
    public decimal? pcCustoProducao { get; init; }

    [Column("CFOPPRODUTO")]
    public string? cfopProduto { get; init; }

    [Column("CFOPIF")]
    public string? cfopIf { get; init; }

    [Column("CFOPIC")]
    public string? cfopIc { get; init; }

    [Column("CFOPEF")]
    public string? cfopEf { get; init; }

    [Column("CFOPEC")]
    public string? cfopEc { get; init; }

    [Column("LOJAVIRTUAL")]
    public string? lojaVirtual { get; init; }

    [Column("DESTAQUE")]
    public string? destaque { get; init; }

    [Column("LANCAMENTO")]
    public string? lancamento { get; init; }

    [Column("DTALTLJ")]
    public DateTime? dtAltLj { get; init; }

    [Column("TPVALIDADE")]
    public string? tpValidade { get; init; }

    [Column("VALIDADE")]
    public int? validade { get; init; }

    [Column("CUSTOLOCALIDADE")]
    public string? custoLocalidade { get; init; }

    [Column("TEMPOINICIOPRODUCAO")]
    public int? tempoInicioProducao { get; init; }

    [Column("TPTEMPOINICIOPRODUCAO")]
    public string? tpTempoInicioProducao { get; init; }

    [Column("COMPOSICAO")]
    public string? composicao { get; init; }

    [Column("TEMESTRELAS")]
    public string? temEstrelas { get; init; }

    [Column("ESTRELAS")]
    public int? estrelas { get; init; }

    [Column("OFERTA")]
    public string? oferta { get; init; }

    [Column("TPCOMPOSICAO")]
    public string? tpComposicao { get; init; }

    [Column("DETALHE01")]
    public string? detalhe01 { get; init; }

    [Column("DETALHE02")]
    public string? detalhe02 { get; init; }

    [Column("DETALHE03")]
    public string? detalhe03 { get; init; }

    [Column("DETALHE04")]
    public string? detalhe04 { get; init; }

    [Column("PESOKGL")]
    public decimal? pesoKgl { get; init; }

    [Column("CCLASSTRIB")]
    public string? cClassTrib { get; init; }

    [Column("CCLASSTRIBNCM")]
    public string? cClassTribNcm { get; init; }

    [Column("PISELETIVO")]
    public decimal? pisEletivo { get; init; }

    [Column("PIBS")]
    public decimal? piBs { get; init; }

    [Column("RIBS")]
    public decimal? riBs { get; init; }

    [Column("PCBS")]
    public decimal? pcBs { get; init; }

    [Column("RCBS")]
    public decimal? rcBs { get; init; }
}