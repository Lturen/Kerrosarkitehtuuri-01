using LeagueHub.Repository;
using LeagueHub.Models;

namespace LeagueHub.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _player;

        public PlayerService(IPlayerRepository player)
        {
            _player = player;
        }

        public List<Player> GetAllPlayers()
        {
            return _player.GetAllPlayers();
        }

        public Player GetPlayerById(int id)
        {
            var player = _player.GetPlayerById(id);

            if (player == null)
            {
                throw new Exception($"Player with ID {id} not found.");
            }

            return player;
        }

        public Player? CustomisePlayerById(int id)
        {
            var player = _player.CustomisePlayerById(id);

            if (player == null)
            {
                throw new Exception($"Player with ID {id} not found.");
            }
            

            return player;
        }

        public void RemovePlayer(Player player)
        {
            _player.RemovePlayer(player);
        }

    }
}
