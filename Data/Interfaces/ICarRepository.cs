using Entity;

namespace Data.Interfaces
{
    public interface ICarRepository
    {

        public void SaveCar(Car car);


        public void DeleteCar(Car car);


        public void UpdateUser(Car car);


        public User GetUserByName(string userName);
        
    }
}