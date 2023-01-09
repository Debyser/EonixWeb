using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using ApplicationCore.Services;
using Infrastructure.Entities.Exceptions;
using System.Linq;

namespace Infrastructure.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICountryService _countryService;

        public AddressService(IAddressRepository addressRepository, ICountryRepository countryRepository, ICountryService countryService)
        {
            _addressRepository = addressRepository;
            _countryRepository = countryRepository;
            _countryService = countryService;
        }

        public async ValueTask<long> CreateAsync(Address address, CancellationToken cancellationToken = default)
        {
            // fix. SqlException: Cannot insert explicit value for identity column in table 'Address' when IDENTITY_INSERT is set to OFF.
            //  * assign to 0 otherwise ValueGeneratedOnAdd() is not working
            try
            {
                address.Id = 0;
                address.Country.Id = 0;
                address.Address2country = 0;
                var countries = await _countryService.GetList();
                address.Country.Id = countries.FirstOrDefault(p => p.Name == address.Country.Name).Id;
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
                throw new EntityNotFoundException(nameof(Address),id);
            _addressRepository.Remove(address);
            await _addressRepository.CommitAsync(cancellationToken);
        }

        public async ValueTask<Address> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var address = await _addressRepository.GetByIdAsync(id, cancellationToken);
            if (address == null)
                throw new EntityNotFoundException(nameof(Address), id);
            return address;
        }

        public async ValueTask ModifyAsync(long addressId, Address model, CancellationToken cancellationToken = default)
        {
            try
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
                //_countryRepository.Update(prevCountry);
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
