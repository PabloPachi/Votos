using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;

namespace VotingSystem.Application.UseCases;
public class ObtenerGradoParaleloUseCase
{
    private readonly IVotanteRepository _repo;

    public ObtenerGradoParaleloUseCase(IVotanteRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<string>> Execute()
    {
        return await _repo.GetGradoParaleloAsync();
    }
}