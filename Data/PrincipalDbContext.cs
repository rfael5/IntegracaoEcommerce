using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class PrincipalDbContext : DbContext
{
    public PrincipalDbContext(DbContextOptions<PrincipalDbContext> options) : base(options) {}

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProdutoServico>().HasNoKey();

        base.OnModelCreating(modelBuilder);
    }

}