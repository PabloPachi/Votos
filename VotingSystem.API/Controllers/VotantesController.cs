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
    ObtenerGradoParaleloUseCase _getGradoParalelo;
    private readonly ObtenerVotantesPorCursoUseCase _getVotantes;
    private readonly ObtenerVotantesUseCase _getAllVotantes;
    private readonly ObtenerVotanteUseCase _getVotante;
    private readonly ImportarVotantesCsvUseCase _importVotantes;
    private readonly CrearVotanteUseCase _crearUseCase;
    private readonly ActualizarVotanteUseCase _actualizarUseCase;
    private readonly EliminarVotanteUseCase _eliminarUseCase;

    public VotantesController(IVotanteRepository repo,
    ObtenerGradoParaleloUseCase getGradoParalelo,
    ObtenerVotantesPorCursoUseCase getVotantes,
    ObtenerVotantesUseCase getAllVotantes,
    ObtenerVotanteUseCase getVotante,
    ImportarVotantesCsvUseCase importVotantes,
    CrearVotanteUseCase crearUseCase,
    ActualizarVotanteUseCase actualizarUseCase,
    EliminarVotanteUseCase eliminarUseCase)
    {
        _crearUseCase = crearUseCase;
        _actualizarUseCase = actualizarUseCase;
        _repo = repo;
        _getVotantes = getVotantes;
        _getAllVotantes = getAllVotantes;
        _getVotante = getVotante;
        _importVotantes = importVotantes;
        _eliminarUseCase = eliminarUseCase;
        _getGradoParalelo = getGradoParalelo;
    }

    [HttpGet("GradoParalelo")]
    public async Task<IActionResult> GetGradoParalelo()
    {
        var result = await _getGradoParalelo.Execute();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAllVotantes.Execute();
        return Ok(result);
    }

    [HttpGet("curso")]
    public async Task<IActionResult> GetPorCurso(
            [FromQuery] string grado,
            [FromQuery] string paralelo)
    {
        var result = await _getVotantes.Execute(grado, paralelo);
        return Ok(result);
    }

    [HttpGet("{codigo}")]
    public async Task<IActionResult> Get(string codigo)
    {
        var result = await _getVotante.Execute(codigo.ToUpper());
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
        await _actualizarUseCase.Execute(codigo.ToUpper(), request);
        return Ok(new { message = "Votante actualizado" });
    }
    [HttpDelete("{codigo}")]
    public async Task<IActionResult> Eliminar(string codigo)
    {
        await _eliminarUseCase.Execute(codigo.ToUpper());
        return Ok(new { message = "Votante eliminado" });
    }
    [HttpPut("habilitar/{codigo}")]
    public async Task<IActionResult> Habilitar(string codigo)
    {
        var votante = await _repo.GetByCodigoAsync(codigo.ToUpper());

        if (votante == null) return NotFound();

        votante.Habilitado = true;

        await _repo.UpdateAsync(votante);

        return Ok(new { message = "Votante habilitado" });
    }
    [HttpPut("habilitacion/{habilitado}")]
    public async Task<IActionResult> Habilitacion(bool habilitado)
    {
        await _repo.MakeAllAsync(habilitado);

        return Ok(new { message = habilitado ? "Votantes habilitados" : "Votantes deshabilitados" });
    }
}