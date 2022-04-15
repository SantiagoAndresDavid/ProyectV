using System.Threading.Tasks;
using Data.Interfaces;
using Entity;
using Entity.Exceptions;


namespace Services
{
    public class UsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public void SaveUser(User user)
        {
            try
            {
                GetUserByName(user.UserName);
            }
            catch (UserNotFoundException)
            {
                _usersRepository.Save(user);
                return;
            }

            throw new UserAlreadyExistsException("El usuario ya existe");
        }

        public void DeleteUser(User user)
        {
            _usersRepository.Delete(user);
        }

        public async Task<User> GetUserByName(string userName)
        {
            return await _usersRepository.GetUserByName(userName);
        }
    }
}