namespace VotingSystem.Mvc.Models;

public class VotanteViewModel
{
    public string Codigo { get; set; } = "";
    public string Grado { get; set; } = "";
    public string Paralelo { get; set; } = "";
    public string Paterno { get; set; } = "";
    public string Materno { get; set; } = "";
    public string Nombre { get; set; } = "";

    public bool Habilitado { get; set; }
    public bool YaVoto { get; set; }
}