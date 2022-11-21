using ApplicationCore.Entities;

namespace ApplicationCore.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        ValueTask<IEnumerable<Person>> GetByFilterAsync(Person filter, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<Person>> GetByIds(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    }
}
