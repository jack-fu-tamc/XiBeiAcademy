using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HC.Core.Infrastructure;
using System.Web.Mvc;

namespace HC.Web.Framework.Mvc
{
    /// <summary>
    /// 自定义解析器
    /// </summary>
    public class HCDependencyResolver : IDependencyResolver
    {

        public object GetService(Type serviceType)
        {
            return EngineContext.Current.ContainerManager.ResolveOptional(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                var type = typeof(IEnumerable<>).MakeGenericType(serviceType);
                return (IEnumerable<object>)EngineContext.Current.Resolve(type);
            }
            catch
            {
                return null;
            }
        }
    }
}
