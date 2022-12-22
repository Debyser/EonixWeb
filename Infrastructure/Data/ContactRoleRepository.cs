using ApplicationCore.Repositories;
using WebApi.Models;

namespace Infrastructure.Data
{
    public class ContactRoleRepository : DbRepository<ContactRole> , IContactRoleRepository
    {
        private readonly EonixDbContext _context;
        private readonly IContactRepository _contactRepository;

        public ContactRoleRepository(EonixDbContext context, IContactRepository contactRepository) : base(context)
        {
            _context = context;
            _contactRepository = contactRepository;
            _contactRepository.SetDbContext(context);
        }

        public new void Add(ContactRole entity) 
        {
            _context.Add(entity);
            _contactRepository.Add(entity.Contact);
        }

        public ValueTask<ContactRole> GetByAsync(ContactRole contactRole, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}