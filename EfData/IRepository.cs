using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;

namespace ResposityOfEf
{
    /// <summary>
    /// 用于提供实现了该接口以获取、添加、删除、更新、查询功能的Repository模式
    /// </summary>
    /// <typeparam name="T">参数T继承自实体基类BaseEntity</typeparam>
    public partial interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// 根据id（主键）获取实体
        /// </summary>
        T GetById(object id);
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        void Insert(T entity);
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// 查询实体集
        /// </summary>
        IQueryable<T> Table { get; }
    }
}
