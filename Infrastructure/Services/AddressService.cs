using ApplicationCore.Repositories;
using ApplicationCore.Services;
using Infrastructure.Entities.Exceptions;
using WebApi.Models;

namespace Infrastructure.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICountryRepository _countryRepository;

        public AddressService(IAddressRepository addressRepository, ICountryRepository countryRepository)
        {
            _addressRepository = addressRepository;
            _countryRepository = countryRepository;
        }

        public async ValueTask<int> CreateAsync(Address model, CancellationToken cancellationToken = default)
        {
            _addressRepository.Add(model);
            await _addressRepository.CommitAsync(cancellationToken);
            return model.Id;

            // fix. SqlException: Cannot insert explicit value for identity column in table 'Address' when IDENTITY_INSERT is set to OFF.
            //  * assign to 0 otherwise ValueGeneratedOnAdd() is not working
            //contact.Id = 0L;
            //contact.MainAddressId = 0L;
            //contact.MainAddress.Id = 0L;
            //_addressRepository.Add(contact.MainAddress);
            //await _addressRepository.CommitAsync(cancellationToken);

            //_contactRepository.Add(contact);
            //await _contactRepository.CommitAsync(cancellationToken);

            //return contact.Id;
        }

        public async ValueTask DeleteIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var country = await GetByIdAsync(id, cancellationToken);
            if (country == null)
                throw new AddressNotFoundException(id);
            _addressRepository.Remove(country);
            await _addressRepository.CommitAsync(cancellationToken);
        }

        public async ValueTask<Address> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var address = await _addressRepository.FindByIdAsync(id, cancellationToken);
            if (address == null)
                throw new AddressNotFoundException(id);
            return address;
        }

        public async ValueTask ModifyAsync(int addressId, Address model, CancellationToken cancellationToken = default)
        {
            var prevAddress = await GetByIdAsync(addressId, cancellationToken);
            prevAddress.Street = model.Street;
            prevAddress.BoxNumber = model.BoxNumber;
            _addressRepository.Update(prevAddress);
            await _addressRepository.CommitAsync(cancellationToken);

            //var prevContact = await GetByIdAsync(contactId, cancellationToken);
            //var prevMainAddress = await _addressRepository.FindByIdAsync(contact.MainAddressId, cancellationToken);
            ////TODO: validation here  + dynamic mapping
            //prevContact.FirstName = contact.FirstName;
            //prevContact.LastName = contact.LastName;
            //prevContact.PhoneNumber = contact.PhoneNumber;
            //prevContact.Vat = contact.Vat;
            ////
            //prevMainAddress.City = contact.MainAddress.City;
            //prevMainAddress.ZipCode = contact.MainAddress.ZipCode;
            //prevMainAddress.Description = contact.MainAddress.Description;
            //// update 
            //_contactRepository.Update(prevContact);
            //_addressRepository.Update(prevMainAddress);
            //await _contactRepository.CommitAsync(cancellationToken);
        }
    }
}
