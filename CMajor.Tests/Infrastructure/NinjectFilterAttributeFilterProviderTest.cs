using System.Web.Mvc;
using CMajor.Infrastructure;
using Moq;
using Ninject;
using Xunit;

namespace CMajor.Tests.Infrastructure {
    public class NinjectFilterAttributeFilterProviderTest {
        [Fact]
        public void Test_Ninject_should_inject_for_Controller_Attributes() {
            // arrange
            var controllerContext = new ControllerContext() { Controller = new ContollerToBeInjected() };
            var mockControllerDescriptor = new Mock<ControllerDescriptor>();
            mockControllerDescriptor
                .Setup(cd => cd.GetCustomAttributes(typeof(FilterAttribute), true))
                .Returns(new FilterAttribute[] { new MyFilterAttribute() });
            var mockActionDescriptor = new Mock<ActionDescriptor>();
            mockActionDescriptor.Setup(ad => ad.ControllerDescriptor).Returns(mockControllerDescriptor.Object);

            var mockKernel = new Mock<IKernel>();
            mockKernel.Setup(k => k.Inject(It.IsAny<MyFilterAttribute>())).Verifiable();
            var provider = new NinjectFilterAttributeFilterProvider(mockKernel.Object);

            // act
            var filters = provider.GetFilters(controllerContext, mockActionDescriptor.Object);

            // assert
            mockKernel.Verify();
        }

        [Fact]
        public void Test_Ninject_should_inject_for_Action_Attributes() {
            // Arrange
            var context = new ControllerContext { Controller = new ControllerWithActionAttribute() };
            var controllerDescriptor = new ReflectedControllerDescriptor(context.Controller.GetType());
            var action = context.Controller.GetType().GetMethod("ActionMethod");
            var actionDescriptor = new ReflectedActionDescriptor(action, "ActionMethod", controllerDescriptor);

            var mockKernel = new Mock<IKernel>();
            mockKernel.Setup(k => k.Inject(It.IsAny<MyFilterAttribute>())).Verifiable();
            var provider = new NinjectFilterAttributeFilterProvider(mockKernel.Object);

            // act
            provider.GetFilters(context, actionDescriptor);

            // assert
            mockKernel.Verify();
        }

        private class MyFilterAttribute : FilterAttribute {
            
        }

        [MyFilter]
        private class ContollerToBeInjected : Controller {

        }

        private class ControllerWithActionAttribute : Controller {
            [MyFilter]
            public ActionResult ActionMethod() {
                return View();
            }
        }
    }
}
