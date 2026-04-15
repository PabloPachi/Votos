using Microsoft.AspNetCore.Mvc;
using VotingSystem.Application.DTOs;
using VotingSystem.Application.UseCases;

namespace VotingSystem.API.Controllers;

[ApiController]
[Route("api/candidatos")]
public class CandidatosController : ControllerBase
{
    private readonly CrearCandidatoUseCase _crear;
    private readonly ActualizarCandidatoUseCase _actualizar;
    private readonly EliminarCandidatoUseCase _eliminar;
    private readonly ListarCandidatosUseCase _listar;
    private readonly ObtenerCandidatoUseCase _obtener;

    public CandidatosController(
        CrearCandidatoUseCase crear,
        ActualizarCandidatoUseCase actualizar,
        EliminarCandidatoUseCase eliminar,
        ListarCandidatosUseCase listar,
        ObtenerCandidatoUseCase obtener)
    {
        _crear = crear;
        _actualizar = actualizar;
        _eliminar = eliminar;
        _listar = listar;
        _obtener = obtener;
    }

    // ➕ Crear
    [HttpPost]
    public async Task<IActionResult> Crear(CrearCandidatoRequest request)
    {
        await _crear.Execute(request);
        return Ok(new { message = "Candidato creado" });
    }

    // ✏️ Actualizar
    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(Guid id, ActualizarCandidatoRequest request)
    {
        await _actualizar.Execute(id, request);
        return Ok(new { message = "Candidato actualizado" });
    }

    // ❌ Eliminar
    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(Guid id)
    {
        await _eliminar.Execute(id);
        return Ok(new { message = "Candidato eliminado" });
    }

    // 📋 Listar
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var result = await _listar.Execute();
        return Ok(result);
    }

    // 🔍 Obtener por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> Obtener(Guid id)
    {
        var candidato = await _obtener.Execute(id);

        if (candidato == null)
            return NotFound();

        return Ok(candidato);
    }
}