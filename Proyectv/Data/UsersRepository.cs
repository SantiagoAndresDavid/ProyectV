using System;
using System.Collections.Generic;
using System.Linq;
using Data.Interfaces;
using Entity;
using Entity.Exceptions;


namespace Data
{
    public class UsersRepository : IUsersRepository
    {
        
        private List<User> Users { get; }

        public UsersRepository()
        {
            Users = new List<User>();
        }

        public void SaveUser(User user)
        {
            Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public User GetUserByName(string userName)
        {
            {
                try
                {
                    return Users.First(user => user.UserName == userName);
                }
                catch (InvalidOperationException e)
                {
                    throw new UserNotFoundException("No se encontro el usuario",e);
                }
            }
        }

        public string VerifyUserName(User user)
        {
            throw new System.NotImplementedException();
        } 
        
    }
}