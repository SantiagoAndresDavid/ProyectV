
using Entity;

namespace Data.Interfaces
{
    public interface IUsersRepository
    {

        public void SaveUser(User user);


        public void DeleteUser(User user);


        public void UpdateUser(User user);


        public User GetUserByName(string userName);
        
    }
}