using Infrastructure.Models;
using System.Collections.Generic;
using Xunit;

namespace ProductApp.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void AddProduct_ShouldAddToList()
        {
            //Arrange (Förberedelse)
            var products = new List<Product>();
            var product = new Product { Name = "Test", Price = 10 };

            // Act (agera)
            products.Add(product);

            // Assert (kontrollera)
            Assert.Single(products); // kolla att det finns exakt en produkt i listan
            Assert.Equal("Test", products[0].Name); // kolla att namnet stämmer
            Assert.Equal(10, products[0].Price);    //kolla att priset stämmer
        }
    }
}