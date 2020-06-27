using System.Threading.Tasks;
using ApiDomain.Entities;

namespace ApiDomain.Contracts
{
    public interface IInterest
    {
        double GetInterest();
        Task<double> GetInterestRate();
        Task<string> CalculateInterest(InterestEntity parameters);
    }
}
