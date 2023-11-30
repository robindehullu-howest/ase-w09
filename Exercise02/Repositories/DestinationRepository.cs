using Microsoft.EntityFrameworkCore;

namespace Exercise03.Repositories;

public interface IDestinationRepository
{
    Task<List<Destination>> GetAllAsync();
    Task<Destination> GetByIdAsync(int id);
    Task<Destination> CreateAsync(Destination destination);
    Task<Destination> UpdateAsync(Destination destination);
    Task<Destination> DeleteAsync(int id);
}

public class DestinationRepository : IDestinationRepository
{
    private TravelContext _context;

    public DestinationRepository(TravelContext context)
    {
        _context = context;
    }

    public async Task<List<Destination>> GetAllAsync()
    {
        return await _context.Destinations.ToListAsync();
    }

    public async Task<Destination> GetByIdAsync(int id)
    {
        return await _context.Destinations.FindAsync(id);
    }

    public async Task<Destination> CreateAsync(Destination destination)
    {
        _context.Destinations.Add(destination);
        await _context.SaveChangesAsync();
        return destination;
    }

    public async Task<Destination> UpdateAsync(Destination destination)
    {
        _context.Destinations.Update(destination);
        await _context.SaveChangesAsync();
        return destination;
    }

    public async Task<Destination> DeleteAsync(int id)
    {
        var destination = await _context.Destinations.FindAsync(id);
        _context.Destinations.Remove(destination);
        await _context.SaveChangesAsync();
        return destination;
    }
}