using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Core.Infrastructure
{
    /// <summary>
    /// 提供用于启动任务的接口
    /// </summary>
    public interface IStartupTask
    {
        void Execute();
        /// <summary>
        /// 排序
        /// </summary>
        int Order { get; }
    }
}
