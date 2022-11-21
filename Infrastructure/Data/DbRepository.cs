using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Data
{
    public class DbRepository<T> : IRepository<T> where T : class, IEntityBase, new()
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<T> _dbSet;

        protected DbRepository(DbContext context)
        {
            DbContext = context;
            _dbSet = context.Set<T>();
        }

        public List<string> Includes { get; private set; }

        public virtual void Add(T entity) => _dbSet.Add(entity);

        public async ValueTask CommitAsync(CancellationToken cancellationToken = default) => await DbContext.SaveChangesAsync(cancellationToken);

        public async ValueTask<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
            => await _dbSet.Where(expression).AsNoTracking().ToListAsync();

        public async ValueTask<T> FindByIdAsync(int id, CancellationToken cancellationToken = default) => await _dbSet.FindAsync(new object[] { id }, cancellationToken);

        public async ValueTask<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _dbSet.ToListAsync(cancellationToken);
        public void Remove(T entity) => _dbSet.Remove(entity);
        public void RemoveById(int id) => Remove(_dbSet.Find(id));
        public void Update(T entity) => DbContext.Entry(entity).State = EntityState.Modified;

        public void AddInclude(string include)
        {
            if (Includes == null) Includes = new List<string>();
            Includes.Add(include);
        }

        public async ValueTask RollbackAsync(CancellationToken cancellationToken = default) 
            => await DbContext.Database.RollbackTransactionAsync(cancellationToken);
    }
}