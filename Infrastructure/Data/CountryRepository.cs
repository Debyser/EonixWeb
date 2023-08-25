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
            //_context.AttachRange(_context.Countries);
        }


        // Why Adding AsNotTracking ? :
        // The instance of entity type cannot be tracked because another instance with the same key value for {'Id'}
        // is already being tracked
        public IEnumerable<Country> GetAll()
        {
            return _context.Countries.AsNoTracking().AsEnumerable();

        }

    }
}