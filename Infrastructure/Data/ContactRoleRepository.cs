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

        public void Add(List<ContactRole> contactRoles)
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

        public async ValueTask Update(IEnumerable<ContactRole> contactRoles)
        {
            if (contactRoles == null) return;

            var prevContactRoles = await GetListById(contactRoles.Select(x => x.Id).ToArray());
            if (prevContactRoles == null) return;
            foreach (var prevRole in prevContactRoles)
            {
                var currentContacRole = contactRoles.FirstOrDefault(p => p.Id == prevRole.Id);
                if (currentContacRole == null) continue;
                AttachOrUpdateCompany(prevRole);
                prevRole.Name = currentContacRole.Name;
                _context.Update(prevRole);
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