using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApi.Models;

namespace Infrastructure.Data
{
    public class CountryRepository : DbRepository<Country>, ICountryRepository
    {
        private readonly EonixDbContext _context;
        public CountryRepository(EonixDbContext context) : base(context)
        {
            _context = context; 
        }

        public async ValueTask<IEnumerable<Country>> GetByFilterAsync(Country filter, CancellationToken cancellationToken = default)
        {
            return (!string.IsNullOrWhiteSpace(filter.Name)) ? 
                await _context.Countries
                .Where(p => p.Name.StartsWith(filter.Name))
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken)              : 
                await _context.Countries.ToListAsync();
        }

        //TODO : create 9.6 Model binding in API
        public async ValueTask<IEnumerable<Country>> GetByIds(IEnumerable<int> ids, CancellationToken cancellationToken = default)
            => await _context.Countries.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
    }
}
