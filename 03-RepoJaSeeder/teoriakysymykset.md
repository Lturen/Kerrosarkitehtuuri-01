# Teoriakysymykset — Repo ja HTTP-seeder

Näitä **ei palauteta**. Käy läpi, kun ohjatut osiot 1–15 on tehty. Vastaa omin sanoin — samat teemat kysytään [välitehtävässä](../Valitehtava.md), jossa repo avataan ja sen päätökset selitetään.

Kaksitoista kysymystä. Jokainen koskee yhtä päätöstä, jonka teit tehtävässä. Hyvä vastaus osoittaa repostasi kohdan, jossa päätös näkyy: haaran, commitin, specin rivin tai koodirivin.

Mallivastaukset: [examples/teoriakysymykset-vastaukset.md](examples/teoriakysymykset-vastaukset.md). Avaa vasta, kun omat vastaukset on kirjoitettu.

Avaa alta **yksi teema kerrallaan**.

---

<details>
<summary>Haarat ja pull requestit — osiot 3, 4, 11 ja 12</summary>

1. Miksi tässä on `development` ja `master`, eikä pelkkä `master`, johon feature yhdistetään?

   Mieti ainakin: mitä kumpikin haara kuvaa MyLeaguessa, ja mistä haarasta masteriin avattu PR saa lähteä.

2. Miksi muutos tehdään feature-haarassa eikä commitoida suoraan developmentiin?

   Mieti ainakin: mitä PR:n diffissä näkyy, kun haarassa on vain yksi asia; mitä developmentille tapahtuu, kun seeder on puoliksi kirjoitettu; ja mitä maksaa hylätä väärä ratkaisu.

3. Miksi suora push masteriin ja developmentiin estetään, vaikka repo on sinun omasi?

   Mieti ainakin: ketä suoja oikeasti pysäyttää — sinut kiireessä, agentin, tulevan tiimikaverin — ja mitä masterin historiasta voi silloin sanoa.

</details>

---

<details>
<summary>Seeder ja arkkitehtuurin raja — osiot 6–10</summary>

4. Miksi seeder lisää demodatan APIn kautta eikä kirjoita suoraan `leaguehub.db`-tiedostoon?

   Mieti ainakin: mitkä säännöt jäisivät väliin, jos rivi kirjoitettaisiin tiedostoon itse; mitä yksi seeder-ajo samalla todistaa; ja mihin instanssiin tiedostoon kirjoittava ohjelma ei koskaan pääse.

5. Miksi seeder etsii joukkueen nimellä eikä id:llä, ja mitä lokirivi `oli jo` kertoo, mitä `luotiin` ei kerro?

   Mieti ainakin: mikä Mikkelin Myrskyn id on sinun kannassasi ja mikä toisessa; mitä **luonnollinen avain** tarkoittaa; ja miltä toinen ajo näyttäisi ilman `oli jo` -riviä.

6. Miksi seederin loppukysely on integraatiotesti, eikä riitä että POST palautti 201?

   Mieti ainakin: mitä 201 kertoo ja mitä ei; mitkä osat ovat mukana, kun GET kulkee seederistä SQLiteen asti; ja miksi tarkistus ei saa avata `leaguehub.db`:tä itse.

7. Miksi reitissä lukee `/api/v1`, vaikka käytössä on vasta yksi versio?

   Mieti ainakin: kuka on kirjoitettu polkua vasten; mitä tapahtuu kaikille asiakkaille, kun `name` vaihtuu `teamName`ksi ilman numeroa; ja missä tiedostossa versio asuu.

</details>

---

<details>
<summary>Spec ja tekoäly — osiot 5, 13 ja 15</summary>

8. Miksi spec kirjoitetaan ja commitoidaan ennen ensimmäistäkään riviä seederin koodia?

   Mieti ainakin: mihin PR:n diffiä verrataan; mihin myöhemmät promptit viittaavat; ja milloin väärään paikkaan merkitty sääntö on halvin korjata.

9. Miksi koko tehtävää ei anneta agentille yhdellä promptilla?

   Mieti ainakin: mistä kohdasta repo tarkistetaan ja mistä seeder; mitä agentti ehtii tehdä ennen ensimmäistäkään checkpointia, jos kaikki on samassa viestissä; ja kumpaa prompti rajaa osioissa 1–4 ja kumpaa osioissa 5–10.

10. Miksi agentti ei commitoi tässä tehtävässä, vaikka se osaisi?

    Mieti ainakin: mitä commit merkitsee historiassa, kuka ajaa checkpointin, ja miksi sama sääntö koskee PR:n yhdistämistä.

11. Miksi AGENTS.md ei luettele demodatan nimiä, vaikka ne ovat specissä?

    Mieti ainakin: mitä tapahtuu kahdelle listalle, kun demodata muuttuu; mitä seuraava sessio lukee; ja mikä sama sääntö pätee koodissa.

</details>

---

<details>
<summary>Savutesti ja CLI — osio 14</summary>

12. Miksi savutestin tulos on paluukoodi eikä pelkkä tulosterivi?

    Mieti ainakin: kuka lukee tulosterivin ja kuka paluukoodin; mitä tapahtuu kaikelle savutestin perään ketjutetulle, jos se päättyy nollaan myös virheessä; ja miksi `/health` ei ole `/api/v1`:n alla.

</details>
