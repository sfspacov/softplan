using ApiDomain.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api2.Controllers
{
    /// <summary>
    /// Github Controller
    /// </summary>
    [Route("")]
    [ApiController]
    public class GithubController : ControllerBase
    {
        private readonly IGithub _github;

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="interest">Interest interface</param>
        public GithubController(IGithub github)
        {
            _github = github;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Method that returns the URL that contains the source code in Github
        /// </summary>
        /// <returns>Url on Github</returns>
        [HttpGet("showmethecode")]
        public ActionResult<string> Get()
        {
            try
            {
                var result = _github.ShowMeTheCode();

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