using System;
using ProjectStructureDemo.Entities.Core;
using ProjectStructureDemo.IRepository;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SqlSugar;
namespace ProjectStructureDemo.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, new()
    {
        protected readonly ISqlSugarClient _db;
        //初始化SqlSuagarClient
        protected RepositoryBase(string connectionString)
        {
            _db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
        }
        internal ISqlSugarClient Db
        {
            get { return _db; }
        }
        #region Insert && Update
        public async Task<bool> InsertAsync(TEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandIdentityIntoEntityAsync();
        }
        public async Task<int> InsertBulkAsync(IList<TEntity> list)
        {
            var newList = new List<TEntity>();
            newList.AddRange(list);
            return await _db.Insertable(newList).ExecuteCommandAsync();
        }
        public async Task<long> InsertReturnIdentityAsync(TEntity entity)
        {
            return await _db.Insertable(entity).ExecuteReturnBigIdentityAsync();
        }
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await _db.Updateable(entity).ExecuteCommandHasChangeAsync();
        }
        public async Task<int> UpdateAsync(IList<TEntity> entities)
        {
            var newList = new List<TEntity>();
            newList.AddRange(entities);
            return await _db.Updateable(newList).ExecuteCommandAsync();
        }
        public async Task<int> UpdateAsync(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> where)
        {
            return await _db.Updateable<TEntity>().SetColumns(columns).Where(where).ExecuteCommandAsync();
        }
        #endregion
        #region Delete
        public async Task<bool> DeleteAsync(object id)
        {
            return await _db.Deleteable<TEntity>(id).ExecuteCommandHasChangeAsync();
        }
        public async Task<bool> DeleteAsync(TEntity entity)
        {
            return await _db.Deleteable(entity).ExecuteCommandHasChangeAsync();
        }
        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _db.Deleteable<TEntity>().Where(where).ExecuteCommandHasChangeAsync();
        }
        public async Task<bool> DeleteBulkAsync(object[] ids)
        {
            return await _db.Deleteable<TEntity>().In(ids).ExecuteCommandHasChangeAsync();
        }
        #endregion
        #region Query
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _db.Queryable<TEntity>().Where(where).CountAsync();
        }
        /// <summary>
        /// 符合某条件的记录是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        /// <remarks>WARNING:此处SqlSugar用的count(1)方式查看返回的数量确定是否存在，没有提供Exists方法的实现。后期性能如果有问题可能需要修改</remarks>
        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _db.Queryable<TEntity>().AnyAsync(where);
        }
        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _db.Queryable<TEntity>().WhereIF(where != null, where).ToListAsync();
        }
        public async Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> orderby, bool isAsc = true)
        {
            return await _db.Queryable<TEntity>().OrderByIF(orderby != null, orderby, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(where != null, where).ToListAsync();
        }
        public async Task<IList<TEntity>> GetListAsync(int top = 10, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> orderby = null, bool isAsc = true)
        {
            return await _db.Queryable<TEntity>().OrderByIF(orderby != null, orderby, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(where != null, where).Take(top).ToListAsync();
        }
        public async Task<IList<TEntity>> GetListByIdsAsync(object[] ids)
        {
            return await _db.Queryable<TEntity>().In(ids).ToListAsync();
        }
        public async Task<Page<TEntity>> GetPageListAsync(int pageIndex = 1, int pageSize = 20, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> orderby = null, bool isAsc = true)
        {
            var pageData = new Page<TEntity>
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            RefAsync<int> totalCount = 0;
            var result = await _db.Queryable<TEntity>().WhereIF(null != where, where).OrderByIF(orderby != null, orderby, isAsc ? OrderByType.Asc : OrderByType.Desc).ToPageListAsync(pageIndex, pageSize, totalCount);
            pageData.TotalCount = totalCount;
            pageData.DataList = result;
            return pageData;
        }
        public async Task<TEntity> GetSingleAsync(object objId)
        {
            return await _db.Queryable<TEntity>().InSingleAsync(objId);
        }
        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> where = null)
        {
            return await _db.Queryable<TEntity>().WhereIF(null != where, where).SingleAsync();
        }
        #endregion
    }
}