using VotingSystem.Application.DTOs;
using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;

namespace VotingSystem.Application.UseCases;
public class VotarUseCase
{
    private readonly IVotanteRepository _votanteRepo;
    private readonly IVotoRepository _votoRepo;

    public VotarUseCase(IVotanteRepository votanteRepo, IVotoRepository votoRepo)
    {
        _votanteRepo = votanteRepo;
        _votoRepo = votoRepo;
    }

    public async Task Execute(VotarRequest request)
    {
        var votante = await _votanteRepo.GetByCodigoAsync(request.VotanteCodigo);

        if (votante == null)
            throw new Exception("Votante no existe");

        if (!votante.Habilitado)
            throw new Exception("Votante no habilitado");

        if (votante.YaVoto)
            throw new Exception("Ya votó");

        votante.YaVoto = true;

        var voto = new Voto
        {
            Id = Guid.NewGuid(),
            VotanteCodigo = request.VotanteCodigo,
            CandidatoId = request.CandidatoId,
            Fecha = DateTime.UtcNow
        };

        await _votoRepo.AddAsync(voto);
        await _votanteRepo.UpdateAsync(votante);
    }
}