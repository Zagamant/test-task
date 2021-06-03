using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Models.CompanyManagement;
using DAL.Repositories.CompanyRepo;
using DAL.Repositories.WorkerRepo;

namespace BLL.CompanyManagement
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IWorkerRepository _workerRepository;

        public CompanyService(ICompanyRepository companyRepository, IWorkerRepository workerRepository)
        {
            _companyRepository = companyRepository;
            _workerRepository = workerRepository;
        }

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


        public async Task<CompanyModel> AddAsync(CompanyModel model)
        {
            var entity = CompanyMapper.Map(model);
            var result = await _companyRepository.InsertAsync(entity);

            return CompanyMapper.Map(result);
        }

        public async Task<CompanyModel> EditAsync(int id, CompanyModel model)
        {
            var entity = CompanyMapper.Map(model);
            var result = await _companyRepository.UpdateAsync(id, entity);

            return CompanyMapper.Map(result);
        }

        public async Task DeleteAsync(int id) => await _companyRepository.DeleteAsync(id);

        public async Task<CompanyModel> GetAsync(int id)
        {
            var entity = await _companyRepository.GetByIdAsync(id);
            return CompanyMapper.Map(entity);
        }
    }
}