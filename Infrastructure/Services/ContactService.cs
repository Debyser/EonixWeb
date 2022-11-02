using ApplicationCore.Repositories;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Entities.Exceptions;
using System.Net;
using WebApi.Models;

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
        public async ValueTask<int> CreateAsync(Contact contact, CancellationToken cancellationToken = default)
        {

            contact.Id = 0;
            contact.Address.Id = 0;
            contact.Contact2address = 0;
            _addressRepository.Add(contact.Address);
            await _addressRepository.CommitAsync(cancellationToken);

            _contactRepository.Add(contact);
            await _contactRepository.CommitAsync(cancellationToken);

            return contact.Id;


            // fix. SqlException: Cannot insert explicit value for identity column in table 'Address' when IDENTITY_INSERT is set to OFF.
            //  * assign to 0 otherwise ValueGeneratedOnAdd() is not working
            //address.Id = 0;
            //address.Country.Id = 0;
            //address.Address2country = 0;
            //_countryRepository.Add(address.Country);
            //await _countryRepository.CommitAsync(cancellationToken);

            //_addressRepository.Add(address);
            //await _addressRepository.CommitAsync(cancellationToken);

            //return address.Id;
        }

        public async ValueTask DeleteIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var contact = await GetByIdAsync(id, cancellationToken);
            if (contact == null)
                throw new ContactNotFoundException(id);
            _contactRepository.Remove(contact);
            await _contactRepository.CommitAsync(cancellationToken);

        }

        public async ValueTask<Contact> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var contact = await _contactRepository.FindByIdAsync(id, cancellationToken);
            if (contact == null)
                throw new ContactNotFoundException(id);
            return contact;
        }

        public async ValueTask ModifyAsync(int id, Contact model, CancellationToken cancellationToken = default)
        {
            var prevContact = await GetByIdAsync(id, cancellationToken);
            prevContact.Firstname = model.Firstname;
            prevContact.Lastname = model.Lastname;
            _contactRepository.Update(prevContact);
            await _contactRepository.CommitAsync(cancellationToken);
        }
    }
}
