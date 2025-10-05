using System;
using Infrastructure.Models;


namespace Infrastructure.Models
{
    public class Product // För att lagra fullständig data (som i databasen)
    {
        public Guid Id { get; set; } = Guid.NewGuid();//unik ID skapas automatiskt

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }


        
    }
}
