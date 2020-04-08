using System;
using FluentAssertions;
using Xunit;

namespace Ketum.Web.Tests
{
    public class KeysTest
    {
        [Fact]
        public void ConnectionStringKeyMustEqual()
        {
            Keys.ConnectionString.Should().Be("KETUM_CONNECTIONSTRING");
        }

        [Fact]
        public void StripeApiKeyMustEqual()
        {
            Keys.StripeAPIKey.Should().Be("STRIPE_API_KEY");
        }
    }
}
