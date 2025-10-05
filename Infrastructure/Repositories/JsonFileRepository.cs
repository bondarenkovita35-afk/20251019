using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class JsonFileRepository : IJsonFileRepository // klass som jobbar med Json-fil
    {
        private readonly string _filePath;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true
        };
        public JsonFileRepository(string fileName = "products.json")
        {
            var baseDirectory = AppContext.BaseDirectory;
            var dataDirectory = Path.Combine(baseDirectory, "Data");
            _filePath = Path.Combine(dataDirectory, fileName);

            EnsureInitialized(dataDirectory, _filePath);
        }
        private static void EnsureInitialized(string dataDirectory, string filePath)
        {
            if (!Directory.Exists(dataDirectory))
                Directory.CreateDirectory(dataDirectory);

            if (!File.Exists(filePath))
                File.WriteAllText(filePath, "[]");
        }
        public async Task WriteAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default) //skriver produkt till Json-fil
        {
            await using var stream = File.Create(_filePath);
            await JsonSerializer.SerializeAsync(stream, products, _jsonOptions, cancellationToken);
        }
        public async ValueTask<IReadOnlyList<Product>> ReadAsync(CancellationToken cancellationToken = default) //läser produkter från Json-fil
        {
            try
            {
                await using var stream = File.OpenRead(_filePath);
                var products = await JsonSerializer.DeserializeAsync<List<Product>>(stream, _jsonOptions, cancellationToken);
                return products ?? new List<Product>();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }
    }
}
