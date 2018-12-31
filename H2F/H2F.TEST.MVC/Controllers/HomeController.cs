using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using H2F.TEST.Interface;
using H2F.Standard.Common.Ioc;
using Autofac.Features.Indexed;
using H2F.TEST.MVC.Common;
using Autofac;
using System.Threading.Tasks;

namespace H2F.TEST.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IPerson person;

        public HomeController(IPerson p)
        {
            person = p;
        }
        public ActionResult Index()
        {
           var na=  IocContainerManager.Resolve<IPerson>().GetNames();
            Response.Write(na);
            Response.Write(person.GetNames());

            IIndex<PersonType, IPerson> IIndex = IocContainerManager.Container.Resolve<IIndex<PersonType, IPerson>>();
            IPerson p = IIndex[PersonType.Worker];

            Response.Write(p.GetNames());

            string str = "";
            var t = Task.Factory.StartNew(() => {
                str = IocContainerManager.Resolve<IPerson>().GetNames();
            });
          //  t.Start();
            t.Wait();

            //Task.WaitAll();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}