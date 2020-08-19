using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

/// <summary>
/// Classe that makes Integration Tests in the Api2
/// </summary>
public class Api2Tests
{
    HttpClient client = new HttpClient();

    public Api2Tests()
    {
    }

    [Fact]
    public async Task ShowMeTheCode_CallMethod_Ok()
    {
        var url = "http://localhost:5002/";

        var resource = "/showmethecode";

        var response = await client.GetAsync(url + resource);

        var actual = string.Empty;

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            actual = await response.Content.ReadAsStringAsync();
        else
            actual = await client.GetAsync(url + resource).Result.Content.ReadAsStringAsync();

        var expected = "https://github.com/sfspacov/testesUnitariosEIntegrados/";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CalculaJuros_Param100And5_Ok()
    {
        var url = "http://localhost:5002/";

        var resource = "/calculajuros?valorinicial=100&meses=5";

        var response = await client.GetAsync(url + resource);

        var actual = string.Empty;

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            actual = await response.Content.ReadAsStringAsync();
        else
            actual = await client.GetAsync(url + resource).Result.Content.ReadAsStringAsync();

        var expected = "110,41";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CalculaJuros_100AndNegativeMonth_BadRequest()
    {
        var url = "http://localhost:5002/";

        var resource = "/calculajuros?valorinicial=100&meses=-5";

        var response = await client.GetAsync(url + resource);

        var actual = string.Empty;

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            actual = await response.Content.ReadAsStringAsync();
        else
            actual = await client.GetAsync(url + resource).Result.Content.ReadAsStringAsync();

        var expected = "Mês não pode ser negativo";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CalculaJuros_NegativeInitialValueAndMonth_BadRequest()
    {
        var url = "http://localhost:5002/";

        var resource = "/calculajuros?valorinicial=-100&meses=5";

        var response = await client.GetAsync(url + resource);

        var actual = string.Empty;

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            actual = await response.Content.ReadAsStringAsync();
        else
            actual = await client.GetAsync(url + resource).Result.Content.ReadAsStringAsync();

        var expected = "-110,41";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CalculaJuros_WithoutParams_Zero()
    {
        var url = "http://localhost:5002/";

        var resource = "/calculajuros";

        var response = await client.GetAsync(url + resource);

        var actual = string.Empty;

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            actual = await response.Content.ReadAsStringAsync();
        else
            actual = await client.GetAsync(url + resource).Result.Content.ReadAsStringAsync();

        var expected = "0,00";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CalculaJuros_InvalidParam_ErrorMessage()
    {
        var url = "http://localhost:5002/";

        var resource = "/calculajuros?valorinicial=t";

        var response = await client.GetAsync(url + resource);

        JToken actual;

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            actual = JObject.Parse(await response.Content.ReadAsStringAsync())["ValorInicial"].First;
        else
            actual = JObject.Parse(await client.GetAsync(url + resource).Result.Content.ReadAsStringAsync())["ValorInicial"].First;

        var expected = "The value 't' is not valid for ValorInicial.";

        Assert.Equal(expected, actual);
    }
}