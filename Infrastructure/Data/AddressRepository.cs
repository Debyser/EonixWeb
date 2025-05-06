using ApplicationCore.Entities;
using ApplicationCore.Repositories;

namespace Infrastructure.Data
{
    public class AddressRepository : IAddressRepository
    {
        private readonly EonixDbContext _context;
        private const string TABLE_NAME = "address";


        public AddressRepository(EonixDbContext context, ICountryRepository countryRepository)
        {
            _context = context;
        }

        public void Add(Address entity)
        {
            if (entity == null) return;
            // Attach existing country for address
            if (entity.Country == null || entity.Country.Id <= 0)
                return;

            _context.Attach(entity.Country);
            // Add address
            _context.Add(entity);
        }

        public void Update(Address prevAddress, Address currentAddress)
        {
            if (currentAddress == null) return;
            Update(prevAddress);
            prevAddress.BoxNumber = currentAddress.BoxNumber;
            prevAddress.Zipcode = currentAddress.Zipcode;
            prevAddress.Street = currentAddress.Street;
            prevAddress.BoxNumber = currentAddress.BoxNumber;
            prevAddress.City = currentAddress.City;
        }

        public async ValueTask<Address> GetByIdAsync(long id, CancellationToken cancellationToken = default)
            => await _context.Addresses.Where(p => p.Id == id && p.Active).Include(p => p.Country).FirstOrDefaultAsync(cancellationToken);
    }
}