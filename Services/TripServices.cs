using Data.Interfaces;
using Entity;
using Entity.Exceptions;

namespace Services;

public class TripServices
{
    private readonly ITripRepository _tripRepository;

    public TripServices(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task SaveTrip(Trip trip)
    {
        try
        {
            await GetTripById(trip.IdTrip.ToString());
        }
        catch (NotFoundException)
        {
            await _tripRepository.Save(trip);
            return;
        }

        throw new AlreadyExistsException("ya existe un Trayecto.");
    }

    public async Task<Trip> GetTripById(string id)
    {
        return await _tripRepository.GetTripbyId(id);
    }

    public async Task DeleteTrip(Trip trip)
    {
        await _tripRepository.Delete(trip);
    }

    public async Task<List<Trip>> GetAllTrip()
    {
        return await _tripRepository.GetAll();
    }

    public async Task UpdateTrip(Trip tripModify)
    {
        await _tripRepository.Update(tripModify);
    }
}