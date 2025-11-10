using hombresYMujeresFacturan.Components;
using hombresYMujeresFacturan.Components.Data;
using hombresYMujeresFacturan.Components.Servicios;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<ServicioControlador>();
builder.Services.AddSingleton<ServicioFactura>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

string ruta = "mibase.db";

using var conexion = new SqliteConnection($"DataSource={ruta}");
conexion.Open();
var comando = conexion.CreateCommand();
comando.CommandText = @"
create table if not exists
facturas ( identificador integer, fecha text, nombreCliente text, articulos text, total integer )
";
comando.ExecuteNonQuery();

app.Run();
