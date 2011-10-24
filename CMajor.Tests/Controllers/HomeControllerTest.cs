using System.Web.Mvc;
using System.Web.Routing;
using CMajor.Controllers;
using Xunit;

namespace CMajor.Tests.Controllers {
    public class HomeControllerTest {
        [Fact]
        public void Test_root_should_route_to_home_index() {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            RouteTestHelper.AssertRoute(routes, "~/", new { controller = "home", action = "index" });
        }

        [Fact]
        public void Test_home_index_should_return_ViewResult() {
            var controller = new HomeController();
            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }
    }
}
