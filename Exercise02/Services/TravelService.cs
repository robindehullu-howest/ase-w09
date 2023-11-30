using System.ComponentModel;
using Org.BouncyCastle.Asn1.Crmf;

namespace Exercise03.Services;

public interface ITravelService
{
    Task<List<Traveler>> GetAllTravelersAsync();
    Task<Traveler> GetTravelerByIdAsync(int id);
    Task<List<Destination>> GetAllDestinationsAsync();
    Task<List<Guide>> GetAllGuidesAsync();
    Task<List<Tour>> GetAllToursAsync();
    Task AddTravelerAsync(Traveler traveler);
    Task AddDestinationAsync(Destination destination);
    Task AddTravelerToDestinationAsync(int travelerId, int destinationId);
    Task<List<Destination>> GetDestinationsByTravelerIdAsync(int travelerId);
    Task AddNewGuideAsync(Guide guide);
}

public class TravelService : ITravelService
{
    private readonly ITravelerRepository _travelerRepository;
    private readonly IDestinationRepository _destinationRepository;
    private readonly IPassportRepository _passportRepository;
    private readonly ITourRepository _tourRepository;
    private readonly IGuideRepository _guideRepository;

    public TravelService(TravelContext context)
    {
        _travelerRepository = new TravelerRepository(context);
        _destinationRepository = new DestinationRepository(context);
        _passportRepository = new PassportRepository(context);
        _tourRepository = new TourRepository(context);
        _guideRepository = new GuideRepository(context);
    }

    public async Task<List<Traveler>> GetAllTravelersAsync()
    {
        return await _travelerRepository.GetAllAsync();
    }

    public async Task<Traveler> GetTravelerByIdAsync(int id)
    {
        return await _travelerRepository.GetByIdAsync(id);
    }

    public async Task<List<Destination>> GetAllDestinationsAsync()
    {
        return await _destinationRepository.GetAllAsync();
    }

    public async Task<List<Guide>> GetAllGuidesAsync()
    {
        return await _guideRepository.GetAllAsync();
    }

    public async Task<List<Tour>> GetAllToursAsync()
    {
        return await _tourRepository.GetAllAsync();
    }

    public async Task AddTravelerAsync(Traveler traveler)
    {
        await _travelerRepository.CreateAsync(traveler);
    }

    public async Task AddDestinationAsync(Destination destination)
    {
        await _destinationRepository.CreateAsync(destination);
    }

    public async Task AddTravelerToDestinationAsync(int travelerId, int destinationId)
    {
        var traveler = await _travelerRepository.GetByIdAsync(travelerId);
        var destination = await _destinationRepository.GetByIdAsync(destinationId);

        if (traveler != null && destination != null)
        {
            if (traveler.Destinations == null)
            {
                traveler.Destinations = new List<Destination>();
            }

            traveler.Destinations.Add(destination);
            await _travelerRepository.UpdateAsync(traveler);
        }
    }

    public async Task<List<Destination>> GetDestinationsByTravelerIdAsync(int travelerId)
    {
        var traveler = await _travelerRepository.GetByIdAsync(travelerId);
        return traveler.Destinations;
    }

    public async Task AddNewGuideAsync(Guide guide)
    {
        await _guideRepository.CreateAsync(guide);
    }
}