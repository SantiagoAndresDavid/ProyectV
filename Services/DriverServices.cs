using Data.Interfaces;
using Entity;
using Entity.Exceptions;

namespace Services
{
    public class DriverServices
    {
        private readonly IDriverRepository _driverRepository;

        public DriverServices(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task SaveDriver(Driver driver)
        {
            try
            {
                await GetDriverByDocumentId(driver.DocumentId);
            }
            catch(NotFoundException)
            {
                await _driverRepository.Save(driver);
                return;
            }
            throw new AlreadyExistsException("El conductor ya existe");
        }

        public async Task DeleteDriver(Driver driver)
        {
            await _driverRepository.Delete(driver);
        }

        public async Task<Driver> GetDriverByDocumentId(int documentId)
        {
            return await _driverRepository.GetDriverByDocumentId(documentId);
        }

        public async Task UpdateDriver(Driver driverModify)
        {
            await _driverRepository.Update(driverModify);
        }

        public async Task<List<Driver>> GetAll()
        {
            return await _driverRepository.GetAll();
        }



    }
}