using System;
using System.Collections.Generic;
using System.Linq;
using HC.Core.Configuration;
namespace HC.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// 配置控制turnBack服务
    /// </summary>
    public class ContainerConfigurer
    {
        ///// <summary>
        ///// 已知配置密钥配置服务
        ///// </summary>
        //public static class ConfigurationKeys
        //{
        //    /// <summary>中等信任.</summary>
        //    public const string MediumTrust = "MediumTrust";
        //    /// <summary>完全信任.</summary>
        //    public const string FullTrust = "FullTrust";
        //}


        //public virtual void Configure(IEngine engine, ContainerManager containerManager, EventBroker broker, HCConfig configuration)
        //{
        //    //其它依赖
        //    containerManager.AddComponentInstance<HCConfig>(configuration, "HC.configuration");
        //    containerManager.AddComponentInstance<IEngine>(engine, "HC.engine");
        //    containerManager.AddComponentInstance<ContainerConfigurer>(this, "HC.containerConfigurer");

        //    //类型查找
        //    containerManager.AddComponent<ITypeFinder, WebAppTypeFinder>("HC.typeFinder");

        //    //提供其它程序集注册的依赖关系
        //    var typeFinder = containerManager.Resolve<ITypeFinder>();
        //    containerManager.UpdateContainer(x =>
        //    {
        //        var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
        //        var drInstances = new List<IDependencyRegistrar>();
        //        foreach (var drType in drTypes)
        //            drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
        //        //排序
        //        drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
        //        //drInstances = drInstances.AsQueryable().ToList();
        //        foreach (var dependencyRegistrar in drInstances)
        //            dependencyRegistrar.Register(x, typeFinder);
        //    });

        //    //事件代理
        //    containerManager.AddComponentInstance(broker);

        //    //注册服务
        //    containerManager.AddComponent<DependencyAttributeRegistrator>("HC.serviceRegistrator");
        //    var registrator = containerManager.Resolve<DependencyAttributeRegistrator>();
        //    var services = registrator.FindServices();
        //    var configurations = GetComponentConfigurations(configuration);
        //    services = registrator.FilterServices(services, configurations);
        //    registrator.RegisterServices(services);
        //}


        //// <summary>
        ///// 获取组件配置信息
        ///// </summary>
        ///// <param name="configuration"></param>
        ///// <returns></returns>
        //protected virtual string[] GetComponentConfigurations(HCConfig configuration)
        //{
        //    var configurations = new List<string>();
        //    string trustConfiguration = (CommonHelper.GetTrustLevel() > System.Web.AspNetHostingPermissionLevel.Medium)
        //        ? ConfigurationKeys.FullTrust
        //        : ConfigurationKeys.MediumTrust;
        //    configurations.Add(trustConfiguration);
        //    return configurations.ToArray();
        //}





        public virtual void Configure(IEngine engine, ContainerManager containerManager, EventBroker broker, HCConfig configuration)
        {
            //other dependencies
            containerManager.AddComponentInstance<HCConfig>(configuration, "nop.configuration");
            containerManager.AddComponentInstance<IEngine>(engine, "nop.engine");
            containerManager.AddComponentInstance<ContainerConfigurer>(this, "nop.containerConfigurer");

            //type finder
            containerManager.AddComponent<ITypeFinder, WebAppTypeFinder>("nop.typeFinder");

            //register dependencies provided by other assemblies
            var typeFinder = containerManager.Resolve<ITypeFinder>();
            containerManager.UpdateContainer(x =>
            {
                var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
                var drInstances = new List<IDependencyRegistrar>();
                foreach (var drType in drTypes)
                    drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
                //sort
                drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
                foreach (var dependencyRegistrar in drInstances)
                    dependencyRegistrar.Register(x, typeFinder);
            });

            //event broker
            containerManager.AddComponentInstance(broker);
        }


    }
}
