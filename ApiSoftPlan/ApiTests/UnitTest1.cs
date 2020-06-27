using Api1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using System.Threading.Tasks;
using Xunit;

public class BasicTests
{
    private readonly string path = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, @"..\..\..\..\");
    private readonly string environment = "Development";

    [Fact]
    public async Task TaxaJuros_CallMethod_Ok()
    {
        var testedApiName = "Api1";
        var resource = "/taxaJuros";

        var builder = new WebHostBuilder()
          .UseContentRoot(path + testedApiName)
          .UseEnvironment(environment)
          .UseStartup<Startup>();

        var testServer = new TestServer(builder);

        var client = testServer.CreateClient();

        var response = await client.GetAsync(resource);

        // Fail the test if non-success result
        response.EnsureSuccessStatusCode();

        var actual = await response.Content.ReadAsStringAsync();

        var expected = "0.01";

        Assert.Equal(expected, actual);
    }
}