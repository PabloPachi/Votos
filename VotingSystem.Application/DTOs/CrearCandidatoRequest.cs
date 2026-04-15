namespace VotingSystem.Application.DTOs;

public record CrearCandidatoRequest(
    string Nombre,
    string Grupo,
    string FotoUrl
);