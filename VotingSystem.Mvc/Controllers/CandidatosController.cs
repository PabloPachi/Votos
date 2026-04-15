using Microsoft.AspNetCore.Mvc;
using VotingSystem.Mvc.Models;

namespace VotingSystem.Mvc.Controllers;

public class CandidatosController : Controller
{
    private readonly IHttpClientFactory _httpFactory;

    public CandidatosController(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    // 📋 LISTAR
    public async Task<IActionResult> Index()
    {
        var client = _httpFactory.CreateClient("api");

        var candidatos = await client
            .GetFromJsonAsync<List<CandidatoViewModel>>("api/candidatos");

        return View(candidatos ?? new List<CandidatoViewModel>());
    }

    // ➕ CREAR (GET)
    public IActionResult Create()
    {
        return View();
    }

    // ➕ CREAR (POST)
    [HttpPost]
    public async Task<IActionResult> Create(CandidatoViewModel model)
    {
        var client = _httpFactory.CreateClient("api");

        await client.PostAsJsonAsync("api/candidatos", new
        {
            nombre = model.Nombre,
            grupo = model.Grupo,
            fotoUrl = model.FotoUrl
        });

        return RedirectToAction("Index");
    }

    // ✏️ EDITAR (GET)
    public async Task<IActionResult> Edit(Guid id)
    {
        var client = _httpFactory.CreateClient("api");

        var candidato = await client
            .GetFromJsonAsync<CandidatoViewModel>($"api/candidatos/{id}");

        return View(candidato);
    }

    // ✏️ EDITAR (POST)
    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, CandidatoViewModel model)
    {
        var client = _httpFactory.CreateClient("api");

        await client.PutAsJsonAsync($"api/candidatos/{id}", new
        {
            nombre = model.Nombre,
            grupo = model.Grupo,
            fotoUrl = model.FotoUrl,
            activo = true
        });

        return RedirectToAction("Index");
    }

    // ❌ ELIMINAR
    public async Task<IActionResult> Delete(Guid id)
    {
        var client = _httpFactory.CreateClient("api");

        await client.DeleteAsync($"api/candidatos/{id}");

        return RedirectToAction("Index");
    }
}
