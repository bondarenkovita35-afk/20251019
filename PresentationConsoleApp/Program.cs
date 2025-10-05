using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Infrastructure.Models; // vi använder Product från Models

namespace PresentationConsoleApp
{
    class Program
    {
        // lista i minnet som lagrar produkter
        private static readonly List<Product> _products = new();

        static async Task Main(string[] args)
        {
            while (true)
            {
                // Visa meny
                Console.WriteLine("\n=== Meny ===");
                Console.WriteLine("1) Lägg till produkt");
                Console.WriteLine("2) Visa alla produkter");
                Console.WriteLine("3) Läs från textfil (products.txt)");
                Console.WriteLine("4) Spara till JSON (products.json)");
                Console.WriteLine("0) Avsluta");
                Console.Write("> ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProduct(); // lägg till ny produkt
                        break;
                    case "2":
                        ListProducts(); // visa alla produkter
                        break;
                    case "3":
                        await LoadFromTextFileAsync("products.txt"); // importera produkter från fil
                        break;
                    case "4":
                        await SaveToJsonAsync("products.json"); // spara produkter till json-fil
                        break;
                    case "0":
                        return; // avsluta programmet
                    default:
                        Console.WriteLine("Fel val, försök igen.");
                        break;
                }
            }
        }

        // Lägg till en produkt (användaren skriver namn och pris)
        private static void AddProduct()
        {
            Console.Write("Ange produktnamn: ");
            var name = Console.ReadLine() ?? "";

            Console.Write("Ange pris: ");
            if (!decimal.TryParse(Console.ReadLine()?.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out var price))
            {
                Console.WriteLine("Ogiltigt prisformat.");
                return;
            }

            var product = new Product { Name = name, Price = price };
            _products.Add(product);

            File.AppendAllText("products.txt", $"{name},{price}{Environment.NewLine}");
            

           
           
        }

        // Visa alla produkter (ID | Namn | Pris)
        private static void ListProducts()
        {
            if (_products.Count == 0)
            {
                Console.WriteLine("Listan är tom.");
                return;
            }

            foreach (var p in _products)
                Console.WriteLine($"{p.Id} | {p.Name} | {p.Price.ToString(CultureInfo.InvariantCulture)}");
        }

        // Läs produkter från textfil (radformat: Namn;Pris)
        private static async Task LoadFromTextFileAsync(string path)
            
        {
            _products.Clear(); // töm listan innan inläsning
            if (!File.Exists(path))
            {
                Console.WriteLine($"Filen {path} hittades inte!");
                await File.WriteAllTextAsync(path, ""); //skapa tom fil
                return;
            }
           

            var lines = await File.ReadAllLinesAsync(path);
            int added = 0;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(',');
                if (parts.Length != 2) continue;

                var name = parts[0].Trim();
                var priceText = parts[1].Trim().Replace(',', '.'); // stöd för både , och .

                if (decimal.TryParse(priceText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price))
                {
                    _products.Add(new Product { Name = name, Price = price });
                    added++;
                }
            }

            Console.WriteLine($"Importerade: {added} st.");
        }

        // Spara alla produkter till JSON-fil
        private static async Task SaveToJsonAsync(string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_products, options);
            await File.WriteAllTextAsync(path, json);
            Console.WriteLine($"Sparat till fil: {path}");
        }
    }
}