using Domain.Contracts;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repository = new Dictionary<string, object>();
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var TypeName = typeof(TEntity).Name;
            // if Repository already exists, return it
            if (_repository.ContainsKey(TypeName))
            {
                return (IGenericRepository<TEntity, TKey>) _repository[TypeName];
            }
            // else create new Repository and add it to the dictionary
            var repository = new GenericRepository<TEntity, TKey>(_dbContext);
            // Store instance of the repository
            _repository[TypeName] = repository;
            // Return the repository
            return repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
