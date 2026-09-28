namespace LeagueHub.Expections
{
    public abstract class AppExpection : Exception
    {
        public int StatusCode { get; }

        protected AppExpection(string message, int statusCode = 400) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
