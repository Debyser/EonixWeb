using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public interface ICountryService : IBaseService<Country>
    {
        ValueTask<IEnumerable<Country>> GetListAsync();
        ValueTask<Country> GetById(long id);
    }
}