using ApplicationCore.Entities;
using System.Linq.Expressions;

namespace ApplicationCore.Repositories
{
    public interface IRepository<T> where T : class, IEntityBase, new()
    {
        void Add(T entity);
        void Remove(T entity);
        void RemoveById(int id);
        void Update(T entity);
        List<string> Includes { get; }
        void AddInclude(string include);
        ValueTask<T> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        ValueTask CommitAsync(CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    }
}
