using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public interface ICompanyService : IBaseService<Company>
    {
        ValueTask<IEnumerable<Company>> GetAllAsync();
        ValueTask<IEnumerable<Company>> GetByIdsAsync(IEnumerable<long> ids, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<Company>> GetByFilterAsync(Company filter, CancellationToken cancellationToken = default);
        ValueTask<(IEnumerable<Company> companies, string ids)> CreateCompanyCollection(IEnumerable<Company> companyCollection, CancellationToken cancellationToken = default);
        ValueTask<Company> CreateCompanyAsync(Company company, CancellationToken cancellationToken = default);
        ValueTask<Company> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    }
}
