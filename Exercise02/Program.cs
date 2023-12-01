using Microsoft.EntityFrameworkCore;

bool exitRequested = false;

LogService logService = new();
TravelContext context = new(Console.WriteLine, logService.LogException);
TravelService service = new(context, Console.WriteLine, logService.LogException);
context.Database.EnsureCreated();

TravelerRepository travelerRepository = new(context);
Traveler t = await travelerRepository.GetByIdAsync(5);
Console.WriteLine(t);
foreach (var destination in t.Destinations)
{
    Console.WriteLine(destination);
}

while (!exitRequested)
{
    try
    {
    Console.WriteLine("\n[TravelDatabase]");
    Console.WriteLine("1. Show all travelers with passport number");
    Console.WriteLine("2. Show all destinations");
    Console.WriteLine("3. Show all guides and tours");
    Console.WriteLine("4. Add new traveler");
    Console.WriteLine("5. Add new destination");
    Console.WriteLine("6. Add traveler to a destination");
    Console.WriteLine("7. Show destinations for a traveler");
    Console.WriteLine("8. Add a new guide");
    Console.WriteLine("99. Exit");

    Console.WriteLine();
    Console.WriteLine("Please enter your choice: ");

    switch (Console.ReadLine())
    {
        case "1":
            await ShowAllTravelersWithPassportNumber();
            break;
        case "2":
            await ShowAllDestinations();
            break;
        case "3":
            await ShowAllGuidesAndTours();
            break;
        case "4":
            await AddNewTraveler();
            break;
        case "5":
            await AddNewDestination();
            break;
        case "6":
            await AddTravelerToDestination();
            break;
        case "7":
            await ShowDestinationsForTraveler();
            break;
        case "8":
            await AddNewGuide();
            break;
        case "99":
            exitRequested = true;
            return;
        default:
            Console.WriteLine("Invalid choice.");
            break;
    }

    Console.WriteLine();
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        logService.LogException($"[{DateTime.Now}] {ex.Message}\n{ex.StackTrace}");
    }
}

async Task ShowAllTravelersWithPassportNumber()
{
    Console.WriteLine();
    Console.WriteLine("Travelers with passport number: ");

    var travelers = await service.GetAllTravelersAsync();

    foreach (var traveler in travelers)
    {
        Console.WriteLine($"{traveler.Passport.Passportnumber} - {traveler.FullName}");
    }
}

async Task ShowAllDestinations()
{
    Console.WriteLine();
    Console.WriteLine("Destinations: ");

    var destinations = await service.GetAllDestinationsAsync();

    foreach (var destination in destinations)
    {
        Console.WriteLine(destination);
    }
}

async Task ShowAllGuidesAndTours()
{
    Console.WriteLine();
    Console.WriteLine("Guides: ");

    var guides = await service.GetAllGuidesAsync();

    foreach (var guide in guides)
    {
        Console.WriteLine(guide);
    }

    Console.WriteLine();
    Console.WriteLine("Tours: ");

    var tours = await service.GetAllToursAsync();

    foreach (var tour in tours)
    {
        Console.WriteLine(tour);
    }
}

async Task AddNewTraveler()
{
    Console.WriteLine();

    Console.WriteLine("Enter traveler's full name: ");
    string fullName = Console.ReadLine();

    Console.WriteLine("Enter traveler's passport number: ");
    string passportNumber = Console.ReadLine();

    Traveler traveler = new()
    {
        FullName = fullName,
        Passport = new Passport()
        {
            Passportnumber = passportNumber
        }
    };

    service.AddTravelerAsync(traveler);
}

async Task AddNewDestination()
{
    Console.WriteLine();
    Console.WriteLine("Enter destination's name: ");
    string name = Console.ReadLine();

    Destination destination = new()
    {
        Name = name
    };

    service.AddDestinationAsync(destination);
}

async Task AddTravelerToDestination()
{
    Console.WriteLine();
    Console.WriteLine("Enter traveler's id: ");
    int travelerId = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter destination's id: ");
    int destinationId = int.Parse(Console.ReadLine());

    await service.AddTravelerToDestinationAsync(travelerId, destinationId);
}

async Task ShowDestinationsForTraveler()
{
    Console.WriteLine();
    Console.WriteLine("Enter traveler's id: ");
    int travelerId = int.Parse(Console.ReadLine());

    var traveler = await service.GetTravelerByIdAsync(travelerId);

    Console.WriteLine();
    Console.WriteLine($"Destinations for traveler {traveler.FullName}: ");

    var destinations = await service.GetDestinationsByTravelerIdAsync(travelerId);

    foreach (var destination in destinations)
    {
        Console.WriteLine($" - {destination.Name}");
    }
}

async Task AddNewGuide()
{
    Console.WriteLine();
    Console.WriteLine("Enter guide's full name: ");
    string fullName = Console.ReadLine();

    Guide guide = new()
    {
        Name = fullName
    };

    await service.AddNewGuideAsync(guide);
}