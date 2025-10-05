using Infrastructure.Models;
using System.Collections.Generic;


namespace InInfrastructure.Models
{
    public class ProductResult // Standardiserat svar från tjänsten (lyckad/misslyckad + meddelande)
    {
        public bool Success { get; set; } //true = lyckad, false = misslyckad

        public int StatusCode { get; set; } //enkelt kod för status

        public string? Error { get; set; }// felmedelande om det inte går bra

    }

    public class ProductListResult : ProductResult // resultat som innehåller flera produkter
    {
        public IReadOnlyList<Product> Product { get; set; } = []; //lista med produkter
    }

    
        
    
}
