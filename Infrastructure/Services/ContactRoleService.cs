using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using ApplicationCore.Services;
using Infrastructure.Entities.Exceptions;

namespace Infrastructure.Services
{
    public class ContactRoleService : IContactRoleService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IContactRoleRepository _contactRoleRepository;
        private readonly ICompanyRepository _companyRepository;


        public ContactRoleService(IContactRepository contactRepository, IContactRoleRepository contactRoleRepository, ICompanyRepository companyRepository)
        {
            _contactRoleRepository = contactRoleRepository;
            _contactRepository = contactRepository;
            _companyRepository = companyRepository;
        }
        public async ValueTask<int> CreateAsync(ContactRole model, CancellationToken cancellationToken = default)
        {
            model.Id = 0;
            var contact = await _contactRepository.GetByIdAsync(model.ContactId, cancellationToken);
            if (contact == null)
                throw new ContactNotFoundException(model.Id);

            var company = await _companyRepository.FindByIdAsync(model.CompanyId, cancellationToken);
            if (company == null)
                throw new CompanyNotFoundException(model.CompanyId);

            _contactRoleRepository.Add(model);
            await _contactRoleRepository.CommitAsync(cancellationToken);

            return model.Id;
        }

        public async ValueTask DeleteIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var contactRole = await _contactRoleRepository.FindByIdAsync(id, cancellationToken);
            if (contactRole == null)
                throw new ContactRoleNotFoundException(id);
            _contactRoleRepository.Remove(contactRole);
            await _contactRoleRepository.CommitAsync(cancellationToken);
        }

        public async ValueTask<ContactRole> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var contactRole = await _contactRoleRepository.FindByIdAsync(id, cancellationToken);
            if (contactRole == null)
                throw new ContactRoleNotFoundException(id);
            var company = await _companyRepository.FindByIdAsync(contactRole.CompanyId, cancellationToken);
            contactRole.Company = company;

            var contact = await _contactRepository.FindByIdAsync(contactRole.ContactId, cancellationToken);
            contactRole.Contact = contact;

            return contactRole;
        }

        public async ValueTask ModifyAsync(int id, ContactRole model, CancellationToken cancellationToken = default)
        {
            var prevCompany = await GetByIdAsync(id, cancellationToken);
            prevCompany.Name = model.Name;
            _contactRoleRepository.Update(prevCompany);
            await _companyRepository.CommitAsync(cancellationToken);
        }
    }
}