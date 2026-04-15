using VotingSystem.Domain.Interfaces;

namespace VotingSystem.Application.UseCases;
public class ObtenerResultadosUseCase
{
    private readonly ICandidatoRepository _candidatoRepo;
    private readonly IVotoRepository _votoRepo;

    public ObtenerResultadosUseCase(ICandidatoRepository candidatoRepo, IVotoRepository votoRepo)
    {
        _candidatoRepo = candidatoRepo;
        _votoRepo = votoRepo;
    }

    public async Task<List<object>> Execute()
    {
        var candidatos = await _candidatoRepo.GetAllAsync();

        var resultado = new List<object>();

        foreach (var c in candidatos)
        {
            var votos = await _votoRepo.ContarPorCandidato(c.Id);

            resultado.Add(new
            {
                c.Nombre,
                c.Grupo,
                c.FotoUrl,
                Votos = votos
            });
        }

        return resultado;
    }
}