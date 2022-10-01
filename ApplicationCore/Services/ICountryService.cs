using WebApi.Models;

namespace ApplicationCore.Services
{
    public interface ICountryService : IBaseService<Country>
    {
        ValueTask<IEnumerable<Country>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<Country>> GetByFilterAsync(Country filter, CancellationToken cancellationToken = default);
    }
}