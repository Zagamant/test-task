using System;
using DAL.Entities;
using DAL.Entities.Enums;

namespace BLL.Models.WorkerManagement
{
    public class WorkerModel
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Patronymic { get; init; }
        public DateTime EmploymentDate { get; init; }
        public Position Position { get; init; }
        public Company Company { get; init; }
        public int CompanyId { get; init; }

        public WorkerModel()
        {
        }

        public WorkerModel(int id, string firstName, string lastName, string patronymic, DateTime employmentDate, Position position, int companyId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            EmploymentDate = employmentDate;
            Position = position;
            CompanyId = companyId;
        }

        public WorkerModel(Worker worker)
        {
            Id = worker.Id;
            FirstName = worker.FirstName;
            LastName = worker.LastName;
            Patronymic = worker.Patronymic;
            EmploymentDate = worker.EmploymentDate;
            Position = worker.Position;
            CompanyId = worker.CompanyId;
        }
    }
}