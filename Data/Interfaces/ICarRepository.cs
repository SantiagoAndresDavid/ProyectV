using System.Threading.Tasks;
using Entity;

namespace Data.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        public Task<Car> GetCarByID(string IdCar);
        
    }
} 