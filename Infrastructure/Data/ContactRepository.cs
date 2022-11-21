using ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Models;

namespace Infrastructure.Data
{
    public class ContactRepository : DbRepository<Contact>, IContactRepository
    {
        private readonly IAddressRepository _addressRepository;
        private readonly EonixWebApiContext _context;

        public ContactRepository(EonixWebApiContext context) : base(context)
        {
            _context = context;
            _addressRepository = new AddressRepository(context);
        }

        // new : erase the Add from DbRepository
        public new void Add(Contact entity)
        {
            _context.Add(entity);
            _addressRepository.Add(entity.Address);
        }

        public void CreateContactForCompany(int companyId, Contact contact, CancellationToken cancellationToken = default)
        {
        }

        public async ValueTask<Contact> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Contacts
                .Where(p => p.Id == id)
                .Include(p => p.Address)
                .FirstOrDefaultAsync(cancellationToken);
        }

        void IContactRepository.CreateContactForCompany(int companyId, Contact contact, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        ValueTask<Contact> IContactRepository.GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}