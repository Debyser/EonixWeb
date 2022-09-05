using EonixWebApi.ApplicationCore.Entities;

namespace EonixWebApi.ApplicationCore.Repositories
{
    public interface IPersonRepository : IRepository<PersonDto>
    {
        ValueTask<IEnumerable<PersonDto>> GetByFilterAsync(PersonDto filter, CancellationToken cancellationToken = default);
    }
}
