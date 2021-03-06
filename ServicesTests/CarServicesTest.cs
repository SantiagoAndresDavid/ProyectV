using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Interfaces;
using Entity;
using Entity.Exceptions;
using NUnit.Framework;
using Services;

namespace ServicesTests
{
    public class FakeCarRepository : ICarRepository
    {
        private List<Car> Cars { get; }

        public FakeCarRepository()
        {
            Cars = new List<Car>();
        }


        public async Task Save(Car car)
        {
            Cars.Add(car);
        }

        public async Task Delete(Car car)
        {
            Cars.Remove(car);
        }

        public async Task Update(Car car)
        {
            Cars.Remove(Cars.First(c => c.Id == car.Id));
            Cars.Add(car);
        }

        public async Task<List<Car>> GetAll()
        {
            return Cars;
        }

        public async Task<Car> GetCarById(string idCar)
        {
            try
            {
                return Cars.First(c => c.Id == idCar);
            }
            catch (InvalidOperationException e)
            {
                throw new NotFoundException("No se encontro el vehiculo", e);
            }
        }
    }

    public class CarServicesTest
    {
        [Test]
        public async Task TestThatVerifiesIfTheCarIsSaved()
        {
            Car car = new Car("1234", "aprimal", "amarillo", "121-asda", 2);
            CarServices carServices = new(new FakeCarRepository());
            await carServices.SaveCar(car);
            Car carFound = await carServices.GetCarById(car.Id);
            Assert.AreEqual(car, carFound);
        }

        [Test]
        public void TestThatVerifiesIfCarIsNotFound()
        {
            CarServices carServices = new(new FakeCarRepository());
            Assert.ThrowsAsync<NotFoundException>(() => carServices.GetCarById("123"));
        }


        [Test]
        public async Task TestThatVerifiesIfCarAlreadyExists()
        {
            Car car = new Car("1234", "aprimal", "amarillo", "121-asda", 2);
            CarServices carServices = new(new FakeCarRepository());
            await carServices.SaveCar(car);
            Assert.ThrowsAsync<AlreadyExistsException>(() => carServices.SaveCar(car));
        }

        [Test]
        public async Task TestThatVerifiesIfTheCarIsDeleted()
        {
            CarServices carServices = new(new FakeCarRepository());
            Car car = new Car("1234", "aprimal", "amarillo", "121-asda", 2);
            await carServices.SaveCar(car);
            await carServices.DeleteCar(car);
            Assert.ThrowsAsync<NotFoundException>(async () => await carServices.GetCarById("121-asda"));
        }

        [Test]
        public async Task TestThatVerifiesIfTheCarIsUpdated()
        {
            CarServices carServices = new(new FakeCarRepository());
            Car carModify = new Car("9999", "asasdasd", "azul", "121-gdfg", 4);
            Car car = new Car("1234", "aprimal", "amarillo", "121-gdfg", 2);
            await carServices.SaveCar(car);
             await carServices.UpdateCar(carModify);
            Assert.AreEqual(carModify, await carServices.GetCarById(carModify.Id));
        }


        [Test]
        public async Task TestThatVerifiesIfTheCarIsReturned()
        {
            CarServices carServices = new(new FakeCarRepository());
            Car car = new Car("1234", "aprimal", "amarillo", "121-asda", 2);
            Car car2 = new Car("9999", "asasdasd", "azul", "121-gdfg", 4);
            await carServices.SaveCar(car);
            await carServices.SaveCar(car2);
            List<Car> carTest = new[] { car, car2 }.ToList();
            Assert.AreEqual(carTest, await carServices.GetAll());
        }
    }
}