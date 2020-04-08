using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Ketum.Web.Tests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Ketum.Web.Tests.Integration
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
        
        // Integration Test çünkü proje ayağa kalkıyor ve client tarafından dönen cevap üzerinden hareket ediyoruz. Çalışan bir proje üzerinden yapılır. Süreç test edilmiş olur.
        [Fact]
        public async Task HomeIndexShouldRedirectToLogin()
        {
            var response = await _client.GetAsync("/Home/Index");
            response.IsSuccessStatusCode.Should().BeFalse();
            response.Headers.Location.AbsoluteUri.Should().StartWith("http://localhost/Identity/Account/Login");
            response.StatusCode.Should().Be(302);
        }
    }
}