using Microsoft.EntityFrameworkCore;
using VotingSystem.Infrastructure.Persistence;
using VotingSystem.Domain.Interfaces;
using VotingSystem.Infrastructure.Repositories;
using VotingSystem.Application.UseCases;
using VotingSystem.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<VotingDbContext>(options =>
    options.UseSqlite("Data Source=voting.db"));
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repos
builder.Services.AddScoped<IVotanteRepository, VotanteRepository>();
builder.Services.AddScoped<ICandidatoRepository, CandidatoRepository>();
builder.Services.AddScoped<IVotoRepository, VotoRepository>();
builder.Services.AddScoped<IFileVotanteReader, CsvVotanteReader>();

// UseCases
builder.Services.AddScoped<VotarUseCase>();
builder.Services.AddScoped<ObtenerResultadosUseCase>();
builder.Services.AddScoped<ObtenerVotantesPorCursoUseCase>();
builder.Services.AddScoped<ImportarVotantesCsvUseCase>();
builder.Services.AddScoped<CrearVotanteUseCase>();
builder.Services.AddScoped<ActualizarVotanteUseCase>();
builder.Services.AddScoped<EliminarVotanteUseCase>();
builder.Services.AddScoped<CrearCandidatoUseCase>();
builder.Services.AddScoped<ActualizarCandidatoUseCase>();
builder.Services.AddScoped<EliminarCandidatoUseCase>();
builder.Services.AddScoped<ListarCandidatosUseCase>();
builder.Services.AddScoped<ObtenerCandidatoUseCase>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazor");

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VotingDbContext>();
    db.Database.EnsureCreated();
}
app.Run();