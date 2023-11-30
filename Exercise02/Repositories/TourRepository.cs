using Microsoft.EntityFrameworkCore;

namespace Exercise03.Repositories;

public interface ITourRepository
{
    Task<List<Tour>> GetAllAsync();
    Task<Tour> GetByIdAsync(int id);
    Task<Tour> CreateAsync(Tour tour);
    Task<Tour> UpdateAsync(Tour tour);
    Task<Tour> DeleteAsync(int id);
}

public class TourRepository : ITourRepository
{
    private TravelContext _context;

    public TourRepository(TravelContext context)
    {
        _context = context;
    }

    public async Task<List<Tour>> GetAllAsync()
    {
        return await _context.Tours.ToListAsync();
    }

    public async Task<Tour> GetByIdAsync(int id)
    {
        return await _context.Tours.FindAsync(id);
    }

    public async Task<Tour> CreateAsync(Tour tour)
    {
        _context.Tours.Add(tour);
        await _context.SaveChangesAsync();
        return tour;
    }

    public async Task<Tour> UpdateAsync(Tour tour)
    {
        _context.Tours.Update(tour);
        await _context.SaveChangesAsync();
        return tour;
    }

    public async Task<Tour> DeleteAsync(int id)
    {
        var tour = await _context.Tours.FindAsync(id);
        _context.Tours.Remove(tour);
        await _context.SaveChangesAsync();
        return tour;
    }
}