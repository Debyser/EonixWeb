using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Models;

namespace Infrastructure.Data
{
    public class ContactRepository : DbRepository<Contact>, IContactRepository
    {
        private readonly EonixWebApiContext _context;

        public ContactRepository(EonixWebApiContext context) : base(context)
        {
            _context = context;
        }

        public async ValueTask<Contact> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Contacts
                .Where(p => p.Id == id)
                .Include(p => p.Address)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}