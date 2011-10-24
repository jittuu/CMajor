using System.Web.Mvc;

namespace CMajor.Controllers {
    public class ErrorsController : ApplicationController {
        public ActionResult NotFound() {
            return View("404");
        }
    }
}
