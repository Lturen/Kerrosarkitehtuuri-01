namespace LeagueHub.Expections
{
    public class MatchScoreExpection : AppExpection
    {
        public MatchScoreExpection(string message, int statusCode = 400) : base(message, statusCode)
        {
        }
    }
}
