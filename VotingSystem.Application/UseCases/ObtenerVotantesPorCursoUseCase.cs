using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;

namespace VotingSystem.Application.UseCases;
public class ObtenerVotantesPorCursoUseCase
{
    private readonly IVotanteRepository _repo;

    public ObtenerVotantesPorCursoUseCase(IVotanteRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Votante>> Execute(string grado, string paralelo)
    {
        return await _repo.GetByGradoParaleloAsync(grado, paralelo);
    }
}