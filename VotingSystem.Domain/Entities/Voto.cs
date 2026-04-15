namespace VotingSystem.Domain.Entities;

public class Voto
{
    public Guid Id { get; set; }
    public Guid CandidatoId { get; set; }
    public string VotanteCodigo { get; set; } = "";
    public DateTime Fecha { get; set; }
}