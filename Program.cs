using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddCors(options =>
{
    options.AddPolicy("All",
        policy =>
        {
            policy.AllowAnyOrigin()
		  .AllowAnyMethod()
		  .AllowAnyHeader();
        });
});

builder.Services.AddScoped<AcessoEcommerce>();
builder.Services.AddScoped<IntegracaoTray>();
builder.Services.AddScoped<AcessoTPA>();
builder.Services.AddScoped<UsuariosTPA>();
builder.Services.AddScoped<CadastroPedido>();
builder.Services.AddScoped<ProdutosTPA>();
builder.Services.AddScoped<Ajustes>();
builder.Services.AddScoped<GeracaoContrato>();
builder.Services.AddControllers();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddDbContext<PrincipalDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseForwardedHeaders();
app.UseRouting();
app.UseCors("All");
app.Run();
