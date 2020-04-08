using System;
using FluentAssertions;
using Xunit;

namespace Ketum.Web.Tests.Unit
{
    public class EnvironmentVariableTests
    {
        [Fact]
        public void ConnectionStringMustBeInEnvironmentVariable()
        {
            var myCustomValue = "ABCDEFGH";
            Environment.SetEnvironmentVariable(Keys.ConnectionString, myCustomValue);
            var value = Environment.GetEnvironmentVariable(Keys.ConnectionString);
            value.Should().NotBeNullOrEmpty();
            value.Should().Be(myCustomValue);
        }
        

        [Fact]
        public void StripeApiKeyMustBeInEnvironmentVariable()
        {
            var myCustomValue = "TEST";
            Environment.SetEnvironmentVariable(Keys.StripeAPIKey, myCustomValue);
            var value = Environment.GetEnvironmentVariable(Keys.StripeAPIKey);
            value.Should().NotBeNullOrEmpty();
            value.Should().Be(myCustomValue);
        }
    }
}