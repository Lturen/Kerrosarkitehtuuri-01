# Kertauskysymykset — esimerkkivastaukset

Opiskelijan ei tarvitse osata näitä ulkoa sanasta sanaan. Tärkeää on sama ajatus LeagueHubin ja Lainaamon kautta.

---

## CA vastaan kerrosarkkitehtuuri

**1. Mitä ongelmia CA ratkaisi verrattuna layered-versioon?**

Kolme eroa, jotka näkyvät koodissa:

1. **Kansiot eivät valvoneet mitään.** Kerralla 1 kerrokset olivat kansioita yhdessä projektissa. Controller pystyi kutsumaan repositorya suoraan, ja projekti kääntyi — sääntö eli ihmisten muistissa. CA:ssa kerrokset ovat projekteja: väärä `using` on käännösvirhe.
2. **Aneeminen malli.** Kerralla 1 `Match` oli tietosäiliö: `new Match { HomeGoals = -1 }` kääntyi, koska säännöt asuivat `MatchServicessä`. CA:ssa factory ja `private set` estävät virheellisen olion. Sääntö kulkee olion mukana.
3. **Data oli pohjalla.** Kerrosarkkitehtuurissa Business kutsuu Dataa — tietokanta on riippuvuusketjun päässä. CA:ssa Domain on keskellä eikä viittaa mihinkään. Tietokanta riippuu domainista (`IMatchRepository` Domainissa, EF Infrassa), ei toisinpäin.

Lisäksi: sammutus ei tyhjennä dataa (SQLite vs muistilista), ja domain-säännön voi testata ilman fakea.

**2. Kääntyykö `new Match { HomeTeamId = 1, AwayTeamId = 1, HomeGoals = -1 }` Domainissasi?**

Ei. Konstruktori on privaatti ja setterit `private set`. Olio syntyy vain `Match.Createsta`, joka heittää jos koti ja vieras ovat sama. Maaleja ei voi asettaa suoraan — vain `RecordResult` muuttaa niitä ja tarkistaa invariantit.

Kerralla 1 sääntöä suojeli service (jos joku muisti kutsua sitä). Nyt sääntöä suojelee olio itse.

**3. Miksi controller ei voi kutsua repositorya suoraan?**

API-projekti viittaa Applicationiin, ei Domainin toteutuskerrokseen sillä tavalla, että controllerin kuuluisi käyttää sitä. Oikeasti controller saa konstruktorissa use casen. Jos joku yrittäisi `using`illa kiertää Domainiin tai Infrastructureen väärästä paikasta, suunta on silti väärä: sääntö ja orkestrointi eivät kuulu controlleriin.

Käytännön hyöty: HTTP-kerros voi vaihtua (Swagger, toinen API) ilman että säännöt muuttuvat. Ja kääntäjä estää “pienen oikopolun”, joka kerralla 1 olisi kääntynyt.

---

## Clean Architecture — rakenne ja käytänteet

**4. Neljä projektia ja vastuut**

- **Domain**: entiteetit, value objectit, invariantit, repository-rajapinnat. `Match.RecordResult` on täällä.
- **Application**: use caset, lukumallit (`Standing`), `NotFoundException`.
- **Infrastructure**: EF, `LeagueHubDbContext`, repository-toteutukset, seeder, migraatiot.
- **API**: controllerit, requestit, `Program.cs`. `return BadRequest(...)` on täällä.

**5. Dependency Rule**

Riippuvuudet osoittavat aina **sisäänpäin, kohti Domainia**. Application → Domain, Infrastructure → Domain, API → Application + Infrastructure. Domain ei viittaa mihinkään. Projektiviittaus on sääntö konkreettisesti: väärä suunta ei käänny (osio 12).

**6. Miksi rajapinta Domainissa, toteutus Infrassa?**

Domain kertoo, mitä se tarvitsee (`GetByIdAsync`, `AddAsync`) — ei miten rivi kirjoitetaan. Infrastructure toteuttaa sen EF:llä. Jos rajapinta olisi Infrassa, Applicationin pitäisi viitata Infrastructureen ja Dependency Rule kaatuisi. Testitkin näkevät vain rajapinnan, joten fake kelpaa.

**7. Composition root**

API on ainoa paikka, joka näkee molemmat puolet: use caset ja `TeamRepositoryn`. `Program.cs` sanoo: “kun tarvitaan `ITeamRepository`, anna `TeamRepository`.” Controllerit eivät tee `new`-kutsuja. Siksi API viittaa Infrastructureen — kytkentää varten, ei siksi että controller avaisi tietokannan.

**8. Mistä repositoryn metodit syntyvät?**

Osion 3 tekemisistä. “Perusta joukkue” → `AddAsync`. “Katso joukkueet” → `GetAllAsync`. Deleteä ei ole, koska asiakas ei pyytänyt poistoa. Metodia ei lisätä siltä varalta, että sitä ehkä tarvitaan.

---

## DDD — nimet, säännöt ja niiden paikka

**9. Entiteetti, value object, ei kumpikaan**

Kysymys: jos kaksi lappua näyttää samalta, ovatko ne silti eri asiat?

- `Team` = entiteetti. Kaksi “Mikkelin Mailaa” eri kaupungeissa ovat eri joukkueita. `Id` erottaa.
- `Score` = value object. 2–1 on 2–1. Ei `Id`:tä.
- Pelinumero = ei kumpikaan. Yksi luku pelaajan tiedoissa.
- Sarjataulukko = ei kumpikaan. Lasketaan otteluista, ei tallenneta.

**10. Invariantti**

Sääntö, jonka pitää olla voimassa **aina**, kun olio on olemassa. Ei “tarkistetaan joskus lomakkeella”.

- `Match`: koti ja vieras eivät ole sama; tulos vain kerran.
- `Team`: nimi ei tyhjä; `maxRoster > 0`.

Valvonta on `Create`-factoryssa tai metodissa (`RecordResult`). Rikkomus → `DomainException` → API palauttaa 400.

**11. Aneeminen vs rikas**

Aneeminen: pelkkiä julkisia propertyja, säännöt servicessä (kerran 1 `Match`). Rikas: `Match.Create`, `private set`, `RecordResult`. Virheellistä oliota ei pysty luomaan, eikä tilaa voi muuttaa ohi metodin.

**12. Koti ≠ vieras vs joukkueet olemassa**

- Koti ≠ vieras → `Match.Create`. Molemmat id:t ovat tämän ottelun omaa dataa.
- Joukkueet olemassa → `CreateMatchUseCase`. Vaatii haun. Entiteetti ei kysy repositorylta.

Nyrkkisääntö: yhden olion data → entiteetti. Haku tai toinen olio → use case. HTTP-status → API.

**13. Pelinumeron kaksi sääntöä**

- Positiivinen → `Player.Create`. Pelaaja näkee oman numeronsa.
- Uniikki joukkueessa → `AddPlayerToTeamUseCase`. Yksittäinen `Player` ei näe joukkuetovereitaan. Siksi domain-testi ei voi testata uniikkiutta.

**14. Lainaamo: huolto vs avoin laina**

Molemmat estävät lainaamisen, mutta tieto on eri. Huolto on välineen oma tila → `Item`. Avoin laina on *toinen rivi* lainataulussa → use case hakee ja tulkitsee.

**15. Miksi `Standing` ei ole entiteetti**

Sitä ei tallenneta, sillä ei ole `Id`:tä eikä omia sääntöjä. Se lasketaan otteluista. Applicationin lukumalli.

**16. Miksi Lainaamossa ei ole VO:ta**

`Score` oli VO, koska kaksi lukua kuului yhteen ja validoitui yhdessä. Lainaajan nimi tai eräpäivä on yksi arvo lainan tiedoissa. VO:ta ei keksitä siksi, että “DDD:ssä kuuluu olla”.

**17. Mitä use case ei saa tehdä**

Se ei toista entiteetin sääntöä. `if (match.HasResult)` kuuluu `RecordResult`iin, ei `RecordMatchResultUseCaseen`. Use case hakee ottelun, kutsuu `RecordResult`, tallentaa.

---

## Suunnittelu käytännössä

**18. `POST /api/matches/1/result`**

1. `MatchesController` lukee bodyn ja kutsuu use casea — ei sääntöjä.
2. `RecordMatchResultUseCase` hakee ottelun. `null` → `NotFoundException` → 404.
3. `Score.Create` ja `Match.RecordResult` päättävät, saako tuloksen kirjata. Rikkomus → `DomainException` → 400.
4. `MatchRepository.UpdateAsync` kirjoittaa SQLiteen.
5. Controller palauttaa 200.

Säännön päättää Domain. Controller vain kääntää poikkeuksen HTTP-kielelle.

**19. Domain-testi ilman fakea, use case fakella**

Sääntö testataan siellä missä se asuu. `Match.RecordResult` on olion metodi — testi kutsuu sitä suoraan. Use case tarvitsee repositoryn, joten testi antaa faken, joka toteuttaa saman rajapinnan listalla. Kumpikaan ei avaa HTTP:tä eikä SQLiteä.
