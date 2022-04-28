using Entity;

namespace Data.Interfaces;

public interface  IAdministrativeStaffRepository : IRepository<AdminstrativeStaff>
{
    public Task<AdminstrativeStaff> GetAdministrativeStaffByDocumentId(int documentId);
}