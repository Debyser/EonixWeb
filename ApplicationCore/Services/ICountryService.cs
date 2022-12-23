using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public interface ICountryService : IBaseService<Country>
    {
        ValueTask<IEnumerable<Country>> GetList(CancellationToken cancellationToken = default);
    }
}