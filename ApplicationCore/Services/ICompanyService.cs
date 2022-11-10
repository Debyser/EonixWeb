﻿using ApplicationCore.Entities;
using WebApi.Models;

namespace ApplicationCore.Services
{
    public interface ICompanyService : IBaseService<Company>
    {
        ValueTask<IEnumerable<Company>> GetByFilterAsync(Company filter, CancellationToken cancellationToken = default);
    }
}