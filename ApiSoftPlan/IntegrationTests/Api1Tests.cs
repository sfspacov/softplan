using Api1.Controllers;
using ApiApplication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Xunit;

/// <summary>
/// Classe that makes Integration Tests in the Api1
/// </summary>
public class Api1Tests
{
    readonly IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    readonly InterestController interestController;

    public Api1Tests()
    {
        interestController = new InterestController(new Interest(config));
    }

    [Fact]
    public void RetornarTaxaJuros_CallMethod_Ok()
    {
        var resultado = interestController.Get();

        var actual = (resultado.Result as OkObjectResult).Value;

        var expected = 0.02;

        Assert.Equal(expected, actual);
    }
}