namespace Lainaamo.Exceptions
{
    public class LoanCantBeDeleted : BusinessRuleException
    {
        public LoanCantBeDeleted(string message) : base(message)
        {

        }
    }
}
