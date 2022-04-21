using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Entity.Exceptions;
using Hackedb;
using Hackedb.Contracts;

namespace Data.Interfaces
{
    public class DriverRepository : Repository<Driver>, IDriverRepository
    {
        private List<Driver> Drivers { get; }

        public DriverRepository(IDbChannel dbChannel) : base(dbChannel)
        {
            Drivers = new List<Driver>();
        }
        
        public async Task Save(Driver driver)
        {
            const string query = "INTO conductores " +
                                 "documento_identificacion,licencia_conduccion, primer_nombre, segundo_nombre, apellido, foto"+
                                 "VALVUES (@0, @1, @2, @3, @4, @5)";
            await Insert(query,driver.DocumentId,driver.IdLicenceDriver,driver.FirstName,driver.SecondName,driver.Surname,driver.Photo);
        }

        public async Task Delete(Driver driver)
        {
            const string query = "FROM conductores WHERE documento_identificacion = @0";
            await Delete(query,driver.DocumentId);
        }

        public async Task Update(Driver driver)
        {
            const string query = "conductores SET "+
                                 "documento_identificacion = @0, licencia_conduccion = @1, primer_nombre = @2, segundo_nombre = @3, apellido = @4, foto = @5 "+
                                 "WHERE documento_identificacion = @0";
            await Update(query,driver.DocumentId,driver.IdLicenceDriver,driver.FirstName,driver.SecondName,driver.Surname,driver.Photo);
        }

        public async Task<List<Driver>> GetAll()
        {
            return (await Select("* FROM conductores")).ToList();
        }

        public async Task<Driver> GetDriverByDocumentId(int documentId)
        {
            try
            {
                return (await Select("* FROM conductores WHERE documento_identificacion = @0", documentId)).First();
            }
            catch (InvalidOperationException  e)
            {
                throw new NotFoundException("No se encontro el conductor", e);
            }
        }

        protected override Driver DefaultMap(IDataRecord record)
        {
            return new Driver
            {
                DocumentId = record.GetInt32(0),
                IdLicenceDriver = record.GetInt32(1),
                FirstName = record.GetString(2),
                SecondName = record.GetString(3),
                Surname = record.GetString(4),
                Photo = record.GetString(5)
            };
        }
    }
}