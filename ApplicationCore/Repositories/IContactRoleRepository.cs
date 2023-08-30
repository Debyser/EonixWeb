using ApplicationCore.Entities;

namespace ApplicationCore.Repositories
{
    public interface IContactRoleRepository : IRepository<ContactRole>
    {
        void Add(List<ContactRole> contactRoles);
    }
}
