using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPAMOVTOPED", Schema="dbo")]
public record TpaMovtopedDTO 
{
    [Key] 
    [Column("PK_MOVTOPED")]
    public int pkMovtoped { get; init; }
    [Column("RDX_DOCTOPED")]
    public int? rdxDoctoped { get; init; }
    [Column("OPERACAO")]
    public string operacao { get; init; } = "PV";
    [Column("SINAL")]
    public string sinal { get; init; } = "-";
    [Column("DATA")]
    public DateTime data { get; init; } = BrazilTime.Now();
    [Column("IDX_DEPTO")]
    public string? idxDepto { get; init; } = "          10";
    [Column("ITEM")]
    public int item { get; init; } //VARIA - ??????
    [Column("TPCODIGO")]
    public string tpCodigo { get; init; } = "P";
    [Column("CODPRODUTO")]
    public required string codProduto { get; init; } //VARIA - ENVIAR DO FRONT
    [Column("DESCRICAO")]
    public string? descricao { get; init; } //VARIA - ENVIAR DO FRONT
    [Column("REFERENCIA")]
    public string? referencia { get; init; } = "";
    [Column("TIPOPROD")]
    public string tipoProd { get; init; } = "P";
    [Column("RESERVA")]
    public string reserva { get; init; } = "N";
    [Column("IDX_PRODUTO")] 
    public string? idxProduto { get; init; } //VARIA - ENVIAR DO FRONT
    [Column("UNIDADE")] 
    public string unidade { get; init; } //VARIA - ENVIAR DO FRONT
    [Column("CST")] 
    public string cst { get; init; } //VARIA - ????
    [Column("L_QUANTIDADE")] 
    public decimal l_quantidade { get; init; } //VARIA - ENVIAR DO FRONT
    [Column("L_P")] 
    public string l_p { get; init; } = "1";
    [Column("L_PRECOUNI")] 
    public decimal l_precouni { get; init; }
    [Column("L_TPDESCONTO")] 
    public string l_tpdesconto { get; init; } = "N";
    [Column("L_DESCONTO")] 
    public decimal l_desconto { get; init; } = 0;
    [Column("L_TPACRESCIMO")] 
    public string l_tpacrescimo { get; init; } = "N";
    [Column("L_ACRESCIMO")] 
    public decimal l_acrescimo { get; init; } = 0;
    [Column("L_IPIALIQ")] 
    public decimal l_ipialiq { get; init; } = 0;
    [Column("L_IPIVALOR")] 
    public decimal l_ipivalor { get; init; } = 0;
    [Column("L_PRECOFINAL")] 
    public decimal l_precofinal { get; init; }
    [Column("L_PRECOTOTAL")] 
    public decimal l_precototal { get; init; }
    [Column("L_ICMSBASE")] 
    public decimal l_icmsbase { get; init; } = 0;
    [Column("L_ICMSALIQ")] 
    public decimal l_icmsaliq { get; init; } = 0;
    [Column("L_PRECOVENDA")] 
    public decimal l_precovenda { get; init; }
    [Column("L_PRECOAVISTA")] 
    public decimal l_precoavista { get; init; }
    [Column("L_VALORBEM")] 
    public decimal l_valorbem { get; init; } = 0;
    [Column("L_ISSQNALIQ")] 
    public decimal l_issqnaliq { get; init; } = 0;
    [Column("L_QUANTENTREGUE")] 
    public decimal l_quantentregue{ get; init; } = 0; //VARIA - ?????
    [Column("L_QUANTRESTANTE")] 
    public decimal l_quantrestante { get; init; } = 0;
    [Column("IDX_VENDEDOR1")] 
    public string? idxVendedor1 { get; init; } = "            ";
    [Column("CMSALIQVENDEDOR1")] 
    public decimal cmsAliqVendedor1 { get; init; } = 0;
    [Column("IDX_VENDEDOR2")] 
    public string? idxVendedor2 { get; init; } = "            ";
    [Column("CMSALIQVENDEDOR2")] 
    public decimal cmsAliqVendedor2 { get; init; } = 0;
    [Column("IDX_VENDEDOR3")] 
    public string? idxVendedor3 { get; init; } = "            ";
    [Column("CMSALIQVENDEDOR3")] 
    public decimal cmsAliqVendedor3 { get; init; } = 0;
    [Column("ATZCUSTO")] 
    public string atzCusto { get; init; } = "N";
    [Column("E_QUANTIDADE")] 
    public decimal e_quantidade { get; init; } = 0;
    [Column("E_PRECOUNI")] 
    public decimal e_precouni { get; init; } = 0;
    [Column("E_PRECOCUSTO")] 
    public decimal e_precocusto { get; init; } //VARIA - ?????????
    [Column("E_PRECOMEDIO")] 
    public decimal e_precomedio { get; init; } //VARIA - ?????????
    [Column("E_PRECOFINAL")] 
    public decimal e_precofinal { get; init; } = 0;
    [Column("PESO")] 
    public decimal peso { get; init; }
    [Column("DTVALIDADE")] 
    public DateTime? dtValidade { get; init; } = BrazilTime.Now();
    [Column("SITUACAO")] 
    public string situacao { get; init; } = "Z"; //CONFIRMAR
    [Column("ESTOQUE")] 
    public string estoque { get; init; } = "S";
    [Column("MOVTO")] 
    public string movto { get; init; } = "N";
    [Column("TEXTO")] 
    public string? texto { get; init; } = "";
    [Column("APURAINSS")] 
    public string apuraInss { get; init; } = "N";
    [Column("APURAIR")] 
    public string apuraIr { get; init; } = "N";
    [Column("IDX_PATRIMONIO")] 
    public string? idxPatrimonio { get; init; } = "            ";
    [Column("LOCACAO")] 
    public string locacao { get; init; } = "N"; //PODE VARIAR
    [Column("DTSAIDA")] 
    public DateTime dtSaida { get; init; } = BrazilTime.Now();
    [Column("DTRETORNO")] 
    public DateTime dtRetorno { get; init; } = BrazilTime.Now();
    [Column("STATUSLOCACAO")] 
    public string statusLocacao { get; init; } = "N";
    [Column("PERIODOLOCACAO")] 
    public int periodoLocacao { get; init; } = 0; //PODE VARIAR
    [Column("TPPERIODOLOCACAO")] 
    public string tpPeriodoLocacao { get; init; } = "D";
    [Column("DTINC")] 
    public DateTime dtInc { get; init; } = BrazilTime.Now();
    [Column("OPINC")] 
    public int opInc { get; init; } //ENVIAR DO FRONT;
    [Column("DTALT")] 
    public DateTime dtAlt { get; init; } = BrazilTime.Now();
    [Column("OPALT")] 
    public int opAlt { get; init; } //ENVIAR DO FRONT;
    [Column("L_CALCDESCONTO")] 
    public string? l_calcdesconto { get; init; } = "U";
    [Column("LOCACAOBP")] 
    public string? locacaoBp{ get; init; } = ""; //VARIA - ??????????
    [Column("GERACONTRATO")] 
    public string? geraContrato { get; init; } = "N";
    [Column("CFOP")] 
    public string cfop { get; init; } = "";
    [Column("CODPATRIMONIO")] 
    public string codPatrimonio { get; init; } = "";
    [Column("NSERIE")] 
    public string nSerie { get; init; } = "";
    [Column("IDX_PATRIMONIOMOVTO")] 
    public string idxPatrimonioMovto { get; init; } = "";
    [Column("IDX_GRUPOPED")] 
    public string idxGrupoPed { get; init; } = "";
    [Column("IDX_EVENTOORCGRUPO")] 
    public string idxEventoOrcGrupo { get; init; } = "";
    [Column("IDX_MOVTO")] 
    public int? idxMovto { get; init; } = 0;
    [Column("IDX_DEPTOENT")] 
    public string idxDeptoEnt { get; init; } = "";
    [Column("IDX_ETAPA")] 
    public int? idxEtapa { get; init; } = 0;
    [Column("NCM")] 
    public string ncm { get; init; } //ENVIAR DO FRONT
    [Column("L_ICMSALIQSUBS")] 
    public decimal l_icmsaliqsubs { get; init; } = 0;
    [Column("L_ICMSVALSUBS")] 
    public decimal l_icmsvalsubs { get; init; } = 0;
    [Column("TAMANHO")] 
    public decimal tamanho { get; init; } = 0;
    [Column("TABELAMOVTO")] 
    public string tabelaMovto { get; init; } = "";
    [Column("FATORTABELA")] 
    public decimal fatorTabela { get; init; } = 0;
    [Column("L_ICMSVALOR")] 
    public decimal l_icmsvalor { get; init; } = 0;
    [Column("L_ICMSBASEVALOR")] 
    public decimal l_icmsbasevalor { get; init; } = 0;
    [Column("L_ICMSSTVALOR")] 
    public decimal l_icmsstvalor { get; init; } = 0;
    [Column("NPECAS")] 
    public decimal npecas { get; init; } = 0;
    [Column("L_FRETEVALOR")] 
    public decimal l_fretevalor { get; init; } = 0;
    [Column("L_SEGUROVALOR")] 
    public decimal l_segurovalor { get; init; } = 0;
    [Column("L_OUTROVALOR")] 
    public decimal l_outrovalor { get; init; } = 0;
    [Column("L_DESCONTOVALOR")] 
    public decimal l_descontovalor { get; init; } = 0;
    [Column("L_ICMSBASESUBS")] 
    public decimal l_icmsbasesubs { get; init; } = 0;
    [Column("MVA")] 
    public decimal mva { get; init; } = 0;
    [Column("L_ICMSSTBASE")] 
    public decimal l_icmsstbase { get; init; } = 0;
    [Column("CALCCUSTO")] 
    public string calcCusto { get; init; } = "0";
    [Column("L_DIVERSOS")] 
    public decimal l_diversos { get; init; } = 0;
    [Column("SITUACAOOP")] 
    public string situacaoOp { get; init; } = "";
    [Column("SITUACAOSP")] 
    public string situacaoSp { get; init; } = "";
    [Column("L_PISCST")] 
    public string l_piscst { get; init; } = "";
    [Column("L_PISBC")] 
    public decimal l_pisbc { get; init; } = 0;
    [Column("L_PISP")] 
    public decimal l_pisp { get; init; } = 0;
    [Column("L_PISV")] 
    public decimal l_pisv { get; init; } = 0;
    [Column("L_COFINSCST")] 
    public string l_cofinscst { get; init; } = "";
    [Column("L_COFINSBC")] 
    public decimal l_cofinsbc { get; init; } = 0;
    [Column("L_COFINSP")] 
    public decimal l_cofinsp { get; init; } = 0;
    [Column("L_COFINSV")] 
    public decimal l_cofinsv { get; init; } = 0;
    [Column("GRUPO")] 
    public string grupo { get; init; } = "";
    [Column("GRUPOOPCAO")] 
    public int grupoOpcao { get; init; } = 0;
    [Column("IDX_DOCTOPEDATIVIDADE")] 
    public int? idxDoctopedAtividade { get; init; } = 0;
    [Column("VOLUME")] 
    public int volume { get; init; } = 0;
    [Column("PEDIDOEXTERNO")] 
    public string pedidoExterno { get; init; } = "";
    [Column("ITEMPEDIDOEXTERNO")] 
    public string itemPedidoExterno { get; init; } = "";
    [Column("IDX_KIT")] 
    public string idxKit { get; init; } = "";
    [Column("KITPARAM")] 
    public string kitParam { get; init; } = "NNN";
    [Column("CORTESIA")] 
    public string cortesia { get; init; } = "N";
    [Column("QUANTIDADE")] 
    public decimal quantidade { get; init; } = 1;
    [Column("NCONV")] 
    public int nconv { get; init; } = 1;
    [Column("QTPROP")] 
    public decimal qtProp { get; init; } = 1;
    [Column("L_IPICST")] 
    public string l_ipicst { get; init; } = "00";
    [Column("L_IPICENQ")] 
    public string l_ipicenq { get; init; } = "999";
    [Column("LOTE")] 
    public string? lote { get; init; } = "";
    [Column("ID_ESTOQUEITEM")] 
    public int? idEstoqueItem { get; init; } = 0;
    [Column("CONTROLELOTE")] 
    public string? controleLote { get; init; } = "N";
    [Column("PLANEJAMENTO")] 
    public string? planejamento { get; init; } = "N";
    [Column("CCLASSTRIB")] 
    public string? cclasstrib { get; init; } = null;
    [Column("L_PISELETIVO")] 
    public decimal? l_piseletivo { get; init; } = null;
    [Column("L_PIBS")] 
    public decimal? l_pibs { get; init; } = null;
    [Column("L_RIBS")] 
    public decimal? l_ribs { get; init; } = null;
    [Column("L_PCBS")] 
    public decimal? l_pcbs { get; init; } = null;
    [Column("L_RCBS")] 
    public decimal? l_rcbs { get; init; } = null;
    [Column("L_VIBS")] 
    public decimal? l_vibs { get; init; } = null; 
    [Column("L_VCBS")] 
    public decimal? l_vcbs { get; init; } = null;
}