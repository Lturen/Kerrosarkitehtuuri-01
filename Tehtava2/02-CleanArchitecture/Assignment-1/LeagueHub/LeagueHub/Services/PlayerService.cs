using LeagueHub.Repository;
using LeagueHub.Models;
using LeagueHub.Expections;

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

        public Player? GetPlayerById(int id)
        {
            if (id <= 0)
            {
                throw new PlayerExpection("Id must be greater than 0", 400);
            }

            return _player.GetPlayerById(id);
        }

        public Player? CustomisePlayerById(int id)
        {
            if (id <= 0)
            {
                throw new PlayerExpection("Id must be greater than 0", 400);
            }

            return _player.CustomisePlayerById(id);
        }

        public Player AddPlayer(Player player)
        {
            if (player == null)
            {
                throw new PlayerExpection("Player cannot be null", 400);
            }
            else if (string.IsNullOrEmpty(player.Name))
            {
                throw new PlayerExpection("Player name cannot be null or empty", 400);
            }
            else if (player.Age <= 0)
            {
                throw new PlayerExpection("Player age must be greater than 0", 400);
            }
            else if (player.PlayerNumber <= 0)
            {
                throw new PlayerExpection("Player number must be greater than 0", 400);
            }
            else if (_player.GetAllPlayers().Any(p => p.PlayerNumber == player.PlayerNumber))
            {
                throw new PlayerExpection("Player with the same player number already exists", 409);
            }
            else
            {
                return _player.AddPlayer(player);
            }
        }

        public void RemovePlayer(Player player)
        {
            var existingPlayers = _player.GetAllPlayers();

            if (existingPlayers.Any(p => p.PlayerNumber == player.PlayerNumber))
            {
                _player.RemovePlayer(player);
            }
            else
            {
                throw new PlayerExpection("Player does not exist", 404);
            }
        }

    }
}
