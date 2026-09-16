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

        public Loan Create(int itemId, string borrowerName)
        {
            if (string.IsNullOrWhiteSpace(borrowerName))
            {
                throw new LoanCantBeCreated("Borrower name is invalid or name length is too short.");
            }

            Item? item = _items.GetItem(itemId);

            if (item == null)
            {
                throw new NotFoundException($"Item {itemId} not found.");
            }

            bool alreadyOnLoan = _loans.GetLoans()
                .Any(l => l.ItemId == itemId && l.ReturnedAt == null);

            if (alreadyOnLoan)
            {
                throw new ItemAlreadyOnLoan($"Item {itemId} is already on loan.");
            }

            return _loans.Add(new Loan
            {
                ItemId = itemId,
                BorrowerName = borrowerName.Trim(),
                BorrowedAt = DateTime.UtcNow,
                ReturnedAt = null
            });
        }

        public Loan Return(int id)
        {
            Loan loan = _loans.GetLoanById(id)
                ?? throw new NotFoundException("Loan not found.");

            if (loan.ReturnedAt != null)
            {
                throw new LoanCantBeReturned("Loan has already been returned.");
            }

            loan.ReturnedAt = DateTime.UtcNow;
            _loans.Update(loan);

            return loan;
        }

        public void Delete(int id)
        {
            Loan loan = _loans.GetLoanById(id)
                ?? throw new NotFoundException("Loan not found.");

            if (loan.ReturnedAt == null)
            {
                throw new LoanCantBeDeleted("An open loan must be returned before it can be deleted.");
            }

            _loans.Remove(loan);
        }
    }
}
