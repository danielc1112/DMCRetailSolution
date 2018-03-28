using System.Web.Mvc;

namespace PosWebApp.Controllers
{
    public class HomeController : Controller
    {   
        public ActionResult Index()
        {
            return View();
        }
    }
}