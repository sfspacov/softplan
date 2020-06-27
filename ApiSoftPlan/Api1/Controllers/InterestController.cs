using ApiDomain.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api1.Controllers
{
    [Route("")]
    [ApiController]
    public class InterestController : ControllerBase
    {
        private readonly IInterest _interest;

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
        [HttpGet("taxaJuros")]
        public ActionResult<double> Get()
        {
            try
            {
                var result = _interest.GetInterest();

                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }
        } 
        #endregion
    }
}
