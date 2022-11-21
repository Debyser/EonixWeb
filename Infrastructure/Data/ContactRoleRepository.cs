using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace Infrastructure.Data
{
    public class ContactRoleRepository : DbRepository<ContactRole> , IContactRoleRepository
    {
        private readonly EonixWebApiContext _context;

        public ContactRoleRepository(EonixWebApiContext context) : base(context)
        {
            _context = context;
        }

        public ValueTask<ContactRole> GetByAsync(ContactRole contactRole, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
