# Kertauskysymykset — Clean Architecture ja DDD

Näitä **ei palauteta**. Käy läpi sen jälkeen kun ohjattu osio ja Lainaamo-suunnitelma on tehty. Vastaa omin sanoin — samat teemat tulevat [välikokeeseen](../Koelukualueet.md#välikoe) ja [välitehtävään](../Valitehtava.md).

Avaa alta **yksi osio kerrallaan**.

---

<details>
<summary>CA vastaan kerrosarkkitehtuuri</summary>

1. Rakensit saman LeagueHubin kahdesti: kerralla 1 kerrosarkkitehtuurilla, kerralla 2 Clean Architecturella. **Mitä ongelmia CA ratkaisi** verrattuna layered-versioon? Anna vähintään kolme eroa ja näytä kukin koodista (ei jargonia).

   Mieti ainakin: kerrokset kansioina vs projekteina, aneeminen vs rikas `Match`, “data pohjalla” vs domain keskellä. Mitä käytännössä tapahtuu, jos sääntöä rikotaan?

2. Kerralla 1 rivi `new Match { HomeTeamId = 1, AwayTeamId = 1, HomeGoals = -1 }` kääntyi. Kääntyykö se Domainissasi? Mitä tämä kertoo mallista — ja kuka suojeli sääntöä kummallakin kerralla?

3. Kerralla 1 controller pystyi kutsumaan repositorya suoraan, ja projekti kääntyi. Miksi sama ei onnistu CA-versiossa? Miksi se on hyvä uutinen, ei pelkkä kiusa?

</details>

---

<details>
<summary>Clean Architecture — rakenne ja käytänteet</summary>

4. Nimeä neljä projektia ja kunkin vastuu yhdellä lauseella. Mihin projektiin kuuluu `Match.RecordResult`? Entä `LeagueHubDbContext`? Entä `return BadRequest(...)`?

5. Mitä **Dependency Rule** tarkoittaa? Mihin suuntaan riippuvuudet saavat osoittaa? Miten kääntäjä valvoo sitä?

6. Miksi repository-**rajapinta** on Domainissa, mutta **toteutus** Infrastructuressa? Mitä menisi rikki, jos `IMatchRepository` siirrettäisiin Infrastructureen?

7. Miksi API viittaa myös Infrastructureen, vaikka controllerit kutsuvat vain use caseja? Mitä **composition root** tarkoittaa, ja miksi se on juuri `Program.cs`?

8. Repositoryn metodit eivät synny CRUD-paketista. Mistä ne syntyvät? Miksi LeagueHubissa ei ole `Delete`-metodia?

</details>

---

<details>
<summary>DDD — nimet, säännöt ja niiden paikka</summary>

9. Mikä on **entiteetti**, mikä **value object**, mikä **ei kumpikaan**? Sijoita: `Team`, `Score`, pelinumero, sarjataulukko. Kysymys on aina: jos kaksi lappua näyttää samalta, ovatko ne silti eri asiat?

10. Mikä on **invariantti**? Nimeä yksi `Match`ista ja yksi `Team`ista. Missä koodissa se valvottaan — ja mitä tapahtuu, jos se rikkoutuu?

11. Mitä eroa on **aneemisella** ja **rikkaalla** mallilla? Näytä omasta `Match`-luokastasi factory, `private set` ja metodi.

12. Mihin kuuluu sääntö *koti ja vieras eivät ole sama joukkue*? Mihin kuuluu *molemmat joukkueet ovat olemassa*? Miksi ne eivät ole samassa paikassa? Sano nyrkkisääntö omin sanoin.

13. Pelinumerolla on kaksi sääntöä: *numero on positiivinen* ja *numero on uniikki joukkueessa*. Kumpi on `Player.Createssa`, kumpi `AddPlayerToTeamUseCasessa` — ja miksi ne eivät voi olla samassa paikassa?

14. Lainaamossa *huollossa olevaa ei saa lainata* ja *välineellä ei saa olla kahta avointa lainaa* estävät molemmat lainaamisen. Miksi ne silti kuuluvat eri paikkoihin?

15. Miksi sarjataulukko (`Standing`) ei ole domain-entiteetti? Mihin kerrokseen se kuuluu?

16. Miksi Lainaamoon ei tarvita value objectia, vaikka LeagueHubissa `Score` oli perusteltu? Milloin VO:ta **ei** pidä keksiä?

17. Use casen resepti on *hae, kutsu domainia, tallenna*. Mitä use case **ei** saa tehdä? Anna esimerkki `if`-lauseesta, joka kuuluu entiteettiin eikä `RecordMatchResultUseCaseen`.

</details>

---

<details>
<summary>Suunnittelu käytännössä</summary>

18. Käy läpi `POST /api/matches/1/result`. Nimeä tiedosto jokaisessa askeleessa ja kerro, kuka päättää säännöstä ja kuka vain kääntää sen HTTP-kielelle.

19. Domain-testissä ei ole fakea. Use case -testissä on. Miksi ero — ja mitä se kertoo siitä, *missä sääntö asuu*?

</details>
