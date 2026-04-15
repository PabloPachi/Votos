using VotingSystem.Application.DTOs;
using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;

namespace VotingSystem.Application.UseCases;
public class ActualizarCandidatoUseCase
{
    private readonly ICandidatoRepository _repo;

    public ActualizarCandidatoUseCase(ICandidatoRepository repo)
    {
        _repo = repo;
    }

    public async Task Execute(Guid id, ActualizarCandidatoRequest request)
    {
        var candidato = await _repo.GetByIdAsync(id);

        if (candidato == null)
            throw new Exception("Candidato no encontrado");

        candidato.Nombre = request.Nombre;
        candidato.Grupo = request.Grupo;
        candidato.FotoUrl = request.FotoUrl;
        candidato.Activo = request.Activo;

        await _repo.UpdateAsync(candidato);
    }
}