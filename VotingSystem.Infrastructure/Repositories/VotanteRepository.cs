using Microsoft.EntityFrameworkCore;
using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;
using VotingSystem.Infrastructure.Persistence;

namespace VotingSystem.Infrastructure.Repositories;

public class VotanteRepository : IVotanteRepository
{
    private readonly VotingDbContext _context;

    public VotanteRepository(VotingDbContext context)
    {
        _context = context;
    }

    public async Task<Votante?> GetByCodigoAsync(string codigo)
    {
        return await _context.Votantes.Where(v => v.Codigo == codigo).FirstOrDefaultAsync();
    }

    public async Task<List<Votante>> GetAllAsync()
    {
        return await _context.Votantes.ToListAsync();
    }

    public async Task AddAsync(Votante votante)
    {
        _context.Votantes.Add(votante);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Votante votante)
    {
        _context.Votantes.Update(votante);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Votante>> GetByGradoParaleloAsync(string grado, string paralelo)
    {
        return await _context.Votantes
            .Where(v => v.Grado == grado && v.Paralelo == paralelo)
                .OrderBy(v => v.Paterno)
                .ThenBy(v => v.Materno)
                .ThenBy(v => v.Nombre)
            .ToListAsync();
    }
    public async Task AddRangeAsync(List<Votante> votantes)
    {
        _context.Votantes.AddRange(votantes);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string codigo)
    {
        var votante = await _context.Votantes.Where(v => v.Codigo == codigo).FirstOrDefaultAsync();
        if (votante == null)
            return;
        votante.Activo = false;
        await _context.SaveChangesAsync();
    }

    public async Task<List<string>> GetCodigosByPrefixAsync(string prefix)
    {
        return await _context.Votantes
            .Where(v => v.Codigo.StartsWith(prefix))
            .Select(v => v.Codigo)
            .ToListAsync();
    }
}