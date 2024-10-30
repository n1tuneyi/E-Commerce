namespace Application.Repositories;

public interface IGenericRepository<T>
{
    T Create(T entity);
    List<T> GetAll();
    T FindById(long entityID);
    void Update(T updatedEntity);
    void Remove(long entityId);
    void Remove(T entity);
    bool Exists(long entityID);
}
