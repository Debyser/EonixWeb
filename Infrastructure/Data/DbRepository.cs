using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Data
{
    public class DbRepository<T> : IRepository<T> where T : class, IEntityBase, new()
    {
        private DbContext DbContext;
        protected readonly DbSet<T> _dbSet;

        protected DbRepository(DbContext context)
        {
            DbContext = context;
            _dbSet = context.Set<T>();
        }

        public List<string> Includes { get; private set; }

        public void SetDbContext(object context)
        {
            DbContext = context as DbContext;
        }

        public virtual void Add(T entity) => _dbSet.Add(entity);

        public async ValueTask CommitAsync(CancellationToken cancellationToken = default) => await DbContext.SaveChangesAsync(cancellationToken);

        public async ValueTask<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
            => await _dbSet.Where(expression).AsNoTracking().ToListAsync();

        public async ValueTask<T> FindSingleByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
            => await _dbSet.Where(expression).AsNoTracking().FirstOrDefaultAsync();

        public async ValueTask<T> FindByIdAsync(long id, CancellationToken cancellationToken = default) => await _dbSet.FindAsync(new object[] { (int)id }, cancellationToken);

        public async ValueTask<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

        public void Remove(T entity) => _dbSet.Remove(entity);
        public void RemoveById(long id) => Remove(_dbSet.Find((int)id));
        public virtual void Update(T entity) => DbContext.Entry(entity).State = EntityState.Modified;

        public async ValueTask RollbackAsync(CancellationToken cancellationToken = default) => await DbContext.Database.RollbackTransactionAsync(cancellationToken);


    }
}