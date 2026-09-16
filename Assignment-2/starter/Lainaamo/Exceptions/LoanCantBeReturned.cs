namespace Lainaamo.Exceptions
{
    public class LoanCantBeReturned : BusinessRuleException
    {
        public LoanCantBeReturned(string message) : base(message)
        {

        }
    }
}
