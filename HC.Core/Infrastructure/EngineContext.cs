using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HC.Core.Configuration;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace HC.Core.Infrastructure
{
    /// <summary>
    /// 用于提供单例模式访问的引擎
    /// </summary>
    public class EngineContext
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forceRecreate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate)
        {

            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                var config = ConfigurationManager.GetSection("HCConfig") as HCConfig;//HCConfig被实例时会自动执行它的create方法
                Singleton<IEngine>.Instance = CreateEngineInstance(config);//构建引擎
                Singleton<IEngine>.Instance.Initialize(config);//初始化引擎  就是调用HCEngine中的 RunStartupTasks
            }
            return Singleton<IEngine>.Instance;
        }


        /// <summary>
        /// 根据配置去实现最初的 new实例化  如果没有配置则初始化new一个默认的
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IEngine CreateEngineInstance(HCConfig config)
        {
            if (config != null && !string.IsNullOrEmpty(config.EngineType))
            {
                var engineType = Type.GetType(config.EngineType);
                if (engineType == null)
                    throw new Exception("类型 '" + engineType + "' 没有找到. 请检查配置文件 /configuration/eisms/engine[@engineType] 或检查是否缺少程序集.");
                if (!typeof(IEngine).IsAssignableFrom(engineType))
                    throw new Exception("类型 '" + engineType + "' 不能执行 'EISMS.Core.Infrastructure.IEngine' 并且在 /configuration/eisms/engine[@engineType] 不能配置用于这一目的.");
                return Activator.CreateInstance(engineType) as IEngine;//根据config文件配置去 反射一个实例
            }
            return new HCEngine();
        }



        /// <summary>
        /// 返回一个实现IEngine接口的类
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Initialize(false);
                }
                return Singleton<IEngine>.Instance;
            }
        }
    }
}
