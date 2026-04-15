using VotingSystem.Application.DTOs;
using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;

namespace VotingSystem.Application.UseCases;
public class ListarCandidatosUseCase
{
    private readonly ICandidatoRepository _repo;

    public ListarCandidatosUseCase(ICandidatoRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Candidato>> Execute()
    {
        return await _repo.GetAllAsync();
    }
}