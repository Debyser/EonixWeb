
namespace ApplicationCore.Services
{
    public interface IBaseService<T> where T : class
    {
        ValueTask<int> CreateAsync(T model, CancellationToken cancellationToken = default);
        ValueTask ModifyAsync(int id, T model, CancellationToken cancellationToken = default);
        ValueTask DeleteIdAsync(int id, CancellationToken cancellationToken = default);
        ValueTask<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    }
}
