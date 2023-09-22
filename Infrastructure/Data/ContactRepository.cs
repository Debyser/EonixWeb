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
            entity.CreationTime = DateTime.UtcNow;

            _contactRoleRepository.Add(entity.ContactRoles.ToList());
            _addressRepository.Add(entity.Address);

            // Add contact
            _context.Add(entity);
        }

        public async ValueTask<Contact> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.Contacts.AsNoTracking()
                .Where(p => p.Id == id && p.Active)
                .Include(p => p.Address)
                    .ThenInclude(p => p.Country)
                .Include(p => p.ContactRoles)
                    .ThenInclude(p => p.Company)
                        .ThenInclude(p => p.Address)
                            .ThenInclude(p => p.Country)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async ValueTask Update(long id, Contact model, CancellationToken cancellationToken = default)
        {
            var prevContact = await GetByIdAsync(id, cancellationToken);
            _context.Entry(prevContact).State = EntityState.Modified;
            if (prevContact != null)
            {
                if (prevContact.ContactRoles != null)
                {
                    foreach (var role in model.ContactRoles)
                    {
                        var prevContacRole = prevContact.ContactRoles.FirstOrDefault(p => p.Id == role.Id);
                        if (prevContacRole == null) continue;
                        _context.Entry(prevContacRole).State = EntityState.Modified;
                        prevContacRole.Name = role.Name;
                        prevContacRole.Active = role.Active;
                    }
                }

                _context.Entry(prevContact.Address).State = EntityState.Modified;
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

        #region Tracking way
        // The Tracking way below
        //public async ValueTask<Contact> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        //{
        //    var contact = await _context.Contacts
        //        .Where(p => p.Id == id && p.Active)
        //        .Include(p => p.Address)
        //            .ThenInclude(p => p.Country)
        //        .Include(p => p.ContactRoles)
        //            .ThenInclude(p => p.Company)
        //                .ThenInclude(p => p.Address)
        //                    .ThenInclude(p => p.Country)
        //        .FirstOrDefaultAsync(cancellationToken);
        //    return contact;
        //}

        // the tracking way below
        //public async ValueTask Update(long id, Contact model, CancellationToken cancellationToken = default)
        //{
        //    var prevContact = await GetByIdAsync(id, cancellationToken);
        //    if (prevContact != null)
        //    {
        //        if (prevContact.ContactRoles != null)
        //        {
        //            foreach (var prevRole in prevContact.ContactRoles)
        //            {
        //                var currentContacRole = model.ContactRoles.FirstOrDefault(p => p.Id == prevRole.Id);
        //                if (currentContacRole == null) continue;
        //                prevRole.ContactId = id;
        //                prevRole.Name = currentContacRole.Name;
        //                prevRole.Active = currentContacRole.Active;
        //            }
        //        }

        //        prevContact.Address.BoxNumber = model.Address.BoxNumber;
        //        prevContact.Address.Zipcode = model.Address.Zipcode;
        //        prevContact.Address.Street = model.Address.Street;
        //        prevContact.Address.BoxNumber = model.Address.BoxNumber;
        //        prevContact.Address.City = model.Address.City;
        //        prevContact.Firstname = model.Firstname;
        //        prevContact.Lastname = model.Lastname;
        //        prevContact.PhoneNumber = model.PhoneNumber;
        //    }
        //} 
        #endregion
    }
}