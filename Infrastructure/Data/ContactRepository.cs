using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace Infrastructure.Data
{
    public class ContactRepository : DbRepository<Contact>, IContactRepository
    {
        private readonly IAddressRepository _addressRepository;
        private readonly EonixDbContext _context;

        public ContactRepository(EonixDbContext context, IAddressRepository addressRepository) : base(context)
        {
            _context = context;
            _addressRepository = addressRepository;
            _addressRepository.SetDbContext(context);
        }

        // new : erase the Add from DbRepository
        public new void Add(Contact entity)
        {
            _context.Add(entity);
            _addressRepository.Add(entity.Address);
        }

        public async ValueTask<Contact> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Contacts
                .Where(p => p.Id == id)
                .Include(p => p.Address)
                .Include(p => p.Address.Country)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}