using DAL.Entities;
using DAL.Helpers;

namespace DAL.Repositories.WorkerRepo
{
    /// <summary>
    /// Empty worker service interface for DI
    /// </summary>
    public interface IWorkerRepository: IRepository<int, Worker>
    {
        
    }
}