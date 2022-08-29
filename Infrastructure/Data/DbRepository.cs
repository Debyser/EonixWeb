using EonixWebApi.ApplicationCore.Entities;
using EonixWebApi.ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EonixWebApi.Infrastructure.Data
{
    public class DbRepository<T> : IRepository<T> where T : class, IEntityBase, new()
    {
        protected readonly DbContext DbContext;
        private readonly DbSet<T> _dbSet;

        protected DbRepository(DbContext context)
        {
            DbContext = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity) => _dbSet.Add(entity);

        public async ValueTask CommitAsync(CancellationToken cancellationToken = default) => await DbContext.SaveChangesAsync(cancellationToken);

        public async ValueTask<T> FindByIdAsync(Guid id, CancellationToken cancellationToken = default) => await _dbSet.FindAsync(new object[] { id }, cancellationToken);

        public async ValueTask<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) => await _dbSet.ToListAsync(cancellationToken);
        public void Remove(T entity) => _dbSet.Remove(entity);
        public void RemoveById(Guid id) => Remove(_dbSet.Find(id));
        public void Update(T entity) => DbContext.Entry(entity).State = EntityState.Modified;
    }
}