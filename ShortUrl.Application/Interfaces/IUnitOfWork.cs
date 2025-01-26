

namespace ShortUrl.Application.Interfaces
{
    public  interface IUnitOfWork
    {
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default);

        DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

        EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
               where TEntity : class;

        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(
        TEntity entity,
        CancellationToken cancellationToken = default)
        where TEntity : class;

        EntityEntry<TEntity> Attach<TEntity>(TEntity entity)
        where TEntity : class;

        EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        where TEntity : class;

        EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        where TEntity : class;

        EntityEntry Add(object entity);

        ValueTask<EntityEntry> AddAsync(
        object entity,
        CancellationToken cancellationToken = default);

        EntityEntry Attach(object entity);

        EntityEntry Update(object entity);

        void AddRange(params object[] entities);

        Task AddRangeAsync(params object[] entities);
        void AttachRange(params object[] entities);
        void UpdateRange(params object[] entities);

        void RemoveRange(params object[] entities);
        void AddRange(IEnumerable<object> entities);
        Task AddRangeAsync(
        IEnumerable<object> entities,
        CancellationToken cancellationToken = default);
        void AttachRange(IEnumerable<object> entities);
        void UpdateRange(IEnumerable<object> entities);
        void RemoveRange(IEnumerable<object> entities);
    }
}
