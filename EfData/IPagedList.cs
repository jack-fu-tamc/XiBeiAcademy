
using System.Collections.Generic;

namespace ResposityOfEf
{
    /// <summary>
    /// 用于提供以实现分页列表的接口
    /// </summary>
    public interface IPagedList<T> : IList<T>
    {
        /// <summary>
        /// 获取当前页面索引
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// 获取分页数
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// 获取总记录数
        /// </summary>
        int TotalCount { get; }
        /// <summary>
        /// 获取总页数
        /// </summary>
        int TotalPages { get; }
        /// <summary>
        /// 是否上一页
        /// </summary>
        bool HasPreviousPage { get; }
        /// <summary>
        /// 是否下一页
        /// </summary>
        bool HasNextPage { get; }
    }
}
