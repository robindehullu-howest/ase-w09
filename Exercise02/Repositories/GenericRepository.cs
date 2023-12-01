using Microsoft.EntityFrameworkCore;

namespace Exercise02.Repositories;

public interface IEntity
{
    int Id { get; }
}

public interface IRepository<T> where T : class, IEntity
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(int id);
}

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly TravelContext _context;

    public Repository(TravelContext context)
    {
        _context = context;
    }

    public virtual async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> CreateAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> DeleteAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}

