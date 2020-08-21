using ApiApplication;
using ApiDomain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ApiSoftPlan.Test
{
    /// <summary>
    /// Classe that makes Unit Tests in the Application layer
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        private readonly Github _github;
        private readonly IConfiguration _configuration;
        private readonly Mock<IJuros> _mockInterest;

        public UnitTest1()
        {
            var myConfiguration = new Dictionary<string, string>();

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            _github = new Github();
            _mockInterest = new MockRepository(MockBehavior.Default).Create<IJuros>(_configuration);
            _mockInterest.Setup(x => x.RetornarTaxaDeJuros()).Returns(0.02);
        }

        [TestMethod]
        public void ShowMeTheCode_CallMethod_Ok()
        {
            var result = _github.ShowMeTheCode();
            var expected = "https://github.com/sfspacov/unitAndIntegrationTest/";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void RetornarTaxaDeJuros_CallMethod_Ok()
        {
            var result = _mockInterest.Object.RetornarTaxaDeJuros();
            var expected = 0.02;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalcularDivida_100E5_Ok()
        {
            var interestParams = new InterestEntity { ValorInicial = 100, Meses = 5 };
            var result = _mockInterest.Object.CalcularDivida(interestParams);
            var expected = "110,41";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalcularDivida_0E0_Ok()
        {
            var interestParams = new InterestEntity { ValorInicial = 0, Meses = 0 };
            var result = _mockInterest.Object.CalcularDivida(interestParams);
            var expected = "0,00";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalcularDivida_ValorInicialNegativoE5_Ok()
        {
            var interestParams = new InterestEntity { ValorInicial = -100, Meses = 5 };
            var result = _mockInterest.Object.CalcularDivida(interestParams);
            var expected = "-110,41";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalcularDivida_100EMesNegativo_Ok()
        {
            var interestParams = new InterestEntity { ValorInicial = 100, Meses = -1 };

            var result = _mockInterest.Object.CalcularDivida(interestParams);
        }
    }
}