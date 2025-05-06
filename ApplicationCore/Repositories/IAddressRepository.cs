using ApplicationCore.Entities;

namespace ApplicationCore.Repositories
{
    public interface IAddressRepository
    {
        ValueTask<Address> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        void Update(Address prevAddress, Address currentAddress);
    }
}
