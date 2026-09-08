using Lainaamo.Models;

namespace Lainaamo.Services
{
    public interface ILoanservice
    {
        List<Loan> GetLoans();

        Loan GetById(int id);

        Loan Create (int itemId, string borrowerName, DateTime BorrowedAt, DateTime returnedAt);
    }
}
