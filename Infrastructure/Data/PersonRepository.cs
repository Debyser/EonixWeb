using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class PersonRepository : DbRepository<Person>, IPersonRepository
    {
        public PersonRepository(EonixWebApiDbContext context) : base(context)
        {
        }

        public async ValueTask<IEnumerable<Person>> GetByFilterAsync(Person filter, CancellationToken cancellationToken = default) 
        {

            if (string.IsNullOrWhiteSpace(filter.LastName))
                return await ((EonixWebApiDbContext)DbContext).Persons
                .Where(p => p.FirstName.StartsWith(filter.FirstName) || p.FirstName.EndsWith(filter.FirstName))
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync(cancellationToken);

            if (string.IsNullOrWhiteSpace(filter.FirstName))
                return await ((EonixWebApiDbContext)DbContext).Persons
                .Where(p => p.LastName.StartsWith(filter.LastName) || p.LastName.EndsWith(filter.LastName))
                 .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync(cancellationToken);

            return 
                await ((EonixWebApiDbContext)DbContext).Persons
                .Where(p => (p.FirstName.StartsWith(filter.FirstName) || p.FirstName.EndsWith(filter.FirstName))
                 && (p.LastName.StartsWith(filter.LastName) || p.LastName.EndsWith(filter.LastName)))
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync(cancellationToken);
        }

        public ValueTask<IEnumerable<Person>> GetByIds(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
            => FindByConditionAsync(x => ids.Contains(x.Id), cancellationToken);
    }
}
