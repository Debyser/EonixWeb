using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public interface IContactService : IBaseService<Contact>
    {
        ValueTask<int> CreateEmployeeForCompany(int companyId, Contact contact, CancellationToken cancellationToken = default);
    }
}
