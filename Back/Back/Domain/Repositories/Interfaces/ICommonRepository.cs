namespace Back.Domain.Repositories.Interfaces;

public interface ICommonRepository<T>
{
    void SaveChanges();
    Task<int> SaveChangesAsync();
    Task<IEnumerable<T>> ListAsync();
    Task<T?> FindAsync(int id);
    T Add(T item);
    T Update(T item);
    void Remove(T item);
}