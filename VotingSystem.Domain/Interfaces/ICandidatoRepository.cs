using VotingSystem.Domain.Entities;

namespace VotingSystem.Domain.Interfaces;
public interface ICandidatoRepository
{
    Task<List<Candidato>> GetAllAsync();
    Task<Candidato?> GetByIdAsync(Guid id);
    Task AddAsync(Candidato candidato);
    Task UpdateAsync(Candidato candidato);
    Task DeleteAsync(Guid id);
}