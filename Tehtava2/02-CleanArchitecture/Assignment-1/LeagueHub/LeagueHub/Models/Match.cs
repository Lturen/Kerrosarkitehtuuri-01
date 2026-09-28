namespace LeagueHub.Models
{
    public class Match
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }

        public string Location { get; set; }

        
    }
}
