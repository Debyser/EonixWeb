using EonixWebApi.ApplicationCore.Entities;
using EonixWebApi.ApplicationCore.Repositories;
using EonixWebApi.Infrastructure.Data;

namespace Infrastructure.Data
{
    public class PersonRepository : DbRepository<Person>, IPersonRepository
    {
        public PersonRepository(EonixWebApiDbContext context) : base(context)
        {
        }
    }
}
