using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Repositories;
using ApplicationCore.Services;

namespace Infrastructure.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async ValueTask<long> CreateAsync(Contact contact, CancellationToken cancellationToken = default)
        {
            try
            {
                //contact.Address.Country = null; // to prevent to add a country
                contact.Active = true;
                _contactRepository.Add(contact);
                await _contactRepository.CommitAsync(cancellationToken);

            }
            catch
            {
                await _contactRepository.RollbackAsync(cancellationToken);
                throw;
            }
            return contact.Id;
        }

        public async ValueTask<long> CreateEmployeeForCompany(long companyId, Contact contact, CancellationToken cancellationToken = default)
        {
            try
            {
                contact.Id = 0;
                contact.Address.Id = 0;
                contact.AddressId = 0;
                _contactRepository.Add(contact);
                await _contactRepository.CommitAsync(cancellationToken);
            }
            catch
            {
                await _contactRepository.RollbackAsync(cancellationToken);
                throw;
            }
            return contact.Id;
        }

        public async ValueTask DeleteIdAsync(long id, CancellationToken cancellationToken = default)
        {
            try
            {
                var contact = await _contactRepository.GetByIdAsync(id, cancellationToken);
                if (contact == null) throw new EntityNotFoundException(typeof(Contact), id);

                contact.Active = false;
                await _contactRepository.CommitAsync(cancellationToken);
            }
            catch
            {
                await _contactRepository.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async ValueTask<Contact> GetByIdAsync(long id, CancellationToken cancellationToken = default)
            => await _contactRepository.GetByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException(typeof(Contact), id);

        public async ValueTask ModifyAsync(long id, Contact model, CancellationToken cancellationToken = default)
        {
            try
            {
                //var prevContact = await _contactRepository.GetByIdAsync(id, cancellationToken);
                //if (prevContact == null) throw new EntityNotFoundException(typeof(Contact), id);

                await _contactRepository.Update(id, model, cancellationToken);
                await _contactRepository.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await _contactRepository.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}

/*
SqlException: The UPDATE statement conflicted with the FOREIGN KEY constraint "contact_role_contact_role2contact_fkey". 
The conflict occurred in database "eonix", table "dbo.contact", column 'id'.
 
 */