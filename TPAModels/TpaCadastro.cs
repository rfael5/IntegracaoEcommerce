using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPACADASTRO", Schema="dbo")]
public record TpaCadastroDTO
{
    [Key] 
    [Column("ID")]
    public int id { get; set; }
    [Column("PK_CADASTRO")]
    public required string pkCadastro { get; init; }
    [Column("CODCADASTRO")]
    public required string codCadastro { get; init; }
    [Column("NOME")]
    public required string nome { get; init; }
    [Column("FANTASIA")]
    public required string fantasia { get; init; }
    [Column("NOMEREPRESENTANTE")]
    public string nomeRepresentante { get; init; } = "";
    [Column("CODCLI")]
    public int codCli { get; init; } = 0;
    [Column("CLIENTE")]
    public string cliente { get; init; } = "S";
    [Column("FORNECEDOR")]
    public string fornecedor { get; init; } = "N";
    [Column("REPRESENTANTE")]
    public string representante { get; init; } = "N";
    [Column("TRANSPORTADORA")]
    public string transportadora { get; init; } = "N";
    [Column("EMPREITEIRO")]
    public string empreiteiro { get; init; } = "N";
    [Column("AGENDA")]
    public string agenda { get; init; } = "S";
    [Column("PESSOAFJ")]
    public string pessoaFj { get; init; } = "F";
    [Column("CNPJCPF")]
    public required string cnpjCpf { get; init; }
    [Column("INSCCI")]
    public string? inscci { get; init; } = "ISENTO";
    [Column("INSCMUN")]
    public string? inscmun { get; init; } = "ISENTO";
    [Column("IDX_CCUSTO")]
    public string? idxCcusto { get; init; } = null;
    [Column("IDX_FORMAPAG")]
    public string? idxFormaPag { get; init; } = "";
    [Column("IDX_RAMOATIV")]
    public string idxRamoAtiv { get; init; } = "Cliente";
    [Column("IDX_SEGMENTO")]
    public string idxSegmento { get; init; } = "Banco";
    [Column("IDX_PERFIL")]
    public string idxPerfil { get; init; } = "Perfil A";
    [Column("PORTE")]
    public string porte { get; init; } = "";
    [Column("FATURAMENTO")]
    public decimal faturamento { get; init; } = 0;
    [Column("IDX_VENDEDOR")]
    public string? idxVendedor { get; init; } = "";
    [Column("IDX_VENDEDOR2")]
    public string? idxVendedor2 { get; init; } = "";
    [Column("DTCOMEMORACAO")]
    public DateTime? dtComemoracao { get; init; } = null;
    [Column("IDX_REGIAO")]
    public string idxRegiao { get; init; } = "Região A";
    [Column("IDX_ROTA")]
    public string idxRota { get; init; } = "Rota A";
    [Column("DIAATENDIMENTO")]
    public int diaAtendimento { get; init; } = 0;
    [Column("SEQUENCIA")]
    public int sequencia { get; init; } = 0;
    [Column("DTLIMITE")]
    public DateTime? dtLimite { get; init; } = null;
    [Column("VALORLIMITE")]
    public decimal valorLimite { get; init; } = 0;
    [Column("SALDOLIMITE")]
    public decimal saldoLimite { get; init; } = 0;
    [Column("TPDESCONTO")]
    public string tpDesconto { get; init; } = "N";
    [Column("DESCONTO")]
    public decimal desconto { get; init; } = 0;
    [Column("TRANSPORTECLIENTE")]
    public string transporteCliente { get; init; } = "N";
    [Column("OBS")]
    public string? obs { get; init; } = null;
    [Column("REFERENCIA")]
    public string? referencia { get; init; } = null;
    [Column("DTCADASTRO")]
    public DateTime dtCadastro  { get; init; } = BrazilTime.Now();
    [Column("RETENCAOPIS")]
    public decimal retencaoPis { get; init; } = 0;
    [Column("RETENCAOISS")]
    public decimal retencaoIss { get; init; } = 0;
    [Column("STATUS")]
    public string status { get; init; } = "A";
    [Column("MOTIVO")]
    public string motivo { get; init; } = "";
    [Column("CFG")]
    public string cfg { get; init; } = "";
    [Column("RG")]
    public string rg { get; init; } = "";
    [Column("CODANTES")]
    public string codantes { get; init; } = "";
    [Column("IDX_BLOQUEIO")]
    public string idxBloqueio { get; init; } = "";
    [Column("DTBLOQUEIO")]
    public DateTime? dtBloqueio { get; init; } = null;
    [Column("DDD")]
    public string ddd { get; init; } = "";
    [Column("OBSCLIENTE")]
    public string? obsCliente { get; init; } = null;
    [Column("DDD1")]
    public string ddd1 { get; init; } = "";
    [Column("TELEFONE1")]
    public string telefone1 { get; init; } = "";
    [Column("TPTELEFONE1")]
    public string tpTelefone1 { get; init; } = "Celular";
    [Column("OBSTELEFONE1")]
    public string obsTelefone1 { get; init; } = "";
    [Column("DDD2")]
    public string ddd2 { get; init; } = "";
    [Column("TELEFONE2")] 
    public string telefone2 { get; init; } = "";
    [Column("TPTELEFONE2")]
    public string tpTelefone2 { get; init; } = "";
    [Column("OBSTELEFONE2")]
    public string obsTelefone2 { get; init; } = "";
    [Column("DDD3")]
    public string ddd3 { get; init; } = "";
    [Column("TELEFONE3")] 
    public string telefone3 { get; init; } = "";
    [Column("TPTELEFONE3")]
    public string tpTelefone3 { get; init; } = "";
    [Column("OBSTELEFONE3")]
    public string obsTelefone3 { get; init; } = "";
    [Column("DDD4")]
    public string ddd4 { get; init; } = "";
    [Column("TELEFONE4")] 
    public string telefone4{ get; init; } = "";
    [Column("TPTELEFONE4")]
    public string tpTelefone4 { get; init; } = "";
    [Column("OBSTELEFONE4")]
    public string obsTelefone4 { get; init; } = "";
    [Column("DDD5")]
    public string ddd5 { get; init; } = "";
    [Column("TELEFONE5")] 
    public string telefone5{ get; init; } = "";
    [Column("TPTELEFONE5")]
    public string tpTelefone5 { get; init; } = "";
    [Column("OBSTELEFONE5")]
    public string obsTelefone5 { get; init; } = "";
    [Column("DDD6")]
    public string ddd6 { get; init; } = "";
    [Column("TELEFONE6")] 
    public string telefone6{ get; init; } = "";
    [Column("TPTELEFONE6")]
    public string tpTelefone6 { get; init; } = "";
    [Column("OBSTELEFONE6")]
    public string obsTelefone6 { get; init; } = "";
    [Column("EMAIL")]
    public string email { get; init; } = "";
    [Column("EMAILVENDA1")]
    public string emailVenda1 { get; init; } = "";
    [Column("EMAILVENDA2")]
    public string emailVenda2 { get; init; } = "";
    [Column("SITE")]
    public string site { get; init; } = "";
    [Column("DTNASCIMENTO")]
    public DateTime? dtNascimento { get; init; } = BrazilTime.Now();
    [Column("EMAILCOMPRA")]
    public string emailCompra { get; init; } = "";
    [Column("SEXO")]
    public string sexo { get; init; }
    [Column("ESTADOCIVIL")]
    public string estadoCivil { get; init; }
    [Column("FILIACAOPAI")]
    public string filiacaoPai { get; init; } = "";
    [Column("FILIACAOMAE")]
    public string filiacaoMae { get; init; } = "";
    [Column("NATURALCIDADE")]
    public string naturalCidade { get; init; }
    [Column("NATURALUF")]
    public string naturalUf { get; init; }
    [Column("EMPRESA")]
    public string empresa { get; init; } = "";
    [Column("TRATAMENTO")]
    public string tratamento { get; init; } = "";
    [Column("PAIS")]
    public string pais { get; init; } = "";
    [Column("IDX_CCR")]
    public string idxCcr{ get; init; } = "";
    [Column("IDX_CAPTACAO")]
    public string idxCaptacao { get; init; } = "";
    [Column("BLOQUEIOCLIENTE")]
    public string bloqueioCliente { get; init; } = "N";
    [Column("BLOQUEIOFORNECEDOR")]
    public string bloqueioFornecedor{ get; init; } = "N";
    [Column("BLOQUEIOTRANSPORTADORA")]
    public string bloqueioTransportadora { get; init; } = "N";
    [Column("BLOQUEIOREPRESENTANTE")]
    public string bloqueioRepresentante { get; init; } = "N";
    [Column("ETAPACAPTACAO")]
    public string etapaCaptacao { get; init; } = "";
    [Column("ORIGEM")]
    public string origem { get; init; } = "";
    [Column("DTINC")]
    public DateTime dtInc { get; init; } = BrazilTime.Now();
    [Column("OPINC")]
    public int opInc { get; init; } 
    [Column("DTALT")]
    public DateTime dtAlt { get; init; } = BrazilTime.Now();
    [Column("OPALT")]
    public int opAlt { get; init; } 
    [Column("CONFERIRCADASTRO")]
    public string conferirCadastro { get; init; } = "N";
    [Column("NEGOCIACAO")]
    public int negociacao { get; init; } = 0;
    [Column("IDX_EMPRESA")]
    public string idxEmpresa { get; init; } = "";
    [Column("IDX_FILIAL")]
    public string idxFilial { get; init; } = "";
    [Column("TOTALCREDITO")]
    public decimal totalCredito { get; init; } = 0;
    [Column("TOTALDEBITO")]
    public decimal totalDebito { get; init; } = 0;
    [Column("VALORCREDITO")]
    public decimal valorCredito { get; init; } = 0;
    [Column("CONTROLALIMITE")]
    public string controlaLimite { get; init; } = "N";
    [Column("ACERTOLIMITE")]
    public decimal acertoLimite { get; init; } = 0; 
    [Column("LIMITEPORPERFIL")]
    public string limitePorPerfil { get; init; } = "S"; 
    [Column("PROFISSIONAL")]
    public string profissional { get; init; } = "N";
    [Column("CASAEVENTO")]
    public string casaEvento { get; init; } = "N";
    [Column("REGIMETRIBUTARIO")]
    public string regimeTributario { get; init; } = "O";
    [Column("MSGPV")]
    public string? msgPv { get; init; } = null;
    [Column("MSGOV")]
    public string? msgOv { get; init; } = null;
    [Column("MSGOS")]
    public string? msgOs { get; init; } = null;
    [Column("MSGVD")]
    public string? msgVd { get; init; } = null;
    [Column("MSGOC")]
    public string? msgOc { get; init; }= null; 
    [Column("MSGCP")]
    public string? msgCp { get; init; } = null;
    [Column("MSGPVAUTORIZAR")]
    public string? msgPvAutorizar { get; init; } = null;
    [Column("MSGATD")]
    public string? msgAtd { get; init; } = null;
    [Column("EXCLUSIVIDADEVENDEDOR")]
    public string exclusividadeVendedor { get; init; } = "N";
    [Column("IDX_RELACIONAMENTO")]
    public string idxRelacionamento { get; init; } = "";
    [Column("COMISSAOFORMATURA")]
    public string comissaoFormatura { get; init; } = ""; 
    [Column("CODBANCO")]
    public string codBanco { get; init; } = "";
    [Column("CODAGENCIA")]
    public string codAgencia { get; init; } = "";
    [Column("DVAGENCIA")]
    public string dvAgencia { get; init; } = "";
    [Column("CODTIPOCONTA")]
    public string codTipoConta { get; init; } = "";
    [Column("CODCONTA")]
    public string codConta { get; init; } = "";
    [Column("DVCONTA")]
    public string dvConta { get; init; } = "";
    [Column("CONTRIBUINTE")]
    public string contribuinte { get; init; } = "9";
    [Column("TABELAFIXA")]
    public string tabelaFixa { get; init; } = "N";
    [Column("IDX_TABELA")]
    public string idxTabela { get; init; } = "";
    [Column("MSGNFSE")]
    public string? msgNfSe{ get; init; } = null;
    [Column("MSGNFE")]
    public string? msgNfe{ get; init; } = null;
    [Column("CSLLALIQ")]
    public decimal csllAliq { get; init; } = 0;
    [Column("IRALIQ")]
    public decimal irAliq { get; init; } = 0;
    [Column("INSSALIQ")]
    public decimal inssAliq { get; init; }= 0; 
    [Column("PISALIQ")]
    public decimal pisAliq { get; init; } = 0;
    [Column("COFINSALIQ")]
    public decimal cofinsAliq { get; init; } = 0;
    [Column("CSLLCALC")]
    public string csllCalc { get; init; } = "I";
    [Column("IRCALC")]
    public string irCalc { get; init; } = "I";
    [Column("INSSCALC")]
    public string inssCalc { get; init; } = "I";
    [Column("PISCALC")]
    public string pisCalc { get; init; } = "I";
    [Column("COFINSCALC")]
    public string cofinsCalc { get; init; } = "I";
    [Column("ISSQNALIQ")]
    public decimal issqnAliq{ get; init; } = 0;
    [Column("IDX_PAIS")]
    public int idxPais { get; init; } = 1058;
    [Column("LJ_HASH")]
    public string? ljHash { get; init; } = null;
    [Column("LJ_IDX_CATEGORIA")]
    public int? ljIdxCategoria { get; init; } = null;
    [Column("CODIBGE")]
    public string codIbge { get; init; } = "3106200";
    [Column("LOGIN")]
    public string? login { get; init; } = null;
    [Column("SENHA")]
    public string? senha { get; init; } = null;
    [Column("INSCPRODUTORRURAL")]
    public string inscProdutorRural { get; init; } = "";
    [Column("PIX")]
    public string? pix { get; init; } = null;
    [Column("TEMANEXO")]
    public string? temAnexo { get; init; } = null;
    [Column("TITULARCONTA")]
    public string? titularConta { get; init; } = null;
    [Column("TITULARPIX")]
    public string? titularPix { get; init; } = null;
    
}