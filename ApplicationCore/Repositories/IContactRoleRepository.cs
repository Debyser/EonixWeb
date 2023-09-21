using ApplicationCore.Entities;

namespace ApplicationCore.Repositories
{
    public interface IContactRoleRepository : IRepository<ContactRole>
    {
        void Add(List<ContactRole> contactRoles);
        ValueTask Update(IEnumerable<ContactRole> contactRoles);
        ValueTask<ContactRole> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<ContactRole>> GetListById(IEnumerable<long> idList, CancellationToken cancellationToken = default);
    }
}
