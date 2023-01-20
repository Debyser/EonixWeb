using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AddressRepository : DbRepository<Address>, IAddressRepository
    {
        private readonly EonixDbContext _context;
        private readonly ICountryRepository _countryRepository;

        public AddressRepository(EonixDbContext context, ICountryRepository countryRepository) : base(context)
        {
            _context = context;
            _countryRepository = countryRepository;
        }

        public new void Add(Address entity)
        {
            _context.Add(entity);
        }

        public new void Update(Address entity) 
        {
            _context.Update(entity);
        }

        public async ValueTask<Address> GetByIdAsync(long id, CancellationToken cancellationToken = default) 
            => await _context.Addresses.Where(p => p.Id == id).Include(p => p.Country).FirstOrDefaultAsync(cancellationToken);
    }
}