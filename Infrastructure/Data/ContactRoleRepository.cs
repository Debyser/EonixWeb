using ApplicationCore.Entities;
using ApplicationCore.Repositories;

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
                if (role.Company == null || role.Company.Id == 0) continue;
                // Check if the company is already tracked
                var existingCompany = _context.Companies.Local.FirstOrDefault(c => c.Id == role.Company.Id);
                if (existingCompany == null)// Attach existing company
                    _context.Attach(role.Company);
                else // Use the existing tracked company
                    role.Company = existingCompany;
                _context.Add(role);
            }
        }
    }
}