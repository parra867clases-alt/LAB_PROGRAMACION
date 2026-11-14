using Microsoft.EntityFrameworkCore;
using SistemaEscolarApi.Data;
using SistemaEscolarApi.Services;

var builder = WebApplication.CreateBuilder(args);

string cadena = builder.Configuration.GetConnectionString("Escuela")
    ?? throw new InvalidOperationException("Falta la cadena de conexión 'Escuela'.");

builder.Services.AddDbContext<EscuelaContext>(opciones =>
    opciones.UseMySql(cadena, new MySqlServerVersion(new Version(8, 0, 36))));

builder.Services.AddScoped<ReporteDapper>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();