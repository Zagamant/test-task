using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.CompanyManagement;
using BLL.Models.CompanyManagement;
using BLL.Models.WorkerManagement;
using DAL.Entities;
using DAL.Repositories.CompanyRepo;
using DAL.Repositories.WorkerRepo;

namespace BLL.WorkerManagement
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly ICompanyRepository _companyRepository;

        public WorkerService(IWorkerRepository workerRepository, ICompanyRepository companyRepository)
        {
            _workerRepository = workerRepository;
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<WorkerModel>> GetAllAsync()
        {
            var entities = await _workerRepository
                .GetAllAsync();

            return entities.Select(selector: e =>
            {
                var result = WorkerMapper.Map(e);
                if (result.CompanyId != 0)
                {
                    var company = _companyRepository.GetById(result.CompanyId);
                    result.Company = CompanyMapper.Map(company);
                }

                return result;
            });
        }

        public async Task<WorkerModel> GetAsync(int id)
        {
            var entity = await _workerRepository.GetByIdAsync(id);
            var result = WorkerMapper.Map(entity);
            
            if (entity.CompanyId != 0)
            {
                var company = await _companyRepository.GetByIdAsync(result.CompanyId);
                result.Company = CompanyMapper.Map(company);
            }

            return result;
        }

        public async Task<WorkerModel> AddAsync(WorkerModel model)
        {
            var entity = WorkerMapper.Map(model);
            var result = await _workerRepository.InsertAsync(entity);
            Company company = null;
            if (result.CompanyId != 0)
            {
                company = await _companyRepository.GetByIdAsync(result.CompanyId);
                company.Size++;
                await _companyRepository.UpdateAsync(result.CompanyId, company);
            }

            var returnValue = WorkerMapper.Map(result);
            if (company != null)
            {
                returnValue.Company = CompanyMapper.Map(company);
            }

            return returnValue;
        }

        public async Task<WorkerModel> EditAsync(int id, WorkerModel model)
        {
            var worker = await _workerRepository.GetByIdAsync(id);
            if (worker.CompanyId != model.CompanyId)
            {
                if (worker.CompanyId != 0)
                {
                    var companyOld = await _companyRepository.GetByIdAsync(worker.CompanyId);
                    companyOld.Size--;
                    await _companyRepository.UpdateAsync(worker.CompanyId, companyOld);
                }

                var companyNew = await _companyRepository.GetByIdAsync(model.CompanyId);
                companyNew.Size++;
                await _companyRepository.UpdateAsync(model.CompanyId, companyNew);
            }

            var entity = WorkerMapper.Map(model);
            var result = await _workerRepository.UpdateAsync(id, entity);

            return WorkerMapper.Map(result);
        }

        public async Task DeleteAsync(int id) => await _workerRepository.DeleteAsync(id);

    }
}