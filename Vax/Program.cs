using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vax.Data;
using Vax.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.AddControllersWithViews();

// Adiciona a configuração do banco de dados.
builder.Services.AddDbContext<DataContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DataContext")));

// Adiciona o serviço de inicialização de dados.
builder.Services.AddTransient<DataInitializationService>();

var app = builder.Build();

// Configura o pipeline de solicitações HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor padrão HSTS é de 30 dias. Você pode querer alterar isso para cenários de produção, veja https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Obtenha o serviço de inicialização de dados e execute a inicialização.
var dataInitializationService = app.Services.GetRequiredService<DataInitializationService>();
dataInitializationService.InitializeData();

app.Run();
