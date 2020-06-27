using System;
using System.Threading.Tasks;
using ApiDomain.Contracts;
using ApiDomain.Entities;
using Microsoft.AspNetCore.Mvc;

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
        /// Method returns the calculates interest
        /// </summary>
        /// <param name="valorInicial">It's a decimal</param>
        /// <param name="meses">It's a integer</param>
        /// <returns>Calculation result, in decimal format with two places</returns>
        [HttpGet("calculajuros")]
        public async Task<ActionResult<string>> Get([FromQuery]InterestEntity parameters)
        {

            if (parameters.Meses < 0)
                return BadRequest("Mês não pode ser negativo");

            try
            {
                var result = await _interest.CalculateInterest(parameters);

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