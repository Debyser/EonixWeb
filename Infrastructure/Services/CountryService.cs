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
        private static Dictionary<long, Country> _countries;
        private static readonly object _syncRoot = new object();

        public CountryService(ICountryRepository countryRepository)
        {
            _repository = countryRepository;
            LoadCache();
        }

        // todo : use long instead of int
        public async ValueTask<Country> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            if (_countries.ContainsKey(id)) return _countries[id];
            var country = await _repository.FindByIdAsync(id, cancellationToken);
            if (country == null) throw new CountryNotFoundException(id); // TOdo: entityNotfound
            return country;
        }

        //TOdo
        public async ValueTask<IEnumerable<Country>> GetList(CancellationToken cancellationToken = default)
            => (await _repository.GetAllAsync(cancellationToken));

        public ValueTask DeleteIdAsync(long id, CancellationToken cancellationToken = default) 
            => throw new NotImplementedException();

        public ValueTask<long> CreateAsync(Country model, CancellationToken cancellationToken = default) 
            => throw new NotImplementedException();

        public ValueTask ModifyAsync(long countryId, Country country, CancellationToken cancellationToken = default)
          => throw new NotImplementedException();

        private void LoadCache()
        {
            if (_cacheLoaded) return; // for lock once
            lock (_syncRoot)
            {
                if (_cacheLoaded) return; // for security

                _countries = new Dictionary<long, Country>();
                var countries = _repository.GetAll();
                foreach (var country in countries) _countries.Add(country.Id, country);
                _cacheLoaded = true;
            }
        }
    }
}

/*
 patter Singleton

public sealed class Singleton
{
    private static readonly object Instancelock = new object();
    private Singleton()
    {
    }
    private static Singleton instance = null;

    public static Singleton GetInstance
    {
        get
        {
            if (instance == null)
            {
                lock (Instancelock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }
    }
}
 
 */