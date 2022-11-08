using ApplicationCore.Repositories;
using ApplicationCore.Services;
using Infrastructure.Entities.Exceptions;
using WebApi.Models;

namespace Infrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IAddressRepository _addressRepository;


        public CompanyService(ICompanyRepository companyRepository, IAddressRepository addressRepository)
        {
            _companyRepository = companyRepository;
            _addressRepository = addressRepository;
        }

        public async ValueTask<int> CreateAsync(Company model, CancellationToken cancellationToken = default)
        {
            model.Id = 0;
            model.Address.Id = 0;
            model.Company2address = 0;
            _addressRepository.Add(model.Address);
            await _addressRepository.CommitAsync(cancellationToken);

            _companyRepository.Add(model);
            await _companyRepository.CommitAsync(cancellationToken);

            return model.Id;
        }

        public async ValueTask DeleteIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var contact = await GetByIdAsync(id, cancellationToken);
            if (contact == null)
                throw new CompanyNotFoundException(id);
            _companyRepository.Remove(contact);
            await _companyRepository.CommitAsync(cancellationToken);
        }

        public async ValueTask<Company> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var country = await _companyRepository.FindByIdAsync(id, cancellationToken);
            if (country == null)
                throw new CompanyNotFoundException(id);
            return country;
        }

        public async ValueTask ModifyAsync(int id, Company model, CancellationToken cancellationToken = default)
        {
            var prevCompany = await GetByIdAsync(id, cancellationToken);
            prevCompany.Name = model.Name;
            prevCompany.Name = model.Name;
            _companyRepository.Update(prevCompany);
            await _companyRepository.CommitAsync(cancellationToken);
        }
    }
}