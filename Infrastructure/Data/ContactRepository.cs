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

            _contactRoleRepository.Add(entity.ContactRoles.ToList());
            _addressRepository.Add(entity.Address);

            // Add contact
            _context.Add(entity);
        }

        public async ValueTask<Contact> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var contact = await _context.Contacts
                .Where(p => p.Id == id && p.Active)
                .Include(p => p.Address)
                    .ThenInclude(p => p.Country)
                .Include(p => p.ContactRoles)
                    .ThenInclude(p => p.Company)
                        .ThenInclude(p => p.Address)
                            .ThenInclude(p => p.Country)
                .FirstOrDefaultAsync(cancellationToken);
            return contact;
        }

        public async ValueTask Update(long id, Contact model, CancellationToken cancellationToken = default)
        {
            var prevContact = await GetByIdAsync(id, cancellationToken);
            if (prevContact != null)
            {
                if (prevContact.ContactRoles != null)
                {
                    foreach (var prevRole in prevContact.ContactRoles)
                    {
                        var currentContacRole = model.ContactRoles.FirstOrDefault(p => p.Id == prevRole.Id);
                        if (currentContacRole == null) continue;
                        prevRole.ContactId = id;
                        prevRole.Name = currentContacRole.Name;
                        prevRole.Active = currentContacRole.Active;
                    }
                }

                prevContact.Address.BoxNumber = model.Address.BoxNumber;
                prevContact.Address.Zipcode = model.Address.Zipcode;
                prevContact.Address.Street = model.Address.Street;
                prevContact.Address.BoxNumber = model.Address.BoxNumber;
                prevContact.Address.City = model.Address.City;
                prevContact.Firstname = model.Firstname;
                prevContact.Lastname = model.Lastname;
                prevContact.PhoneNumber = model.PhoneNumber;
            }
        }

        private void AttachOrUpdateCompany(ContactRole role)
        {
            if (role.Company == null || role.Company.Id == 0) return;

            var existingCompany = _context.Companies.Local.FirstOrDefault(c => c.Id == role.Company.Id);
            if (existingCompany == null)
                _context.Attach(role.Company);
            else
                role.Company = existingCompany;
        }
    }
}