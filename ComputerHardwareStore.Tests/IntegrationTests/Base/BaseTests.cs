using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ComputerHardwareStore.Tests.IntegrationTests.Base
{
    public class BaseTests : IClassFixture<WebApplicationFactory<Startup>>, IDisposable, IAsyncDisposable
    {
        protected readonly WebApplicationFactory<Startup> Factory;

        public BaseTests()
        {
            Factory = new WebApplicationFactory<Startup>();
        }
        
        public void Dispose() => Factory.Dispose();

        public async ValueTask DisposeAsync()
        {
            await Task.Run(() =>
            {
                Factory.Dispose();
                return Task.FromResult(true);
            });
        }
    }
}