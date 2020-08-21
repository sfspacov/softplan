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
    public class Divida : IDivida
    {
        #region Attributes
        private readonly IConfiguration _configuration;
        private readonly IJuros _juros;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iconfiguration">Parameter that contains all configurations from the appsettings.json</param>
        public Divida(IConfiguration iconfiguration, IJuros juros)
        {
            _configuration = iconfiguration;
            _juros = juros;
        }
        #endregion

        #region Public Methods
               /// <summary>
        /// Method that calculates memory, compound interest, according to a formula: Valor Final = Valor Inicial * (1 + juros) ^ Tempo
        /// </summary>
        /// <param name="valorInicial">It's a decimal</param>
        /// <param name="meses">It's a integer</param>
        /// <returns>Calculation result, in decimal format with two places</returns>
        public string CalcularDivida(DividaEntity interestParams)
        {
            try
            {
                if (interestParams.Meses < 0)
                    throw new ArgumentException("Mês não pode ser negativo");

                var apiResult = _juros.RetornarTaxaDeJuros();
                var valorDivida = (double)(interestParams.ValorInicial * (decimal)Math.Pow(1 + apiResult, interestParams.Meses));
                var valorFormatado = valorDivida.ToString("F");

                return valorFormatado;
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
