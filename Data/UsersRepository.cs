using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Data.Interfaces;
using Entity;
using Entity.Exceptions;
using Hackedb;
using Hackedb.Contracts;


namespace Data
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        private List<User> Users { get; }
        public UsersRepository(IDbChannel dbChannel) : base(dbChannel)
        {
            Users = new List<User>();
        }

        public async Task Save(User user)
        {
            var query = "INTO usuarios " +
                        "(nombre_usuario, correo_usuario, contrasena_usuario, rol_usuario) " +
                        "VALUES (@0, @1, @2, @3)";
            await Insert(query, user.UserName, user.UserEmail, user.Password, user.Role);
        }

        public async Task Delete(User user)
        {
            var query = "FROM usuarios WHERE nombre_usuario = @0";
            await Delete(query, user.UserName);
        }

        public async Task Update(User user)
        {
            var query = "usuarios SET " +
                        "nombre_usuario = @0, correo_usuario = @1, contrasena_usuario = @2, rol_usuario = @3 " +
                        "WHERE nombre_usuario = @0";
            await Update(query, user.UserName, user.UserEmail, user.Password, user.Role);
        }

        public async Task<List<User>> GetAll()
        {
            return (await Select("* FROM usuarios")).ToList();
        }

        public async Task<User> GetUserByName(string userName)
        {
            try
            {
                return (await Select("* FROM usuarios WHERE nombre_usuario = @0", userName)).First();
            }
            catch (InvalidOperationException e)
            {
                throw new NotFoundException("No se encontro el usuario", e);
            }
        }

        protected override User DefaultMap(IDataRecord record)
        {
            return new User
            {
                UserName = record.GetString(0),
                UserEmail = record.GetString(1),
                Password = record.GetString(2),
                Role = record.GetString(3)
            };
        }
    }
}