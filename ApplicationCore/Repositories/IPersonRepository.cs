using EonixWebApi.ApplicationCore.Entities;

namespace EonixWebApi.ApplicationCore.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        ValueTask<IEnumerable<Person>> GetByFilterAsync(Person filter, CancellationToken cancellationToken = default);
    }
}
