using ApiDomain.Contracts;
using ApiDomain.Entities;
using Microsoft.Extensions.Configuration;
using SimpleLogger;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiApplication
{
    /// <summary>
    /// Interest application class
    /// </summary>
    public class Interest : IInterest
    {
        #region Attributes
        private IConfiguration configuration;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iConfig">Parameter that contains all configurations from the appsettings.json</param>
        public Interest(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method that returns interest rates
        /// </summary>
        /// <returns>Interest rates</returns>
        public double GetInterest()
        {
            return 0.01;
        }

        /// <summary>
        /// Method that get the interest value from Api1
        /// </summary>
        /// <returns>Interest value</returns>
        public virtual async Task<double> GetInterestRate()
        {
            double result = 0;

            //MaxTries and while are part of a resilience system, so the consumed Api is called three times until throw an exception.
            var maxTries = configuration.GetValue<int>("Api:MaxTries");
            var tries = 0;

            while (tries < maxTries)
            {
                try
                {
                    var interest = string.Empty;
                    var client = new HttpClient();
                    var url = configuration.GetValue<string>("Api:Url");
                    var resource = configuration.GetValue<string>("Api:Resource");

                    var apiReturn = await client.GetAsync(url + resource);

                    if (apiReturn.StatusCode == System.Net.HttpStatusCode.OK)
                        interest = await apiReturn.Content.ReadAsStringAsync();
                    else
                        interest = await client.GetAsync(url + resource).Result.Content.ReadAsStringAsync();

                    var provider = new NumberFormatInfo
                    {
                        NumberDecimalSeparator = ".",
                    };

                    result = Convert.ToDouble(interest, provider);

                    return result;
                }
                catch (Exception e)
                {
                    tries++;
                    if (tries == maxTries)
                    {
                        SimpleLog.Error("Message: " + e.Message + "; InnerException: " + e.InnerException);
                        throw e;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Method that calculates memory, compound interest, according to a formula: Valor Final = Valor Inicial * (1 + juros) ^ Tempo
        /// </summary>
        /// <param name="valorInicial">It's a decimal</param>
        /// <param name="meses">It's a integer</param>
        /// <returns>Calculation result, in decimal format with two places</returns>
        public async Task<string> CalculateInterest(InterestEntity interestParams)
        {
            try
            {
                if (interestParams.Meses < 0)
                    throw new ArgumentException("Mês não pode ser negativo");

                var apiResult = await GetInterestRate();
                var result = (double)(interestParams.ValorInicial * (decimal)Math.Pow(1 + apiResult, interestParams.Meses));

                return result.ToString("F");
            }
            catch (Exception e)
            {
                SimpleLog.Error("Message: " + e.Message + "; InnerException: " + e.InnerException);
                throw e;
            }
        }
        #endregion
    }
}
