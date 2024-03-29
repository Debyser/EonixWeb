﻿using ApplicationCore.Entities;
using System.Linq.Expressions;

namespace ApplicationCore.Repositories
{
    public interface IRepository<T> where T : class, IEntityBase, new()
    {
        void Add(T entity);
        void Remove(T entity);
        void RemoveById(long id);
        void Update(T entity);
        void SetDbContext(object context);
        ValueTask<T> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        ValueTask CommitAsync(CancellationToken cancellationToken = default);
        ValueTask RollbackAsync(CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        ValueTask<T> FindSingleByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    }
}