
namespace ApplicationCore.Services
{
    public interface IBaseService<T> where T : class
    {
        ValueTask<long> CreateAsync(T model, CancellationToken cancellationToken = default);
        ValueTask ModifyAsync(long id, T model, CancellationToken cancellationToken = default);
        ValueTask DeleteIdAsync(long id, CancellationToken cancellationToken = default);
        ValueTask<T> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    }
}
