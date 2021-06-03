using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Models.CompanyManagement;
using BLL.Models.WorkerManagement;
using DAL.Repositories.WorkerRepo;

namespace BLL.WorkerManagement
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository _repository;

        public WorkerService(IWorkerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<WorkerModel>> GetAllAsync()
        {
            var entities = await _repository
                .GetAllAsync();

            return entities.Select(WorkerMapper.Map);
        }


        public async Task<WorkerModel> AddAsync(WorkerModel model)
        {
            var entity = WorkerMapper.Map(model);
            var result = await _repository.InsertAsync(entity);

            return WorkerMapper.Map(result);
        }

        public async Task<WorkerModel> EditAsync(int id, WorkerModel model)
        {
            var entity = WorkerMapper.Map(model);
            var result = await _repository.UpdateAsync(id, entity);

            return WorkerMapper.Map(result);
        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task<WorkerModel> GetAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return WorkerMapper.Map(entity);
        }
    }
}