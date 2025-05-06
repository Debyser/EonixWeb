using ApplicationCore.Entities;

namespace ApplicationCore.Repositories
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetAll();
    }
}
