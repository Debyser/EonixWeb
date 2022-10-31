using ApplicationCore.Repositories;
using ApplicationCore.Services;
using Infrastructure.Data;
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

        public async ValueTask<int> CreateAsync(Address address, CancellationToken cancellationToken = default)
        {
            // fix. SqlException: Cannot insert explicit value for identity column in table 'Address' when IDENTITY_INSERT is set to OFF.
            //  * assign to 0 otherwise ValueGeneratedOnAdd() is not working
            address.Id = 0;
            address.Address2countryNavigation.Id = 0;
            address.Address2country = 0;
            _countryRepository.Add(address.Address2countryNavigation);
            await _countryRepository.CommitAsync(cancellationToken);

            _addressRepository.Add(address);
            await _addressRepository.CommitAsync(cancellationToken);

            return address.Id;
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
            _addressRepository.AddInclude("Address2countryNavigation");
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
