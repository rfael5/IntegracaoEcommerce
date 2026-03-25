using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class PrincipalDbContext : DbContext
{
    public PrincipalDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
         var connectionString=$"Server={Environment.GetEnvironmentVariable("DB_HOST_SERVER")};" +
                             $"User Id={Environment.GetEnvironmentVariable("DB_USER")};" +
                             $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD_SERVER")};" +  
                             $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                             "TrustServerCertificate=True;";

        optionsBuilder.UseSqlServer(connectionString); 
    }

    public DbSet<ProdutoEvento> ProdutosEvento { get; set; }
    public DbSet<TipoServico> Servicos { get; set; }

}