using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using ApplicationCore.Services;
using Infrastructure.Entities.Exceptions;
using System.Data;

namespace Infrastructure.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async ValueTask<Guid> CreateAsync(Person person, CancellationToken cancellationToken = default)
        {
            _personRepository.Add(person);
            await _personRepository.CommitAsync(cancellationToken);
            return person.Id;
        }

        public async ValueTask DeleteIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var person = await GetByIdAsync(id, cancellationToken);
            // not found 
            if (person == null) return;
            _personRepository.Remove(person);
            await _personRepository.CommitAsync(cancellationToken);
        }

        public async ValueTask ModifyAsync(Guid personId, Person person, CancellationToken cancellationToken = default)
        {
            var prevPerson = await GetByIdAsync(personId, cancellationToken);
            prevPerson.FirstName = person.FirstName;
            prevPerson.LastName = person.LastName;
            _personRepository.Update(prevPerson);
            await _personRepository.CommitAsync(cancellationToken);
        }

        private async ValueTask<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default)
            => (await _personRepository.GetAllAsync(cancellationToken)).OrderBy(p => p.LastName).ThenBy(p => p.FirstName);

        public async ValueTask<Person> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var person = await _personRepository.FindByIdAsync(id, cancellationToken);
            if (person == null)
                throw new PersonNotFoundException(id);
            return person;
        }

        public async ValueTask<IEnumerable<Person>> GetByFilterAsync(Person filter, CancellationToken cancellationToken = default)
            => string.IsNullOrWhiteSpace(filter.LastName) && string.IsNullOrWhiteSpace(filter.LastName) ?
               await GetAllAsync(cancellationToken) :
               await _personRepository.GetByFilterAsync(filter, cancellationToken);


        //public IEnumerable<Person> GetByIds(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        //{
        //    //if (ids is null)
        //    //    throw new IdParametersBadRequestException(); 
        //    var companyEntities = _personRepository.GetByIds(ids, cancellationToken);
        //    //if (ids.Count() != companyEntities..Count())
        //    //    throw new CollectionByIdsBadRequestException(); 
        //    //return companyEntities;

        //    return null;
        //}

        public ValueTask<IEnumerable<Person>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}