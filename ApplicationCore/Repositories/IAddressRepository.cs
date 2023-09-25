using ApplicationCore.Entities;

namespace ApplicationCore.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        ValueTask<Address> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        void Update(Address prevAddress, Address currentAddress);
    }
}
