using EonixWebApi.ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public interface IPersonService
    {
        ValueTask<Guid> CreateAsync(PersonDto person, CancellationToken cancellationToken = default);
        ValueTask ModifyAsync(Guid personId, PersonDto person, CancellationToken cancellationToken = default);
        ValueTask DeleteIdAsync(Guid id, CancellationToken cancellationToken = default);
        ValueTask<PersonDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<PersonDto>> GetByFilterAsync(PersonDto filter, CancellationToken cancellationToken = default);
    }
}
