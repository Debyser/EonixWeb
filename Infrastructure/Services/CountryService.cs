using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using ApplicationCore.Services;
using Infrastructure.Entities.Exceptions;

namespace Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _repository;
        private static Dictionary<string, int> _cacheCountries;
        private const int _countryCount = 195; // current numbers of country

        public CountryService(ICountryRepository countryRepository)
        {
            _repository = countryRepository;
            Initialize = SetCache();
        }

        public Task Initialize { get; }

        private async Task SetCache()
        {
            // reloader à chaque instance => poub
            // faire du lazy
            if (_cacheCountries == null) _cacheCountries = new Dictionary<string, int>(_countryCount * 2 );
            var countries = await _repository.GetAllAsync();
            foreach (var country in countries)
            {
                if (!_cacheCountries.ContainsKey(country.Iso3Code))
                    _cacheCountries.Add(country.Iso3Code, country.Id);
            }
        }

        public async ValueTask<int> CreateAsync(Country country, CancellationToken cancellationToken = default)
        {
            _repository.Add(country);
            await _repository.CommitAsync(cancellationToken);
            return country.Id;
        }

        public async ValueTask ModifyAsync(int countryId, Country country, CancellationToken cancellationToken = default)
        {
            var prevCountry = await GetByIdAsync(countryId, cancellationToken);
            prevCountry.Iso3Code = country.Iso3Code;
            prevCountry.Name = country.Name;
            _repository.Update(prevCountry);
            await _repository.CommitAsync(cancellationToken);
        }

        public async ValueTask<Country> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var country = await _repository.FindByIdAsync(id, cancellationToken);
            if (country == null) throw new CountryNotFoundException(id);
            return country;
        }

        public async ValueTask<IEnumerable<Country>> GetList(CancellationToken cancellationToken = default)
            => (await _repository.GetAllAsync(cancellationToken)).OrderBy(p => p.Name);

        //todo
        public ValueTask DeleteIdAsync(int id, CancellationToken cancellationToken = default) 
            => throw new NotImplementedException();
    }
}
