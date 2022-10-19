using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using ApplicationCore.Services;
using Infrastructure.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _repository;

        public CountryService(ICountryRepository countryRepository)
        {
            _repository = countryRepository;
        }

        public async ValueTask<int> CreateAsync(Country country, CancellationToken cancellationToken = default)
        {
            _repository.Add(country);
            await _repository.CommitAsync(cancellationToken);
            return country.Id;
        }

        public async ValueTask DeleteIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var country = await GetByIdAsync(id, cancellationToken);
            // not found 
            if (country == null) return;
            _repository.Remove(country);
            await _repository.CommitAsync(cancellationToken);
        }

        public async ValueTask ModifyAsync(int countryId, Country country, CancellationToken cancellationToken = default)
        {
            var prevCountry = await GetByIdAsync(countryId, cancellationToken);
            prevCountry.Name = country.Name;
            prevCountry.Iso3Code = country.Iso3Code;
            _repository.Update(prevCountry);
            await _repository.CommitAsync(cancellationToken);
        }

        private async ValueTask<IEnumerable<Country>> GetAllAsync(CancellationToken cancellationToken = default)
            => (await _repository.GetAllAsync(cancellationToken)).OrderBy(p => p.Name);

        public async ValueTask<Country> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var country = await _repository.FindByIdAsync(id, cancellationToken);
            if (country == null)
                throw new CountryNotFoundException(id);
            return country;
        }

        public IEnumerable<Country> GetByIds(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            //if (ids is null)
            //   throw new IdParametersBadRequestException(); 
            var companyEntities = _repository.GetByIds(ids, cancellationToken);
            //if (ids.Count() != companyEntities..Count())
            //    throw new CollectionByIdsBadRequestException(); 
            //return companyEntities;

            return null;
        }

        public ValueTask<IEnumerable<Country>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<IEnumerable<Country>> GetByFilterAsync(Country filter, CancellationToken cancellationToken = default)
            => string.IsNullOrWhiteSpace(filter.Name) ?
               await GetAllAsync(cancellationToken) :
               await _repository.GetByFilterAsync(filter, cancellationToken);
    }
}
