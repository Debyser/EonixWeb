﻿using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using ApplicationCore.Services;
using ApplicationCore.Exceptions;

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
        // TODO : have a get by id non async for the address where no exception is thrown
        public async ValueTask<Country> GetByIdAsync(long id, CancellationToken cancellationToken = default)
            => _countries.ContainsKey(id) ?
               _countries[id] :
               await _repository.FindByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException(typeof(Country),id);

        public async ValueTask<IEnumerable<Country>> GetListAsync() => await Task.Run(() => _countries.Values);

        public ValueTask DeleteIdAsync(long id, CancellationToken cancellationToken = default) => throw new NotImplementedException();

        public ValueTask<long> CreateAsync(Country country, CancellationToken cancellationToken = default) => throw new NotImplementedException();

        public ValueTask ModifyAsync(long id, Country country, CancellationToken cancellationToken = default) => throw new NotImplementedException();

        private void LoadCache()
        {
            if (_cacheLoaded) return; // for lock once
            lock (_syncRoot)
            {
                if (_cacheLoaded) return; // for security

                var countries = _repository.GetAll().ToList();
                _countries = new Dictionary<long, Country>(countries.Count + countries.Count);

                foreach (var country in countries) _countries.Add(country.Id, country);
                _cacheLoaded = true;
            }
        }

        public Country GetById(long id) => _countries.ContainsKey(id) ? _countries[id] : null;
    }
}