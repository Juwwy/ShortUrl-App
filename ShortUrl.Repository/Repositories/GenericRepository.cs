
namespace ShortUrl.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ShortUrlDbContext dbContext;

        public DbSet<T> Table { get; set; }
        public GenericRepository(ShortUrlDbContext dbContext)
        {
            this.dbContext = dbContext;
            Table = dbContext.Set<T>();
        }

        public async ValueTask<T> FindByIdAsync(object id)
        {
            return await Table.FindAsync(id);
        }

        public IQueryable<T> FindAll()
        {
            return Table.AsQueryable();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return Table.Where(predicate).AsQueryable<T>();
        }

        public async ValueTask<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            var e = await Table.AddAsync(entity, cancellationToken);
            return e.Entity;
        }

        public async ValueTask AddRangeAsync(List<T> entities, CancellationToken cancellationToken)
        {
            await Table.AddRangeAsync(entities, cancellationToken);
        }
        public T Update(T entity)
        {
            var u = Table.Update(entity);
            return u.Entity;
        }
        public T Remove(T entity)
        {
            var e = Table.Remove(entity);
            return e.Entity;
        }

        public void RemoveRange(List<T> entities)
        {
            Table.RemoveRange(entities);
        }
    }
}
