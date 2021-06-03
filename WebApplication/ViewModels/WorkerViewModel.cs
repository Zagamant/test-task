using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.Models.CompanyManagement;
using BLL.Models.WorkerManagement;
using DAL.Entities.Enums;

namespace WebApplication.ViewModels
{
    public class WorkerViewModel
    {
        public static readonly WorkerViewModel Empty = new();
        public int Id { get; init; }

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [DataType(DataType.Date)]
        public DateTime EmploymentDate { get; set; }

        public int CompanyId { get; set; }
        public CompanyModel Company { get; set; }
        public string Position { get; set; }
        
        public IEnumerable<CompanyModel> CompaniesList { get; set; }

        public static WorkerViewModel FromModel(WorkerModel e) =>
            new()
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Patronymic = e.Patronymic,
                EmploymentDate = e.EmploymentDate,
                CompanyId = e.CompanyId,
                Position = e.Position.ToString()
            };

        public static WorkerModel ToModel(
            WorkerViewModel vm) =>
            new()
            {
                Id = vm.Id,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Patronymic = vm.Patronymic,
                EmploymentDate = vm.EmploymentDate,
                CompanyId = vm.CompanyId,
                Position = (Position)Enum.Parse(typeof(Position), vm.Position) 
            };
    }
}