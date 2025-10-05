using Infrastructure.Models;
using InInfrastructure.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IProductService  //Interface för logik runt produkter
    {
        Task<ProductResult> AddProductAsync(ProductRequest productRequest, CancellationToken cancellationToken = default);//lägga till ny produkt
        Task<ProductListResult> GetAllProductsAsync(CancellationToken cancellationToken = default);//visa alla produkter
        Task<ProductResult> SaveProductsToFileAsync(CancellationToken cancellationToken = default);//spara produkter till fil
        Task<ProductResult> LoadProductsFromFileAsync(CancellationToken cancellationToken = default);//läsa produkter från fil
    }
}
