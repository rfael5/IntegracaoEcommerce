using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class SafeDateTimeConverter : ValueConverter<DateTime?, DateTime?>
{
    public SafeDateTimeConverter() : base(
        v => v.HasValue && v.Value.Year < 1753 ? null : v,  // ao gravar: ano < 1753 vira NULL
        v => v) { }
}

public class SafeDateTimeNonNullableConverter : ValueConverter<DateTime, DateTime>
{
    public SafeDateTimeNonNullableConverter() : base(
        v => v.Year < 1753 ? new DateTime(1753, 1, 1) : v,  // ao gravar: usa 1753-01-01 como fallback
        v => v) { }
}

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // var connectionString=$"Server={Environment.GetEnvironmentVariable("DB_HOST_SERVER")};" +
        //                      $"User Id={Environment.GetEnvironmentVariable("DB_USER")};" +
        //                      $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD_SERVER")};" +  
        //                      $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
        //                      "TrustServerCertificate=True;";

        // var connectionString=$"Server=192.168.1.55;" +
        //                       $"User Id='Sa';" +
        //                       $"Password='P@ssw0rd2023';" +  
        //                       $"Database='SOUTTOMAYOR';" +
        //                       "TrustServerCertificate=True;";

        var connectionString=$"Server=192.168.1.45;" +
                              $"User Id='Sa';" +
                              $"Password='bcsm@122#';" + 
                              $"Database='SOUTTOMAYOR';" +
                              "TrustServerCertificate=True;";

        optionsBuilder.UseSqlServer(connectionString);  
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime?>().HaveConversion<SafeDateTimeConverter>();
        configurationBuilder.Properties<DateTime>().HaveConversion<SafeDateTimeConverter>();
    }

    public DbSet<TpaCadastroDTO> CadastroUsuarioTPA { get; set; }
    public DbSet<TpaDoctopedDTO> Doctoped { get; set; }
    public DbSet<TpaEventoOrcDTO> EventoOrc { get; set; }
    public DbSet<TpaMovtopedDTO> Movtoped { get; set; }
    public DbSet<TpaEventoOrcPedDTO> EventoOrcPed { get; set; }
    public DbSet<InformacoesProdutoTPA> Produto { get; set; }
    public DbSet<TpaEnderecoDTO> Enderecos { get; set; }
    public DbSet<TpaContatoDTO> Contatos { get; set; }
    public DbSet<TpaDoctoPedFpDTO> DoctopedFp { get; set; }
    public DbSet<VendedoresDTO> Vendedores { get; set; }
    public DbSet<ServicoProduto> ServicosProdutos { get; set; }
    public DbSet<TpaAjustePedDTO> AjustePed { get; set; }
    public DbSet<TpaAjustePedItemDTO> AjustePedItem { get; set; }
    public DbSet<TpaContratoDTO> Contratos { get; set; }
    public DbSet<TpaContratoMovDTO> ContratoMov { get; set; }
    public DbSet<TpaContratoItemDTO> ContratoItem { get; set; }
    public DbSet<TpaDoctopedHistoricoDTO> DoctopedHistorico { get; set; }
   public DbSet<ServicoMateriais> ServicosMateriais { get; set; }
   public DbSet<TpaContratoAdendoDTO> ContratosAdendos { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TpaCadastroDTO>().ToTable(tb => tb.UseSqlOutputClause(false));
        modelBuilder.Entity<TpaEnderecoDTO>().ToTable(tb => tb.UseSqlOutputClause(false));
        modelBuilder.Entity<TpaContatoDTO>().ToTable(tb => tb.UseSqlOutputClause(false));
        modelBuilder.Entity<TpaEventoOrcDTO>().ToTable(tb => tb.UseSqlOutputClause(false));
        modelBuilder.Entity<TpaEventoOrcPedDTO>().ToTable(tb => tb.UseSqlOutputClause(false));
    }

    
}