using VotingSystem.Application.DTOs;
using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;

namespace VotingSystem.Application.UseCases;
public class EliminarCandidatoUseCase
{
    private readonly ICandidatoRepository _repo;

    public EliminarCandidatoUseCase(ICandidatoRepository repo)
    {
        _repo = repo;
    }

    public async Task Execute(Guid id)
    {
        var candidato = await _repo.GetByIdAsync(id);

        if (candidato == null)
            throw new Exception("Candidato no encontrado");

        candidato.Activo = false;

        await _repo.UpdateAsync(candidato);
    }
}