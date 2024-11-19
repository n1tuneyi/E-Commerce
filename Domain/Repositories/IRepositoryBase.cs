using System.Linq.Expressions;

namespace Domain.Repositories;

public interface IRepositoryBase<TEntity>
{
    IQueryable<TEntity> FindAll(bool trackChanges);
    IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges);
    TEntity Create(TEntity entity);
    TEntity Delete(TEntity entity);
    TEntity Update(TEntity entity);
}
