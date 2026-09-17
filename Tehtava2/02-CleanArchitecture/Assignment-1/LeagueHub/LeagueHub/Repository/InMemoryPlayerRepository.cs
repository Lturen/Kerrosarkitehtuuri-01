using LeagueHub.Models;

namespace LeagueHub.Repository
{
    public class InMemoryPlayerRepository : IPlayerRepository
    {
        private readonly List<Player> _players = new()
        {
            new Player { Name = "John Doe", Age = 25, PlayerNumber = 10 },
            new Player { Name = "Jane Smith", Age = 22, PlayerNumber = 7 }
        };

        public List<Player> GetAllPlayers()
        {
            return _players;
        }

        public Player? GetPlayerById(int id)
        {
            return _players.FirstOrDefault(p => p.PlayerNumber == id);
        }

        public Player? CustomisePlayerById(int id)
        {
            var player = _players.FirstOrDefault(p => p.PlayerNumber == id);
            return player;
        }

        public void RemovePlayer(Player player)
        {
            _players.Remove(player);
        }



    }
}
