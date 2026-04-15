using VotingSystem.Application.DTOs;
using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;

namespace VotingSystem.Application.UseCases;

public class EliminarVotanteUseCase
{
    private readonly IVotanteRepository _repo;

    public EliminarVotanteUseCase(IVotanteRepository repo)
    {
        _repo = repo;
    }

    public async Task Execute(string codigo)
    {
        var votante = await _repo.GetByCodigoAsync(codigo);
        if (votante == null)
            throw new Exception("Votante no encontrado");
        if (votante.YaVoto)
            throw new Exception("No se puede eliminar un votante que ya votó");
        await _repo.DeleteAsync(codigo);
    }
}