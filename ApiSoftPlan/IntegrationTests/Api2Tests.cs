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

        client = new TestServer(builder)
            .CreateClient();
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

        var expected = "100,00";

        /*
         * OBSERVAÇÃO IMPORTANTE!
         * Como a Api2 está rodando através da classe TestServer (linha 28), qdo ela chamar a Api1 internamente, não terá resposta, pois está não está sendo executada.
         * Ainda que se execute a Api1 usando o mesmo esquema do TestServer, qdo se executar o teste integrado da Api2, ela tentará chamar a Api1 sendo executada em algum Host ou IIS.
         * Logo, neste cenário, não é possível fazer um teste integrado POR AQUI, em que a Api2 chame a Api1 e obtenha o resultado real.
         * Por isso o resultado esperado é "100,00" e não "105,10", pois a Api1 retorna Juros = 0 para a Api2.
         * Seria necessário usar o Fiddler ou Postman pra ter um teste integrado fiel, mas isso foge totalmente do escopo do desafio proposto pela Softplan.
         */

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CalculaJuros_100AndNegativeMonth_BadRequest()
    {
        var resource = "/calculajuros?valorinicial=100&meses=-5";

        var response = await client.GetAsync(resource);

        var actual = await response.Content.ReadAsStringAsync();

        var expected = "Mês não pode ser negativo";

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
