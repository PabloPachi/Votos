namespace VotingSystem.Mvc.Models;

public class CandidatoViewModel
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Grupo { get; set; } = "";
    public string FotoUrl { get; set; } = "";
}