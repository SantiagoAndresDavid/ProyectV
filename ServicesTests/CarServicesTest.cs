
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Interfaces;
using Entity;

namespace ServicesTests
{

    public class FakeRepositoryCar : ICarRepository
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

        public Task<Car> GetCarByID(string idCar)
        {
            throw new System.NotImplementedException();
        }
        
    }
    public class CarServicesTest
    {
    
    }
}