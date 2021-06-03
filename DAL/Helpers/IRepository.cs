using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Helpers
{
    /// <summary>
    /// Provide base interface to create Repositories
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TId, TEntity> where TEntity : BaseEntity<TId>
    {
        IDbConnection Connection { get; set; }
        
        /// <summary>
        /// Send request to get all <see cref="TEntity"/> entities from database
        /// </summary>
        /// <returns>Enumerable of <see cref="TEntity"/> entities</returns>
        IEnumerable<TEntity> GetAll();
        
        /// <summary>
        /// Send request to get all <see cref="TEntity"/> entities from database
        /// </summary>
        /// <returns>Enumerable of <see cref="TEntity"/> entities</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();
        
        /// <summary>
        /// Get single record of <see cref="TEntity"/>
        /// </summary>
        /// <param name="id"><see cref="TEntity"/>'s identity number</param>
        /// <returns>Entity of <see cref="TEntity"/></returns>
        TEntity GetById(TId id);
        
        /// <summary>
        /// Get single record of <see cref="TEntity"/>
        /// </summary>
        /// <param name="id"><see cref="TEntity"/>'s identity number</param>
        /// <returns>Entity of <see cref="TEntity"/></returns>
        Task<TEntity> GetByIdAsync(TId id);
        
        /// <summary>
        /// Create new record of <see cref="TEntity"/>
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/></param>
        /// <returns>Entity of <see cref="TEntity"/></returns>
        TEntity Insert(TEntity entity);
        
        /// <summary>
        /// Create new record of <see cref="TEntity"/>
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/></param>
        /// <returns>Entity of <see cref="TEntity"/></returns>
        Task<TEntity> InsertAsync(TEntity entity);
        
        /// <summary>
        /// Update fields of record by id
        /// </summary>
        /// <param name="id"><see cref="TEntity"/>'s identity number</param>
        /// <param name="entity"><see cref="TEntity"/></param>
        /// <returns>Entity of <see cref="TEntity"/></returns>
        TEntity Update(TId id, TEntity entity);
        
        /// <summary>
        /// Update fields of record by id
        /// </summary>
        /// <param name="id"><see cref="TEntity"/>'s identity number</param>
        /// <param name="entity"><see cref="TEntity"/></param>
        /// <returns>Entity of <see cref="TEntity"/></returns>
        Task<TEntity> UpdateAsync(TId id, TEntity entity);
        
        /// <summary>
        /// Remove record from table by id
        /// </summary>
        /// <param name="id"><see cref="TEntity"/>'s identity number</param>
        void Delete(TId id);
        
        /// <summary>
        /// Remove record from table by id
        /// </summary>
        /// <param name="id"><see cref="TEntity"/>'s identity number</param>
        Task DeleteAsync(TId id);
    }
}