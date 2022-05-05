using Data.Interfaces;
using Entity;

namespace Services;

public class TechnoMechanicsServices : ITechnoMechanicsRepository
{
    private readonly ITechnoMechanicsRepository _technoMechanicsRepository;


    public TechnoMechanicsServices(ITechnoMechanicsRepository technoMechanicsRepository)
    {
        _technoMechanicsRepository = technoMechanicsRepository;
    }


    public Task Save(TechnoMechanics entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(TechnoMechanics entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(TechnoMechanics entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<TechnoMechanics>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<TechnoMechanics> GetTechnoMechanicsById(string id)
    {
        throw new NotImplementedException();
    }
} 