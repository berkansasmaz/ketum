using FluentAssertions;
using Ketum.Web.Controllers;
using Ketum.Web.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Ketum.Web.Tests.Unit
{
    public class HomeConrollerTest
    {
        [Fact]
        public void HomeIndexShouldReturnView()
        {
            var controller = new HomeController();
            var result = controller.Index();
            result.Should().BeOfType<ViewResult>();
        }
        
        [Fact]
        public void HomeErrorShouldReturnView()
        {
            var controller = Helper.GetController<HomeController>();
            var result = controller.Error();
            result.Should().BeOfType<ViewResult>();
        }
    }
}