using Microsoft.EntityFrameworkCore;

namespace Exercise02.Repositories;

public interface IPassportRepository
{
    Task<Passport> GetByIdAsync(int id);
    Task<Passport> GetByPassportNumberAsync(string passportNumber);
    Task<Passport> CreateAsync(Passport passport);
    Task<Passport> UpdateAsync(Passport passport);
    Task DeleteAsync(int id);
}

public class PassportRepository : IPassportRepository
{
    private readonly TravelContext _context;

    public PassportRepository(TravelContext context)
    {
        _context = context;
    }

    public async Task<Passport> GetByIdAsync(int id)
    {
        return await _context.Passports.FindAsync(id);
    }

    public async Task<Passport> GetByPassportNumberAsync(string passportNumber)
    {
        return await _context.Passports
            .Where(p => p.Passportnumber == passportNumber)
            .FirstOrDefaultAsync();
    }

    public async Task<Passport> CreateAsync(Passport passport)
    {
        _context.Passports.Add(passport);
        await _context.SaveChangesAsync();
        return passport;
    }

    public async Task<Passport> UpdateAsync(Passport passport)
    {
        _context.Passports.Update(passport);
        await _context.SaveChangesAsync();
        return passport;
    }

    public async Task DeleteAsync(int id)
    {
        var passport = await _context.Passports.FindAsync(id);
        _context.Passports.Remove(passport);
        await _context.SaveChangesAsync();
    }
}