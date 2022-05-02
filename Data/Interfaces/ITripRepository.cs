using Entity;
using Hackedb;

namespace Data.Interfaces;

public interface ITripRepository : IRepository<Trip>
{
    public Task<Trip> GetTripbyId(string id);   
}