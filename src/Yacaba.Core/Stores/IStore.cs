namespace Yacaba.Core.Stores {
    public interface IStore<TEntity, Tkey, TCreateRequest, TUpdateRequest> {

        Task<TEntity?> GetByIdAsync(Tkey id, CancellationToken cancellationToken = default);
        IQueryable<TEntity> GetAll();
        Task<TEntity> CreateAsync(TCreateRequest request, CancellationToken cancellationToken = default);
        Task UpdateAsync(TUpdateRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Tkey id, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
