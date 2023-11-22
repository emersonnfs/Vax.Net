using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vax.Data;
using Vax.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao cont�iner.
builder.Services.AddControllersWithViews();

// Adiciona a configura��o do banco de dados.
builder.Services.AddDbContext<DataContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DataContext")));

// Adiciona o servi�o de inicializa��o de dados.
builder.Services.AddTransient<DataInitializationService>();

var app = builder.Build();

// Configura o pipeline de solicita��es HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor padr�o HSTS � de 30 dias. Voc� pode querer alterar isso para cen�rios de produ��o, veja https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Obtenha o servi�o de inicializa��o de dados e execute a inicializa��o.
var dataInitializationService = app.Services.GetRequiredService<DataInitializationService>();
dataInitializationService.InitializeData();

app.Run();
