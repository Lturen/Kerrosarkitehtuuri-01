using Lainaamo.Models;
using Lainaamo.Repositories;
using Lainaamo.Exceptions;

namespace Lainaamo.Services
{
    public class LoanService : ILoanservice
    {
        private readonly ILoanRepository _loanRepository;

        private readonly IItemRepository _itemRepository;

        public LoanService(ILoanRepository loanRepository, IItemRepository itemRepository)
        {
            _loanRepository = loanRepository;
            _itemRepository = itemRepository;
        }

        public List<Loan> GetLoans()
        {
            return _loans.GetLoans();
        }

        public Loan GetById(int id)
        {
            Loan? loan = _loans

            return _loanRepository.GetLoan(id);
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
            else if ()
            {

                Loan newLoan = new Loan
                {
                    ItemId = itemId,
                    BorrowerName = borrowerName,
                    BorrowedAt = BorrowedAt,
                    ReturnedAt = returnedAt
                };
            }
            return _loanRepository.Add(newLoan);
        }
        

        




    }
}
