namespace LeagueHub.Expections
{
    public class LeagueTableExpection : AppExpection
    {
        public LeagueTableExpection(string message, int statusCode = 400) : base(message, statusCode)
        {
        }
    }
}
