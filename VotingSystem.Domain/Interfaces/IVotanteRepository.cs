using VotingSystem.Domain.Entities;

namespace VotingSystem.Domain.Interfaces;
public interface IVotanteRepository
{
    Task<Votante?> GetByCodigoAsync(string codigo);
    Task<List<Votante>> GetAllAsync();
    Task AddAsync(Votante votante);
    Task UpdateAsync(Votante votante);
    Task DeleteAsync(string codigo);
    Task<List<Votante>> GetByGradoParaleloAsync(string grado, string paralelo);
    Task AddRangeAsync(List<Votante> votantes);
    Task<List<string>> GetCodigosByPrefixAsync(string prefix);
    Task<List<string>> GetGradoParaleloAsync();
    Task MakeAllAsync(bool habilitado);
}