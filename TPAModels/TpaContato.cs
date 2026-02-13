using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPACONTATO", Schema = "dbo")]
public record TpaContatoDTO
{
    [Key]
    [Column("ID")]
    public int id { get; set; }

    [Column("PK_CONTATO")]
    public required string pkContato { get; init; }

    [Column("IDX_CADASTRO")]
    public required string idxCadastro { get; init; }

    [Column("NOME")]
    public string? nome { get; init; }

    [Column("EMAIL")]
    public string email { get; init; } = "";

    [Column("DDD")]
    public string ddd { get; init; } = "";

    [Column("TELEFONE1")]
    public string telefone1 { get; init; } = "";

    [Column("TELEFONE2")]
    public string telefone2 { get; init; } = "";

    [Column("CARGO")]
    public string cargo { get; init; } = "";

    [Column("SETOR")]
    public string setor { get; init; } = "";

    [Column("COMEMORACAO")]
    public string comemoracao { get; init; } = "";

    [Column("TRATAMENTO")]
    public string tratamento { get; init; } = "";

    [Column("TIPOCONTATO")]
    public string tipoContato { get; init; } = "";

    [Column("SEXO")]
    public string sexo { get; init; } = "";

    [Column("DTINC")]
    public DateTime dtInc { get; init; } = BrazilTime.Now();

    [Column("OPINC")]
    public int opInc { get; init; }

    [Column("DTALT")]
    public DateTime dtAlt { get; init; } = BrazilTime.Now();

    [Column("OPALT")]
    public int opAlt { get; init; }

    [Column("NFE")]
    public string nfe { get; init; } = "";

    [Column("MSN")]
    public string msn { get; init; } = "";

    [Column("SKYPE")]
    public string skype { get; init; } = "";

    [Column("TELEFONE3")]
    public string telefone3 { get; init; } = "";

    [Column("COBRANCA")]
    public string cobranca { get; init; } = "N";

    [Column("STATUS")]
    public string status { get; init; } = "A";

    [Column("MOTIVOSTATUS")]
    public string? motivoStatus { get; init; }

    [Column("CPF")]
    public string cpf { get; init; } = "";

    [Column("DTEMAILENVIADO")]
    public DateTime? dtEmailEnviado { get; init; }

    [Column("CAMPANHA")]
    public string campanha { get; init; } = "";

    [Column("NOTIFICACAOCHAMADO")]
    public string? notificacaoChamado { get; init; }

    [Column("IDX_ENDERECO")]
    public string? idxEndereco { get; init; } = "";

    [Column("TEL1")]
    public string? tel1 { get; init; }

    [Column("TEL2")]
    public string? tel2 { get; init; }

    [Column("TEL3")]
    public string? tel3 { get; init; }

    [Column("CHAMADOSUPORTE")]
    public string? chamadoSuporte { get; init; } = "S";

    [Column("CHAMADOSERVICO")]
    public string? chamadoServico { get; init; } = "S";

    [Column("CHAMADOFINANCEIRO")]
    public string? chamadoFinanceiro { get; init; } = "N";

    [Column("CHAMADOBLOQUEADO")]
    public string? chamadoBloqueado { get; init; } = "N";

    [Column("CHAMADOAUTORIZACAO")]
    public string? chamadoAutorizacao { get; init; }

    [Column("DDDPAIS")]
    public string? dddPais { get; init; } = "55";

    [Column("SENHA")]
    public string? senha { get; init; }

    [Column("LOGIN")]
    public string? login { get; init; }
}
