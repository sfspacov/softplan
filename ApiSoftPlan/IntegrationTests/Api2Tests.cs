using Api2.Controllers;
using ApiApplication;
using ApiDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

/// <summary>
/// Classe that makes Integration Tests in the Api2
/// </summary>
public class Api2Tests
{
    readonly IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    readonly InterestController interestController;
    readonly GithubController githubController;


    public Api2Tests()
    {
        interestController = new InterestController(new IJuros(config));
        githubController = new GithubController(new Github());
    }

    [Fact]
    public void ShowMeTheCode_CallMethod_Ok()
    {
        var actual = (githubController.Get().Result as OkObjectResult).Value;
        var expected = "https://github.com/sfspacov/unitAndIntegrationTest/";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CalcularDivida_100E5_Ok()
    {
        var parameters = new InterestEntity { Meses = 5, ValorInicial = 100 };
        var resultado = interestController.Get(parameters);
        var actual = (resultado.Result as OkObjectResult).Value;
        var expected = "110,41";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CalcularDivida_100EMesNegativo_BadRequest()
    {
        var parameters = new InterestEntity { Meses = -5, ValorInicial = 100 };
        var resultado = interestController.Get(parameters);
        var actual = (resultado.Result as BadRequestObjectResult).Value;
        var expected = "Mês não pode ser negativo";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CalcularDivida_ValorInicialNegativoE5_BadRequest()
    {
        var parameters = new InterestEntity { Meses = 5, ValorInicial = -100 };
        var resultado = interestController.Get(parameters);
        var actual = (resultado.Result as OkObjectResult).Value;
        var expected = "-110,41";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CalcularDivida_SemParametros_Zero()
    {
        var parameters = new InterestEntity();
        var resultado = interestController.Get(parameters);
        var actual = (resultado.Result as OkObjectResult).Value;
        var expected = "0,00";

        Assert.Equal(expected, actual);
    }
}