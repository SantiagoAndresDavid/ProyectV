
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
                _usersRepository.SaveUser(user);
                return;
            }
            throw new UserAlreadyExistsException("El usuario ya existe");
        }

        public void DeleteUser(User user)
        {
            _usersRepository.DeleteUser(user);
        }

        public User GetUserByName(string userName)
        {
            return _usersRepository.GetUserByName(userName);
        }
    }
}