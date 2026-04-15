using Microsoft.EntityFrameworkCore;
using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;
using VotingSystem.Infrastructure.Persistence;

namespace VotingSystem.Infrastructure.Repositories;

public class VotoRepository : IVotoRepository
{
    private readonly VotingDbContext _context;

    public VotoRepository(VotingDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Voto voto)
    {
        _context.Votos.Add(voto);
        await _context.SaveChangesAsync();
    }

    public async Task<int> ContarPorCandidato(Guid candidatoId)
    {
        return await _context.Votos
            .CountAsync(v => v.CandidatoId == candidatoId);
    }
}