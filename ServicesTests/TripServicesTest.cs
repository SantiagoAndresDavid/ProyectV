using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Interfaces;
using Entity;
using Entity.Exceptions;
using NUnit.Framework;
using Services;

namespace ServicesTests;

public class FakeTripRespository : ITripRepository
{
    private List<Trip> TripList { get; }


    public FakeTripRespository()
    {
        TripList = new List<Trip>();
    }

    public async Task Save(Trip trip)
    {
        TripList.Add(trip);
    }

    public async Task Delete(Trip trip)
    {
        TripList.Remove(trip);
    }

    public async Task Update(Trip trip)
    {
        TripList.Remove(TripList.First(t => t.IdTrip == trip.IdTrip));
        TripList.Add(trip);
    }

    public async Task<List<Trip>> GetAll()
    {
        return TripList;
    }

    public async Task<Trip> GetTripbyId(string id)
    {
        try
        {
            return TripList.First(trip => trip.IdTrip == Guid.Parse(id));
        }
        catch (InvalidOperationException e)
        {
            throw new NotFoundException("No se encontro el trayecto", e);
        }
    }
}

public class TipServicesTest
{
    [Test]
    public async Task TestThatVerifiesTripIsSaved()
    {
        Driver driver = new("santiago", "Andres", "David", 12324134, 4534534, "RUTA");
        Car car = new Car("1234", "aprimal", "amarillo", "121-asda", 2);
        Trip trip = new(Guid.NewGuid(), 2, driver, car);
        TripServices tripServices = new(new FakeTripRespository());
        await tripServices.SaveTrip(trip);
        Trip tripFound = await tripServices.GetTripById(trip.IdTrip.ToString());
        Assert.AreEqual(trip, tripFound);
    }

    [Test]
    public async Task TestThatVerifiesTripAlreadyExists()
    {
        Driver driver = new("santiago", "Andres", "David", 12324134, 4534534, "RUTA");
        Car car = new Car("1234", "aprimal", "amarillo", "121-asda", 2);
        Trip trip = new(Guid.NewGuid(), 2, driver, car);
        TripServices tripServices = new(new FakeTripRespository());
        await tripServices.SaveTrip(trip);
        Assert.ThrowsAsync<AlreadyExistsException>(async () => await tripServices.SaveTrip(trip));
    }

    [Test]
    public async Task TestThatVerifiesIfTheTripIsDeleted()
    {
        Driver driver = new("santiago", "Andres", "David", 12324134, 4534534, "RUTA");
        Car car = new Car("1234", "aprimal", "amarillo", "121-asda", 2);
        Trip trip = new(Guid.NewGuid(), 2, driver, car);
        TripServices tripServices = new(new FakeTripRespository());
        await tripServices.SaveTrip(trip);
        await tripServices.DeleteTrip(trip);
        string id = trip.IdTrip.ToString();
        Assert.ThrowsAsync<NotFoundException>(async () => await tripServices.GetTripById(id));
    }

    [Test]
    public async Task TestThatVerifiesIfTheTripIsUpdated()
    {
        Driver driver = new("santiago", "Andres", "David", 12324134, 4534534, "RUTA");
        Car car = new Car("1234", "aprimal", "amarillo", "121-asda", 2);
        Trip trip = new(Guid.NewGuid(), 2, driver, car);
        TripServices tripServices = new(new FakeTripRespository());
    }

    [Test]
    public async Task TestThatVerifiesIfTheTripAreReturned()
    {
        Driver driver = new("santiago", "Andres", "David", 12324134, 4534534, "RUTA");
        Car car = new Car("1234", "aprimal", "amarillo", "121-asda", 2);
        Trip trip = new(Guid.NewGuid(), 2, driver, car);
        Trip trip2 = new(Guid.NewGuid(),5,driver, car);
        TripServices tripServices = new(new FakeTripRespository());
        await tripServices.SaveTrip(trip);
        await tripServices.SaveTrip(trip2);
        List<Trip> TestList = new List<Trip>();
        TestList.Add(trip);
        TestList.Add(trip2);
        Assert.AreEqual(TestList, await tripServices.GetAllTrip());
    } 
}