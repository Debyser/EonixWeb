using ApplicationCore.Entities;

namespace ApplicationCore.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        ValueTask<Contact> GetByIdAsync(long id, CancellationToken cancellationToken = default);

        ValueTask Update(long id, Contact prevContact, CancellationToken cancellationToken = default);
    }
}