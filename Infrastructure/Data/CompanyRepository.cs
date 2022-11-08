using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace Infrastructure.Data
{
    public class CompanyRepository : DbRepository<Company>, ICompanyRepository
    {
        private readonly EonixWebApiContext _context;
        public CompanyRepository(EonixWebApiContext context) : base(context)
        {
            _context = context;
        }

        public async ValueTask<Company> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Companies
                .Where(p => p.Id == id)
                .Include(p => p.Address)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}