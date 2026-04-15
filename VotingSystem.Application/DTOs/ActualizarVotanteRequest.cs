namespace VotingSystem.Application.DTOs;

public record ActualizarVotanteRequest(
    string Codigo,
    string Grado,
    string Paralelo,
    string Paterno,
    string Materno,
    string Nombre,
    bool Habilitado
);