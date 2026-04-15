using VotingSystem.Application.DTOs;
using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;

namespace VotingSystem.Application.UseCases;
public class ObtenerCandidatoUseCase
{
    private readonly ICandidatoRepository _repo;

    public ObtenerCandidatoUseCase(ICandidatoRepository repo)
    {
        _repo = repo;
    }

    public async Task<Candidato?> Execute(Guid id)
    {
        return await _repo.GetByIdAsync(id);
    }
}