using Microsoft.EntityFrameworkCore;
using DotNetEnv;

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
builder.Services.AddScoped<AcessoTPA>();
builder.Services.AddScoped<UsuariosTPA>();
builder.Services.AddScoped<CadastroPedido>();
builder.Services.AddScoped<GetIp>();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseForwardedHeaders();
app.UseCors("All");
app.Run();
