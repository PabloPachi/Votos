using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;

namespace VotingSystem.Application.UseCases;
public class ObtenerVotanteUseCase
{
    private readonly IVotanteRepository _repo;

    public ObtenerVotanteUseCase(IVotanteRepository repo)
    {
        _repo = repo;
    }

    public async Task<Votante?> Execute(string codigo)
    {
        return await _repo.GetByCodigoAsync(codigo.ToUpper());
    }
}