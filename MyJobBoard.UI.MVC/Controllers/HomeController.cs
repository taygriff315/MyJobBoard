using System.Web.Mvc;

namespace MyJobBoard.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserProfile()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Application()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
