using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Integration.Mvc;

namespace HC.Core.Infrastructure.DependencyManagement
{



    public class ContainerManager
    {
        private readonly IContainer _container;

        public ContainerManager(IContainer container)
        {
            _container = container;
        }

        public IContainer Container
        {
            get { return _container; }
        }

        public void AddComponent<TService>(string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
        {
            AddComponent<TService, TService>(key, lifeStyle);
        }

        public void AddComponent(Type service, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
        {
            AddComponent(service, service, key, lifeStyle);
        }

        public void AddComponent<TService, TImplementation>(string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
        {
            AddComponent(typeof(TService), typeof(TImplementation), key, lifeStyle);
        }

        public void AddComponent(Type service, Type implementation, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
        {
            UpdateContainer(x =>
            {
                var serviceTypes = new List<Type> { service };

                if (service.IsGenericType)
                {
                    var temp = x.RegisterGeneric(implementation).As(
                        serviceTypes.ToArray()).PerLifeStyle(lifeStyle);
                    if (!string.IsNullOrEmpty(key))
                    {
                        temp.Keyed(key, service);
                    }
                }
                else
                {
                    var temp = x.RegisterType(implementation).As(
                        serviceTypes.ToArray()).PerLifeStyle(lifeStyle);
                    if (!string.IsNullOrEmpty(key))
                    {
                        temp.Keyed(key, service);
                    }
                }
            });
        }

        public void AddComponentInstance<TService>(object instance, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
        {
            AddComponentInstance(typeof(TService), instance, key, lifeStyle);
        }

        public void AddComponentInstance(Type service, object instance, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
        {
            UpdateContainer(x =>
            {
                var registration = x.RegisterInstance(instance).Keyed(key, service).As(service).PerLifeStyle(lifeStyle);
            });
        }

        public void AddComponentInstance(object instance, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
        {
            AddComponentInstance(instance.GetType(), instance, key, lifeStyle);
        }

        public void AddComponentWithParameters<TService, TImplementation>(IDictionary<string, string> properties, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
        {
            AddComponentWithParameters(typeof(TService), typeof(TImplementation), properties);
        }

        public void AddComponentWithParameters(Type service, Type implementation, IDictionary<string, string> properties, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
        {
            UpdateContainer(x =>
            {
                var serviceTypes = new List<Type> { service };

                var temp = x.RegisterType(implementation).As(serviceTypes.ToArray()).
                    WithParameters(properties.Select(y => new NamedParameter(y.Key, y.Value)));
                if (!string.IsNullOrEmpty(key))
                {
                    temp.Keyed(key, service);
                }
            });
        }

        public T Resolve<T>(string key = "") where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                return Scope().Resolve<T>();
            }
            return Scope().ResolveKeyed<T>(key);
        }

        public object Resolve(Type type)
        {
            return Scope().Resolve(type);
        }

        public T[] ResolveAll<T>(string key = "")
        {
            if (string.IsNullOrEmpty(key))
            {
                return Scope().Resolve<IEnumerable<T>>().ToArray();
            }
            return Scope().ResolveKeyed<IEnumerable<T>>(key).ToArray();
        }

        public T ResolveUnregistered<T>() where T : class
        {
            return ResolveUnregistered(typeof(T)) as T;
        }

        public object ResolveUnregistered(Type type)
        {
            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                try
                {
                    var parameters = constructor.GetParameters();
                    var parameterInstances = new List<object>();
                    foreach (var parameter in parameters)
                    {
                        var service = Resolve(parameter.ParameterType);
                        if (service == null) throw new HCException("Unkown dependency");
                        parameterInstances.Add(service);
                    }
                    return Activator.CreateInstance(type, parameterInstances.ToArray());
                }
                catch (HCException)
                {

                }
            }
            throw new HCException("No contructor was found that had all the dependencies satisfied.");
        }

        public bool TryResolve(Type serviceType, out object instance)
        {
            return Scope().TryResolve(serviceType, out instance);
        }

        public bool IsRegistered(Type serviceType)
        {
            return Scope().IsRegistered(serviceType);
        }

        public object ResolveOptional(Type serviceType)
        {
            return Scope().ResolveOptional(serviceType);
        }


        public void UpdateContainer(Action<ContainerBuilder> action)
        {
            var builder = new ContainerBuilder();
            action.Invoke(builder);
            builder.Update(_container);
        }

        public ILifetimeScope Scope()
        {
            try
            {
                return AutofacRequestLifetimeHttpModule.GetLifetimeScope(Container, null);
            }
            catch
            {
                return Container;
            }
        }
    }
    public static class ContainerManagerExtensions
    {
        public static Autofac.Builder.IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> PerLifeStyle<TLimit, TActivatorData, TRegistrationStyle>(this Autofac.Builder.IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder, ComponentLifeStyle lifeStyle)
        {
            switch (lifeStyle)
            {
                case ComponentLifeStyle.LifetimeScope:
                    return HttpContext.Current != null ? builder.InstancePerHttpRequest() : builder.InstancePerLifetimeScope();
                case ComponentLifeStyle.Transient:
                    return builder.InstancePerDependency();
                case ComponentLifeStyle.Singleton:
                    return builder.SingleInstance();
                default:
                    return builder.SingleInstance();
            }
        }
    }



    ///// <summary>
    ///// 容器管理
    ///// </summary>
    //public class ContainerManager
    //{
    //    private IContainer _container;
    //    public ContainerManager(IContainer container)
    //    {
    //        _container = container;
    //    }

    //    public IContainer Container
    //    {
    //        get { return _container; }
    //    }



    //    /// <summary>
    //    /// 添加组件
    //    /// </summary>
    //    /// <typeparam name="TService"></typeparam>
    //    /// <param name="key"></param>
    //    /// <param name="lifeStyle"></param>
    //    public void AddComponent<TService>(string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
    //    {
    //        AddComponent<TService, TService>(key, lifeStyle);
    //    }
    //    /// <summary>
    //    /// 添加组件
    //    /// </summary>
    //    /// <param name="service"></param>
    //    /// <param name="key"></param>
    //    /// <param name="lifeStyle"></param>
    //    public void AddComponent(Type service, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
    //    {
    //        AddComponent(service, service, key, lifeStyle);
    //    }
    //    /// <summary>
    //    /// 添加组件
    //    /// </summary>
    //    /// <typeparam name="TService"></typeparam>
    //    /// <typeparam name="TImplementation"></typeparam>
    //    /// <param name="key"></param>
    //    /// <param name="lifeStyle"></param>
    //    public void AddComponent<TService, TImplementation>(string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
    //    {
    //        AddComponent(typeof(TService), typeof(TImplementation), key, lifeStyle);
    //    }
    //    /// <summary>
    //    /// 添加组件
    //    /// </summary>
    //    /// <param name="service"></param>
    //    /// <param name="implementation"></param>
    //    /// <param name="key"></param>
    //    /// <param name="lifeStyle"></param>
    //    public void AddComponent(Type service, Type implementation, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
    //    {
    //        UpdateContainer(x =>
    //        {
    //            var serviceTypes = new List<Type> { service };

    //            if (service.IsGenericType)
    //            {
    //                var temp = x.RegisterGeneric(implementation).As(
    //                    serviceTypes.ToArray()).PerLifeStyle(lifeStyle);
    //                if (!string.IsNullOrEmpty(key))
    //                {
    //                    temp.Keyed(key, service);
    //                }
    //            }
    //            else
    //            {
    //                var temp = x.RegisterType(implementation).As(
    //                    serviceTypes.ToArray()).PerLifeStyle(lifeStyle);
    //                if (!string.IsNullOrEmpty(key))
    //                {
    //                    temp.Keyed(key, service);
    //                }
    //            }
    //        });
    //    }
    //    /// <summary>
    //    /// 添加组件实例
    //    /// </summary>
    //    /// <typeparam name="TService"></typeparam>
    //    /// <param name="instance"></param>
    //    /// <param name="key"></param>
    //    /// <param name="lifeStyle"></param>
    //    public void AddComponentInstance<TService>(object instance, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
    //    {
    //        AddComponentInstance(typeof(TService), instance, key, lifeStyle);
    //    }
    //    /// <summary>
    //    /// 添加组件实例
    //    /// </summary>
    //    /// <param name="service"></param>
    //    /// <param name="instance"></param>
    //    /// <param name="key"></param>
    //    /// <param name="lifeStyle"></param>
    //    public void AddComponentInstance(Type service, object instance, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
    //    {
    //        UpdateContainer(x =>
    //        {
    //            var registration = x.RegisterInstance(instance).Keyed(key, service).As(service).PerLifeStyle(lifeStyle);
    //        });
    //    }
    //    /// <summary>
    //    /// 添加组件实例
    //    /// </summary>
    //    /// <param name="instance"></param>
    //    /// <param name="key"></param>
    //    /// <param name="lifeStyle"></param>
    //    public void AddComponentInstance(object instance, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
    //    {
    //        AddComponentInstance(instance.GetType(), instance, key, lifeStyle);
    //    }
    //    /// <summary>
    //    /// 添加组件参数
    //    /// </summary>
    //    /// <typeparam name="TService"></typeparam>
    //    /// <typeparam name="TImplementation"></typeparam>
    //    /// <param name="properties"></param>
    //    /// <param name="key"></param>
    //    /// <param name="lifeStyle"></param>
    //    public void AddComponentWithParameters<TService, TImplementation>(IDictionary<string, string> properties, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
    //    {
    //        AddComponentWithParameters(typeof(TService), typeof(TImplementation), properties);
    //    }
    //    /// <summary>
    //    /// 添加组件参数
    //    /// </summary>
    //    /// <param name="service"></param>
    //    /// <param name="implementation"></param>
    //    /// <param name="properties"></param>
    //    /// <param name="key"></param>
    //    /// <param name="lifeStyle"></param>
    //    public void AddComponentWithParameters(Type service, Type implementation, IDictionary<string, string> properties, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
    //    {
    //        UpdateContainer(x =>
    //        {
    //            var serviceTypes = new List<Type> { service };

    //            var temp = x.RegisterType(implementation).As(serviceTypes.ToArray()).
    //                WithParameters(properties.Select(y => new NamedParameter(y.Key, y.Value)));
    //            if (!string.IsNullOrEmpty(key))
    //            {
    //                temp.Keyed(key, service);
    //            }
    //        });
    //    }
    //    //
    //    public T Resolve<T>(string key = "") where T : class
    //    {
    //        if (string.IsNullOrEmpty(key))
    //        {
    //            return Scope().Resolve<T>();
    //        }
    //        return Scope().ResolveKeyed<T>(key);
    //    }

    //    public object Resolve(Type type)
    //    {
    //        return Scope().Resolve(type);
    //    }

    //    public T[] ResolveAll<T>(string key = "")
    //    {
    //        if (string.IsNullOrEmpty(key))
    //        {
    //            return Scope().Resolve<IEnumerable<T>>().ToArray();
    //        }
    //        return Scope().ResolveKeyed<IEnumerable<T>>(key).ToArray();
    //    }

    //    public T ResolveUnregistered<T>() where T : class
    //    {
    //        return ResolveUnregistered(typeof(T)) as T;
    //    }

    //    public object ResolveUnregistered(Type type)
    //    {
    //        var constructors = type.GetConstructors();
    //        foreach (var constructor in constructors)
    //        {
    //            try
    //            {
    //                var parameters = constructor.GetParameters();
    //                var parameterInstances = new List<object>();
    //                foreach (var parameter in parameters)
    //                {
    //                    var service = Resolve(parameter.ParameterType);
    //                    if (service == null) throw new HCException("Unkown dependency");
    //                    parameterInstances.Add(service);
    //                }
    //                return Activator.CreateInstance(type, parameterInstances.ToArray());
    //            }
    //            catch (HCException)
    //            {

    //            }
    //        }
    //        throw new HCException("eisms contructor was found that had all the dependencies satisfied.");
    //    }

    //    public void UpdateContainer(Action<ContainerBuilder> action)
    //    {
    //        var builder = new ContainerBuilder();
    //        action.Invoke(builder);
    //        builder.Update(_container);
    //    }

    //    /// <summary>
    //    /// 生命周期
    //    /// </summary>
    //    /// <returns></returns>
    //    public ILifetimeScope Scope()
    //    {
    //        try
    //        {
    //            return AutofacRequestLifetimeHttpModule.GetLifetimeScope(Container, null);
    //        }
    //        catch
    //        {
    //            return Container;
    //        }
    //    }
    //}

    ///// <summary>
    ///// ContainerManager扩展方法
    ///// </summary>
    //public static class ContainerManagerExtensions
    //{
    //    public static Autofac.Builder.IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> PerLifeStyle<TLimit, TActivatorData, TRegistrationStyle>(this Autofac.Builder.IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder, ComponentLifeStyle lifeStyle)
    //    {
    //        switch (lifeStyle)
    //        {
    //            case ComponentLifeStyle.LifetimeScope:
    //                return HttpContext.Current != null ? builder.InstancePerHttpRequest() : builder.InstancePerLifetimeScope();
    //            case ComponentLifeStyle.Transient:
    //                return builder.InstancePerDependency();
    //            case ComponentLifeStyle.Singleton:
    //                return builder.SingleInstance();
    //            default:
    //                return builder.SingleInstance();
    //        }
    //    }

    //}
}
