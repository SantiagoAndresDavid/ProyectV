
using Data.Interfaces;

namespace Services
{
    public class CarServices
    {
        private readonly ICarRepository _carRepository;

        public CarServices(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        
        
        
        
    }
}