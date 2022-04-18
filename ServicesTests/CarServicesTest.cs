
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Interfaces;
using Entity;
using NUnit.Framework;
using Services;

namespace ServicesTests
{

    public class FakeCarRepository : ICarRepository
    {
        public Task Save(Car entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(Car entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Car entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Car>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Car> GetCarById(string idCar)
        {
            throw new System.NotImplementedException();
        }
        
        
    }
    public class CarServicesTest
    {
        [Test]
        public async Task TestThatVerifiesIfTheUserIsSaved()
        {
            Car car = new Car("asda", "asdasd", "asdadsa", "asdasd12", 2);
            CarServices carService = new(new FakeCarRepository());
            await carService.GetCarById("1234");
        }
    }
}