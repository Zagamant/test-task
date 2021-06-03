using BLL.Helpers;
using BLL.Models.CompanyManagement;
using DAL.Entities;

namespace BLL.CompanyManagement
{
    public static class CompanyMapper
    {
        public static Company Map(CompanyModel element) =>
            new()
            {
                Id = element.Id,
                Title = element.Title,
                TypeOfBusiness = element.TypeOfBusiness
            };

        public static CompanyModel Map(Company element) => 
            new(element.Id, element.Title, element.TypeOfBusiness);
    }
}