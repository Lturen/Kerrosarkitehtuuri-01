using Lainaamo.Models;
using Lainaamo.Repositories;
using Lainaamo.Exceptions;

namespace Lainaamo.Services
{
    public class LoanService : ILoanservice
    {
        private readonly ILoanRepository _loans;

        private readonly IItemRepository _items;

        public LoanService(ILoanRepository loans, IItemRepository items)
        {
            _loans = loans;
            _items = items;
        }

        public List<Loan> GetLoans()
        {
            return _loans.GetLoans();
        }

        public Loan GetById(int id)
        {
            Loan? loan = _loans.GetLoanById(id);

            return loan ?? throw new NotFoundException("Loan not found.");
        }

        public Loan Create(int itemId, string borrowerName, DateTime BorrowedAt, DateTime returnedAt)
        {
            if (BorrowedAt > returnedAt)
            {
                throw new LoanCantBeCreated("Borrowed date cannot be after returned date.");
            }
            else if (borrowerName == null || borrowerName.Length < 1)
            {
                throw new LoanCantBeCreated("Borrower name is invalid or name length is too short.");
            }
            
            

            return _loans.Add(new Loan
            {
                ItemId = itemId,
                BorrowerName = borrowerName,
                BorrowedAt = BorrowedAt,
                ReturnedAt = returnedAt
            });

        }
        

        




    }
}
