using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using HC.Core.Configuration;

using Autofac;
using HC.Core.Infrastructure.DependencyManagement;


namespace HC.Core.Infrastructure
{
    public class HCEngine:IEngine
    {

        #region filed
        private ContainerManager _containerManager;
        #endregion

        #region Ctor
        public HCEngine()
            : this(EventBroker.Instance, new ContainerConfigurer())
        { 
          
        }

        public HCEngine(EventBroker broker,ContainerConfigurer configurer)
        {
            var config = ConfigurationManager.GetSection("HCConfig") as HCConfig;
            InitializeContainer(configurer, broker, config);//初始化容器
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 启动任务
        /// </summary>
        private void RunStartupTasks()
        {
            var typeFinder = _containerManager.Resolve<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
            //排序
            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }

        /// <summary>
        /// 初始化容器
        /// </summary>
        /// <param name="configurer"></param>
        /// <param name="broker"></param>
        /// <param name="config"></param>
        private void InitializeContainer(ContainerConfigurer configurer, EventBroker broker, HCConfig config)
        {
            var builder = new ContainerBuilder();
            _containerManager = new ContainerManager(builder.Build());
            configurer.Configure(this, _containerManager, broker, config);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 初始化组件和插件的HCHE环境。
        /// </summary>
        /// <param name="config">Config</param>
        public void Initialize(HCConfig config)
        {
            //bool databaseInstalled = DataSettingsHelper.DatabaseIsInstalled();
           // if (databaseInstalled)
            //{
                //启动任务
                //RunStartupTasks();
            //}

                if (!config.IgnoreStartupTasks)
                {
                    RunStartupTasks();
                }
        }

        /// <summary>
        /// Resolve T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }
        /// <summary>
        /// Resolve object
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        public Array ResolveAll(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }





        #endregion

        #region Properties

        public IContainer Container
        {
            get { return _containerManager.Container; }
        }

        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }

        #endregion
    }
}
