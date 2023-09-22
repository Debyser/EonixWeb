using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ContactRoleRepository : DbRepository<ContactRole>, IContactRoleRepository
    {
        private readonly EonixDbContext _context;

        public ContactRoleRepository(EonixDbContext context) : base(context)
        {
            _context = context;
        }

        public void Add(IEnumerable<ContactRole> contactRoles)
        {
            foreach (var role in contactRoles)
            {
                AttachOrUpdateCompany(role);
                _context.Add(role);
            }
        }

        public ValueTask<ContactRole> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Update(IEnumerable<ContactRole> prevContactRoles, IEnumerable<ContactRole> currentContactRoles)
        {
            if (currentContactRoles == null) return;
            foreach (var role in currentContactRoles)
            {
                var prevContacRole = prevContactRoles.FirstOrDefault(p => p.Id == role.Id);
                if (prevContacRole == null) continue;
                Update(prevContacRole);
                prevContacRole.Name = role.Name;
                prevContacRole.Active = role.Active;
            }
        }

        public async ValueTask<IEnumerable<ContactRole>> GetListById(IEnumerable<long> idList, CancellationToken cancellationToken = default)
            => await _context.ContactRoles.Where(p => idList.Contains(p.Id)).AsNoTracking().ToListAsync(cancellationToken);

        private void AttachOrUpdateCompany(ContactRole role)
        {
            if (role.Company == null || role.Company.Id == 0) return;

            var existingCompany = _context.Companies.Local.FirstOrDefault(c => c.Id == role.Company.Id);
            if (existingCompany == null)
                _context.Attach(role.Company);
            else
                role.Company = existingCompany;
        }
    }
}