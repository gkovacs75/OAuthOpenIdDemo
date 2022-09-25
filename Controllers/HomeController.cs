using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ISomethingToInject _somethingToInject { get; }

        public HomeController(ISomethingToInject somethingToInject)
        {
            _somethingToInject = somethingToInject;

            _somethingToInject.MyProperty = "XYZ";
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = $"Your {_somethingToInject.MyProperty} application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}