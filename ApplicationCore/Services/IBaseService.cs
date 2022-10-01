
namespace ApplicationCore.Services
{
    public interface IBaseService<T> where T : class
    {
        ValueTask<int> CreateAsync(T model, CancellationToken cancellationToken = default);
        ValueTask ModifyAsync(int personId, T model, CancellationToken cancellationToken = default);
        ValueTask DeleteIdAsync(int id, CancellationToken cancellationToken = default);
        ValueTask<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        //ValueTask<IEnumerable<T>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
        //ValueTask<IEnumerable<T>> GetByFilterAsync(T filter, CancellationToken cancellationToken = default);

    }
}
