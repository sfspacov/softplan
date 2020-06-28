using Api2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

/// <summary>
/// Classe that makes Integration Tests in the Api2
/// </summary>
public class Api2Tests
{
    HttpClient client;

    public Api2Tests()
    {
        var path = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, @"..\..\..\..\Api2");
        var environment = "Development";

        var builder = new WebHostBuilder()
          .UseContentRoot(path)
          .UseEnvironment(environment)
          .UseStartup<Startup>();

        var testServer = new TestServer(builder);
        client = testServer.CreateClient();

    }

    [Fact]
    public async Task ShowMeTheCode_CallMethod_Ok()
    {
        var resource = "/showmethecode";

        var response = await client.GetAsync(resource);

        var actual = await response.Content.ReadAsStringAsync();

        var expected = "https://github.com/sfspacov/softplan";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CalculaJuros_Param100And5_Ok()
    {
        var resource = "/calculajuros?valorinicial=100&meses=5";

        var response = await client.GetAsync(resource);

        var actual = await response.Content.ReadAsStringAsync();

        /*
         * OBSERVA��O IMPORTANTE!
         * Como a Api2 est� rodando atrav�s da classe TestServer (linha 24), qdo ela chamar a Api1 internamente, n�o ter� resposta, pois est� n�o est� sendo executada.
         * Ainda que se execute a Api1 usando o mesmo esquema do TestServer, no teste integrado a Api2 tentar� chamar a Api1 sendo executada em algum Host ou IIS.
         * Logo, neste cen�rio, n�o � poss�vel fazer um teste integrado POR AQUI, em que a Api2 chame a Api1 e obtenha o resultado real.
         * Seria necess�rio usar o Selenium ou outra ferramenta do tipo, mas isso foge totalmente do escopo do desafio que me foi proposto pela Softplan.
         */

        var expected = "100,00";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CalculaJuros_100AndNegativeMonth_BadRequest()
    {
        var resource = "/calculajuros?valorinicial=100&meses=-5";

        var response = await client.GetAsync(resource);

        var actual = await response.Content.ReadAsStringAsync();

        var expected = "M�s n�o pode ser negativo";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CalculaJuros_NegativeInitialValueAndMonth_BadRequest()
    {
        var resource = "/calculajuros?valorinicial=-100&meses=5";

        var response = await client.GetAsync(resource);

        var actual = await response.Content.ReadAsStringAsync();

        var expected = "-100,00";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CalculaJuros_WithoutParams_Zero()
    {
        var resource = "/calculajuros";

        var response = await client.GetAsync(resource);

        var actual = await response.Content.ReadAsStringAsync();

        var expected = "0,00";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CalculaJuros_InvalidParam_ErrorMessage()
    {
        var resource = "/calculajuros?valorinicial=t";

        var response = await client.GetAsync(resource);

        var actual = JObject.Parse(await response.Content.ReadAsStringAsync())["ValorInicial"].First;

        var expected = "The value 't' is not valid for ValorInicial.";

        Assert.Equal(expected, actual);
    }
}