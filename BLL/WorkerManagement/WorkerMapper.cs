using BLL.Models.CompanyManagement;
using BLL.Models.WorkerManagement;
using DAL.Entities;

namespace BLL.WorkerManagement
{
    public static class WorkerMapper
    {
        public static Worker Map(WorkerModel element) =>
            new()
            {
                Id = element.Id,
                FirstName = element.FirstName,
                LastName = element.LastName,
                Patronymic = element.Patronymic,
                Position = element.Position,
                EmploymentDate = element.EmploymentDate,
                CompanyId = element.CompanyId
            };

        public static WorkerModel Map(Worker element) => 
            new(element);
    }
}