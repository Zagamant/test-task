using DAL.Entities;
using DAL.Helpers;

namespace DAL.Repositories.CompanyRepo
{
    /// <summary>
    /// Empty company service interface for DI
    /// </summary>
    public interface ICompanyRepository: IRepository<int, Company>
    {
        
    }
}