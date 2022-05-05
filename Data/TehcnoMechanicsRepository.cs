using System.Data;
using Data.Interfaces;
using Entity;
using Entity.Exceptions;
using Hackedb;
using Hackedb.Contracts;

namespace Data
{
    public class TechnoMechanicsRepository : Repository<TechnoMechanics>, ITechnoMechanicsRepository
    {
        private List<TechnoMechanics> TechnoMechanicsList { get; }
        private readonly ICarRepository _carRepository;

        public TechnoMechanicsRepository(IDbChannel dbChannel, ICarRepository carRepository) : base(dbChannel)
        {
            _carRepository = carRepository;
            TechnoMechanicsList = new List<TechnoMechanics>();
        }

        public async Task Save(TechnoMechanics technoMechanics)
        {
            await _carRepository.Save(technoMechanics.Car);
            const string query = "INTO tecnomecanicas " +
                                 "(id,fecha,estado_carroceria,estado_frenos,estado_amoritiguador,estado_ruedas)" +
                                 "VALVUES (@0, @1, @2, @3, @4, @5)";
            await Insert(query, technoMechanics.Id.ToString(), technoMechanics.Date, technoMechanics.BodyworkCondition,
                technoMechanics.BrakeCondition, technoMechanics.DamperCondition, technoMechanics.WeelsCondition);
        }

        public async Task Delete(TechnoMechanics technoMechanics)
        {
            var query = "FROM tecnomecanicas WHERE id = @0";
            await Delete(query, technoMechanics.Id.ToString());
        }

        public async Task Update(TechnoMechanics technoMechanics)
        {
            await _carRepository.Save(technoMechanics.Car);
            var query = "tecnomecanicas SET" +
                        "id = @0, fecha = @1, estado_carroceria = @2, estado_frenos = @3, estado_amortiguador = @4,estado_ruedas = @5";
        }

        public async Task<List<TechnoMechanics>> GetAll()
        {
            return (await Select("* FROM tecnomecanicas")).ToList();
        }

        public async Task<TechnoMechanics> GetTechnoMechanicsById(string id)
        {
            try
            {
                return (await Select("* FROM tecnomecanicas WHERE id = @0", id)).First();
            }
            catch (InvalidOperationException e)
            {
                throw new NotFoundException("No se contro la tecnomecancia asociada.",e);
            }
        }

        protected override TechnoMechanics DefaultMap(IDataRecord record)
        {
            return new TechnoMechanics
            {
                Id = Guid.Parse(record.GetString(0)),
                Date = record.GetDateTime(1),
                BodyworkCondition = record.GetString(2),
                BrakeCondition = record.GetString(3),
                DamperCondition = record.GetString(4),
                WeelsCondition = record.GetString(5),
                Car = new Car
                {
                    Id = record.GetString(6),
                    LicensePlate = record.GetString(7),
                    Model = record.GetString(8),
                    Colour = record.GetString(9),
                    NumSeating = record.GetInt32(10)
                }

            };
        }
    }
}