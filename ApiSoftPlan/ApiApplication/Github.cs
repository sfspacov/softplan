using ApiDomain.Contracts;

namespace ApiApplication
{
    /// <summary>
    /// Github application class
    /// </summary>
    public class Github : IGithub
    {
        #region Public Methods
        /// <summary>
        /// Method that returns the project's code
        /// </summary>
        /// <returns>Url's project</returns>
        public string ShowMeTheCode()
        {
            return "https://github.com/sfspacov/testesUnitariosEIntegrados/";
        } 
        #endregion
    }
}
