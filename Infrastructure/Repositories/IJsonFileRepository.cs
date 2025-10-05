using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IJsonFileRepository //Interface för att spara och läsa filer asynkront
    {
        Task WriteAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default);//spara alla produkter till fil
        ValueTask<IReadOnlyList<Product>> ReadAsync(CancellationToken cancellationToken = default); //läsa alla produkter från fil
    }
}
