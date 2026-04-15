namespace VotingSystem.Application.DTOs;

public record ActualizarCandidatoRequest(
    string Nombre,
    string Grupo,
    string FotoUrl,
    bool Activo
);