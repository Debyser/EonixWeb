using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public interface ICountryService : IBaseService<Country>
    {
        ValueTask<IEnumerable<Country>> GetListAsync();
        Country GetById(long id);
        ValueTask<Country> GetByName(string name);
    }
}