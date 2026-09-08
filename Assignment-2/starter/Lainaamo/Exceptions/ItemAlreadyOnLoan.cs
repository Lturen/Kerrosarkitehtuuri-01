namespace Lainaamo.Exceptions
{
    public class ItemAlreadyOnLoan : Exception
    {
        public ItemAlreadyOnLoan(string message) : base(message)
        {
        }
    }
}
