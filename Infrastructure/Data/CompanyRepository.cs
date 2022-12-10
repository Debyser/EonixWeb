using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace Infrastructure.Data
{
    public class CompanyRepository : DbRepository<Company>, ICompanyRepository
    {
        private readonly EonixDbContext _context;
        private readonly IAddressRepository _addressRepository;
        private readonly IContactRoleRepository _contactRoleRepository;

        public CompanyRepository(EonixDbContext context, IAddressRepository addressRepository , IContactRoleRepository contactRoleRepository) : base(context)
        {
            _context = context;
            _addressRepository = addressRepository;
            _contactRoleRepository = contactRoleRepository;
            _addressRepository.SetDbContext(context);
            _contactRoleRepository.SetDbContext(context);
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

        public async ValueTask<Company> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Companies
                .Where(p => p.Id == id)
                .Include(p => p.Address)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}