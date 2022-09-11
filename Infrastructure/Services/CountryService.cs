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

        public CountryService(ICountryRepository personRepository)
        {
            _repository = personRepository;
        }

        public async ValueTask<int> CreateAsync(Country person, CancellationToken cancellationToken = default)
        {
            //_personRepository.Add(person);
            //await _personRepository.CommitAsync(cancellationToken);
            //return person.Id;

            return -1;
        }

        public async ValueTask DeleteIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var person = await GetByIdAsync(id, cancellationToken);
            // not found 
            if (person == null) return;
            _repository.Remove(person);
            await _repository.CommitAsync(cancellationToken);
        }

        public async ValueTask ModifyAsync(int personId, Country person, CancellationToken cancellationToken = default)
        {
            //var prevPerson = await GetByIdAsync(personId, cancellationToken);
            //prevPerson.FirstName = person.FirstName;
            //prevPerson.LastName = person.LastName;
            //_repository.Update(prevPerson);
            await _repository.CommitAsync(cancellationToken);
        }

        private async ValueTask<IEnumerable<Country>> GetAllAsync(CancellationToken cancellationToken = default)
            => (await _repository.GetAllAsync(cancellationToken)).OrderBy(p => p.Name);

        public async ValueTask<Country> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var person = await _repository.FindByIdAsync(id, cancellationToken);
            if (person == null)
                throw new CountryNotFoundException(id);
            return person;
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

        public ValueTask<IEnumerable<Country>> GetByFilterAsync(Country filter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
