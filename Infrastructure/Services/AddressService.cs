using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Repositories;
using ApplicationCore.Services;

namespace Infrastructure.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICountryService _countryService;

        public AddressService(IAddressRepository addressRepository, ICountryService countryService)
        {
            _addressRepository = addressRepository;
            _countryService = countryService;
        }

        public async ValueTask<long> CreateAsync(Address address, CancellationToken cancellationToken = default)
        {
            // fix. SqlException: Cannot insert explicit value for identity column in table 'Address' when IDENTITY_INSERT is set to OFF.
            //  * assign to 0 otherwise ValueGeneratedOnAdd() is not working
            try
            {
                address.Id = 0;

                var country = await _countryService.GetByIdAsync(address.Country.Id, cancellationToken);
                address.CountryId = country.Id;
                address.Country = null; // to no add new country

                _addressRepository.Add(address);
                await _addressRepository.CommitAsync(cancellationToken);
            }
            catch
            {
                await _addressRepository.RollbackAsync(cancellationToken);
                throw;
            }
            return address.Id;
        }

        public async ValueTask DeleteIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var address = await GetByIdAsync(id, cancellationToken);
            if (address == null)
                throw new EntityNotFoundException(typeof(Address), id);

            address.Active = false;
            _addressRepository.Update(address);
            await _addressRepository.CommitAsync(cancellationToken);
        }

        public async ValueTask<Address> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var address = await _addressRepository.GetByIdAsync(id, cancellationToken);
            if (address == null)
                throw new EntityNotFoundException(typeof(Address), id);
            return address;
        }

        public async ValueTask ModifyAsync(long addressId, Address model, CancellationToken cancellationToken = default)
        {
            try
            {
                var prevAddress = await GetByIdAsync(addressId, cancellationToken);

                prevAddress.Street = model.Street;
                prevAddress.BoxNumber = model.BoxNumber;
                prevAddress.City = model.City;
                prevAddress.Zipcode = model.Zipcode;
                //
                var updatedCountry = await _countryService.GetByIdAsync(model.Country.Id, cancellationToken);
                if (updatedCountry == null)
                    throw new EntityNotFoundException(typeof(Country), model.Country.Id);

                prevAddress.Country = updatedCountry;
                // update
                _addressRepository.Update(prevAddress);
                await _addressRepository.CommitAsync(cancellationToken);
            }
            catch
            {
                await _addressRepository.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}