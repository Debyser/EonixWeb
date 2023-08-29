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

        public void Add(List<ContactRole> contactRoles, Contact contact)
        {
            if (contactRoles == null) return;
            foreach (var contactRole in contactRoles)
            {
                if (contactRole.CompanyId <= 0) continue;
                _context.Entry(contactRole.Company).State = EntityState.Unchanged;
                contactRole.Contact = contact;
                contactRole.Active = true;
                _context.Add(contactRole);
            }
        }
    }
}