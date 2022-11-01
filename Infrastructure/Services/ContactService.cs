using ApplicationCore.Repositories;
using ApplicationCore.Services;
using Infrastructure.Entities.Exceptions;
using WebApi.Models;

namespace Infrastructure.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;

        public ContactService(IContactRepository contactRepository, IAddressRepository addressRepository)
        {
            _repository = contactRepository;
        }
        public async ValueTask<int> CreateAsync(Contact model, CancellationToken cancellationToken = default)
        {
            _repository.Add(model);
            await _repository.CommitAsync(cancellationToken);
            return model.Id;
        }

        public async ValueTask DeleteIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var contact = await GetByIdAsync(id, cancellationToken);
            if (contact == null)
                throw new ContactNotFoundException(id);
            _repository.Remove(contact);
            await _repository.CommitAsync(cancellationToken);

        }

        public async ValueTask<Contact> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var contact = await _repository.FindByIdAsync(id, cancellationToken);
            if (contact == null)
                throw new ContactNotFoundException(id);
            return contact;
        }

        public async ValueTask ModifyAsync(int id, Contact model, CancellationToken cancellationToken = default)
        {
            var prevContact = await GetByIdAsync(id, cancellationToken);
            prevContact.Firstname = model.Firstname;
            prevContact.Lastname = model.Lastname;
            _repository.Update(prevContact);
            await _repository.CommitAsync(cancellationToken);
        }
    }
}
