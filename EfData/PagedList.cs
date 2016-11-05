using System.Collections.Generic;
using System.Linq;

namespace ResposityOfEf
{
    /// <summary>
    /// 分页列表实现
    /// </summary>
    /// <param name="T">T</param>
    public class PagedList<T> : List<T>, IPagedList<T>
    {



        /// <summary>
        /// 构造（1）
        /// </summary>
        /// <param name="source">实现IQueryable<T>的查询集</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">分页数</param>
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            int total = source.Count();
            this.TotalCount = total;
            this.TotalPages = total / pageSize;
            
            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// 构造（2）
        /// </summary>
        /// <param name="source">实现IList<T>的查询集</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">分页数</param>
        public PagedList(IList<T> source, int pageIndex, int pageSize)
        {
            TotalCount = source.Count();
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;
            
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// 构造（3）
        /// </summary>
        /// <param name="source">实现IEnumerable<T>的查询集</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">分页数</param>
        /// <param name="totalCount">总记录</param>
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            TotalCount = totalCount;
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source);
        }
        /// <summary>
        /// 当前页面索引
        /// </summary>
        public int PageIndex { get; private set; }
        /// <summary>
        /// 分页数
        /// </summary>
        public int PageSize { get; private set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; private set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; private set; }
        /// <summary>
        /// 是否上一页
        /// </summary>
        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }
        /// <summary>
        /// 是否下一页
        /// </summary>
        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }
    }
}
