using WebApi.Models;

namespace ApplicationCore.Repositories
{
    public interface IContactRoleRepository : IRepository<ContactRole>
    {
        ValueTask<ContactRole> GetByAsync(ContactRole contactRole, CancellationToken cancellationToken = default);

    }
}
