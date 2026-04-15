namespace VotingSystem.Domain.Entities;
public class Candidato
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Grupo { get; set; } = string.Empty;
    public string FotoUrl { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
}