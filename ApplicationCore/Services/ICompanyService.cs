using WebApi.Models;

namespace ApplicationCore.Services
{
    public interface ICompanyService : IBaseService<Company>
    {
        ValueTask<IEnumerable<Company>> GetByFilterAsync(Company filter, CancellationToken cancellationToken = default);
        ValueTask<(IEnumerable<Company> companies, string ids)> CreateCompanyCollection(IEnumerable<Company> companyCollection, CancellationToken cancellationToken = default);
    }
}
