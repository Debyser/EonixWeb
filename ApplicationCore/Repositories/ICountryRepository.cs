using ApplicationCore.Entities;
using WebApi.Models;

namespace ApplicationCore.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        ValueTask<IEnumerable<Country>> GetByIds(IEnumerable<int> ids, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<Country>> GetByFilterAsync(Country filter, CancellationToken cancellationToken = default);
    }
}
