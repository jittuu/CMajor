using System.Web.Mvc;
using Ninject;

namespace CMajor.Infrastructure {
    public class UnitOfWorkAttribute : ActionFilterAttribute {

        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            base.OnActionExecuted(filterContext);

            if (filterContext.Exception == null) {
                this.UnitOfWork.Commit();
            }
        }
    }
}