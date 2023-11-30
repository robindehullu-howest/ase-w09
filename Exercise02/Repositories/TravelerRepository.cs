using Microsoft.EntityFrameworkCore;

namespace Exercise03.Repositories;

public interface ITravelerRepository
{
    Task<List<Traveler>> GetAllAsync();
    Task<Traveler> GetByIdAsync(int id);
    Task<Traveler> CreateAsync(Traveler traveler);
    Task<Traveler> UpdateAsync(Traveler traveler);
    Task DeleteAsync(int id);
}

public class TravelerRepository : ITravelerRepository
{
    private readonly TravelContext _context;

    public TravelerRepository(TravelContext context)
    {
        _context = context;
    }

    public async Task<List<Traveler>> GetAllAsync()
    {
        return await _context.Travelers.Include(p => p.Passport).ToListAsync();
    }

    public async Task<Traveler> GetByIdAsync(int id)
    {
        return await _context.Travelers.Include(t => t.Destinations).FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Traveler> CreateAsync(Traveler traveler)
    {
        _context.Travelers.Add(traveler);
        await _context.SaveChangesAsync();
        return traveler;
    }

    public async Task<Traveler> UpdateAsync(Traveler traveler)
    {
        _context.Travelers.Update(traveler);
        await _context.SaveChangesAsync();
        return traveler;
    }

    public async Task DeleteAsync(int id)
    {
        var traveler = await _context.Travelers.FindAsync(id);
        _context.Travelers.Remove(traveler);
        await _context.SaveChangesAsync();
    }

}