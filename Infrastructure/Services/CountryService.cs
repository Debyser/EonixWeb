using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using ApplicationCore.Services;
using Infrastructure.Entities.Exceptions;

namespace Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _repository;
        private static bool _cacheLoaded = false;
        private static Dictionary<int, Country> _countries;
        private static readonly object _syncRoot = new object();

        public CountryService(ICountryRepository countryRepository)
        {
            _repository = countryRepository;
            LoadCache();
        }

        public async ValueTask<Country> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var country = await _repository.FindByIdAsync(id, cancellationToken);
            if (country == null) throw new CountryNotFoundException(id);
            return country;
        }

        public async ValueTask<IEnumerable<Country>> GetList(CancellationToken cancellationToken = default)
            => (await _repository.GetAllAsync(cancellationToken)).OrderBy(p => p.Name);

        public ValueTask DeleteIdAsync(int id, CancellationToken cancellationToken = default) 
            => throw new NotImplementedException();

        public ValueTask<int> CreateAsync(Country model, CancellationToken cancellationToken = default) 
            => throw new NotImplementedException();

        public ValueTask ModifyAsync(int countryId, Country country, CancellationToken cancellationToken = default)
          => throw new NotImplementedException();

        private void LoadCache()
        {
            lock (_syncRoot) 
            {
                if (!_cacheLoaded)
                {
                    _countries = new Dictionary<int, Country>();
                    var countries = _repository.GetAll();
                    foreach (var country in countries) _countries.Add(country.Id, country);
                    _cacheLoaded = true;
                }
            }
        }
    }
}
