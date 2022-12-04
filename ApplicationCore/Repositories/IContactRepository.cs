﻿using WebApi.Models;

namespace ApplicationCore.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        ValueTask<Contact> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}