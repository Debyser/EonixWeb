using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public interface IContactService : IBaseService<Contact>
    {
        ValueTask<long> CreateEmployeeForCompany(long companyId, Contact contact, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<Contact>> GetListAsync();
    }
}
