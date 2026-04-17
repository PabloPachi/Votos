using VotingSystem.Application.DTOs;
using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;

namespace VotingSystem.Application.UseCases;

public class ActualizarVotanteUseCase
{
    private readonly IVotanteRepository _repo;

    public ActualizarVotanteUseCase(IVotanteRepository repo)
    {
        _repo = repo;
    }

    public async Task Execute(string codigo, ActualizarVotanteRequest request)
    {
        var votante = await _repo.GetByCodigoAsync(codigo.ToUpper());

        if (votante == null)
            throw new Exception("Votante no encontrado");

        // 🔒 No permitir modificar si ya votó (recomendado)
        if (votante.YaVoto)
            throw new Exception("No se puede modificar un votante que ya votó");

        votante.Grado = request.Grado;
        votante.Paralelo = request.Paralelo;
        votante.Paterno = request.Paterno;
        votante.Materno = request.Materno;
        votante.Nombre = request.Nombre;
        votante.Habilitado = request.Habilitado;

        await _repo.UpdateAsync(votante);
    }
}