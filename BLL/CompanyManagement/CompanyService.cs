using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Models.CompanyManagement;
using DAL.Repositories.CompanyRepo;
using DAL.Repositories.WorkerRepo;

namespace BLL.CompanyManagement
{
    /// <summary>
    /// Provide service to work with companies models and entities
    /// </summary>
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IWorkerRepository _workerRepository;

        public CompanyService(ICompanyRepository companyRepository, IWorkerRepository workerRepository)
        {
            _companyRepository = companyRepository;
            _workerRepository = workerRepository;
        }

        /// <summary>
        /// Get all companies with count of workers 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CompanyModel>> GetAllAsync()
        {
            var entities = await _companyRepository
                .GetAllAsync();

            var workers = await _workerRepository.GetAllAsync();
            return entities.Select(e =>
            {
                e.Size = workers.Count(w => w.CompanyId == e.Id);
                return new CompanyModel(e);
            });
        }

        /// <summary>
        /// Get <see cref="Company"/> by id
        /// </summary>
        /// <param name="id">Id of <see cref="Company"/></param>
        /// <returns></returns>
        public async Task<CompanyModel> GetAsync(int id)
        {
            var entity = await _companyRepository.GetByIdAsync(id);
            return CompanyMapper.Map(entity);
        }
        
        /// <summary>
        /// Add new company and return <see cref="CompanyModel"/> include count of workers
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<CompanyModel> AddAsync(CompanyModel model)
        {
            var entity = CompanyMapper.Map(model);
            var result = await _companyRepository.InsertAsync(entity);

            return CompanyMapper.Map(result);
        }

        /// <summary>
        /// Update all fields with <see cref="CompanyModel"/>
        /// </summary>
        /// <param name="id">Id of old <see cref="Company"/></param>
        /// <param name="model">Updated fields</param>
        /// <returns>New <see cref="CompanyModel"/></returns>
        public async Task<CompanyModel> EditAsync(int id, CompanyModel model)
        {
            var entity = CompanyMapper.Map(model);
            var result = await _companyRepository.UpdateAsync(id, entity);

            return CompanyMapper.Map(result);
        }

        /// <summary>
        /// Remove <see cref="Company"/> by id
        /// </summary>
        /// <param name="id">Id of <see cref="Company"/></param>
        public async Task DeleteAsync(int id) => await _companyRepository.DeleteAsync(id);

        
    }
}