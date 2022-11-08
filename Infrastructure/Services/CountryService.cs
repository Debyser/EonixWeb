using ApplicationCore.Repositories;
using ApplicationCore.Services;
using Infrastructure.Entities.Exceptions;
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
            if (country == null)
                throw new CountryNotFoundException(id);
            _repository.Remove(country);
            await _repository.CommitAsync(cancellationToken);
        }

        public async ValueTask ModifyAsync(int countryId, Country country, CancellationToken cancellationToken = default)
        {
            var prevCountry = await GetByIdAsync(countryId, cancellationToken);
            prevCountry.Iso3Code = country.Iso3Code;
            prevCountry.Name = country.Name;
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

        public async ValueTask<IEnumerable<Country>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            if (ids == null)
                throw new IdParametersBadRequestException();
            var countryEntities = await _repository.GetByIds(ids, cancellationToken);
            if (ids.Count() != countryEntities.Count())
                throw new CollectionByIdsBadRequestException();
            return countryEntities;
        }

        //public ValueTask<IEnumerable<Country>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        //{
        //    if (ids == null) 
        //        throw new CompanyCollectionBadRequest(); 
        //    var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection); 
        //    foreach (var company in companyEntities) 
        //        _repository.CreateCompany(company);
        //    _repository.Save(); var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities); var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id)); return (companies: companyCollectionToReturn, ids: ids);
        //}

        public async ValueTask<IEnumerable<Country>> GetByFilterAsync(Country filter, CancellationToken cancellationToken = default)
            => string.IsNullOrWhiteSpace(filter.Name) ?
               await GetAllAsync(cancellationToken) :
               await _repository.GetByFilterAsync(filter, cancellationToken);
    }
}
