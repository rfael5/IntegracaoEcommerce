using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPAENDERECO", Schema = "dbo")]
public record TpaEnderecoDTO
{
    [Key]
    [Column("ID")]
    public int id { get; set; }

    [Column("PK_ENDERECO")]
    public required string pkEndereco { get; init; }

    [Column("ENDTP")]
    public string endTp { get; init; } = "";

    [Column("ENDERECO")]
    public required string endereco { get; init; }

    [Column("ENDNUM")]
    public required string endNum { get; init; }

    [Column("ENDCOMP")]
    public string endComp { get; init; } = "";

    [Column("BAIRRO")]
    public required string bairro { get; init; }

    [Column("CIDADE")]
    public required string cidade { get; init; }

    [Column("UF")]
    public required string uf { get; init; }

    [Column("CEP")]
    public required string cep { get; init; }

    [Column("PAIS")]
    public string pais { get; init; } = "BRA";

    [Column("ENDGRUPO")]
    public string endGrupo { get; init; } = "";

    [Column("ENDREFERENCIA")]
    public string endReferencia { get; init; } = "";

    [Column("ENDPRINCIPAL")]
    public string endPrincipal { get; init; } = "S";

    [Column("ENDCOBRANCA")]
    public string endCobranca { get; init; } = "S";

    [Column("DTINC")]
    public DateTime dtInc { get; init; } = BrazilTime.Now();

    [Column("OPINC")]
    public int opInc { get; init; }

    [Column("DTALT")]
    public DateTime dtAlt { get; init; } = BrazilTime.Now();

    [Column("OPALT")]
    public int opAlt { get; init; }

    [Column("IDX_TABELA")]
    public required string idxTabela { get; init; }

    [Column("TIPOTABELA")]
    public string tipoTabela { get; init; } = "C";

    [Column("DESCRICAO")]
    public string descricao { get; init; } = "";

    [Column("STATUS")]
    public string status { get; init; } = "A";

    [Column("MOTIVOSTATUS")]
    public string? motivoStatus { get; init; } = null;

    [Column("IDX_TABFRETE")]
    public string idxTabFrete { get; init; } = "";

    [Column("IDX_ROTA")]
    public string idxRota { get; init; } = "";

    [Column("IDX_PAIS")]
    public int? idxPais { get; init; } = 1058;

    [Column("CODIBGE")]
    public string? codIbge { get; init; } = null;
}
