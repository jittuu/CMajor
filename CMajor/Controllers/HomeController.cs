using System.Web.Mvc;

namespace CMajor.Controllers {
    public class HomeController : ApplicationController {
        
        public ActionResult Index() {
            return View();
        }
    }
}