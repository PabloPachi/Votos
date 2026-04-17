using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using VotingSystem.Mvc.Models;

namespace VotingSystem.Mvc.Controllers;

public class VotantesController : Controller
{
    private readonly IHttpClientFactory _httpFactory;

    public VotantesController(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    // 📋 LISTAR
    public async Task<IActionResult> Index(string? curso)
    {
        var client = _httpFactory.CreateClient("api");


        List<string>? cursos = await client
            .GetFromJsonAsync<List<string>>($"api/votantes/GradoParalelo");
        if (cursos != null && cursos.Count > 0 && string.IsNullOrEmpty(curso))
            curso = cursos[0];
        if (string.IsNullOrEmpty(curso))
            curso = "0-0";
        var gradoparalelo = curso.Split('-');
        string grado = gradoparalelo[0];
        string paralelo = gradoparalelo[1];
        var votantes = await client
            .GetFromJsonAsync<List<VotanteViewModel>>($"api/votantes/curso?grado={grado ?? "0"}&paralelo={paralelo ?? "0"}");

        var model = new VotantesFiltroViewModel
        {
            Curso = curso,
            Votantes = votantes ?? new(),
            Cursos = cursos ?? new()
        };

        return View(model);
    }

    // ➕ CREAR (GET)
    public IActionResult Create()
    {
        return View();
    }

    // ➕ CREAR (POST)
    [HttpPost]
    public async Task<IActionResult> Create(VotanteViewModel model)
    {
        var client = _httpFactory.CreateClient("api");

        await client.PostAsJsonAsync("api/votantes", new
        {
            grado = model.Grado,
            paralelo = model.Paralelo,
            paterno = model.Paterno,
            materno = model.Materno,
            nombre = model.Nombre
        });

        return RedirectToAction("Index");
    }

    // ✏️ EDITAR (GET)
    public async Task<IActionResult> Edit(string codigo)
    {
        var client = _httpFactory.CreateClient("api");

        var votante = await client
            .GetFromJsonAsync<VotanteViewModel>($"api/votantes/{codigo}");

        return View(votante);
    }

    // ✏️ EDITAR (POST)
    [HttpPost]
    public async Task<IActionResult> Edit(VotanteViewModel model)
    {
        var client = _httpFactory.CreateClient("api");

        await client.PutAsJsonAsync($"api/votantes/{model.Codigo}", new
        {
            codigo = model.Codigo,
            grado = model.Grado,
            paralelo = model.Paralelo,
            paterno = model.Paterno,
            materno = model.Materno,
            nombre = model.Nombre,
            habilitado = model.Habilitado
        });
        string curso = $"{model.Grado}-{model.Paralelo}";
        return RedirectToAction("Index", new { curso });
    }

    // ❌ ELIMINAR
    public async Task<IActionResult> Delete(string id, string? curso)
    {
        var client = _httpFactory.CreateClient("api");

        await client.DeleteAsync($"api/votantes/{id}");

        return RedirectToAction("Index", new { curso });
    }

    // 🔥 HABILITAR
    public async Task<IActionResult> Habilitar(string codigo, string? curso)
    {
        var client = _httpFactory.CreateClient("api");

        await client.PutAsync($"api/votantes/habilitar/{codigo}", null);

        return RedirectToAction("Index", new { curso });
    }
    public async Task<IActionResult> HabilitarTodos()
    {
        var client = _httpFactory.CreateClient("api");

        await client.PutAsync($"api/votantes/habilitacion/{true}", null);

        return RedirectToAction("Index");
    }
    public async Task<IActionResult> DeshabilitarTodos()
    {
        var client = _httpFactory.CreateClient("api");

        await client.PutAsync($"api/votantes/habilitacion/{false}", null);

        return RedirectToAction("Index");
    }
    public async Task<IActionResult> ExportarExcel()
    {
        var client = _httpFactory.CreateClient("api");

        string url = "api/votantes";

        var votantes = await client
            .GetFromJsonAsync<List<VotanteViewModel>>(url) ?? new();

        using var package = new ExcelPackage();
        var ws = package.Workbook.Worksheets.Add("Votantes");

        // Cabecera
        ws.Cells[1, 1].Value = "Código";
        ws.Cells[1, 2].Value = "Grado";
        ws.Cells[1, 3].Value = "Paralelo";
        ws.Cells[1, 4].Value = "Paterno";
        ws.Cells[1, 5].Value = "Materno";
        ws.Cells[1, 6].Value = "Nombre";
        ws.Cells[1, 7].Value = "Habilitado";
        ws.Cells[1, 8].Value = "Ya Votó";

        // Datos
        int fila = 2;
        foreach (var v in votantes)
        {
            ws.Cells[fila, 1].Value = v.Codigo;
            ws.Cells[fila, 2].Value = v.Grado;
            ws.Cells[fila, 3].Value = v.Paralelo;
            ws.Cells[fila, 4].Value = v.Paterno;
            ws.Cells[fila, 5].Value = v.Materno;
            ws.Cells[fila, 6].Value = v.Nombre;
            ws.Cells[fila, 7].Value = v.Habilitado ? "SI" : "NO";
            ws.Cells[fila, 8].Value = v.YaVoto ? "SI" : "NO";

            fila++;
        }

        ws.Cells.AutoFitColumns();

        var bytes = package.GetAsByteArray();

        return File(bytes,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "Votantes.xlsx");
    }
}