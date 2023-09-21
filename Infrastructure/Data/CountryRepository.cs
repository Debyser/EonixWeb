using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class CountryRepository : DbRepository<Country>, ICountryRepository
    {
        private readonly EonixDbContext _context;
        public CountryRepository(EonixDbContext context) : base(context)
        {
            _context = context;
        }

        // Why Adding AsNotTracking ? :
        // The instance of entity type cannot be tracked because another instance with the same key value for {'Id'}
        // is already being tracked
        public IEnumerable<Country> GetAll() => _context.Countries.AsNoTracking().AsEnumerable();
    }
}