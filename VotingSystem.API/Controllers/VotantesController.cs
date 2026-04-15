using Microsoft.AspNetCore.Mvc;
using VotingSystem.Application.DTOs;
using VotingSystem.Application.UseCases;
using VotingSystem.Domain.Interfaces;

namespace VotingSystem.API.Controllers;

[ApiController]
[Route("api/votantes")]
public class VotantesController : ControllerBase
{
    private readonly IVotanteRepository _repo;
    private readonly ObtenerVotantesPorCursoUseCase _getVotantes;
    private readonly ImportarVotantesCsvUseCase _importVotantes;
    private readonly CrearVotanteUseCase _crearUseCase;
    private readonly ActualizarVotanteUseCase _actualizarUseCase;
    private readonly EliminarVotanteUseCase _eliminarUseCase;

    public VotantesController(IVotanteRepository repo,
    ObtenerVotantesPorCursoUseCase getVotantes,
    ImportarVotantesCsvUseCase importVotantes,
    CrearVotanteUseCase crearUseCase,
    ActualizarVotanteUseCase actualizarUseCase,
    EliminarVotanteUseCase eliminarUseCase)
    {
        _crearUseCase = crearUseCase;
        _actualizarUseCase = actualizarUseCase;
        _repo = repo;
        _getVotantes = getVotantes;
        _importVotantes = importVotantes;
        _eliminarUseCase = eliminarUseCase;
    }

    [HttpGet("curso")]
    public async Task<IActionResult> GetPorCurso(
            [FromQuery] string grado,
            [FromQuery] string paralelo)
    {
        var result = await _getVotantes.Execute(grado, paralelo);
        return Ok(result);
    }
    [HttpPost("upload-csv")]
    public async Task<IActionResult> UploadCsv(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Archivo inválido");

        using var stream = file.OpenReadStream();

        var total = await _importVotantes.Execute(stream);

        return Ok(new { total });
    }
    [HttpPost]
    public async Task<IActionResult> Crear(CrearVotanteRequest request)
    {
        await _crearUseCase.Execute(request);
        return Ok(new { message = "Votante creado" });
    }

    [HttpPut("{codigo}")]
    public async Task<IActionResult> Actualizar(
        string codigo,
        ActualizarVotanteRequest request)
    {
        await _actualizarUseCase.Execute(request);
        return Ok(new { message = "Votante actualizado" });
    }
    [HttpDelete("{codigo}")]
    public async Task<IActionResult> Eliminar(string codigo)
    {
        await _eliminarUseCase.Execute(codigo);
        return Ok(new { message = "Votante eliminado" });
    }
    [HttpPut("habilitar/{codigo}")]
    public async Task<IActionResult> Habilitar(string codigo)
    {
        var votante = await _repo.GetByCodigoAsync(codigo);

        if (votante == null) return NotFound();

        votante.Habilitado = true;

        await _repo.UpdateAsync(votante);

        return Ok(new { message = "Votante habilitado" });
    }
}