using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey>? specifications); // with specification
        Task<TEntity> GetByIdAsync(Tkey id);
        Task<TEntity> GetByIdAsync(ISpecification<TEntity, Tkey> specifications); // with specification
        Task<int> CountAsync(ISpecification<TEntity, Tkey> specifications);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
