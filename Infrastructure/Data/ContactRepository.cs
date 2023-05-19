using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public new void Add(Contact entity)
        {
            entity.CreationTime = DateTime.UtcNow;
            _context.Add(entity);
            _addressRepository.Add(entity.Address);
            //_context.Entry(entity.Address.Country).State = EntityState.Unchanged; // because we don't want to add/update country
        }

        //TODO: rajouter le n'id de la company , non ? car trop de contact role
        public async ValueTask<Contact> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var contact = await _context.Contacts
                .Where(p => p.Id == id)
                .Include(p => p.ContactRoles)
                .Include(p => p.Address)
                .Include(p => p.Address.Country)
                .Include(p => p.ContactRoles)
                .FirstOrDefaultAsync(cancellationToken);
            return contact;
        }
    }
}