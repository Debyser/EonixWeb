using ApplicationCore.Entities;

namespace ApplicationCore.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        ValueTask<Contact> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}