using Entity;

namespace Data.Interfaces
{
    public interface IDriverRepository : IRepository<Driver>
    {
        public Task<Driver> GetDriverByDocumentId(int documentId);
    }
}