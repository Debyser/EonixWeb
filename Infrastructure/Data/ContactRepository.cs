using ApplicationCore.Repositories;
using System.Linq.Expressions;
using WebApi.Models;

namespace Infrastructure.Data
{
    public class ContactRepository : DbRepository<Country>, IContactRepository
    {
        private readonly EonixWebApiContext _context;

        public ContactRepository(EonixWebApiContext context) : base(context)
        {
            _context = context;
        }
        public void Add(Contact entity)
        {
            throw new NotImplementedException();
        }

        public ValueTask<IEnumerable<Contact>> FindByConditionAsync(Expression<Func<Contact, bool>> expression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Remove(Contact entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Contact entity)
        {
            throw new NotImplementedException();
        }

        ValueTask<Contact> IRepository<Contact>.FindByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        ValueTask<IEnumerable<Contact>> IRepository<Contact>.GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
