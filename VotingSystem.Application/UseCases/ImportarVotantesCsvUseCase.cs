using VotingSystem.Domain.Interfaces;

namespace VotingSystem.Application.UseCases;
public class ImportarVotantesCsvUseCase
{
    private readonly IVotanteRepository _repo;
    private readonly IFileVotanteReader _reader;

    public ImportarVotantesCsvUseCase(
        IVotanteRepository repo,
        IFileVotanteReader reader)
    {
        _repo = repo;
        _reader = reader;
    }

    public async Task<int> Execute(Stream stream)
    {
        var votantes = await _reader.Read(stream);

        await _repo.AddRangeAsync(votantes);

        return votantes.Count;
    }
}