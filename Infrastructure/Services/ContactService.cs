using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Repositories;
using ApplicationCore.Services;

namespace Infrastructure.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IContactRoleRepository _contactRoleRepository;


        public ContactService(IContactRepository contactRepository, IContactRoleRepository contactRoleRepository)
        {
            _contactRepository = contactRepository;
            _contactRoleRepository = contactRoleRepository;
        }

        public async ValueTask<long> CreateAsync(Contact contact, CancellationToken cancellationToken = default)
        {
            try
            {
                contact.Id = 0;
                contact.Address.Id = 0;
                contact.AddressId = 0;
                contact.Address.Country = null; // to prevent to add a country
                contact.Active = true;
                _contactRepository.Add(contact);
                await _contactRepository.CommitAsync(cancellationToken);

                //// Maintenant que le contact principal a un ID, vous pouvez créer les ContactRoles associés
                //foreach (var contactRole in contact.ContactRoles)
                //{
                //    // Assurez-vous que l'ID de la company existe (correspond à une company existante dans la base de données)
                //    if (contactRole.Company.Id > 0)
                //    {
                //        contactRole.Contact = contact;
                //        contactRole.Active = true;
                //        _contactRoleRepository.Add(contactRole);
                //    }
                //}

                //await _contactRoleRepository.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await _contactRepository.RollbackAsync(cancellationToken);
                throw;
            }
            return contact.Id;
        }

        public async ValueTask<long> CreateEmployeeForCompany(long companyId, Contact contact, CancellationToken cancellationToken = default)
        {
            try
            {
                contact.Id = 0;
                contact.Address.Id = 0;
                contact.AddressId = 0;
                _contactRepository.Add(contact);
                await _contactRepository.CommitAsync(cancellationToken);
            }
            catch
            {
                await _contactRepository.RollbackAsync(cancellationToken);
                throw;
            }
            return contact.Id;
        }

        public async ValueTask DeleteIdAsync(long id, CancellationToken cancellationToken = default)
        {
            try
            {
                var contact = await _contactRepository.GetByIdAsync(id, cancellationToken);
                contact.Active = false;
                _contactRepository.Update(contact);
                await _contactRepository.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await _contactRepository.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async ValueTask<Contact> GetByIdAsync(long id, CancellationToken cancellationToken = default)
            => await _contactRepository.GetByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException(typeof(Contact), id);

        public async ValueTask ModifyAsync(long id, Contact model, CancellationToken cancellationToken = default)
        {
            try
            {
                var prevContact = await _contactRepository.GetByIdAsync(id, cancellationToken);
                prevContact.Firstname = model.Firstname;
                prevContact.Lastname = model.Lastname;
                prevContact.Address.BoxNumber = model.Address.BoxNumber;
                prevContact.Address.Zipcode = model.Address.Zipcode;
                prevContact.Address.City = model.Address.City;
                prevContact.Address.Country.Iso3Code = model.Address.Country.Iso3Code;
                prevContact.Address.Country.Name = model.Address.Country.Name; ;

                _contactRepository.Update(prevContact);
                await _contactRepository.CommitAsync(cancellationToken);
            }
            catch
            {
                await _contactRepository.RollbackAsync(cancellationToken);
                throw;
            }
        }

        //TODO: create IActionResult GetEmployeesForCompany(Guid companyId)
    }
}