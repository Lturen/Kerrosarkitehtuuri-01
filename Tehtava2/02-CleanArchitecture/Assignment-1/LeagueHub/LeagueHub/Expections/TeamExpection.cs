namespace LeagueHub.Expections
{
    public class TeamExpection : AppExpection
    {
        public TeamExpection(string message, int statusCode = 400) : base(message, statusCode)
        {
        }
    }
}
