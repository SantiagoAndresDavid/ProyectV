using Entity;
using Hackedb;

namespace Data.Interfaces;

public interface ITechnoMechanicsRepository : IRepository<TechnoMechanics>
{
    public Task<TechnoMechanics> GetTechnoMechanicsById(string id);
}