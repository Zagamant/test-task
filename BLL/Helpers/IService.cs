using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Helpers
{
    public interface IService<TId, TModel>
    {
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> GetAsync(TId id);
        Task<TModel> AddAsync(TModel model);
        Task<TModel> EditAsync(TId id, TModel dto);
        Task DeleteAsync(TId id);
    }
}