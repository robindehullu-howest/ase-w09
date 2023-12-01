using Microsoft.EntityFrameworkCore;

namespace Exercise02.Context;
public class TravelContext : DbContext
{
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Guide> Guides { get; set; }
    public DbSet<Passport> Passports { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<Traveler> Travelers { get; set; }

    private readonly Action<string> _LogAction;
    private readonly Action<string> _LogException;

    public TravelContext(Action<string> LogAction, Action<string> LogException) : base()
    {
        _LogAction = LogAction;
        _LogException = LogException;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        try
        {
            _LogAction("Configuring context...");
            optionsBuilder.UseMySQL("server=localhost;user=student;password=Qwerty123!;database=travels;");
        }
        catch (Exception ex)
        {
            _LogException(ex.Message);
            throw;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        try
        {
            _LogAction("Seeding data...");
            modelBuilder.Entity<Traveler>()
                .HasOne(t => t.Passport)
                .WithOne(p => p.Traveler)
                .HasForeignKey<Passport>(p => p.TravelerId);

            modelBuilder.Entity<Guide>()
                .HasMany(g => g.Tours)
                .WithOne(t => t.Guide)
                .HasForeignKey(t => t.GuideId);

            modelBuilder.Entity<Traveler>()
                .HasMany(t => t.Destinations)
                .WithMany(d => d.Travelers)
                .UsingEntity<TravelerDestination>();

            // Seed Travelers
            modelBuilder.Entity<Traveler>().HasData(
                new Traveler { Id = 1, FullName = "John Doe" },
                new Traveler { Id = 2, FullName = "Jane Doe" },
                new Traveler { Id = 3, FullName = "John Smith" },
                new Traveler { Id = 4, FullName = "Jane Smith" }
            );

            // Seed Passports
            modelBuilder.Entity<Passport>().HasData(
                new Passport { Id = 1, Passportnumber = "123456789", TravelerId = 1 },
                new Passport { Id = 2, Passportnumber = "987654321", TravelerId = 2 },
                new Passport { Id = 3, Passportnumber = "123123123", TravelerId = 3 },
                new Passport { Id = 4, Passportnumber = "456456456", TravelerId = 4 }
            );

            // Seed Guides
            modelBuilder.Entity<Guide>().HasData(
                new Guide { Id = 1, Name = "(G) John Doe" },
                new Guide { Id = 2, Name = "(G) Jane Doe" },
                new Guide { Id = 3, Name = "(G) John Smith" },
                new Guide { Id = 4, Name = "(G) Jane Smith" }
            );

            // Seed Tours
            modelBuilder.Entity<Tour>().HasData(
                new Tour { Id = 1, Title = "Tour 1", GuideId = 1 },
                new Tour { Id = 2, Title = "Tour 2", GuideId = 2 },
                new Tour { Id = 3, Title = "Tour 3", GuideId = 3 },
                new Tour { Id = 4, Title = "Tour 4", GuideId = 4 }
            );

            // Seed Destinations
            modelBuilder.Entity<Destination>().HasData(
                new Destination { Id = 1, Name = "New York City, New York" },
                new Destination { Id = 2, Name = "Los Angeles, California" },
                new Destination { Id = 3, Name = "Chicago, Illinois" },
                new Destination { Id = 4, Name = "Houston, Texas" }
            );

            // Seed TravelerDestinations
            modelBuilder.Entity<TravelerDestination>().HasData(
                new TravelerDestination { TravelerId = 1, DestinationId = 1 },
                new TravelerDestination { TravelerId = 1, DestinationId = 2 },
                new TravelerDestination { TravelerId = 2, DestinationId = 2 },
                new TravelerDestination { TravelerId = 2, DestinationId = 3 },
                new TravelerDestination { TravelerId = 3, DestinationId = 4 }
            );

            _LogAction("Data seeding completed.");
        }
        catch (Exception ex)
        {
            _LogException(ex.Message);
            throw;
        }
    }
}