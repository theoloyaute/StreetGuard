using Back.Domain.Repositories.Interfaces;
using Back.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Back.Domain.Repositories;

public abstract class CommonRepository<T> : ICommonRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    
    protected CommonRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public void SaveChanges() => _context.SaveChanges();
    
    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
    
    public async Task<IEnumerable<T>> ListAsync() => await _context.Set<T>().ToListAsync();
    
    public async Task<T?> FindAsync(int id) => await _context.Set<T>().FindAsync(id).AsTask();
    
    public T Add(T item) => _context.Set<T>().Add(item).Entity;
    
    public T Update(T item) => _context.Set<T>().Update(item).Entity;
    
    public void Remove(T item) => _context.Set<T>().Remove(item);
}