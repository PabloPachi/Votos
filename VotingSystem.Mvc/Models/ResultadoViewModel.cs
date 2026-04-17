namespace VotingSystem.Mvc.Models;

public class ResultadoViewModel
{
    public string Nombre { get; set; } = "";
    public string Grupo { get; set; } = "";
    public string FotoUrl { get; set; } = "";
    public int Votos { get; set; }
    public double Porcentaje { get; set; }
}