using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ContactRepository : DbRepository<Contact>, IContactRepository
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IContactRoleRepository _contactRoleRepository;
        private readonly EonixDbContext _context;

        public ContactRepository(EonixDbContext context, IAddressRepository addressRepository, IContactRoleRepository contactRoleRepository) : base(context)
        {
            _context = context;
            _addressRepository = addressRepository;
            _contactRoleRepository = contactRoleRepository;
            _addressRepository.SetDbContext(context);
            _contactRoleRepository.SetDbContext(context);
        }

        public new void Add(Contact entity)
        {
            //entity.CreationTime = DateTime.UtcNow;
            entity.CreationTime = DateTime.UtcNow;

            _contactRoleRepository.Add(entity.ContactRoles);
            //foreach (var role in entity.ContactRoles)
            //{
            //    if (role.Company != null && role.Company.Id != 0)
            //    {
            //        // Check if the company is already tracked
            //        var existingCompany = _context.Companies.Local.FirstOrDefault(c => c.Id == role.Company.Id);
            //        if (existingCompany == null)
            //        {
            //            // Attach existing company
            //            _context.Attach(role.Company);
            //        }
            //        else
            //        {
            //            // Use the existing tracked company
            //            role.Company = existingCompany;
            //        }
            //    }
            //    _contactRoleRepository.Add(role);
            //}

            _addressRepository.Add(entity.Address);

            // Add contact
            _context.Add(entity);
        }

        public async ValueTask<Contact> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var contact = await _context.Contacts
                .Where(p => p.Id == id)
                .Include(p => p.Address)
                    .ThenInclude(p => p.Country)
                .Include(p => p.ContactRoles)
                    .ThenInclude(p => p.Company)
                        .ThenInclude(p => p.Address)
                            .ThenInclude(p => p.Country)
                .FirstOrDefaultAsync(cancellationToken);
            return contact;
        }
    }
}