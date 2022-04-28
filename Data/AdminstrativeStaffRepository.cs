using System.Data;
using Data.Interfaces;
using Entity;
using Entity.Exceptions;
using Hackedb;
using Hackedb.Contracts;

namespace Data;

public class AdminstrativeStaffRepository : Repository<AdminstrativeStaff>, IAdministrativeStaffRepository
{
    private List<AdminstrativeStaff> AdminstrativeStaffList { get; }


    public AdminstrativeStaffRepository(IDbChannel dbChannel) : base(dbChannel)
    {
        AdminstrativeStaffList = new List<AdminstrativeStaff>();
    }


    public async Task Save(AdminstrativeStaff adminstrativeStaff)
    {
        var query = "INTO personal_Administrativo" +
                    "(documento_identidad,Id,primer_nombre,segundo_nombre,apellido,foto)" +
                    "VALVUES(@0, @1, @2, @3, @4, @5)";
        await Insert(query, adminstrativeStaff.DocumentId, adminstrativeStaff.Id, adminstrativeStaff.FirstName,
            adminstrativeStaff.SecondName, adminstrativeStaff.Surname, adminstrativeStaff.Photos);
    }

    public async Task Delete(AdminstrativeStaff adminstrativeStaff)
    {
        var query = "FROM personal_Administrativo WHERE documento_identidad = @0";
        await Delete(query, adminstrativeStaff);
    }

    public async Task Update(AdminstrativeStaff adminstrativeStaff)
    {
        var query = "personal_administrativo SET " +
                    "documento_identidad = @0, id =@1, primer_nombre = @2, segundo_nombre = @3, apellido = @4, foto = @4";

        await Update(query, adminstrativeStaff.DocumentId, adminstrativeStaff.Id, adminstrativeStaff.FirstName,
            adminstrativeStaff.SecondName, adminstrativeStaff.Surname, adminstrativeStaff.Photos);
    }

    public async Task<List<AdminstrativeStaff>> GetAll()
    {
        return (await Select("* FROM personal_administrativo")).ToList();
    }

    public async Task<AdminstrativeStaff> GetAdministrativeStaffByDocumentId(int documentId)
    {
        try
        {
            return (await Select("* FROM personal_administrativo WHERE documento_identidad = @0", documentId)).First();
        }
        catch (InvalidOperationException e)
        {
            throw new NotFoundException("No se encontro el usuario", e);
        }
    }

    protected override AdminstrativeStaff DefaultMap(IDataRecord record)
    {
        return new AdminstrativeStaff
        {
            DocumentId = record.GetInt32(0),
            Id = record.GetInt32(1),
            FirstName = record.GetString(2),
            SecondName = record.GetString(3),
            Surname = record.GetString(4),
            Photos = record.GetString(5)
        };
    }
}