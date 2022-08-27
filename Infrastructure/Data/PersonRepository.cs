using EonixWebApi.ApplicationCore.Entities;
using EonixWebApi.ApplicationCore.Repositories;
using EonixWebApi.Infrastructure.Data;
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
            return 
                await ((EonixWebApiDbContext)DbContext).Persons
                .Where(p => (p.FirstName.StartsWith(filter.FirstName) || p.FirstName.EndsWith(filter.FirstName))
                 && (p.LastName.StartsWith(filter.LastName) || p.LastName.EndsWith(filter.LastName))).ToListAsync();
        }
    }
}
