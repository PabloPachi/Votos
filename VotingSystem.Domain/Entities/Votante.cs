namespace VotingSystem.Domain.Entities;
public class Votante
{
    public Guid Id { get; set; }

    public string Codigo { get; set; } = string.Empty;   // único
    public string Grado { get; set; } = string.Empty;
    public string Paralelo { get; set; } = string.Empty;
    public string Paterno { get; set; } = string.Empty;
    public string Materno { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public bool Habilitado { get; set; } = false;
    public bool YaVoto { get; set; } = false;
    public bool Activo { get; set; } = true;
}