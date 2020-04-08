using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Ketum.Web.Tests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Ketum.Web.Tests
{
    public class HomeControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public HomeControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = Helper.GetClient(_factory);
        }

        [Fact]
        public async Task HomeIndexShouldReturnAction()
        {
            var response = await _client.GetAsync("/Home/Index");
            response.Headers.Should().Contain("X-Ketum-ResponseId");
        }
    }
}