using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProjectStructureDemo.Entities.Core;
using ProjectStructureDemo.IRepository;
using ProjectStructureDemo.IServices;
namespace ProjectStructureDemo.Services
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        protected IRepositoryBase<TEntity> repo;
        public BaseServices(IRepositoryBase<TEntity> repository)
        {
            repo = repository;
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> where)
        {
            return await repo.CountAsync(where);
        }
        public async Task<bool> DeleteAsync(object id)
        {
            return await repo.DeleteAsync(id);
        }
        public async Task<bool> DeleteAsync(TEntity entity)
        {
            return await repo.DeleteAsync(entity);
        }
        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            return await repo.DeleteAsync(where);
        }
        public async Task<bool> DeleteBulkAsync(object[] ids)
        {
            return await repo.DeleteBulkAsync(ids);
        }
        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where=null)
        {
            return await repo.GetAllAsync(where);
        }
        public async Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> orderby=null, bool isAsc = true)
        {
            return await repo.GetListAsync(where,orderby,isAsc);
        }
        public async Task<IList<TEntity>> GetListAsync(int top = 10,Expression<Func<TEntity, bool>> where=null,  Expression<Func<TEntity, object>> orderby = null, bool isAsc = true)
        {
            return await repo.GetListAsync(top,where, orderby, isAsc);
        }
        public async Task<IList<TEntity>> GetListByIdsAsync(object[] ids)
        {
            return await repo.GetListByIdsAsync(ids);
        }
        public async Task<Page<TEntity>> GetPageListAsync(int pageIndex = 1, int pageSize = 20, Expression<Func<TEntity, bool>> where=null,  Expression<Func<TEntity, object>> orderby = null, bool isAsc = true)
        {
            return await repo.GetPageListAsync(pageIndex, pageSize, where, orderby, isAsc);
        }
        public async Task<TEntity> GetSingleAsync(object objId)
        {
            return await repo.GetSingleAsync(objId);
        }
        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> where=null)
        {
            return await repo.GetSingleAsync(null);
        }
        public async Task<bool> InsertAsync(TEntity entity)
        {
            return await repo.InsertAsync(entity);
        }
        public async Task<int> InsertBulkAsync(IList<TEntity> list)
        {
            return await repo.InsertBulkAsync(list);
        }
        public async Task<long> InsertReturnIdentityAsync(TEntity entity)
        {
            return await repo.InsertReturnIdentityAsync(entity);
        }
        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> where)
        {
            return await repo.IsExistAsync(where);
        }
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await repo.UpdateAsync(entity);
        }
        public async Task<int> UpdateAsync(IList<TEntity> entities)
        {
            return await repo.UpdateAsync(entities);
        }
        public async Task<int> UpdateAsync(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> where)
        {
            return await repo.UpdateAsync(columns,where);
        }
    }
}
