using LeagueHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeagueHub.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(LeagueHubDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Teams.AnyAsync())
        {
            return;
        }

        Team maila = Team.Create("Mikkelin Maila", "Mikkeli", 12);
        Team kiekko = Team.Create("Kouvolan Kiekko", "Kouvola", 12);
        Team sahly = Team.Create("Savonlinnan Sähly", "Savonlinna", 10);

        context.Teams.AddRange(maila, kiekko, sahly);
        await context.SaveChangesAsync();

        context.Players.AddRange(
            Player.Create(maila.Id, "Matti Meikäläinen", 7),
            Player.Create(maila.Id, "Teppo Testaaja", 10),
            Player.Create(kiekko.Id, "Kaisa Kiekkonen", 7));

        context.Matches.AddRange(
            Match.Create(maila.Id, kiekko.Id, DateTime.UtcNow.AddDays(-2)),
            Match.Create(kiekko.Id, sahly.Id, DateTime.UtcNow.AddDays(7)));

        await context.SaveChangesAsync();
    }
}
