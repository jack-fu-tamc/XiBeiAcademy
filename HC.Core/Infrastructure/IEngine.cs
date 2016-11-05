using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Core.Infrastructure.DependencyManagement;
using HC.Core.Configuration;

namespace HC.Core.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEngine
    {
        ContainerManager ContainerManager { get; }

        /// <summary>
        /// 初始化组件和插件的HC环境
        /// </summary>
        /// <param name="config"></param>
        void Initialize(HCConfig config);

        T Resolve<T>() where T : class;

        object Resolve(Type type);

        Array ResolveAll(Type serviceType);

        T[] ResolveAll<T>();
    }
}
