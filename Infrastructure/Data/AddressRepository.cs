using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AddressRepository : DbRepository<Address>, IAddressRepository
    {
        private readonly EonixDbContext _context;

        public AddressRepository(EonixDbContext context, ICountryRepository countryRepository) : base(context)
        {
            _context = context;
        }

        public new void Add(Address entity)
        {
            if (entity == null) return;
            // Attach existing country for address
            if (entity.Country == null || entity.Country.Id == 0)
                return;

            _context.Attach(entity.Country);

            // Add address
            _context.Add(entity);
        }

        public new void Update(Address entity) => _context.Update(entity);// still relevant ?

        public async ValueTask<Address> GetByIdAsync(long id, CancellationToken cancellationToken = default)
            => await _context.Addresses.Where(p => p.Id == id).Include(p => p.Country).FirstOrDefaultAsync(cancellationToken);
    }
}