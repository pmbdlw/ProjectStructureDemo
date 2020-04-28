using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProjectStructureDemo.Entities.Core;
namespace ProjectStructureDemo.IServices
{
    public interface IBaseServices<TEntity> where TEntity : class
    {
        #region Insert methods
        Task<bool> InsertAsync(TEntity entity);
        Task<long> InsertReturnIdentityAsync(TEntity entity);
        Task<int> InsertBulkAsync(IList<TEntity> list);
        #endregion
        #region Update Methods
        Task<bool> UpdateAsync(TEntity entity);
        Task<int> UpdateAsync(IList<TEntity> entities);
        Task<int> UpdateAsync(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> where);
        #endregion
        #region Delete Methods
        Task<bool> DeleteAsync(object id);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> DeleteBulkAsync(object[] ids);
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where);
        #endregion
        #region Exist method
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> where);
        #endregion
        #region Select Methods
        Task<TEntity> GetSingleAsync(object objId);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> where);
        #endregion
        #region Get List
        Task<IList<TEntity>> GetListByIdsAsync(object[] ids);
        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where);
        Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> orderby, bool isAsc = true);
        Task<IList<TEntity>> GetListAsync(int top = 10,Expression<Func<TEntity, bool>> where=null,  Expression<Func<TEntity, object>> orderby = null, bool isAsc = true);
        Task<Page<TEntity>> GetPageListAsync(int pageIndex = 1, int pageSize = 20, Expression<Func<TEntity, bool>> where=null, Expression<Func<TEntity, object>> orderby = null, bool isAsc = true);
        #endregion
        #region Others
        Task<int> CountAsync(Expression<Func<TEntity, bool>> where);
        #endregion
    }
}
