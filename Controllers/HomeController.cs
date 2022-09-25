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

        public ActionResult Callback()
        {
            string code = Request.QueryString["code"];
            string state = Request.QueryString["state"];

            ViewBag.Message = $"Your {_somethingToInject.MyProperty} application description page.";
            ViewBag.Code = code;
            ViewBag.State = state;

            return View();
        }
    }
}