using System.ComponentModel;
using Org.BouncyCastle.Asn1.Crmf;

namespace Exercise02.Services;

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
    private readonly IRepository<Traveler> _travelerRepository;
    private readonly IRepository<Destination> _destinationRepository;
    private readonly IRepository<Passport> _passportRepository;
    private readonly IRepository<Tour> _tourRepository;
    private readonly IRepository<Guide> _guideRepository;
    private readonly Action<string> _LogAction;
    private readonly Action<string> _LogException;

    public TravelService(TravelContext context, Action<string> LogAction, Action<string> LogException)
    {
        _LogAction = LogAction;
        _LogException = LogException;
        _travelerRepository = new TravelerRepository(context);
        _destinationRepository = new Repository<Destination>(context);
        _passportRepository = new Repository<Passport>(context);
        _tourRepository = new Repository<Tour>(context);
        _guideRepository = new Repository<Guide>(context);
    }

    public async Task<List<Traveler>> GetAllTravelersAsync()
    {
        try
        {
            _LogAction("Getting all travelers...");
            return await _travelerRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _LogException(ex.Message);
            throw;
        }
    }

    public async Task<Traveler> GetTravelerByIdAsync(int id)
    {
        try
        {
            _LogAction($"Getting traveler with id {id}...");
            return await _travelerRepository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _LogException(ex.Message);
            throw;
        }
    }

    public async Task<List<Destination>> GetAllDestinationsAsync()
    {
        try
        {
            _LogAction("Getting all destinations...");
            return await _destinationRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _LogException(ex.Message);
            throw;
        }
    }

    public async Task<List<Guide>> GetAllGuidesAsync()
    {
        try
        {
            _LogAction("Getting all guides...");
            return await _guideRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _LogException(ex.Message);
            throw;
        }
    }

    public async Task<List<Tour>> GetAllToursAsync()
    {
        try
        {
            _LogAction("Getting all tours...");
            return await _tourRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _LogException(ex.Message);
            throw;
        }
    }

    public async Task AddTravelerAsync(Traveler traveler)
    {
        try
        {
            _LogAction($"Adding traveler {traveler.FullName}...");
            await _travelerRepository.CreateAsync(traveler);
        }
        catch (Exception ex)
        {
            _LogException(ex.Message);
            throw;
        }
    }

    public async Task AddDestinationAsync(Destination destination)
    {
        try
        {
            _LogAction($"Adding destination {destination.Name}...");
            await _destinationRepository.CreateAsync(destination);
        }
        catch (Exception ex)
        {
            _LogException(ex.Message);
            throw;
        }
    }

    public async Task AddTravelerToDestinationAsync(int travelerId, int destinationId)
    {
        try
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
        catch (Exception ex)
        {
            _LogException(ex.Message);
            throw;
        }
    }

    public async Task<List<Destination>> GetDestinationsByTravelerIdAsync(int travelerId)
    {
        try
        {
            _LogAction($"Getting destinations for traveler with id {travelerId}...");
            var traveler = await _travelerRepository.GetByIdAsync(travelerId);
            return traveler.Destinations;
        }
        catch (Exception ex)
        {
            _LogException(ex.Message);
            throw;
        }
    }

    public async Task AddNewGuideAsync(Guide guide)
    {
        try
        {
            _LogAction($"Adding guide {guide.Name}...");
            await _guideRepository.CreateAsync(guide);
        }
        catch (Exception ex)
        {
            _LogException(ex.Message);
            throw;
        }
    }
}