using ApiDomain.Entities;

namespace ApiDomain.Contracts
{
    /// <summary>
    /// Interface IInterest
    /// </summary>
    public interface IDivida
    {
        /// <summary>
        /// Method that get the interest value from Api1
        /// </summary>
        /// <returns>Interest value</returns>
        string CalcularDivida(DividaEntity parameters);
    }
}
