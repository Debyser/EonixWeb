﻿using WebApi.Models;

namespace ApplicationCore.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        ValueTask<Address> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
