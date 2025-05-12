using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public interface IContactService : IBaseService<Contact>
    {
        ValueTask<long> CreateEmployeeForCompany(long companyId, Contact contact, CancellationToken cancellationToken = default);
        ValueTask<List<Contact>> GetListAsync(string name, CancellationToken cancellationToken = default);
        ValueTask ModifyAsync(Contact model, CancellationToken cancellationToken = default);
    }
}