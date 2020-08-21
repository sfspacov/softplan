using ApiDomain.Contracts;
using Microsoft.AspNetCore.Mvc;
using SimpleLogger;
using System;

namespace Api1.Controllers
{
    /// <summary>
    /// Controller Interest
    /// </summary>
    [Route("")]
    [ApiController]
    public class JurosController : ControllerBase
    {
        #region Attributes
        private readonly IJuros _juros;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="juros">Interest interface</param>
        public JurosController(IJuros juros)
        {
            _juros = juros;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method that returns interest rates
        /// </summary>
        /// <returns>Interest rates</returns>
        [HttpGet("retornarTaxaDeJuros")]
        public ActionResult<double> Get()
        {
            try
            {
                var result = _juros.RetornarTaxaDeJuros();

                return Ok(result);
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
