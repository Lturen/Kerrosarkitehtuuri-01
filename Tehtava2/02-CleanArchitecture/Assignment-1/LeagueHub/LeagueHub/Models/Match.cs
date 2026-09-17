namespace LeagueHub.Models
{
    public class Match
    {
        public DateTime DateTime { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }

        public string Location { get; set; }

        
    }
}
