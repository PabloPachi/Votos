using System.Text;
using VotingSystem.Domain.Entities;
using VotingSystem.Domain.Interfaces;

namespace VotingSystem.Infrastructure.Services;

public class CsvVotanteReader : IFileVotanteReader
{

    public async Task<List<Votante>> Read(Stream stream)
    {
        var votantes = new List<Votante>();

        using var reader = new StreamReader(stream, Encoding.UTF8);

        string? line;
        bool isHeader = true;
        int siguiente = 1;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            siguiente = 1;
            if (isHeader)
            {
                isHeader = false;
                continue;
            }

            var columns = line.Split(',');

            if (columns.Length < 5)
                continue;
            string curso = columns[0].Trim();

            string paterno = string.IsNullOrEmpty(columns[2].Trim()) ? "_" : columns[2].Trim();
            string materno = string.IsNullOrEmpty(columns[3].Trim()) ? "_" : columns[3].Trim();
            string nombre = string.IsNullOrEmpty(columns[4].Trim()) ? "_" : columns[4].Trim();

            string baseCodigo = $"{(curso?.Length <= 2 ? curso : curso.Substring(0, 2))}{columns[1].Trim()}{paterno[0]}{materno[0]}{nombre[0]}";
            string codigoFinal = baseCodigo;
            while (votantes.Exists(r => r.Codigo == codigoFinal))
            {
                codigoFinal = $"{baseCodigo}{siguiente++}";
            }

            var votante = new Votante
            {
                Id = Guid.NewGuid(),
                Codigo = codigoFinal.ToUpper(),
                Grado = columns[0].Trim(),
                Paralelo = columns[1].Trim(),
                Paterno = columns[2].Trim(),
                Materno = columns[3].Trim(),
                Nombre = columns[4].Trim(),
                Habilitado = false,
                YaVoto = false,
                Activo = true
            };

            votantes.Add(votante);
        }

        return votantes;
    }
}