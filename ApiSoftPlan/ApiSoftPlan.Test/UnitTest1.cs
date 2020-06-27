using ApiApplication;
using ApiDomain.Contracts;
using ApiDomain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSoftPlan.Test
{
    [TestClass]
    public class UnitTest1
    {
        private readonly Github _github;
        private readonly Interest _interest;

        public UnitTest1()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"Api:Url", "http://localhost:1970/"},
                {"Api:Resource", "taxaJuros"},
                {"Api:MaxTries", "3"},
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            _github = new Github();
            _interest = new Interest(configuration);
        }

        [TestMethod]
        public void ShowMeTheCode_CallMethod_Ok()
        {
            var result = _github.ShowMeTheCode();
            var expected = "https://github.com/sfspacov/softplan";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetInterest_CallMethod_Ok()
        {
            var result = _interest.GetInterest();
            var expected = 0.01;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetInterestRate_CallMethod_Ok()
        {
            var mockInterest = new Mock<IInterest>();
            mockInterest.Setup(x => x.GetInterestRate()).Returns(Task.FromResult(0.01));
            var result = await mockInterest.Object.GetInterestRate();
            var expected = 0.01;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task Calculate_Param100And5_Ok()
        {
            var interestParams = new InterestEntity { ValorInicial = 100, Meses = 5 };
            var mockInterest = new Mock<IInterest>();
            mockInterest.Setup(x => x.CalculateInterest(interestParams)).Returns(Task.FromResult("105.10"));
            var result = await mockInterest.Object.CalculateInterest(interestParams);
            var expected = "105.10";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task Calculate_Params0And0_Ok()
        {
            var interestParams = new InterestEntity { ValorInicial = 0, Meses = 0 };
            var mockInterest = new Mock<IInterest>();
            mockInterest.Setup(x => x.CalculateInterest(interestParams)).Returns(Task.FromResult("0,00"));
            var result = await mockInterest.Object.CalculateInterest(interestParams);
            var expected = "0,00";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task Calculate_ParamsNegativeValueAnd5_Ok()
        {
            var interestParams = new InterestEntity { ValorInicial = -10, Meses = 5 };
            var mockInterest = new Mock<IInterest>();
            mockInterest.Setup(x => x.CalculateInterest(interestParams)).Returns(Task.FromResult("-10,51"));
            var result = await mockInterest.Object.CalculateInterest(interestParams);
            var expected = "-10,51";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Calculate_Param100AndNegativeMounth_Ok()
        {
            var interestParams = new InterestEntity { ValorInicial = 100, Meses = -1 };

            var result = await _interest.CalculateInterest(interestParams);
        }
    }
}