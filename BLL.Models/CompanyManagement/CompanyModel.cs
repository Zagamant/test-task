using System;
using DAL.Entities;
using DAL.Entities.Enums;

namespace BLL.Models.CompanyManagement
{
    public class CompanyModel
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public TypeOfBusiness TypeOfBusiness { get; init; }

        public int  Size { get; set; }
        public CompanyModel()
        {
        }

        public CompanyModel(int id, string title, TypeOfBusiness tob, int size)
        {
            Id = id;
            Title = title;
            TypeOfBusiness = tob;
            Size = size;
        }
        
        public CompanyModel(int id, string title, string tob, int size)
        {
            Id = id;
            Title = title;
            TypeOfBusiness = (TypeOfBusiness)Enum.Parse(typeof(TypeOfBusiness), tob);
            Size = size;
        }

        public CompanyModel(Company company)
        {
            Id = company.Id;
            Title = company.Title;
            TypeOfBusiness = company.TypeOfBusiness;
            Size = company.Size;
        }
        
        
    }
}