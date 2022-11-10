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

        public async ValueTask<IEnumerable<Company>> GetByFilterAsync(Company filter, CancellationToken cancellationToken = default)
        {
            return (!string.IsNullOrWhiteSpace(filter.Name)) ?
                await _context.Companies
                .Where(p => p.Name.StartsWith(filter.Name))
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken) :
                await _context.Companies.ToListAsync();
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