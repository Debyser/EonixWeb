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
            _countryRepository.SetDbContext(context);
        }

        public new void Add(Address entity)
        {
            _context.Add(entity);
            _countryRepository.Add(entity.Country);
        }

        public new void Update(Address entity) 
        {
            _context.Update(entity);
            _countryRepository.Update(entity.Country);
        }

        public async ValueTask<Address> GetByIdAsync(int id, CancellationToken cancellationToken = default) 
            => await _context.Addresses.Where(p => p.Id == id).Include(p => p.Country).FirstOrDefaultAsync(cancellationToken);
    }
}