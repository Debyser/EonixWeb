using ApplicationCore.Entities;

namespace ApplicationCore.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        IEnumerable<Country> GetAll();
    }
}
