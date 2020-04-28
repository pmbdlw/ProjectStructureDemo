using System;
using System.Collections.Generic;
namespace ProjectStructureDemo.Entities.Core
{
    public class Page<TEntity>
    {
        /// <summary>
        /// 当前页标
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { set; get; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (PageSize < 1) { return 0; }
                else
                {
                    return (TotalCount + PageSize - 1) / PageSize;
                }
            }
        }
        /// <summary>
        /// 数据总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public IList<TEntity> DataList { get; set; }
    }
}
