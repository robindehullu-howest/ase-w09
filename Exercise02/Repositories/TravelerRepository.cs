using Microsoft.EntityFrameworkCore;

namespace Exercise02.Repositories;

public class TravelerRepository : Repository<Traveler>
{
    public TravelerRepository(TravelContext context) : base(context)
    {
    }

    public override async Task<Traveler> GetByIdAsync(int id)
    {
        return await _context.Travelers.Include(d => d.Destinations).FirstOrDefaultAsync(t => t.Id == id);
    }

    public override async Task<List<Traveler>> GetAllAsync()
    {
        return await _context.Travelers.Include(p => p.Passport).ToListAsync();
    }
}
