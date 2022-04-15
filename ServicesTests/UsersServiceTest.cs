using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Interfaces;
using Entity;
using Entity.Exceptions;
using NUnit.Framework;
using Services;

namespace ServicesTests
{
    public class FakeUserRepository : IUsersRepository
    {
        private List<User> Users { get; }

        public FakeUserRepository()
        {
            Users = new List<User>();
        }

        public async Task Save(User user)
        {
            Users.Add(user);
        }

        public async Task Delete(User user)
        {
            Users.Remove(user);
        }

        public async Task Update(User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<User>> GetAll()
        {
            return Users;
        }

        public async Task<User> GetUserByName(string userName)
        {
            try
            {
                return Users.First(user => user.UserName == userName);
            }
            catch (InvalidOperationException e)
            {
                throw new UserNotFoundException("No se encontro el usuario", e);
            }
        }
    }


    public class UsersServiceTest
    {
        [Test]
        public async Task TestThatVerifiesIfTheUserIsSaved()
        {
            User user = new("Pipe", "pipeELpropiokubernetes@gmail.com", "1234", "devops");
            UsersService userService = new(new FakeUserRepository());
            await userService.SaveUser(user);
            User userFound = await userService.GetUserByName(user.UserName);
            Assert.AreEqual(user, userFound);
        }

        [Test]
        public void TestThatVerifiesIfUserIsNotFound()
        {
            UsersService userService = new(new FakeUserRepository());
            Assert.ThrowsAsync<UserNotFoundException>(async () => await userService.GetUserByName("Pipe"));
        }

        [Test]
        public async Task TestThatVerifiesIfUserAlreadyExists()
        {
            User user = new("Pipe", "pipeELpropiokubernetes@gmail.com", "1234", "devops");
            UsersService userService = new(new FakeUserRepository());
            await userService.SaveUser(user);
            Assert.ThrowsAsync<UserAlreadyExistsException>(async () => await userService.SaveUser(user));
        }

        [Test]
        public async Task TestThatVerifiesIfTheUserIsDeleted()
        {
            UsersService userService = new(new FakeUserRepository());
            User user = new("Pipe", "pipeELpropiokubernetes@gmail.com", "1234", "devops");
            await userService.SaveUser(user);
            await userService.DeleteUser(user);
            Assert.ThrowsAsync<UserNotFoundException>(async () => await userService.GetUserByName(user.UserName));
        }
    }
}