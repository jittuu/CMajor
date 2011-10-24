using System.Linq;
using System.Web.Mvc;
using CMajor.Controllers;
using Xunit;

namespace CMajor.Tests.Controllers {
    public class ApplicationControllerTest {
        [Fact]
        public void Test_all_controllers_should_inherits_from_ApplicationController() {
            var controllerTypes = from t in typeof(ApplicationController).Assembly.GetTypes()
                                  where t.IsSubclassOf(typeof(Controller))
                                  && !t.IsAbstract
                                  select t;

            foreach (var controllerType in controllerTypes) {
                Assert.Equal(true, controllerType.IsSubclassOf(typeof(ApplicationController)));
            }
        }
    }
}
