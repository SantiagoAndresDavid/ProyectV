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
            catch (NotFoundException)
            {
                await _usersRepository.Save(user);
                return;
            }

            throw new AlreadyExistsException("El usuario ya existe");
        }

        public async Task DeleteUser(User user)
        {
            await _usersRepository.Delete(user);
        }

        public async Task<User> GetUserByName(string userName)
        {
            return await _usersRepository.GetUserByName(userName);
        }

        public async Task UpdateUser(User userModify)
        {
            await _usersRepository.Update(userModify);
        }

        public async Task<List<User>> GetAll()
        {
            return await _usersRepository.GetAll();
        }
        
        
    }
}