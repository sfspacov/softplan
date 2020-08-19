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
    public class InterestController : ControllerBase
    {
        #region Attributes
        private readonly IInterest _interest;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="interest">Interest interface</param>
        public InterestController(IInterest interest)
        {
            _interest = interest;
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
                var result = _interest.GetInterest();

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
