namespace Lainaamo.Exceptions
{
    public class ItemAlreadyOnLoan : BusinessRuleException
    {
        public ItemAlreadyOnLoan(string message) : base(message)
        {
        }
    }
}
