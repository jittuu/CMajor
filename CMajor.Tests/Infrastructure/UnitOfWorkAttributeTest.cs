using System;
using System.Web.Mvc;
using CMajor.Infrastructure;
using Moq;
using Xunit;

namespace CMajor.Tests.Infrastructure {
    public class UnitOfWorkAttributeTest {
        [Fact]
        public void Test_Should_Commit_if_there_is_no_exception() {
            var uowAttr = new UnitOfWorkAttribute();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(u => u.Commit()).Verifiable();

            uowAttr.UnitOfWork = mockUoW.Object;
            uowAttr.OnActionExecuted(new ActionExecutedContext());

            mockUoW.Verify();
        }

        [Fact]
        public void Test_Should_not_commit_if_there_is_Exception() {
            var uowAttr = new UnitOfWorkAttribute();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(u => u.Commit()).Throws<InvalidOperationException>();

            uowAttr.UnitOfWork = mockUoW.Object;
            uowAttr.OnActionExecuted(new ActionExecutedContext() { Exception = new Exception() });
        }
    }
}
