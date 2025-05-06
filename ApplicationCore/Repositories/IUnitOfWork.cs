namespace ApplicationCore.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository AddressRepository { get; }
        Task SaveChangesAsync();
    }
}
