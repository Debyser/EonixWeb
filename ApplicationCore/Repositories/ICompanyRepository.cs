using WebApi.Models;

namespace ApplicationCore.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        ValueTask<Company> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    }
}
