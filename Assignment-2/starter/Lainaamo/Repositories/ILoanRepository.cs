using Lainaamo.Models;

namespace Lainaamo.Repositories

{
    public interface ILoanRepository
    {
        List<Loan> GetLoans();

        Loan? GetLoanById(int id);

        Loan Add(Loan loan);

        void Remove(Loan loan);

        void Update(Loan loan);
    }
}
