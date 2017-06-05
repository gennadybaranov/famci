using GameForum.DomainModel.Domain;
using GameForum.DomainModel.Services;
using GameForum.DomainModel.Services.Abstractions;
using System.Web.Mvc;
using GameForum.Web.Infrastructure.Filters;

namespace GameForum.Web.Infrastructure.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
           
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
           
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
