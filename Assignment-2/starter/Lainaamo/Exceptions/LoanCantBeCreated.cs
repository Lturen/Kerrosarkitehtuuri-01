namespace Lainaamo.Exceptions
{
    public class LoanCantBeCreated : BusinessRuleException
    {
        public LoanCantBeCreated(string message) : base(message)
        {

        }
    }
}
