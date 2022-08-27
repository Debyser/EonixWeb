using EonixWebApi.ApplicationCore.Entities;

namespace EonixWebApi.ApplicationCore.Repositories
{
    public interface IRepository<T> where T : class, IEntityBase, new()
    {
        void Add(T entity);
        void Remove(T entity);
        void RemoveById(Guid id);
        void Update(T entity);
        ValueTask<T> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        ValueTask<T> FindByFilterAsync(T filter, CancellationToken cancellationToken = default);
        ValueTask CommitAsync(CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
