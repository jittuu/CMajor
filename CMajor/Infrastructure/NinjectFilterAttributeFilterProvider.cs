using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

namespace CMajor.Infrastructure {
    /*
     * For global filters, this NinjectFilterAttributeFilterProvider is NOT used. 
     * So, we need to inject while registering global filters.
     * 
     * e.g.
     * 
     * var filter = new MessageFilterAttribute();
     * kernel.Inject(filter);
     * GlobalFilters.Filters.Add(filter);
     * 
     */
    public class NinjectFilterAttributeFilterProvider : FilterAttributeFilterProvider {
        private readonly IKernel _kernel;

        public NinjectFilterAttributeFilterProvider(IKernel kernel) {
            this._kernel = kernel;
        }

        protected override IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor) {
            var attributes = base.GetControllerAttributes(controllerContext, actionDescriptor);
            foreach (var attribute in attributes) {
                this._kernel.Inject(attribute);
            }

            return attributes;
        }

        protected override IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor) {
            var attributes = base.GetActionAttributes(controllerContext, actionDescriptor);
            foreach (var attribute in attributes) {
                this._kernel.Inject(attribute);
            }

            return attributes;
        }
    }
}