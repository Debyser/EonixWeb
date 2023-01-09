using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using ApplicationCore.Services;
using Infrastructure.Entities.Exceptions;

namespace Infrastructure.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IAddressRepository _addressRepository;

        public ContactService(IContactRepository contactRepository, IAddressRepository addressRepository)
        {
            _contactRepository = contactRepository;
            _addressRepository = addressRepository;
        }

        public async ValueTask<long> CreateAsync(Contact contact, CancellationToken cancellationToken = default)
        {
            try
            {
                contact.Id = 0;
                contact.Address.Id = 0;
                contact.Contact2address = 0;
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
                contact.Contact2address = 0;
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
            var contact = await _contactRepository.GetByIdAsync(id, cancellationToken);
            if (contact == null)
                throw new EntityNotFoundException(nameof(Contact), id);
            _contactRepository.Remove(contact);
            await _contactRepository.CommitAsync(cancellationToken);
        }

        public async ValueTask<Contact> GetByIdAsync(long id, CancellationToken cancellationToken = default) 
            => await _contactRepository.GetByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException(nameof(Contact), id);

        public async ValueTask ModifyAsync(long id, Contact model, CancellationToken cancellationToken = default)
        {
            try
            {
                var prevContact = await _contactRepository.GetByIdAsync(id, cancellationToken);
                prevContact.Firstname = model.Firstname;
                prevContact.Lastname = model.Lastname;
                prevContact.Address.BoxNumber = model.Address.BoxNumber;
                prevContact.Address.Zipcode = model.Address.Zipcode;
                prevContact.Address.City = model.Address.City;
                prevContact.Address.Country.Iso3Code = model.Address.Country.Iso3Code;
                prevContact.Address.Country.Name = model.Address.Country.Name; ;

                _contactRepository.Update(prevContact);
                await _contactRepository.CommitAsync(cancellationToken);
            }
            catch 
            {
                await _contactRepository.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}