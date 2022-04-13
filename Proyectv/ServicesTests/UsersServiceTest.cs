using System;
using System.Collections.Generic;
using System.Linq;
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

        public void SaveUser(User user)
        {
            Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            Users.Remove(user);
        }

        public void UpdateUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public User GetUserByName(string userName)
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
        public void TestThatVerifiesIfTheUserIsSaved()
        {
            User user = new("Pipe", "pipeELpropiokubernetes@gmail.com", "1234", "devops");
            UsersService userService = new(new FakeUserRepository());
            userService.SaveUser(user);
            User userFound = userService.GetUserByName(user.UserName);
            Assert.AreEqual(user, userFound);
        }

        [Test]
        public void TestThatVerifiesIfUserIsNotFound()
        {
            UsersService userService = new(new FakeUserRepository());
            Assert.Throws<UserNotFoundException>(() => userService.GetUserByName("Pipe"));
        }

        [Test]
        public void TestThatVerifiesIfUserAlreadyExists()
        {
            User user = new("Pipe", "pipeELpropiokubernetes@gmail.com", "1234", "devops");
            UsersService userService = new(new FakeUserRepository());
            userService.SaveUser(user);
            Assert.Throws<UserAlreadyExistsException>(() => userService.SaveUser(user));
        }

         [Test]
         public void TestThatVerifiesIfTheUserIsDeleted()
         {
             UsersService userService = new(new FakeUserRepository());
             User user = new("Pipe", "pipeELpropiokubernetes@gmail.com", "1234", "devops");
             userService.SaveUser(user);
             userService.DeleteUser(user);
             Assert.Throws<UserNotFoundException>(() => userService.GetUserByName(user.UserName));
         }

         [Test]
         public void TestVerifiesIfTheUserIsModified()
         {
             
         }
    }
}