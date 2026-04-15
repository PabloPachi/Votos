using Microsoft.AspNetCore.Mvc;
using VotingSystem.Application.DTOs;
using VotingSystem.Application.UseCases;

namespace VotingSystem.API.Controllers;

[ApiController]
[Route("api/votacion")]
public class VotacionController : ControllerBase
{
    private readonly VotarUseCase _useCase;
    private readonly ObtenerResultadosUseCase _resultado;

    public VotacionController(VotarUseCase useCase, ObtenerResultadosUseCase resultado)
    {
        _useCase = useCase;
        _resultado = resultado;
    }

    [HttpPost("votar")]
    public async Task<IActionResult> Votar(VotarRequest request)
    {
        await _useCase.Execute(request);
        return Ok(new { message = "Voto registrado" });
    }
    [HttpGet("resultados")]
    public async Task<IActionResult> Resultados()
    {
        var result = await _resultado.Execute();
        return Ok(result);
    }
}