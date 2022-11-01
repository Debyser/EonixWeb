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
            address.Country.Id = 0;
            address.Address2country = 0;
            _countryRepository.Add(address.Country);
            await _countryRepository.CommitAsync(cancellationToken);

            _addressRepository.Add(address);
            await _addressRepository.CommitAsync(cancellationToken);

            return address.Id;
        }

        public async ValueTask DeleteIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var address = await GetByIdAsync(id, cancellationToken);
            if (address == null)
                throw new AddressNotFoundException(id);
            _addressRepository.Remove(address);
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
            var prevCountry = await _countryRepository.FindByIdAsync(prevAddress.Address2country, cancellationToken);
            prevAddress.Street = model.Street;
            prevAddress.BoxNumber = model.BoxNumber;
            prevAddress.City = model.City;
            prevAddress.Zipcode = model.Zipcode;
            //
            prevCountry.Iso3Code = model.Country.Iso3Code;
            prevCountry.Name = model.Country.Name;
            // update
            _countryRepository.Update(prevCountry);
            _addressRepository.Update(prevAddress);
            await _addressRepository.CommitAsync(cancellationToken);
        }
    }
}
