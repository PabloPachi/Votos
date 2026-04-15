namespace VotingSystem.Application.DTOs;

public record CrearVotanteRequest(
    string Grado,
    string Paralelo,
    string Paterno,
    string Materno,
    string Nombre
);