using Microsoft.AspNetCore.Mvc;
using VotingSystem.Application.UseCases;
using VotingSystem.Domain.Interfaces;

namespace VotingSystem.API.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IVotanteRepository _repo;

    public AdminController(IVotanteRepository repo)
    {
        _repo = repo;
    }

    [HttpPut("habilitar/{id}")]
    public async Task<IActionResult> Habilitar(string codigo)
    {
        var votante = await _repo.GetByCodigoAsync(codigo);

        if (votante == null) return NotFound();

        votante.Habilitado = true;

        await _repo.UpdateAsync(votante);

        return Ok();
    }
}