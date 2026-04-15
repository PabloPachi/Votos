namespace VotingSystem.Application.DTOs;

public record VotarRequest(string VotanteCodigo, Guid CandidatoId);