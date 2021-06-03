using DAL.Entities;
using DAL.Entities.Enums;

namespace BLL.Models.CompanyManagement
{
    public class CompanyModel
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public TypeOfBusiness TypeOfBusiness { get; init; }

        public CompanyModel()
        {
        }

        public CompanyModel(int id, string title, TypeOfBusiness tob)
        {
            Id = id;
            Title = title;
            TypeOfBusiness = tob;
        }

        public CompanyModel(Company company)
        {
            Id = company.Id;
            Title = company.Title;
            TypeOfBusiness = company.TypeOfBusiness;
        }
        
        
    }
}