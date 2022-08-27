using EonixWebApi.ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public interface IPersonService
    {
        ValueTask<Guid> CreateAsync(Person person, CancellationToken cancellationToken = default);
        ValueTask ModifyAsync(Guid personId, Person person, CancellationToken cancellationToken = default);
        ValueTask DeleteIdAsync(Guid id, CancellationToken cancellationToken = default);
        //ValueTask<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default);
        ValueTask<Person> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<Person>> GetByFilterAsync(Person filter, CancellationToken cancellationToken = default);
    }
}
