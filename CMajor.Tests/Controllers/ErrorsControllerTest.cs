using System.Web.Mvc;
using CMajor.Controllers;
using Xunit;

namespace CMajor.Tests.Controllers {
    public class ErrorsControllerTest {
        [Fact]
        public void Test_Errors_NotFound_should_return_ViewResult_with_404_view() {
            var controller = new ErrorsController();
            var result = controller.NotFound();

            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.Equal("404", viewResult.ViewName);
        }
    }
}
