using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using H2F.TEST.Interface;
using H2F.TEST.Implement;
using System.Web.Http;
using H2F.Standard.Common.Ioc;

namespace H2F.TEST.MVC.Common
{
    public class IocConfig
    {
        public static void RegistAll()
        {
            var iocBuilder = new Autofac.ContainerBuilder();
            iocBuilder.RegisterControllers(Assembly.GetExecutingAssembly());
            iocBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //Type baseType = typeof(IH2FBase);
            var iInterfaces = Assembly.Load("H2F.TEST.Interface");
            var implement = Assembly.Load("H2F.TEST.Implement");
            iocBuilder.RegisterAssemblyTypes(iInterfaces, implement).AsImplementedInterfaces();

            iocBuilder.RegisterType<Student>().As<IPerson>();
            iocBuilder.RegisterType<Worker>().Keyed<IPerson>(PersonType.Worker);

            iocBuilder.RegisterType<Student>().Keyed<IPerson>(PersonType.Student);
            var config = GlobalConfiguration.Configuration;
            iocBuilder.RegisterWebApiFilterProvider(config);
            IContainer iocContainer = iocBuilder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(iocContainer);
            System.Web.Mvc.DependencyResolver.SetResolver(new AutofacDependencyResolver(iocContainer));

            IocContainerManager.SetContanier(iocContainer);
        }
    }
}