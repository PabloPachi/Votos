using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VotingSystem.Mvc.Models;

namespace VotingSystem.Mvc.Controllers;

public class ResultadosController : Controller
{
    private readonly IHttpClientFactory _httpFactory;

    public ResultadosController(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _httpFactory.CreateClient("api");

        var resultados = await client
            .GetFromJsonAsync<List<ResultadoViewModel>>("api/votacion/resultados");
        int total = 0;
        if (resultados != null && resultados.Count > 0)
        {
            foreach (var r in resultados)
            {
                total += r.Votos;
            }
            foreach (var r in resultados)
            {
                r.Porcentaje = (double)r.Votos / total * 100;
            }
        }
        if (resultados != null && resultados.Count > 0)
            return View(resultados.OrderByDescending(x => x.Votos).ToList());
        else
            return View(new List<ResultadoViewModel>());
    }
}