using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Interfaces;
using Entity;
using Entity.Exceptions;

namespace Services
{
    public class CarServices
    {
        private readonly ICarRepository _carRepository;

        public CarServices(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task SaveCar(Car car)
        {
            try
            {
                await GetCarById(car.Id);
            }
            catch (NotFoundException)
            {
                await _carRepository.Save(car);
                return;
            }

            throw new AlreadyExistsException("El Vehiculo ya existe");
        }


        public async Task DeleteCar(Car car)
        {
            await _carRepository.Delete(car);
        }

        public async Task UpdateCar(Car carModify)
        {
            await _carRepository.Update(carModify);
        }

        public async Task<List<Car>> GetAll()
        {
            return await _carRepository.GetAll();
        }

        public async Task<Car> GetCarById(string id)
        {
           return await _carRepository.GetCarById(id);
        }
    }
}