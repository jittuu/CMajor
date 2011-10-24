using System.Web.Mvc;
using CMajor.Infrastructure;

namespace CMajor.Controllers {
    [UnitOfWork]
    public abstract class ApplicationController : Controller {        
    }
}