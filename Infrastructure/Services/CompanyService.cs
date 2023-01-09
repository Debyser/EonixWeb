using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using ApplicationCore.Services;
using Infrastructure.Entities.Exceptions;

namespace Infrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IContactRoleRepository _contactRoleRepository;

        public CompanyService(ICompanyRepository companyRepository, IAddressRepository addressRepository, IContactRoleRepository contactRoleRepository)
        {
            _companyRepository = companyRepository;
            _addressRepository = addressRepository;
            _contactRoleRepository = contactRoleRepository; 
        }

        public async ValueTask<long> CreateAsync(Company model, CancellationToken cancellationToken = default)
        {
            try
            {
                model.Id = 0;
                model.Address.Id = 0;
                model.Company2address = 0;
                _companyRepository.Add(model);
                await _companyRepository.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await _companyRepository.RollbackAsync(cancellationToken);
                throw;
            }
            return model.Id;
        }

 
        public async ValueTask DeleteIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var company = await GetByIdAsync(id, cancellationToken);
            if (company == null)
                throw new EntityNotFoundException(nameof(Company), id);// à vérifier , est ce que leservice connait les business exception ?
            company.Active = false;
            _companyRepository.Update(company); // dire que j'update que le champ Actif et pas toute l'entité
            // pas de tracking car lourd , sans tracking trouver comment maj un champ
            // lire code de Steph , minimum syndical
            await _companyRepository.CommitAsync(cancellationToken);
        }

        public async ValueTask<IEnumerable<Company>> GetByFilterAsync(Company filter, CancellationToken cancellationToken = default)
       => string.IsNullOrWhiteSpace(filter.Name) ?
               await GetAllAsync(cancellationToken) :
               await _companyRepository.GetByFilterAsync(filter, cancellationToken);

        public async ValueTask<Company> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var company = await _companyRepository.GetByIdAsync(id, cancellationToken);
            if (company == null)
                throw new EntityNotFoundException(nameof(Company), id);
            return company;
        }

        public async ValueTask<IEnumerable<Company>> GetByIdsAsync(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        {
            if (ids == null)
                throw new IdParametersBadRequestException();

            var companies = await _companyRepository.FindByConditionAsync(x => ids.Contains(x.Id), cancellationToken);
            if(ids.Count() != companies.Count())
                throw new CollectionByIdsBadRequestException();
            return companies;
        }

        public async ValueTask ModifyAsync(long id, Company model, CancellationToken cancellationToken = default)
        {
            var prevCompany = await GetByIdAsync(id, cancellationToken);
            prevCompany.Name = model.Name;
            _companyRepository.Update(prevCompany);
            await _companyRepository.CommitAsync(cancellationToken);
        }

        private async ValueTask<IEnumerable<Company>> GetAllAsync(CancellationToken cancellationToken = default)
            => (await _companyRepository.GetAllAsync(cancellationToken)).OrderBy(p => p.Name);

        public async ValueTask<(IEnumerable<Company> companies, string ids)> CreateCompanyCollection(IEnumerable<Company> companyCollection, CancellationToken cancellationToken = default)
        {
            if (companyCollection is null)
                throw new CompanyCollectionBadRequest();

            string ids = string.Empty;
            try
            {
                foreach (var company in companyCollection) _companyRepository.Add(company);
                await _companyRepository.CommitAsync(cancellationToken);
                ids = string.Join(",", companyCollection.Select(c => c.Id));
            }
            catch
            {
                await _companyRepository.RollbackAsync(cancellationToken);
                throw;
            }
            return (companies: companyCollection, ids);
        }

        public async ValueTask<Company> CreateCompanyAsync(Company company, CancellationToken cancellationToken = default)
        {

            try 
            {
                _companyRepository.Add(company);
                foreach (var contactRole in company.ContactRoles)
                {
                    contactRole.Company.Id = company.Id;
                    _contactRoleRepository.Add(contactRole);
                }
                
                await _companyRepository.CommitAsync(cancellationToken);
            }
            catch
            {
                await _companyRepository.RollbackAsync(cancellationToken);
                throw;
            }

            return company;
        }
    }
}