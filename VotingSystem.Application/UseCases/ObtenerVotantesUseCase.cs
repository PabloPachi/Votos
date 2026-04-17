using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;

namespace VotingSystem.Application.UseCases;
public class ObtenerVotantesUseCase
{
    private readonly IVotanteRepository _repo;

    public ObtenerVotantesUseCase(IVotanteRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Votante>> Execute()
    {
        return await _repo.GetAllAsync();
    }
}