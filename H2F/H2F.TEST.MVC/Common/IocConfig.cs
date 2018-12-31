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
            iocBuilder.RegisterType<Student>().As<IPerson>();
            iocBuilder.RegisterType<Student>().Keyed<IPerson>(PersonType.Student);
            iocBuilder.RegisterType<Worker>().Keyed<IPerson>(PersonType.Worker);
            var config = GlobalConfiguration.Configuration;
            iocBuilder.RegisterWebApiFilterProvider(config);
            IContainer iocContainer = iocBuilder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(iocContainer);
            System.Web.Mvc.DependencyResolver.SetResolver(new AutofacDependencyResolver(iocContainer));

            IocContainerManager.SetContanier(iocContainer);
        }
    }
}