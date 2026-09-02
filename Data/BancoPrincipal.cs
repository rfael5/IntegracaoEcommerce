using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class BancoPrincipal : DbContext
{
    public BancoPrincipal(DbContextOptions<BancoPrincipal> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
         var connectionString=$"Server={Environment.GetEnvironmentVariable("DB_HOST_SERVER")};" +
                             $"User Id={Environment.GetEnvironmentVariable("DB_USER")};" +
                             $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD_SERVER")};" +  
                             $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                             "TrustServerCertificate=True;";

        optionsBuilder.UseSqlServer(connectionString); 
    }

    public DbSet<ProdutoEvento> ProdutosEvento{ get; set; }
    public DbSet<ProdutoPreco> ProdutosPreco { get; set; }
    public DbSet<ItemServico> ItensServico { get; set; }
    public DbSet<ProdutoServico> ProdutosServico { get; set; }
    public DbSet<TabelaPreco> TabelasPreco { get; set; }
    public DbSet<CategoriaEvento> CategoriasEvento { get; set; }
    public DbSet<EventoTp> EventoTps { get; set; }
    public DbSet<EventoTpSv> EventoTpSv { get; set; }
    public DbSet<TpaDoctopedDTO> Doctoped { get; set; }
    public DbSet<TpaMovtopedDTO> Movtoped { get; set; }
    public DbSet<TpaDoctoPedFpDTO> DoctopedFp { get; set; }
    public DbSet<TpaCadastroDTO> CadastroUsuarioTPA { get; set; }
    public DbSet<TpaEnderecoDTO> Enderecos { get; set; }
    public DbSet<TpaContatoDTO> Contatos { get; set; }
    public DbSet<InformacoesProdutoTPA> Produto { get; set; }
    public DbSet <InfoFaturamento> Faturamento { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProdutoServico>().HasNoKey();
        modelBuilder.Entity<RelacaoServicoProduto>().HasNoKey();
        modelBuilder.Entity<TpaCadastroDTO>().ToTable(tb => tb.UseSqlOutputClause(false));
        modelBuilder.Entity<TpaEnderecoDTO>().ToTable(tb => tb.UseSqlOutputClause(false));
        modelBuilder.Entity<TpaContatoDTO>().ToTable(tb => tb.UseSqlOutputClause(false));
        modelBuilder.Entity<TpaEventoOrcDTO>().ToTable(tb => tb.UseSqlOutputClause(false));
        modelBuilder.Entity<TpaEventoOrcPedDTO>().ToTable(tb => tb.UseSqlOutputClause(false));

        base.OnModelCreating(modelBuilder);
    }

}