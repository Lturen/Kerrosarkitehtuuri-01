# Esimerkit — 03 repo ja HTTP-seeder

Avaa vasta, kun olet yrittänyt omaa versiota.

| Tiedosto | Mitä se on |
|----------|------------|
| [LeagueHub/](LeagueHub/) | Koko starter, johon on tehty ohjatut osiot 1–14. Soveltavaa tikettiä ei ole — se tehdään ilman mallikoodia. |
| [LeagueHub/LeagueHub.Seeder/Program.cs](LeagueHub/LeagueHub.Seeder/Program.cs) | Valmis seeder. Kutsuu polkuja `/api/v1`. |
| [LeagueHub/LeagueHub.SmokeTest/Program.cs](LeagueHub/LeagueHub.SmokeTest/Program.cs) | Savutesti: GET `/health`, tulos paluukoodina. |
| [teoriakysymykset-vastaukset.md](teoriakysymykset-vastaukset.md) | Mallivastaukset tehtäväkansion [teoriakysymyksiin](../teoriakysymykset.md) |

Esimerkkikoodissa starteriin on tehty nämä muutokset:

- specit `specs/001-http-seeder.md` ja `specs/002-savutesti.md`
- HTTP-seeder asetuksista, lokilla `luotiin` / `oli jo` ja GET-tarkistuksella
- käynnistys ajaa vain `MigrateAsync` — `DbSeeder.cs` on poistettu
- reitit `/api/v1/...`
- APIn osoite `/health` ja projekti `LeagueHub.SmokeTest`
- `AGENTS.md` ja README:n haaraosio

## Miten ajat esimerkin

Kansiossa `examples/LeagueHub`:

```bash
dotnet run --project LeagueHub.Api
```

Toisessa päätteessä:

```bash
dotnet run --project LeagueHub.SmokeTest
dotnet run --project LeagueHub.Seeder
```

## Malliajo nollauksen jälkeen

API käynnissä osoitteessa `http://localhost:5080`, `leaguehub.db` poistettu, käynnistys ei enää lisää demodataa.

```text
Kohde http://localhost:5080/
luotiin joukkue  Mikkelin Myrsky  id=1
luotiin joukkue  Kouvolan Kiekko  id=2
luotiin joukkue  Savonlinnan Sähly  id=3
luotiin pelaaja  Matti Meikäläinen  id=1
luotiin pelaaja  Teppo Testaaja  id=2
luotiin pelaaja  Kaisa Kiekkonen  id=3
luotiin ottelu  1-2  id=1
luotiin ottelu  2-3  id=2
joukkueet 3/3, pelaajat 3/3, ottelut 2/2
```

Id:t ovat tämän kannan id:itä. Toisessa instanssissa numerot vaihtuvat. Toinen ajo ilman nollausta vaihtaa `luotiin` rivit muotoon `oli jo` ja tulostaa saman yhteenvedon.
