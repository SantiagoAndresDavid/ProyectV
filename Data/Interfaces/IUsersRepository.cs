using Entity;

namespace Data.Interfaces
{
    public interface IUsersRepository : IRepository<User>
    {
        public Task<User> GetUserByName(string userName);
    }
}