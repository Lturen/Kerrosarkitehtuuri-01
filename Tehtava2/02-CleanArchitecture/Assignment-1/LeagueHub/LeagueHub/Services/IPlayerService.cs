using LeagueHub.Models;

namespace LeagueHub.Services
{
    public interface IPlayerService
    {
        List<Player> GetAllPlayers();

        Player GetPlayerById(int id);

        Player? CustomisePlayerById(int id);

        void RemovePlayer(Player player);
    }
}
