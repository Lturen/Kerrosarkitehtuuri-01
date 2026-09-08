using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Lainaamo.Exceptions
{
    public class LoanCantBeCreated : Exception
    {
        public LoanCantBeCreated(string message) : base(message)
        {

        }
    }
}
