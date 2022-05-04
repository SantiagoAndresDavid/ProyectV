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

public class FakeDriverRepository : IDriverRepository
{
    private List<Driver> DriverList { get; }

    public FakeDriverRepository()
    {
        DriverList = new List<Driver>();
    }


    public async Task Save(Driver driver)
    {
        DriverList.Add(driver);
    }

    public async Task Delete(Driver driver)
    {
        DriverList.Remove(driver);
    }

    public async Task Update(Driver driver)
    {
        DriverList.Remove(DriverList.First(d => d.DocumentId == driver.DocumentId));
        DriverList.Add(driver);
    }

    public async Task<List<Driver>> GetAll()
    {
        return DriverList;
    }

    public async Task<Driver> GetDriverByDocumentId(int documentId)
    {
        try
        {
            return DriverList.First(d => d.DocumentId == documentId);
        }
        catch (InvalidOperationException e)
        {
            throw new NotFoundException("No se encontro el Conductor", e);
        }
    }
}

public class DriverServicesTest
{
    [Test]
    public async Task TestThatVerifiesTheDriverIsSave()
    {
        Driver driver = new("santiago", "Andres", "David", 12324134, 4534534, "RUTA");
        DriverServices driverService = new DriverServices(new FakeDriverRepository());
        await driverService.SaveDriver(driver);
        Driver driverFound = await driverService.GetDriverByDocumentId(driver.DocumentId);
        Assert.AreEqual(driver, driverFound);
    }


    [Test]
    public async Task TestThatVerifiesTheDriverIsAlreadyExists()
    {
        Driver driver = new("santiago", "Andres", "David", 12324134, 4534534, "RUTA");
        DriverServices driverService = new DriverServices(new FakeDriverRepository());
        await driverService.SaveDriver(driver);
        Assert.ThrowsAsync<AlreadyExistsException>(async () => await driverService.SaveDriver(driver));
    }

    [Test]
    public async Task TestThatVerifiesTheDriversIsNotFound()
    {
        DriverServices driverServices = new DriverServices(new FakeDriverRepository());
        Assert.ThrowsAsync<NotFoundException>(() => driverServices.GetDriverByDocumentId(1234));
    }

    [Test]
    public async Task TestThatVerifiesTheDriverIsDeleted()
    {
        Driver driver = new("santiago", "Andres", "David", 12324134, 4534534, "RUTA");
        DriverServices driverServices = new DriverServices(new FakeDriverRepository());
        await driverServices.SaveDriver(driver);
        await driverServices.DeleteDriver(driver);
        Assert.ThrowsAsync<NotFoundException>(() => driverServices.GetDriverByDocumentId(driver.DocumentId));
    }

    [Test]
    public async Task TestThatVerifiesTheDriverIsUpdated()
    {
        Driver driver = new("santiago", "Andres", "David", 12324134, 4534534, "RUTA");
        Driver driverModify = new("santiago", "perez", "Gomez", 12324134, 4534534, "RUTA2");
        DriverServices driverServices = new DriverServices(new FakeDriverRepository());
        await driverServices.SaveDriver(driver);
        await driverServices.UpdateDriver(driverModify);
        Assert.AreEqual(driverModify, await driverServices.GetDriverByDocumentId(driverModify.DocumentId));
    }

    [Test]
    public async Task TestThatverifiesThatDriverIsReturned()
    {
        Driver driver = new("santiago", "Andres", "David", 12324134, 4534534, "RUTA");
        Driver driver2 = new("santiago", "perez", "Gomez", 1232423 , 4534534, "RUTA2");
        List<Driver> driverListTest = new List<Driver>();
        DriverServices driverServices = new DriverServices(new FakeDriverRepository());
        await driverServices.SaveDriver(driver);
        await driverServices.SaveDriver(driver2);
        driverListTest.Add(driver);
        driverListTest.Add(driver2);
        Assert.AreEqual(driverListTest,await driverServices.GetAll());
    }
}