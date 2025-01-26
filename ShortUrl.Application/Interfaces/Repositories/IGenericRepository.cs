
namespace ShortUrl.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        ValueTask<T> FindByIdAsync(object id);
        IQueryable<T> FindAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        ValueTask<T> AddAsync(T entity, CancellationToken cancellationToken);
        ValueTask AddRangeAsync(List<T> entities, CancellationToken cancellationToken);
        T Update(T entity);
        T Remove(T entity);
        void RemoveRange(List<T> entities);
    }
}
