using Microsoft.EntityFrameworkCore;

namespace Exercise03.Repositories;

public interface IGuideRepository
{
    Task<List<Guide>> GetAllAsync();
    Task<Guide> GetByIdAsync(int id);
    Task<Guide> CreateAsync(Guide guide);
    Task<Guide> UpdateAsync(Guide guide);
    Task<Guide> DeleteAsync(int id);
}

public class GuideRepository : IGuideRepository
{
    private TravelContext _context;

    public GuideRepository(TravelContext context)
    {
        _context = context;
    }

    public async Task<List<Guide>> GetAllAsync()
    {
        return await _context.Guides.Include(g => g.Tours).ToListAsync();
    }

    public async Task<Guide> GetByIdAsync(int id)
    {
        return await _context.Guides.FindAsync(id);
    }

    public async Task<Guide> CreateAsync(Guide guide)
    {
        _context.Guides.Add(guide);
        await _context.SaveChangesAsync();
        return guide;
    }

    public async Task<Guide> UpdateAsync(Guide guide)
    {
        _context.Guides.Update(guide);
        await _context.SaveChangesAsync();
        return guide;
    }

    public async Task<Guide> DeleteAsync(int id)
    {
        var guide = await _context.Guides.FindAsync(id);
        _context.Guides.Remove(guide);
        await _context.SaveChangesAsync();
        return guide;
    }
}