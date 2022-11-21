using ApplicationCore.Entities;
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
            var company = await GetByIdAsync(id, cancellationToken);
            if (company == null)
                throw new CompanyNotFoundException(id);
            _companyRepository.Remove(company);
            await _companyRepository.CommitAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<Company>> GetByFilterAsync(Company filter, CancellationToken cancellationToken = default)
       => string.IsNullOrWhiteSpace(filter.Name) ?
               await GetAllAsync(cancellationToken) :
               await _companyRepository.GetByFilterAsync(filter, cancellationToken);

        public async ValueTask<Company> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var company = await _companyRepository.FindByIdAsync(id, cancellationToken);
            if (company == null)
                throw new CompanyNotFoundException(id);
            return company;
        }

        public async ValueTask ModifyAsync(int id, Company model, CancellationToken cancellationToken = default)
        {
            var prevCompany = await GetByIdAsync(id, cancellationToken);
            prevCompany.Name = model.Name;
            _companyRepository.Update(prevCompany);
            await _companyRepository.CommitAsync(cancellationToken);
        }

        private async ValueTask<IEnumerable<Company>> GetAllAsync(CancellationToken cancellationToken = default)
            => (await _companyRepository.GetAllAsync(cancellationToken)).OrderBy(p => p.Name);

    }
}