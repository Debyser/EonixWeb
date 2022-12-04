using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace Infrastructure.Data
{
    public class ContactRoleRepository : DbRepository<ContactRole> , IContactRoleRepository
    {
        private readonly EonixDbContext _context;

        public ContactRoleRepository(EonixDbContext context) : base(context)
        {
            _context = context;
        }

        public ValueTask<ContactRole> GetByAsync(ContactRole contactRole, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
