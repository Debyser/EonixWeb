using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace Infrastructure.Data
{
    public class CountryRepository : DbRepository<Country>, ICountryRepository
    {
        private readonly EonixWebApiContext _context;
        public CountryRepository(EonixWebApiContext context) : base(context)
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

        public ValueTask<IEnumerable<Country>> GetByIds(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
