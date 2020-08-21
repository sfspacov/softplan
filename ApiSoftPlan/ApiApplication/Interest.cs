using ApiDomain.Contracts;
using ApiDomain.Entities;
using Microsoft.Extensions.Configuration;
using SimpleLogger;
using System;

namespace ApiApplication
{
    /// <summary>
    /// Interest application class
    /// </summary>
    public class IJuros : ApiDomain.Contracts.IJuros
    {
        #region Attributes
        private IConfiguration configuration;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iConfig">Parameter that contains all configurations from the appsettings.json</param>
        public IJuros(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method that returns interest rates
        /// </summary>
        /// <returns>Interest rates</returns>
        public virtual double RetornarTaxaDeJuros()
        {
            return 0.02;
        }

        /// <summary>
        /// Method that calculates memory, compound interest, according to a formula: Valor Final = Valor Inicial * (1 + juros) ^ Tempo
        /// </summary>
        /// <param name="valorInicial">It's a decimal</param>
        /// <param name="meses">It's a integer</param>
        /// <returns>Calculation result, in decimal format with two places</returns>
        public string CalcularDivida(InterestEntity interestParams)
        {
            try
            {
                if (interestParams.Meses < 0)
                    throw new ArgumentException("Mês não pode ser negativo");

                var apiResult = RetornarTaxaDeJuros();
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
