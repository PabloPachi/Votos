using VotingSystem.Domain.Entities;

namespace VotingSystem.Domain.Interfaces;
public interface IVotoRepository
{
    Task AddAsync(Voto voto);
    Task<int> ContarPorCandidato(Guid candidatoId);
}