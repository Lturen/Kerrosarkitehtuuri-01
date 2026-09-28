using LeagueHub.Models;

namespace LeagueHub.Repository
{
    public interface IPlayerRepository
    {
        List<Player> GetAllPlayers();

        Player? GetPlayerById(int id);

        Player? CustomisePlayerById(int id);

        Player AddPlayer(Player player);

        void RemovePlayer(Player player);
    }
}
