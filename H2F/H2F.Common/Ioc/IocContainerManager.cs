using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
//
using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Integration.Mvc;
using H2F.Standard.Common.Extensions;
namespace H2F.Standard .Common.Ioc
{
    public static class IocContainerManager
    {
        private static IContainer _container;
        public static void SetContanier(IContainer container)
        {
            _container = container;
        }

        public static IContainer Container
        {
            get { return _container; }
        }

        public static T Resolve<T>(string key = "", ILifetimeScope scope = null) where T : class
        {
            scope=scope.InitScope();

            if (key.IsNullOrWhiteSpace())
            {
                return scope.Resolve<T>();
            }

            return scope.ResolveKeyed<T>(key);
        }

        public static object Resolve(Type type, ILifetimeScope scope = null)
        {
            scope=scope.InitScope();
            return scope.Resolve(type);
        }

        public static T[] ResolveAll<T>(string key = "", ILifetimeScope scope = null)
        {
            scope=scope.InitScope();
            if (key.IsNullOrWhiteSpace())
            {
                return scope.Resolve<IEnumerable<T>>().ToArray();
            }

            return scope.ResolveKeyed<IEnumerable<T>>(key).ToArray();
        }

        public static T ResolveUnregisted<T>(ILifetimeScope scope = null) where T : class
        {
            return ResolveUnregisted(typeof(T), scope) as T;
        }

        public static object ResolveUnregisted(Type type, ILifetimeScope scope = null)
        {
            scope=scope.InitScope();
            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                try
                {
                    var parameters = constructor.GetParameters();
                    var parameterInstances = new List<object>();
                    foreach (var param in parameters)
                    {
                        var service = Resolve(param.ParameterType, scope);
                        if (service.IsNull())
                        {
                            throw new ArgumentException("Unkonw Dependency");
                        }
                        parameterInstances.Add(service);
                    }
                    return Activator.CreateInstance(type, parameterInstances.ToArray());
                }
                catch (ArgumentException)
                {
                }
            }
            throw new ArgumentException("can't find your needed constructor from dependecy");
        }

        public static bool TryResolve(Type serviceTye, ILifetimeScope scope,out object instance)
        {
            scope=scope.InitScope();
            return scope.TryResolve(serviceTye, out instance);
        }

        public static bool IsRegisted(Type serviceType, ILifetimeScope scope = null)
        {
            scope=scope.InitScope();
            return scope.IsRegistered(serviceType);
        }

        public static object ResolveOptional(Type serviceType, ILifetimeScope scope = null)
        {
            scope=scope.InitScope();
            return scope.ResolveOptional(serviceType);
        }

        public static ILifetimeScope DefaultScope(bool isHttpContext=true)
        {
            try
            {
                if (isHttpContext)
                {
                    return AutofacDependencyResolver.Current.RequestLifetimeScope;
                }

                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
            catch (Exception )
            {
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
        }

        private static ILifetimeScope InitScope(this ILifetimeScope scope)
        {
            if (scope.IsNull())
            {
                scope = DefaultScope();
            }
            return scope;
        }
    }
}
