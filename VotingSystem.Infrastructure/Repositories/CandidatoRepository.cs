using Microsoft.EntityFrameworkCore;
using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;
using VotingSystem.Infrastructure.Persistence;

namespace VotingSystem.Infrastructure.Repositories;

public class CandidatoRepository : ICandidatoRepository
{
    private readonly VotingDbContext _context;

    public CandidatoRepository(VotingDbContext context)
    {
        _context = context;
    }

    public async Task<List<Candidato>> GetAllAsync()
    {
        return await _context.Candidatos
            .Where(c => c.Activo)
            .ToListAsync();
    }

    public async Task<Candidato?> GetByIdAsync(Guid id)
    {
        return await _context.Candidatos.FindAsync(id);
    }

    public async Task AddAsync(Candidato candidato)
    {
        _context.Candidatos.Add(candidato);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Candidato candidato)
    {
        _context.Candidatos.Update(candidato);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var candidato = await _context.Candidatos.FindAsync(id);

        if (candidato == null)
            return;

        // 🔥 Soft delete (recomendado)
        candidato.Activo = false;

        await _context.SaveChangesAsync();
    }
}