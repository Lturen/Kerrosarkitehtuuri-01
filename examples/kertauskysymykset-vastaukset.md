# Kertauskysymykset — esimerkkivastaukset

Opiskelijan ei tarvitse osata näitä ulkoa sanasta sanaan. Tärkeää on sama ajatus LeagueHubin kautta.

---

## Kerrosarkkitehtuuri

**1. Nimeä kolme kerrosta ja kunkin vastuu.**

- **Presentation** (controllerit): ottaa HTTP-pyynnön vastaan ja palauttaa statuskoodin sekä bodyn. Ei päätä sääntöjä (esim. saako tuloksen kirjata) eikä pidä joukkue- tai otteludataa omissa kokoelmissaan.
- **Business** (servicet): sovelluksen säännöt ja laskenta, esimerkiksi tulos vain kerran ja sarjataulukko. Ei palauta `IActionResult`ia eikä viittaa ASP.NET Coreen — se ei tiedä HTTP:stä.
- **Data** (repositoryt): haku ja tallennus. Tässä tehtävässä data on muistissa oleva kokoelma. Repository ei päätä, saako tuloksen kirjata; se vain antaa ottelun tai tallentaa sen.

**2. Mihin suuntaan riippuvuudet kulkevat? Saako Repository kutsua Serviceä?**

Vain alaspäin: Controller kutsuu Serviceä, Service kutsuu Repositorya. Repository ei saa kutsua Serviceä eikä controlleria. Alempi kerros ei tiedä, että ylempi on olemassa.

**3. Käy läpi, mitä tapahtuu kun `POST /api/matches/1/result` kutsu saapuu.**

1. `MatchesController.RecordResult` lukee bodyn (`homeGoals`, `awayGoals`) ja kutsuu `_matches.RecordResult(1, ...)`.
2. `MatchService` hakee ottelun rajapinnan kautta: `IMatchRepository.GetById(1)`.
3. Service tarkistaa säännöt: onko ottelu olemassa, onko tulos jo kirjattu, onko ajankohta ohi, ovatko maalit ei-negatiivisia.
4. Jos sääntö rikkoutuu, Service heittää `BusinessRuleException`in tai `NotFoundException`in. Controller muuttaa sen HTTP-vastaukseksi `400` tai `404`.
5. Jos säännöt täyttyvät, Service merkitsee maalit otteluun. Repository pitää saman olion muistissa, joten seuraava GET näkee tuloksen. Controller palauttaa `200` ja ottelun.

**4. Mihin kerrokseen kuuluvat: (a) `return NotFound(...)` (b) `if (match.HomeGoals is not null)` (c) `_matches.FirstOrDefault(...)`?**

- (a) Presentation — `NotFound` on HTTP-status 404, ei liiketoimintasääntö.
- (b) Business — tämä on sääntö *tulos kirjataan vain kerran*.
- (c) Data — ottelun etsiminen kokoelmasta (tai myöhemmin tietokannasta).

**5. Miksi `IMatchRepositoryssa` ei ole metodia `IsResultAlreadyRecorded`?**

“Tulos on jo kirjattu” on sääntö, ei datan hakua. Repository palauttaa ottelun; Service katsoo kenttää `HomeGoals`.

Jos tarkistus olisi repositoryssa, sama sääntö pitäisi toistaa muualla (sarjataulukko, peruutus) ja se sekoittuisi tallennustapaan. Kun vaihdat muistilistan tietokantaan, säännön ei pidä muuttaa.

---

## SOLID ja DI

**6. Mitä SRP tarkoittaa? Miten `LeagueController` rikkoi sitä?**

**SRP** (Single Responsibility Principle): luokalla on **yksi syy muuttua**. Se ei tarkoita “yksi metodi per luokka”.

Spagetti-`LeagueController` muuttui kolmesta syystä: reitti vaihtui, sääntö *tulos vain kerran* muuttui, ja tallennus vaihtui listasta johonkin muuhun. Yksi luokka, kolme syytä. Siksi ottelun peruminen piti kopioida moneen metodiin. Kerroksissa controllerilla on yksi syy muuttua: HTTP.

**7. Mitä DIP tarkoittaa? Kaksi hyötyä siitä, että `MatchService` riippuu `IMatchRepositorysta`.**

**DIP** (Dependency Inversion Principle): ylempi kerros riippuu **rajapinnasta**, ei valmiista luokasta. `MatchService` tuntee `IMatchRepositoryn`, ei `InMemoryMatchRepositorya`.

Kaksi hyötyä:

1. **Testaus.** Testissä fake toteuttaa saman rajapinnan: `new MatchService(fakeTeams, fakeMatches)`. HTTP-palvelinta ei tarvita.
2. **Vaihto.** Muistilistan voi myöhemmin korvata tietokannalla (EF Core) kirjoittamatta Serviceä uusiksi.

**8. Mitä konstruktori-injektio tarkoittaa?**

Riippuvuus annetaan luokalle konstruktorin parametrina. Service ei tee itse `new InMemoryMatchRepository()`.

DI-säiliö (`Program.cs`) luo oliot ja syöttää ne: controller saa `IMatchServicen`, `MatchService` saa `IMatchRepositoryn`. Luokka ei tiedä, mikä toteutus sille annettiin.

**9. Miksi in-memory-repositoryt rekisteröidään Singletonina? Mitä tapahtuu, jos ne ovat Scoped?**

Data elää muistissa repository-oliossa. **Singleton** tarkoittaa yhtä oliota koko sovelluksen ajan, joten joukkueet ja ottelut säilyvät pyyntöjen välillä.

**Scoped** luo uuden olion joka HTTP-pyynnöllä. `POST .../result` kirjoittaa yhteen olioon; seuraava `GET` saa uuden, tyhjän olion — tulos katoaa. Siksi tehtävässä kokeillaan `AddScoped` ja vaihdetaan takaisin `AddSingleton`.
