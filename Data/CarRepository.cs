using System.Data;
using Data.Interfaces;
using Entity;
using Entity.Exceptions;
using Hackedb;
using Hackedb.Contracts;


namespace Data
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private List<Car> Cars { get; }

        public CarRepository(IDbChannel dbChannel) : base(dbChannel)
        {
            Cars = new List<Car>();
        }


        public async Task Save(Car car)
        {
            var query = "INTO carros" +
                        "(id, placa, modelo, color,  num_asientos)" +
                        "VALUES(@0, @1, @2, @3, @4)";
            await Insert(query, car.LicensePlate, car.Model, car.Colour, car.Id, car.NumSeating);
        }

        public async Task Delete(Car car)
        {
            var query = "FROM carros WHERE id = @0";
            await Delete(query, car.Id);
        }

        public async Task Update(Car car)
        {
            var query = "carros SET" +
                        "Id = @0, palca = @1, modelo = @2, color = @3, num_asientos = @4 ";
            await Update(query, car.Id, car.Model, car.Colour, car.NumSeating);
        }

        public async Task<List<Car>> GetAll()
        {
            return (await Select("* FROM carros")).ToList();
        }

        public async Task<Car> GetCarById(string idCar)
        {
            try
            {
                return (await Select("* FROM usuarios WHERE nombre_usuario = @0", idCar)).First();
            }
            catch (InvalidOperationException e)
            {
                throw new NotFoundException("No se encontro el usuario", e);
            }
        }

        protected override Car DefaultMap(IDataRecord record)
        {
            return new Car
            {
                Id = record.GetString(0),
                LicensePlate = record.GetString(1),
                Model = record.GetString(2),
                Colour = record.GetString(3),
                NumSeating = record.GetInt32(4)
            };
        }
    }
}