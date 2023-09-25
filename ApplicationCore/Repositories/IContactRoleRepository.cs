using ApplicationCore.Entities;

namespace ApplicationCore.Repositories
{
    public interface IContactRoleRepository : IRepository<ContactRole>
    {
        void Add(IEnumerable<ContactRole> contactRoles);
        void Update(IEnumerable<ContactRole> prevContactRoles, IEnumerable<ContactRole> currentContactRoles);
        ValueTask<ContactRole> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<ContactRole>> GetListById(IEnumerable<long> idList, CancellationToken cancellationToken = default);
    }
}