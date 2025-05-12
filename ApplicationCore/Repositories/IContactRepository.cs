using ApplicationCore.Entities;
using ApplicationCore.Enums;

namespace ApplicationCore.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        ValueTask<Contact> GetByIdAsync(long id, CancellationToken cancellationToken = default);

        ValueTask Update(long id, Contact prevContact, CancellationToken cancellationToken = default);

        ValueTask<List<Contact>> GetList(string filter, SearchableType searchable, CancellationToken cancellationToken = default);

    }
}