# LeagueHub

Starter, johon on tehty tehtävän 03 ohjatut osiot 1–14.

## Haarat

`master` on julkaistu haara. `development` on työhaara.
Muutos tehdään feature-haarassa, joka lähtee developmentista.
Yhdistäminen developmentiin ja masteriin tapahtuu pull requestilla.
Masteriin ei pushata suoraan. Developmentiin ei pushata suoraan sen jälkeen, kun suojat ovat päällä.

## Ajo

API http-profiililla (`http://localhost:5080`):

```bash
dotnet run --project LeagueHub.Api
```

Savutesti ennen seederiä:

```bash
dotnet run --project LeagueHub.SmokeTest
```

Seeder:

```bash
dotnet run --project LeagueHub.Seeder
```
