using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.Models.CompanyManagement;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.ViewModels
{
    public class CompanyViewModel
    {
        public static readonly CompanyViewModel Empty = new();
        public int Id { get; init; }

        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }
        
        [Required]
        [Display(Name = "Организационно-правовая форма")]
        public string TypeOfBusiness { get; set; }
        
        [Display(Name = "Сотрудников фирмы")]
        public int Size { get; set; }



        public static CompanyModel ToModel(CompanyViewModel vm) =>
            new(vm.Id, vm.Title, vm.TypeOfBusiness, vm.Size);

        public static CompanyViewModel FromModel(CompanyModel dto) =>
            new()
            {
                Id = dto.Id,
                Title = dto.Title,
                TypeOfBusiness = dto.TypeOfBusiness.ToString(),
            };
    }
}