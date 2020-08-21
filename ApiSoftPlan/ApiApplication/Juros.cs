using ApiDomain.Contracts;
using Microsoft.Extensions.Configuration;

namespace ApiApplication
{
    /// <summary>
    /// Interest application class
    /// </summary>
    public class Juros : IJuros
    {
        #region Attributes
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iconfiguration">Parameter that contains all configurations from the appsettings.json</param>
        public Juros(IConfiguration iconfiguration)
        {
            _configuration = iconfiguration;
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
        #endregion
    }
}
