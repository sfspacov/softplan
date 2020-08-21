using System.Threading.Tasks;
using ApiDomain.Entities;

namespace ApiDomain.Contracts
{
    /// <summary>
    /// Interface IInterest
    /// </summary>
    public interface IJuros
    {
        /// <summary>
        /// Method that returns interest rates
        /// </summary>
        /// <returns>Interest rates</returns>
        double RetornarTaxaDeJuros();
        /// <summary>
        /// Method that get the interest value from Api1
        /// </summary>
        /// <returns>Interest value</returns>
       
        string CalcularDivida(InterestEntity parameters);
    }
}
