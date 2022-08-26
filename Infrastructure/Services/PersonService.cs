using ApplicationCore.Services;
using EonixWebApi.ApplicationCore.Entities;
using EonixWebApi.ApplicationCore.Repositories;

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
            // fix. SqlException: Cannot insert explicit value for identity column in table 'Address' when IDENTITY_INSERT is set to OFF.
            //  * assign to 0 otherwise ValueGeneratedOnAdd() is not working
            //person.Id = 0L;
            //person.MainAddressId = 0L;
            //person.MainAddress.Id = 0L;
            //_addressRepository.Add(person.MainAddress);
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

        public async ValueTask ModifyAsync(Guid personId, Person contact, CancellationToken cancellationToken = default)
        {
            var prevPerson = await GetByIdAsync(personId, cancellationToken);
            //TODO: validation here  + dynamic mapping
            prevPerson.FirstName = contact.FirstName;
            prevPerson.LastName = contact.LastName;
            // update 
            _personRepository.Update(prevPerson);
            await _personRepository.CommitAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _personRepository.GetAllAsync(cancellationToken);
        }

        public async ValueTask<Person> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _personRepository.FindByIdAsync(id, cancellationToken);
            return result;
        }
    }
}
