using System.Data;
using Data.Interfaces;
using Entity;
using Entity.Exceptions;
using Hackedb;
using Hackedb.Contracts;

namespace Data;

public class TripRepository : Repository<Trip>, ITripRepository
{
    private readonly ICarRepository _carRepository;


    private readonly IDriverRepository _driverRepository;
    private List<Trip> TripList { get; }

    public TripRepository(IDbChannel dbChannel, IDriverRepository driverRepository, ICarRepository carRepository) :
        base(dbChannel)
    {
        _driverRepository = driverRepository;
        _carRepository = carRepository;
        TripList = new List<Trip>();
    }

    public async Task Save(Trip trip)
    {
        await _driverRepository.Save(trip.Driver);
        await _carRepository.Save(trip.Car);
        var query = "INTO viajes" +
                    "(id,numero_pasajeros)" +
                    "VALVUES (@0, @1)";
        await Insert(query, trip.IdTrip.ToString(), trip.NumberPassengers);
    }

    public async Task Delete(Trip trip)
    {
        var query = "FROM viajes WHERRE id = @0 ";
        await Delete(query,trip);
    }

    public async Task Update(Trip trip)
    {
        await _driverRepository.Update(trip.Driver);
        await _carRepository.Update(trip.Car);
        var query = "viajes SET" +
                    "id = @0,numero_pasajeros = @1";
        await Update(query, trip.IdTrip, trip.NumberPassengers);

    }

    public async Task<List<Trip>> GetAll()
    {
        return(await Select("* FROM viajes")).ToList();
    }

    public async Task<Trip> GetTripbyId(string id)
    {
        try
        {
            return (await Select("* FROM viajes WHERE id_viajes = @0", id)).First();
        }
        catch (InvalidOperationException e)
        {
            throw new NotFoundException("trayecto no encontrado", e);
        }
    }
    
    
    protected override Trip DefaultMap(IDataRecord record)
    {
        return new Trip
        {
            IdTrip = Guid.Parse(record.GetString(0)),
            NumberPassengers = record.GetInt32(1),
            Driver = new Driver
            {
                DocumentId = record.GetInt32(2),
                IdLicenceDriver = record.GetInt32(3),
                FirstName = record.GetString(4),
                SecondName = record.GetString(5),
                Surname = record.GetString(6),
                Photo = record.GetString(7)
            },
            Car = new Car
            {
                Id = record.GetString(0),
                LicensePlate = record.GetString(1),
                Model = record.GetString(2),
                Colour = record.GetString(3),
                NumSeating = record.GetInt32(4)
            }
        };
    }
}