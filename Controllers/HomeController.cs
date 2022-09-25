using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
            // Test of Injected class
            ViewBag.Message = $"Your injected value: {_somethingToInject.MyProperty}";

            ViewBag.AuthorizeURL = ConfigurationManager.AppSettings["AuthorizeURL"];
            ViewBag.ClientId = ConfigurationManager.AppSettings["ClientId"];
            ViewBag.RedirectURL = ConfigurationManager.AppSettings["RedirectURL"];
            ViewBag.Scope = ConfigurationManager.AppSettings["Scope"];
            ViewBag.ResponseType = ConfigurationManager.AppSettings["ResponseType"];
            ViewBag.ResponseMode = ConfigurationManager.AppSettings["ResponseMode"];
            ViewBag.GrantType = ConfigurationManager.AppSettings["GrantType"];
            ViewBag.ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            ViewBag.Code = Session["Code"]?.ToString();
            ViewBag.Nonce = 1234;
            ViewBag.State = 5678;

            return View();
        }

        [HttpGet()]
        public async Task<ActionResult> Callback()
        {
            string code = Request.QueryString["code"];
            string state = Request.QueryString["state"];

            // OpenID Code
            ViewBag.Code = code;
            // State needs to be check to see if it changed
            ViewBag.State = state;

            Session["Code"] = code;
            Session["State"] = state;

            return View();
        }

        [HttpGet()]
        public async Task<ActionResult> Token()
        {
            string code = Session["Code"].ToString();

            var x = Response;

            //HttpClient client = new HttpClient();

            //var values = new Dictionary<string, string>();

            //values.Add("grant_type", "authorization_code");
            //values.Add("client_id", "4a9ad1ee0a3795e4d396");
            //values.Add("client_secret", "e0412727156f5b6cde816bfbd67c5e3cb0e3f941");
            //values.Add("redirect_uri", @"https://localhost:44396/Home/Callback");
            //values.Add("code", code);

            //var content = new FormUrlEncodedContent(values);

            //var response = await client.PostAsync(@"https://github.com/login/oauth/authorize", content);

            //var responseString = await response.Content.ReadAsStringAsync();

            //ViewBag.Token = responseString;

            return View();
        }
    }
}