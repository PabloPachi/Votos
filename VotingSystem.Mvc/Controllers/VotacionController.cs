using Microsoft.AspNetCore.Mvc;
using VotingSystem.Mvc.Models;

namespace VotingSystem.Mvc.Controllers;

public class VotacionController : Controller
{
    private readonly IHttpClientFactory _httpFactory;

    public VotacionController(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    // 🟢 Pantalla inicial
    public IActionResult Index()
    {
        return View();
    }

    // 🔍 Validar código
    [HttpPost]
    public async Task<IActionResult> Validar(string codigo)
    {
        var client = _httpFactory.CreateClient("api");
        VotanteViewModel? votante = new VotanteViewModel();
        try
        {
            votante = await client
            .GetFromJsonAsync<VotanteViewModel>($"api/votantes/{codigo.ToUpper()}");
        }
        catch
        {
            votante = null;
        }

        if (votante == null)
        {
            ViewBag.Error = "Votante no encontrado";
            return View("Index");
        }

        if (!votante.Habilitado)
        {
            ViewBag.Error = "Votante no habilitado";
            return View("Index");
        }

        if (votante.YaVoto)
        {
            ViewBag.Error = "Este votante ya emitió su voto";
            return View("Index");
        }
        string nombre = $"{votante.Paterno} {votante.Materno} {votante.Nombre}";
        // 👉 OK → ir a votar
        return RedirectToAction("Votar", new { codigo, nombre });
    }

    // 🗳️ Pantalla de votación
    public async Task<IActionResult> Votar(string codigo, string nombre)
    {
        var client = _httpFactory.CreateClient("api");

        var candidatos = await client
            .GetFromJsonAsync<List<CandidatoViewModel>>("api/candidatos");
        ViewBag.Nombre = nombre;
        ViewBag.Codigo = codigo;

        return View(candidatos);
    }

    // ✅ Registrar voto
    [HttpPost]
    public async Task<IActionResult> Registrar(string codigo, Guid candidatoId)
    {
        var client = _httpFactory.CreateClient("api");

        await client.PostAsJsonAsync("api/votacion/votar", new
        {
            votanteCodigo = codigo,
            candidatoId = candidatoId
        });

        return RedirectToAction("Confirmacion");
    }

    // 🎉 Confirmación
    public IActionResult Confirmacion()
    {
        return View();
    }
}