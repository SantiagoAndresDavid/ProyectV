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

public class FakeTechnoMechanicsRepository : ITechnoMechanicsRepository
{
    private List<TechnoMechanics> TechnoMechanicsList { get; }

    public FakeTechnoMechanicsRepository()
    {
        TechnoMechanicsList = new List<TechnoMechanics>();
    }

    public async Task Save(TechnoMechanics technoMechanics)
    {
        TechnoMechanicsList.Add(technoMechanics);
    }

    public async Task Delete(TechnoMechanics technoMechanics)
    {
        TechnoMechanicsList.Remove(technoMechanics);
    }

    public async Task Update(TechnoMechanics technoMechanics)
    {
        TechnoMechanicsList.Remove(TechnoMechanicsList.First(t => t.Id == technoMechanics.Id));
        TechnoMechanicsList.Add(technoMechanics);
    }

    public async Task<List<TechnoMechanics>> GetAll()
    {
        return TechnoMechanicsList;
    }

    public async Task<TechnoMechanics> GetTechnoMechanicsById(string id)
    {
        try
        {
            return TechnoMechanicsList.First(technoMechanics => technoMechanics.Id == Guid.Parse(id));
        }
        catch (InvalidOperationException e)
        {
            throw new NotFoundException("No se encontro la Tecnomecanica asociada.", e);
        }
    }
}

public class TechnoMechanicsTest
{
    [Test]
    public async Task TestThatVerifiesTechnoMechanicsIsSaved()
    {
        Car car2 = new Car("1234", "aprimal", "amarillo", "121-asda", 2);
        TechnoMechanics technoMechanics = new(Guid.NewGuid(), DateTime.Now, "asdasda", "dasdasdas",
            "asdasda", "dasdasd", car2);
        TechnoMechanicsServices technoMechanicsServices = new(new FakeTechnoMechanicsRepository());
        await technoMechanicsServices.Save(technoMechanics);
        Assert.AreEqual(technoMechanics,
            await technoMechanicsServices.GetTechnoMechanicsById(technoMechanics.Id.ToString()));
    }

    [Test]
    public async Task TestThatVerifiesTechnoMechanicsIsExist()
    {
        Car car2 = new Car("1234", "aprimal", "amarillo", "121-asda", 2);
        TechnoMechanics technoMechanics = new(Guid.NewGuid(), DateTime.Now, "asdasda", "dasdasdas",
            "asdasda", "dasdasd", car2);
        TechnoMechanicsServices technoMechanicsServices = new(new FakeTechnoMechanicsRepository());
        await technoMechanicsServices.Save(technoMechanics);
        Assert.ThrowsAsync<AlreadyExistsException>(async () => await technoMechanicsServices.Save(technoMechanics));
    }

    [Test]
    public async Task TestThatVerifiesIfTTripNotFound()
    {
        TechnoMechanicsServices technoMechanicsServices = new(new FakeTechnoMechanicsRepository());
        Assert.ThrowsAsync<NotFoundException>(async () => await technoMechanicsServices.GetTechnoMechanicsById("1234"));
    }

    [Test]
    public async Task TestThatVerifiesIfTheTechnoMechanicsIsDeleted()
    {
        Car car2 = new Car("1234", "aprimal", "amarillo", "121-asda", 2);
        TechnoMechanics technoMechanics = new(Guid.NewGuid(), DateTime.Now, "asdasda", "dasdasdas",
            "asdasda", "dasdasd", car2);
        TechnoMechanicsServices technoMechanicsServices = new(new FakeTechnoMechanicsRepository());
        await technoMechanicsServices.Save(technoMechanics);
        await technoMechanicsServices.Delete(technoMechanics);
        Assert.ThrowsAsync<NotFoundException>(async () => await technoMechanicsServices.GetTechnoMechanicsById(technoMechanics.Id.ToString()));
    }

    [Test]
    public async Task TestThatVerifiesIfTheTechnoMechanicsIsUpdated()
    {
        Guid guid = Guid.NewGuid();
        Car car2 = new Car("1234", "aprimal", "amarillo", "121-asda", 2);
        TechnoMechanics technoMechanics = new(guid, DateTime.Now, "asdasda", "dasdasdas",
            "asdasda", "dasdasd", car2);
        TechnoMechanics technoMechanicsModify = new(guid, DateTime.Now, "asdasda", "dasdasdas",
            "asdasda", "dasdasd", car2);
        TechnoMechanicsServices technoMechanicsServices = new(new FakeTechnoMechanicsRepository());
        await technoMechanicsServices.Save(technoMechanics);
        await technoMechanicsServices.Update(technoMechanicsModify);
        Assert.AreEqual(technoMechanicsModify, await technoMechanicsServices.GetTechnoMechanicsById(technoMechanicsModify.Id.ToString()));
    }

    [Test]
    public async Task TestThatVerifiesIfTheTechnoMechanicsIsReturned()
    {
        Car car2 = new Car("1234", "aprimal", "amarillo", "121-asda", 2);
        TechnoMechanics technoMechanics = new(Guid.NewGuid(), DateTime.Now, "asdasda", "dasdasdas",
            "asdasda", "dasdasd", car2);
        TechnoMechanics technoMechanics2= new(Guid.NewGuid(), DateTime.Now, "sgsfgdfgdfg", "bcvbcvbcvbcv",
            "gjkghjkhjgk", "yiutyuityu", car2);
        TechnoMechanicsServices technoMechanicsServices = new(new FakeTechnoMechanicsRepository());
        await technoMechanicsServices.Save(technoMechanics);
        await technoMechanicsServices.Save(technoMechanics2);
        List<TechnoMechanics> TestList = new List<TechnoMechanics>();
        TestList.Add(technoMechanics);
        TestList.Add(technoMechanics2);
        Assert.AreEqual(TestList,await technoMechanicsServices.GetAll());
    }
}