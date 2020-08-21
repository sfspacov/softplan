using System.Threading.Tasks;
using ApiDomain.Entities;

namespace ApiDomain.Contracts
{
    /// <summary>
    /// Interface IInterest
    /// </summary>
    public interface IInterest
    {
        /// <summary>
        /// Method that returns interest rates
        /// </summary>
        /// <returns>Interest rates</returns>
        double TaxaDeJuros();
        /// <summary>
        /// Method that get the interest value from Api1
        /// </summary>
        /// <returns>Interest value</returns>
        Task<double> RetornarTaxaDeJuros();
        /// <summary>
        /// Method that calculates memory, compound interest, according to a formula: Valor Final = Valor Inicial * (1 + juros) ^ Tempo
        /// </summary>
        /// <param name="valorInicial">It's a decimal</param>
        /// <param name="meses">It's a integer</param>
        /// <returns>Calculation result, in decimal format with two places</returns>
        Task<string> CalcularDivida(InterestEntity parameters);
    }
}
