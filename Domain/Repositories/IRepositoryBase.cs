using System.Linq.Expressions;

namespace Domain.Repositories;

public interface IRepositoryBase<TEntity>
{
    IQueryable<TEntity> FindAll(bool trackChanges);
    IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);

    Task Save();
}
