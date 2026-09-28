namespace LeagueHub.Expections
{
    public class PlayerExpection : AppExpection
    {
        public PlayerExpection(string message, int statusCode = 400) : base(message, statusCode)
        {
        }
    }
}
