using ApplicationCore.Repositories;
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
    }
}
