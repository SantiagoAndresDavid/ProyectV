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

        public async Task SaveUser(User user)
        {
            try
            {
                await GetUserByName(user.UserName);
            }
            catch (UserNotFoundException)
            {
                await _usersRepository.Save(user);
                return;
            }

            throw new UserAlreadyExistsException("El usuario ya existe");
        }

        public async Task DeleteUser(User user)
        {
            await _usersRepository.Delete(user);
        }

        public async Task<User> GetUserByName(string userName)
        {
            return await _usersRepository.GetUserByName(userName);
        }
    }
}