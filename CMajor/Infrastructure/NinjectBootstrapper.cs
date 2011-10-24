using System.Web.Mvc;
using CMajor.Models;
using Ninject;

namespace CMajor.Infrastructure {
    public static class NinjectBootstrapper {        
        public static void Init() {
            IKernel kernel = new StandardKernel();

            SetupMapping(kernel);

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }

        private static void SetupMapping(IKernel kernel) {
            kernel.Bind<IFilterProvider>().To<NinjectFilterAttributeFilterProvider>().InSingletonScope();

            kernel.Bind<IUnitOfWork>().To<CMajorDbContext>().InRequestScope();
        }
    }
}