using VotingSystem.Application.DTOs;
using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;

namespace VotingSystem.Application.UseCases;

public class CrearVotanteUseCase
{
    private readonly IVotanteRepository _repo;

    public CrearVotanteUseCase(IVotanteRepository repo)
    {
        _repo = repo;
    }

    public async Task Execute(CrearVotanteRequest request)
    {
        string paterno = string.IsNullOrEmpty(request.Paterno) ? "_" : request.Paterno;
        string materno = string.IsNullOrEmpty(request.Materno) ? "_" : request.Materno;
        string nombre = string.IsNullOrEmpty(request.Nombre) ? "_" : request.Nombre;
        string baseCodigo = $"{request.Grado}{request.Paralelo}" +
                        $"{paterno[0]}{materno[0]}{nombre[0]}"
                        .ToUpper();

        var codigosExistentes = await _repo.GetCodigosByPrefixAsync(baseCodigo);

        string codigoFinal = baseCodigo;

        if (codigosExistentes.Any())
        {
            var numeros = codigosExistentes
                .Select(c => c.Replace(baseCodigo, ""))
                .Where(s => int.TryParse(s, out _))
                .Select(int.Parse)
                .ToList();

            int siguiente = numeros.Any() ? numeros.Max() + 1 : 1;

            codigoFinal = $"{baseCodigo}{siguiente}";
        }

        var votante = new Votante
        {
            Id = Guid.NewGuid(),
            Codigo = codigoFinal,
            Grado = request.Grado,
            Paralelo = request.Paralelo,
            Paterno = paterno,
            Materno = materno,
            Nombre = nombre,
            Habilitado = false,
            YaVoto = false,
            Activo = true
        };

        await _repo.AddAsync(votante);
    }
}