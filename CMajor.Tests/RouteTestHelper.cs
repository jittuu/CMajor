using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.Routing;
using Moq;
using Xunit;

namespace CMajor.Tests {
    public class RouteTestHelper {
        public static void AssertRoute(RouteCollection routes, string url, object expectations, string httpMethod = "GET", string formMethod = null) {
            var httpContext = CreateHttpContext(url, httpMethod, formMethod);

            RouteData routeData = routes.GetRouteData(httpContext);

            Assert.True(routeData != null, "There is not matched route for provided url: " + url);

            foreach (PropertyValue property in GetProperties(expectations)) {
                var expected = string.Equals(property.Value.ToString(), routeData.Values[property.Name].ToString(), StringComparison.OrdinalIgnoreCase);
                var msg = string.Format("Expected '{0}', not '{1}' for '{2}'.", property.Value, routeData.Values[property.Name], property.Name);

                Assert.True(expected, msg);
            }
        }

        private static HttpContextBase CreateHttpContext(string url, string httpMethod, string formMethod) {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns(url);
            httpContextMock.Setup(c => c.Request.HttpMethod)
               .Returns(httpMethod);
            httpContextMock.Setup(c => c.Request.Form.Get("_method"))
                .Returns(formMethod);
            var httpContext = httpContextMock.Object;
            return httpContext;
        }

        private static IEnumerable<PropertyValue> GetProperties(object o) {
            if (o != null) {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(o);
                foreach (PropertyDescriptor prop in props) {
                    object val = prop.GetValue(o);
                    if (val != null) {
                        yield return new PropertyValue { Name = prop.Name, Value = val };
                    }
                }
            }
        }

        private sealed class PropertyValue {
            public string Name { get; set; }
            public object Value { get; set; }
        }
    }
}
