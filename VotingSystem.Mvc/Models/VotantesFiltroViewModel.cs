namespace VotingSystem.Mvc.Models;

public class VotantesFiltroViewModel
{
    public string? Curso { get; set; }
    public List<VotanteViewModel> Votantes { get; set; } = new();
    public List<string> Cursos { get; set; } = new();
}