using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Moq;
using ActionDescriptor = Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor;

namespace Ketum.Web.Tests.Helpers
{
    public static class Helper
    {
        internal static HttpClient GetClient<T>(WebApplicationFactory<T> factory) where T : class
        {
            Environment.SetEnvironmentVariable(Keys.TestProject, "true");
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                HandleCookies = true,
                AllowAutoRedirect = false
            };
            var client = factory.CreateClient(clientOptions);
            return client;
        }

        internal static TController GetController<TController>() where TController : Controller, new()
        {
            var request = new Mock<HttpRequest>();
            var serviceProvider = new Mock<IServiceProvider>();

            request.Setup(r => r.Query).Returns(new QueryCollection());
            request.Setup(r => r.Cookies).Returns(new RequestCookieCollection());
            request.Setup(r => r.Headers).Returns(new HeaderDictionary());

            var defaultContext = new DefaultHttpContext();
            defaultContext.Request.Scheme = "http";
            defaultContext.Request.Host = new HostString("localhost");
            defaultContext.RequestServices = serviceProvider.Object;

            var controllerContext = new ControllerContext();
            controllerContext.HttpContext = defaultContext;
            controllerContext.RouteData = new RouteData();

            var controller = new TController();
            controller.ControllerContext = controllerContext;
            controller.TempData = Mock.Of<ITempDataDictionary>();

            var actionContext = new ActionContext
            {
                HttpContext = defaultContext,
                RouteData = controllerContext.RouteData,
                ActionDescriptor = new ActionDescriptor()
            };
            var context = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                controller);

            context.HttpContext.RequestServices = serviceProvider.Object;

            controller.OnActionExecuting(context);

            return controller;
        }
    }
}
