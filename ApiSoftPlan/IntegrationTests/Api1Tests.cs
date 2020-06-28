using Api1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

/// <summary>
/// Classe that makes Integration Tests in the Api1
/// </summary>
public class Api1Tests
{
    HttpClient client;

    public Api1Tests()
    {
        var path = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, @"..\..\..\..\Api1");
        var environment = "Development";

        var builder = new WebHostBuilder()
          .UseContentRoot(path)
          .UseEnvironment(environment)
          .UseStartup<Startup>();

        var testServer = new TestServer(builder);
        client = testServer.CreateClient();

    }

    [Fact]
    public async Task TaxaJuros_CallMethod_Ok()
    {
        var resource = "/taxaJuros";
        
        var response = await client.GetAsync(resource);

        var actual = await response.Content.ReadAsStringAsync();

        var expected = "0.01";

        Assert.Equal(expected, actual);
    }
}