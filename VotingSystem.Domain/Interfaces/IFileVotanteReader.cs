using VotingSystem.Domain.Entities;

namespace VotingSystem.Domain.Interfaces;
public interface IFileVotanteReader
{
    Task<List<Votante>> Read(Stream stream);
}