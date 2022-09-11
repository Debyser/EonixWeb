using ApplicationCore.Entities;
using WebApi.Models;

namespace ApplicationCore.Services
{
    public interface ICountryService
    {
        ValueTask<int> CreateAsync(Country country, CancellationToken cancellationToken = default);
        ValueTask ModifyAsync(int personId, Country country, CancellationToken cancellationToken = default);
        ValueTask DeleteIdAsync(int id, CancellationToken cancellationToken = default);
        ValueTask<Country> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<Country>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<Country>> GetByFilterAsync(Country filter, CancellationToken cancellationToken = default);

    }
}
