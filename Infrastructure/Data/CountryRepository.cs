using ApplicationCore.Entities;
using ApplicationCore.Repositories;

namespace Infrastructure.Data
{
    public class CountryRepository : DbRepository<Country>, ICountryRepository
    {
        private readonly EonixDbContext _context;
        public CountryRepository(EonixDbContext context) : base(context)
        {
            _context = context; 
        }

        public IEnumerable<Country> GetAll() => _context.Countries.AsEnumerable();

    }
}