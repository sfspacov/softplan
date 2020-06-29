namespace ApiDomain.Contracts
{
    /// <summary>
    /// Interface IGithub
    /// </summary>
    public interface IGithub
    {
        /// <summary>
        /// Method that returns the project's code
        /// </summary>
        /// <returns>Url's project</returns>
        string ShowMeTheCode();
    }
}
