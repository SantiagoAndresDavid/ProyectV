using Data.Interfaces;
using Entity;
using Entity.Exceptions;

namespace Services;

public class TechnoMechanicsServices : ITechnoMechanicsRepository
{
    private readonly ITechnoMechanicsRepository _technoMechanicsRepository;


    public TechnoMechanicsServices(ITechnoMechanicsRepository technoMechanicsRepository)
    {
        _technoMechanicsRepository = technoMechanicsRepository;
    }


    public async Task Save(TechnoMechanics technoMechanics)
    {
        try
        {
            await GetTechnoMechanicsById(technoMechanics.Id.ToString());
        }
        catch
        {
            await _technoMechanicsRepository.Save(technoMechanics);
            return;
        }

        throw new AlreadyExistsException("ya existe esta tecnomecanica.");
    }

    public async Task Delete(TechnoMechanics technoMechanics)
    {
        await _technoMechanicsRepository.Delete(technoMechanics);
    }

    public async Task Update(TechnoMechanics technoMechanics)
    {
        await _technoMechanicsRepository.Update(technoMechanics);
    }

    public async Task<List<TechnoMechanics>> GetAll()
    {
        return await _technoMechanicsRepository.GetAll();
    }

    public async Task<TechnoMechanics> GetTechnoMechanicsById(string id)
    {
        return await _technoMechanicsRepository.GetTechnoMechanicsById(id);
    }
} 