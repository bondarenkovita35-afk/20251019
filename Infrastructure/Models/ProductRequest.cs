using System;
using Infrastructure.Models;


namespace Infrastructure.Models
{
    public class ProductRequest //Endast för inmatning (användaren anger inget ID)
    {
        public string Name { get; set; } = null!; // Användaren skriver namn
        public decimal Price { get; set; } // Användaren skriver pris
    }
}
