using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace Infrastructure.Data
{
    public class AddressRepository : DbRepository<Address>, IAddressRepository
    {
        private readonly EonixWebApiContext _context;
         
        public AddressRepository(EonixWebApiContext context) : base(context)
        {
            _context = context;
        }

        public async ValueTask<Address> GetByIdAsync(int id, CancellationToken cancellationToken = default) => await _context.Addresses.Where(Country => Country.Id == id).Include(p => p.Country).FirstOrDefaultAsync(cancellationToken);
    }
}