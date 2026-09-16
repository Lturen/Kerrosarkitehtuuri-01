namespace Lainaamo.Exceptions
{
    public class ItemCantBeDeleted : BusinessRuleException
    {
        public ItemCantBeDeleted(string message) : base(message)
        {

        }
    }
}
