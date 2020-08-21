using System;
using System.Threading.Tasks;
using ApiDomain.Contracts;
using ApiDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using SimpleLogger;

namespace Api2.Controllers
{
    /// <summary>
    /// Controller Interest
    /// </summary>
    [Route("")]
    [ApiController]
    public class InterestController : ControllerBase
    {
        #region Attributes
        private readonly IJuros _interest;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="interest">Interest interface</param>
        public InterestController(IJuros interest)
        {
            _interest = interest;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method returns the calculates interest
        /// </summary>
        /// <param name="valorInicial">It's a decimal</param>
        /// <param name="meses">It's a integer</param>
        /// <returns>Calculation result, in decimal format with two places</returns>
        [HttpGet("calcularDivida")]
        public ActionResult<string> Get([FromQuery]InterestEntity parameters)
        {

            if (parameters.Meses < 0)
            {
                var msg = "Mês não pode ser negativo";
                SimpleLog.Warning(msg);

                return BadRequest(msg);
            }
            try
            {
                var result = _interest.CalcularDivida(parameters);

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