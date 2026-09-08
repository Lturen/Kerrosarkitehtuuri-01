
using Lainaamo.Models;

namespace Lainaamo.Repositories
{
    public class InMemoryLoanRepository : ILoanRepository
    {
        private static readonly List<Loan> _loans = new()
    {
        new Loan { Id = 1, ItemId = 2, BorrowerName = "Aino", BorrowedAt = DateTime.UtcNow.AddDays(-3) },
        new Loan { Id = 2, ItemId = 3, BorrowerName = "Elias", BorrowedAt = DateTime.UtcNow.AddDays(-10), ReturnedAt = DateTime.UtcNow.AddDays(-8) }
    };
        private static int _nextLoanId = 3;

        public List<Loan> GetLoans()
        {
            return _loans;
        }

        public Loan? GetLoanById(int id)
        {
            return _loans.FirstOrDefault(l => l.Id == id);
        }

        public Loan Add(Loan loan)
        {
            loan.Id = _nextLoanId++;
            _loans.Add(loan);
            return loan;
        }

        public void Remove(Loan loan)
        {
            _loans.Remove(loan);
        }

        public void Update(Loan loan)
        {
            var index = _loans.FindIndex(l => l.Id == loan.Id);
            if (index != -1)
            {
                _loans[index] = loan;
            }
        }
    }
}
