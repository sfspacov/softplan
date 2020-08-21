using System;
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
    public class DividaController : ControllerBase
    {
        #region Attributes
        private readonly IDivida _idivida;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idivida">Interest interface</param>
        public DividaController(IDivida idivida)
        {
            _idivida = idivida;
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
        public ActionResult<string> Get([FromQuery]DividaEntity parameters)
        {

            if (parameters.Meses < 0)
            {
                var msg = "Mês não pode ser negativo";
                SimpleLog.Warning(msg);

                return BadRequest(msg);
            }
            try
            {
                var result = _idivida.CalcularDivida(parameters);

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