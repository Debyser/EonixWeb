﻿using ApplicationCore.Entities;

namespace ApplicationCore.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        ValueTask<Company> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<Company>> GetByFilterAsync(Company filter, CancellationToken cancellationToken = default);

        ValueTask<IEnumerable<Company>> GetAllAsync(CancellationToken cancellationToken = default);

    }
}