using VotingSystem.Application.DTOs;
using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;

namespace VotingSystem.Application.UseCases;
public class CrearCandidatoUseCase
{
    private readonly ICandidatoRepository _repo;

    public CrearCandidatoUseCase(ICandidatoRepository repo)
    {
        _repo = repo;
    }

    public async Task Execute(CrearCandidatoRequest request)
    {
        var candidato = new Candidato
        {
            Id = Guid.NewGuid(),
            Nombre = request.Nombre,
            Grupo = request.Grupo,
            FotoUrl = request.FotoUrl,
            Activo = true
        };

        await _repo.AddAsync(candidato);
    }
}