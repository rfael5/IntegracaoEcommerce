using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPADOCTOPED", Schema="dbo")]
public record TpaDoctopedDTO
{
    [Key]
    [Column("PK_DOCTOPED")]
    public int pkDoctoped { get; init; }
    [Column("RDX_EMPRESA")]
    public string? rdxEmpresa { get; init; } = "           1";
    [Column("OPERACAO")]
    public string operacao { get; init; } = "PV";
    [Column("SINAL")]
    public string sinal { get; init; } = "-";
    [Column("TPDOCTO")]
    public string? tpDocto { get; init; } = "EC";
    [Column("DOCUMENTO")]
    public int? documento { get; init; }
    [Column("DATA")]
    public DateTime data { get; init; } = BrazilTime.Now();
    [Column("TPENTIDADE")]
    public string tpEntidade { get; init; } = "C";
    [Column("IDX_ENTIDADE")]
    public string? idxEntidade { get; init; }
    [Column("NOME")]
    public required string nome { get; init; }
    [Column("CNPJCPF")]
    public required string cnpjCpf { get; init; }
    [Column("CIDADE")]
    public string cidade { get; init; } = "";
    [Column("UF")]
    public string uf { get; init; } = " ";
    [Column("IDX_PEDIDO")]
    public int? idxPedido { get; init; } = 0;
    [Column("IDX_DEPTO")]
    public string? idxDepto { get; init; } = "          10";
    [Column("IDX_FILIAL")]
    public string? idxFilial { get; init; } = "           1";
    [Column("IDX_TABELA")]
    public string? idxTabela { get; init; } = "          24";
    [Column("IDX_TABELASUB")]
    public string? idxTabelaSub { get; init; } = "           9";
    [Column("IDX_FORMAPAG")]
    public string? idxFormaPag { get; init; } = "           3";
    [Column("IDX_EQUIPAMENTO")]
    public string? idxEquipamento { get; init; } = null;
    [Column("IDX_TRANSPORTADOR")]
    public string? idxTransportador { get; init; } = "            ";
    [Column("IDX_ROTA")]
    public string idxRota { get; init; } = "";
    [Column("DIASENTREGA")]
    public int diasEntrega { get; init; } = 0;
    [Column("IDX_VENDEDOR1")]
    public string idxVendedor1 { get; init; } = "         907";
    [Column("CMSALIQVENDEDOR1")]
    public decimal cmsAliqVendedor1 { get; init; } = 0;
    [Column("IDX_VENDEDOR2")]
    public string? idxVendedor2 { get; init; } = null;
    [Column("CMSALIQVENDEDOR2")]
    public decimal cmsAliqVendedor2 { get; init; } = 0;
    [Column("IDX_VENDEDOR3")]
    public string? idxVendedor3 { get; init; } = null;
    [Column("CMSALIQVENDEDOR3")]
    public decimal cmsAliqVendedor3 { get; init; } = 0;
    [Column("IDX_CCTDEB")]
    public string? idx_cctdeb { get; init; } = null;
    [Column("IDX_CCTCRE")]
    public string? idx_cctcre { get; init; } = null;
    [Column("GERAESTOQUE")]
    public string geraEstoque { get; init; } = "S";
    [Column("GERAFINANCEIRO")]
    public string geraFinanceiro { get; init; } = "N";
    [Column("TEXTO")]
    public string? texto { get; init; } = null;
    [Column("NFMODELO")]
    public string nfModelo { get; init; } = "00";
    [Column("IDX_MOEDA")]
    public string idxMoeda { get; init; } = "R$";
    [Column("TOTALDOCTO")]
    public decimal totalDocto { get; init; }
    [Column("PRODVALOR")]
    public decimal prodValor { get; init; }
    [Column("PRODTPDESC")]
    public string prodTpDesc { get; init; } = "V";
    [Column("PRODDESC")]
    public decimal prodDesc { get; init; } = 0;
    [Column("PRODTOTAL")]
    public decimal prodTotal { get; init; }
    [Column("FRETECALC")]
    public string freteCalc { get; init; } = "I";
    [Column("FRETEVALOR")]
    public decimal freteValor { get; init; }
    [Column("SEGUROCALC")]
    public string seguroCalc { get; init; } = "I";
    [Column("SEGUROVALOR")]
    public decimal seguroValor { get; init; } = 0;
    [Column("DESPDIVCALC")]
    public string despDivCalc { get; init; } = "I";
    [Column("DESPDIVVALOR")]
    public decimal despDivValor { get; init; }
    [Column("IPIBASE")]
    public decimal ipiBase { get; init; } = 0;
    [Column("IPIALIQ")]
    public decimal ipiAliq { get; init; } = 0;
    [Column("IPICALC")]
    public string ipiCalc { get; init; } = "N";
    [Column("IPIVALOR")]
    public decimal ipiValor { get; init; } = 0;
    [Column("IPIDIF")]
    public decimal ipiDif { get; init; } = 0;
    [Column("ICMSBASE")]
    public decimal icmsBase{ get; init; } = 0;
    [Column("ICMSCALC")]
    public string icmsCalc { get; init; } = "N";
    [Column("ICMSALIQ")]
    public decimal icmsAliq { get; init; } = 0;
    [Column("ICMSVALOR")]
    public decimal icmsValor { get; init; } = 0;
    [Column("ICMSDIF")]
    public decimal icmsDif { get; init; } = 0;
    [Column("ICMSSUBSTBASE")]
    public decimal icmsSubstBase { get; init; } = 0;
    [Column("ICMSSUBST1")] 
    public decimal icmsSubst1 { get; init; } = 0;
    [Column("ICMSSUBST2")] 
    public decimal icmsSubst2 { get; init; } = 0;
    [Column("ICMSSUBSTCALC")] 
    public string icmsSubstCalc { get; init; } = "I";
    [Column("ICMSSUBSTVALOR")] 
    public decimal icmsSubstValor { get; init; } = 0;
    [Column("ICMSSUBSTDIF")] 
    public decimal icmsSubstDif{ get; init; } = 0;
    [Column("SERVVALOR")] 
    public decimal servValor { get; init; } = 0;
    [Column("SERVTPDESC")] 
    public string servTpDesc { get; init; } = "V";
    [Column("SERVDESC")] 
    public decimal servDesc { get; init; } = 0;
    [Column("SERVTOTAL")] 
    public decimal servTotal { get; init; } = 0;
    [Column("ISSQNCALC")] 
    public string issqnCalc { get; init; } = "N";
    [Column("ISSQNALIQ")] 
    public decimal issqnAliq { get; init; } = 0;
    [Column("ISSQNVALOR")] 
    public decimal issqnValor { get; init; } = 0;
    [Column("ISSQNDIF")] 
    public decimal issqnDif { get; init; } = 0;
    [Column("IRCALC")] 
    public string irCalc { get; init; } = "N";
    [Column("IRALIQ")] 
    public decimal irAliq { get; init; } = 0;
    [Column("IRVALOR")] 
    public decimal irValor { get; init; } = 0;
    [Column("IRDIF")] 
    public decimal irDif { get; init; } = 0;
    [Column("INSSCALC")] 
    public string inssCalc { get; init; } = "N";
    [Column("INSSALIQ")] 
    public decimal inssAliq { get; init; } = 0;
    [Column("INSSVALOR")] 
    public decimal inssValor { get; init; } = 0;
    [Column("INSSDIF")] 
    public decimal inssDif { get; init; } = 0;
    [Column("SITUACAO")] 
    public string situacao { get; init; } = "Z";
    [Column("DTSITUACAO")] 
    public DateTime dtSituacao { get; init; } = BrazilTime.Now();
    [Column("MOTIVOSITUACAO")] 
    public string motivoSituacao { get; init; } = "";
    [Column("VALORENTRADA")] 
    public decimal valorEntrada { get; init; } = 0;
    [Column("VALORPARCELAMENTO")] 
    public decimal valorParcelamento { get; init; } = 0;
    [Column("PARCELAS")] 
    public int parcelas { get; init; } = 0;
    [Column("TEMPRODUTO")] 
    public string temProduto { get; init; } = "S";
    [Column("TEMSERVICO")] 
    public string temServico { get; init; } = "N";
    [Column("TEMLOCACAO")] 
    public string temLocacao { get; init; } = "N";
    [Column("TEMICMS")] 
    public string temIcms { get; init; } = "N";
    [Column("TEMIPI")] 
    public string temIpi { get; init; } = "N";
    [Column("TEMISSQN")] 
    public string temIssqn { get; init; } = "N";
    [Column("TEMIR")] 
    public string temIr { get; init; } = "N";
    [Column("TEMINSS")] 
    public string temInss { get; init; } = "N";
    [Column("TEMST")] 
    public string temSt { get; init; } = "N";
    [Column("DTSAIDA")] 
    public DateTime dtSaida { get; init; } = BrazilTime.Now();
    [Column("DTEVENTO")] 
    public DateTime dtEvento { get; init; } = BrazilTime.Now();
    [Column("DTRETORNO")] 
    public DateTime dtRetorno { get; init; } = BrazilTime.Now();
    [Column("PERIODOLOCACAO")] 
    public int periodoLocacao { get; init; } = 1;
    [Column("TPPERIODOLOCACAO")] 
    public string tpPeriodoLocacao { get; init; } = "D";
    [Column("STATUSLOCACAO")] 
    public string statusLocacao { get; init; } = "N";
    [Column("BENSTOTAL")] 
    public decimal bensTotal { get; init; } = 0;
    [Column("TOTALITENSPROD")] 
    public int totalItensProd { get; init; }
    [Column("TOTALITENSSERV")] 
    public int totalItensServ { get; init; } = 0;
    [Column("TOTALITENSLOC")] 
    public int totalItensLoc { get; init; } = 0;
    [Column("TOTALQTPROD")] 
    public decimal totalQtProd { get; init; }
    [Column("TOTALQTSERV")] 
    public decimal totalQtServ { get; init; } = 0;
    [Column("TOTALQTLOC")] 
    public decimal totalQtLoc { get; init; } = 0;
    [Column("DTINC")] 
    public DateTime dtInc { get; init; } = BrazilTime.Now();
    [Column("OPINC")] 
    public int opInc { get; init; }
    [Column("DTALT")] 
    public DateTime dtAlt{ get; init; } = BrazilTime.Now();
    [Column("OPALT")] 
    public int opAlt { get; init; }
    [Column("TPOPERACAO")] 
    public string tpOperacao { get; init; } = "P";
    [Column("CFOP")] 
    public string cfop { get; init; } = "";
    [Column("ENTREGAR")] 
    public required string entregar { get; init; }
    [Column("IDX_ENDERECOENT")] 
    public string idxEnderecoEnt { get; init; } = "            ";
    [Column("IDX_ENDERECOOBRA")] 
    public string idxEnderecoObra { get; init; } = "            ";
    [Column("IDX_ENDERECOCOB")] 
    public string idxEnderecoCob { get; init; } = "            ";
    [Column("IDX_ENDERECOFAT")] 
    public string idxEnderecoFat { get; init; } = "            ";
    [Column("GERACONTRATO")] 
    public string geraContrato { get; init; } = "N";
    [Column("TPCONTRATO")] 
    public string tpContrato { get; init; } = "N";
    [Column("IDX_CONTRATOMOV")] 
    public int idxContratoMov { get; init; } = 0;
    [Column("NFSERIE")] 
    public string nfSerie { get; init; } = "";
    [Column("NFNUMERO")] 
    public string nfNumero { get; init; } = "";
    [Column("NFDTEMISSAO")] 
    public DateTime? nfDtEmissao { get; init; } = BrazilTime.Now();
    [Column("NFDTSAIDA")] 
    public DateTime? nfDtSaida { get; init; } = BrazilTime.Now();
    [Column("STCALC")]
    public string stCalc { get; init; } = "N";
    [Column("NFTPEMISSAO")]
    public string nftPermissao { get; init; } = " ";
    [Column("NFHORASAIDA")]
    public string nfHoraSaida { get; init; } = " ";
    [Column("IDX_DOCTOEVENTO")]
    public string idxDoctoEvento { get; init; } = " ";
    [Column("MODULO")]
    public string? modulo { get; init; } = "BF";
    [Column("OPERACAOTIPO")]
    public string operacaoTipo { get; init; } = "P";
    [Column("OBRA")]
    public string obra { get; init; } = "E";
    [Column("CONTATO")]
    public string? contato { get; init; } = "";
    [Column("TELEFONE")]
    public string telefone { get; init; } = "";
    [Column("EMAIL")]
    public string email { get; init; } = "";
    [Column("PARCIAL")]
    public string parcial { get; init; } = "N";
    [Column("IDX_DEPTOENT")]
    public string idxDeptoEnt { get; init; } = "";
    [Column("NFEMITENTE")]
    public string nfEmitente { get; init; } = "E";
    [Column("TXTHISTORICO")]
    public string? txtHistorico { get; init; } = null;
    [Column("TXTCONCLUSAO")]
    public string? txtConclusao { get; init; } = null;
    [Column("TXTOPCOES")]
    public string? txtOpcoes { get; init; } = null;
    [Column("DTABERTURA")]
    public DateTime? dtAbertura { get; init; } 
    [Column("DTFECHAMENTO")]
    public DateTime? dtFechamento{ get; init; } 
    [Column("IDX_CADASTROEQPTO")]
    public string idxCadastroEqpto{ get; init; } = "";
    [Column("DTPREVISAO")]
    public DateTime dtPrevisao { get; init; }
    [Column("HORAPREVISAO")]
    public required string horaPrevisao { get; init; }
    [Column("HORAABERTURA")]
    public string horaAbertura{ get; init; } = "";
    [Column("HORAFECHAMENTO")]
    public string horaFechamento { get; init; } = "";
    [Column("DIASVALIDADE")]
    public int diasValidade { get; init; } = 0;
    [Column("ASSUNTO")]
    public string assunto { get; init; } = "";
    [Column("TEMPREVISAO")]
    public string temPrevisao { get; init; } = "N";
    [Column("ECF")]
    public string ecf { get; init; } = "N";
    [Column("SITUACAOOP")]
    public string situacaoOp { get; init; } = "";
    [Column("SITUACAOSP")]
    public string situacaoSp { get; init; } = "";
    [Column("ICMSCALCFRETE")]
    public string icmsCalcFrete { get; init; } = "N";
    [Column("LOCVALOR")]
    public decimal locValor { get; init; } = 0;
    [Column("LOCTPDESC")]
    public string locTpDesc { get; init; } = "V";
    [Column("LOCDESC")]
    public decimal locDesc { get; init; } = 0;
    [Column("RELOCTPCALC")]
    public string relocTpCalc { get; init; } = "V";
    [Column("RELOC")]
    public decimal reloc { get; init; } = 0;
    [Column("LOCTOTAL")]
    public decimal locTotal { get; init; } = 0;
    [Column("PISVALOR")]
    public decimal pisValor { get; init; } = 0;
    [Column("COFINSVALOR")]
    public decimal cofinsValor { get; init; } = 0;
    [Column("CELULAR")]
    public string? celular { get; init; } = "";
    [Column("TEMPRODUCAO")]
    public string temProducao { get; init; } = "S";
    [Column("IDX_ASSUNTO")]
    public string idxAssunto { get; init; } = "";
    [Column("CSLLVALOR")]
    public decimal csllValor { get; init; } = 0;
    [Column("IDX_CCUSTO")]
    public string idxcCusto{ get; init; } = "";
    [Column("TEMPROFISSIONAL")] 
    public string temProfissional { get; init; } = "N";
    [Column("IDX_CCUSTOOP")] 
    public string idxcCustoOp{ get; init; } = "            ";
    [Column("NFVOLUME")] 
    public int nfVolume { get; init; } = 0;
    [Column("NFPESOBRUTO")] 
    public decimal nfPesoBruto { get; init; } = 0;
    [Column("NFPESOLIQUIDO")] 
    public decimal nfPesoLiquido { get; init; } = 0;
    [Column("IDX_ENTIDADE2")] 
    public string idxEntidade2 { get; init; } = "";
    [Column("CONFERENCIA")] 
    public string conferencia { get; init; } = "";
    [Column("EMAILENVIADO")] 
    public string emailEnviado { get; init; } = "N";
    [Column("ORIGEM")] 
    public string origem { get; init; } = "L";
    [Column("DTPREVISAOINI")] 
    public DateTime? dtPrevisaoIni { get; init; } = BrazilTime.Now(); 
    [Column("IDX_ORCAMENTO")] 
    public int idxOrcamento { get; init; } = 0;
    [Column("TOTALAJUSTE")] 
    public decimal totalAjuste { get; init; } = 0;
    [Column("TOTALPAGO")] 
    public decimal totalPago { get; init; } = 0;
    [Column("TOTALAPAGAR")] 
    public decimal totalAPagar { get; init; } = 0;
    [Column("ORIGEM_IDX_PEDIDO")] 
    public string origemIdxPedido { get; init; } = "";
    [Column("CSLLCALC")] 
    public string csllCalc { get; init; } = "N";
    [Column("PISALIQ")] 
    public decimal pisAliq { get; init; } = 0;
    [Column("PISCALC")] 
    public string pisCalc { get; init; } = "N";
    [Column("CSLLALIQ")] 
    public decimal csllAliq { get; init; } = 0;
    [Column("COFINSALIQ")] 
    public decimal cofinsAliq { get; init; } = 0;
    [Column("COFINSCALC")] 
    public string cofinsCalc { get; init; } = "N";
    [Column("TOTALFINANCEIRO")] 
    public decimal totalFinanceiro { get; init; }
    [Column("VERSAO")] 
    public int? versao { get; init; } = null;
    [Column("INSSBASE")] 
    public decimal? inssBase { get; init; } = 0;
    [Column("IRBASE")] 
    public decimal? irBase { get; init; } = 0;
    [Column("CONTRIBUINTE")] 
    public string? contribuinte { get; init; } = "9";
    [Column("HASH")] 
    public string? hash { get; init; } = null;
    [Column("LV")] 
    public string? lv { get; init; } = null;
    [Column("PIX")] 
    public string? pix { get; init; } = null;
    [Column("IDX_ORCAMENTOATIV")] 
    public int? idxOrcamentoAtiv { get; init; } = 0;
    [Column("TEMANEXO")] 
    public string? temAnexo { get; init; } = null;
    [Column("MODFRETE")] 
    public string? modFrete { get; init; } = null;
    [Column("INDPRES")] 
    public string? indPres { get; init; } = null;
    [Column("ISELETIVOBASE")] 
    public string? isEletivoBase { get; init; } = null;
    [Column("ISELETIVOVALOR")] 
    public string? isEletivoValor { get; init; } = null;
    [Column("IBSBASE")] 
    public string? ibsBase { get; init; } = null;
    [Column("IBSVALOR")] 
    public string? ibsValor { get; init; } = null;
    [Column("CBSBASE")] 
    public string? cbsBase { get; init; } = null;
    [Column("CBSVALOR")] 
    public string? cbsValor { get; init; } = null;
}