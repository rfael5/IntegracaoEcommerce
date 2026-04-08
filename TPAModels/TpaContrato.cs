using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TPACONTRATO", Schema = "dbo")]
public record TpaContratoDTO
{
    [Key]
    [Column("PK_CONTRATO")]
    public int pkContrato { get; init; }

    [Column("RDX_EMPRESA")]
    public string? rdxEmpresa { get; init; } = "           1";

    [Column("NUMERO")]
    public int? numero { get; init; } = 0;

    [Column("DOCUMENTO")]
    public string? documento { get; init; }

    [Column("DTINICIO")]
    public DateTime? dtInicio { get; init; } = BrazilTime.Now();

    [Column("TPENTIDADE")]
    public string? tpEntidade { get; init; } = "C";

    [Column("IDX_ENTIDADE")]
    public string? idxEntidade { get; init; }

    [Column("TEXTO")]
    public string? texto { get; init; }

    [Column("DTINC")]
    public DateTime? dtInc { get; init; } = BrazilTime.Now();

    [Column("OPINC")]
    public int? opInc { get; init; }

    [Column("DTALT")]
    public DateTime? dtAlt { get; init; } = BrazilTime.Now();

    [Column("OPALT")]
    public int? opAlt { get; init; }

    [Column("IDX_GRUPOCONTRATO")]
    public string? idxGrupoContrato { get; init; } = " ";

    [Column("IDX_OBJETOCONTRATO")]
    public string? idxObjetoContrato { get; init; } = " ";

    [Column("PARAM")]
    public string? param { get; init; }

    [Column("DESCONTO")]
    public decimal? desconto { get; init; } = 0;

    [Column("IDX_DOCTOEST")]
    public int? idxDoctoEst { get; init; }

    [Column("TPCONTRATO")]
    public string? tpContrato { get; init; } = "CM";

    [Column("PRINCIPAL")]
    public string? principal { get; init; } = "S";

    [Column("ORIGEM")]
    public string? origem { get; init; } = "BF";

    [Column("RGSV")]
    public string? rgsv { get; init; } = "N";

    [Column("RGCL")]
    public string? rgcl { get; init; } = "N";

    [Column("IDX_INDEXADOR")]
    public int? idxIndexador { get; init; }

    [Column("DECIMATERCEIRAPARCELA")]
    public string? decimaTerceiraParcela { get; init; }

    [Column("ANOSPREVISAO")]
    public int? anosPrevisao { get; init; }

    [Column("TEMANEXO")]
    public string? temAnexo { get; init; }

    [Column("DATAATIVACAO")]
    public DateTime? dataAtivacao { get; init; } = BrazilTime.Now();

    [Column("OPATIVACAO")]
    public int? opAtivacao { get; init; }
}