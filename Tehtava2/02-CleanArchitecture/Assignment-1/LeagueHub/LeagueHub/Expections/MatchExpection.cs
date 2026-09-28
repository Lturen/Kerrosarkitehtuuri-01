namespace LeagueHub.Expections
{
    public class MatchExpection : AppExpection
    {
        public MatchExpection(string message, int statusCode = 400) : base(message, statusCode)
        {
        }
    }
}
