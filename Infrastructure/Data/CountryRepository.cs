using ApplicationCore.Repositories;
using WebApi.Models;

namespace Infrastructure.Data
{
    public class CountryRepository : DbRepository<Country>, ICountryRepository
    {
        private readonly EonixWebApiContext _context;
        protected CountryRepository(EonixWebApiContext context) : base(context)
        {
            _context = context;
        }

        public ValueTask<IEnumerable<Country>> GetByIds(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
