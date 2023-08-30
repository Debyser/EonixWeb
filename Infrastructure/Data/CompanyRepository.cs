using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class CompanyRepository : DbRepository<Company>, ICompanyRepository
    {
        private readonly EonixDbContext _context;
        private readonly IAddressRepository _addressRepository;

        public CompanyRepository(EonixDbContext context, IAddressRepository addressRepository) : base(context)
        {
            _context = context;
            _addressRepository = addressRepository;
            _addressRepository.SetDbContext(context);
        }

        // new : erase the Add from DbRepository
        public new void Add(Company entity)
        {
            _context.Add(entity);
            _addressRepository.Add(entity.Address);
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

        public async ValueTask<Company> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.Companies
                .Where(p => p.Id == id)
                .Include(p => p.ContactRoles).ThenInclude(x => x.Contact).ThenInclude(x => x.Address).ThenInclude(x => x.Country)
                .Include(p => p.Address)
                .Include(p => p.Address.Country)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}