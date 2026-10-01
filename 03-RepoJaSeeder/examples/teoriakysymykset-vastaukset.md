# 03 — teoriakysymysten mallivastaukset

Kysymykset ovat tiedostossa [../teoriakysymykset.md](../teoriakysymykset.md). Avaa tämä vasta, kun omat vastaukset on kirjoitettu. Sanamuoto saa erota.

---

## Haarat ja pull requestit

**1. Miksi tässä on `development` ja `master`, eikä pelkkä `master` johon feature yhdistetään?**

`master` on julkaistu haara, `development` on työhaara. MyLeaguessa ne vastaavat tuotantoa ja stagingia. Feature yhdistetään ensin developmentiin. Masteriin tulee vain developmentista avattu pull request. Suora push kumpaankin on estetty, kun suojat ovat päällä.

**2. Miksi muutos tehdään feature-haarassa eikä commitoida suoraan developmentiin?**

Feature-haara rajaa yhden muutoksen. PR:n diffissä näkyy vain se, mikä kuuluu tähän asiaan, ja diffin ehtii lukea ennen yhdistämistä. Keskeneräinen työ ei riko developmentia, koska se elää omassa haarassaan — development pysyy koko ajan tilassa, josta seuraava haara voi lähteä. Ja väärä ratkaisu on halpa hylätä: haara poistetaan, työhaaraan ei ehtinyt mitään. Sama koskee agentin tuotosta: se syntyy feature-haaraan ja luetaan diffistä ennen kuin se pääsee työhaaraan.

**3. Miksi suora push masteriin ja developmentiin estetään, vaikka repo on sinun omasi?**

Suoja ei ole epäluottamusta vaan muistin korvike: kiireessä `git push` lähtee väärästä haarasta, ja agentti tottelee sitä komentoa, jonka saa. Kun ainoa reitti pitkäikäiseen haaraan on PR, jokainen muutos kulkee luettavan diffin kautta — myös omasi ja agentin. Silloin `master`in historia sisältää vain kohtia, jotka joku on lukenut ja todennut, ja sama sääntö on valmiina sinä päivänä, kun repoon tulee toinen tekijä.

## Seeder ja arkkitehtuurin raja

**4. Miksi seeder lisää demodatan APIn kautta eikä kirjoita suoraan `leaguehub.db`-tiedostoon?**

Suora kirjoitus tiedostoon ohittaisi controllerin, use caset ja Domainin säännöt — rivi menisi kantaan ilman, että mikään tarkisti sitä. APIn kautta seeder kulkee saman polun kuin oikea asiakas, joten jokainen ajo on samalla koe siitä, että API toimii. Ja koska yhteys on HTTP eikä tiedosto, sama komento käy mihin tahansa instanssiin, kun osoite vaihdetaan asetuksista — pilvi-instanssin kantatiedostoon ei edes pääse käsiksi.

**5. Miksi seeder etsii joukkueen nimellä eikä id:llä, ja mitä lokirivi `oli jo` kertoo, mitä `luotiin` ei kerro?**

Id on kannan juokseva numero: Myrskyn id on tällä koneella 1, toisessa instanssissa vaikka 40. Nimi on luonnollinen avain — sama joka ympäristössä — joten sama seeder toimii kaikkialla. `luotiin` kertoo, että POST tehtiin ja API palautti uuden id:n. `oli jo` kertoo, että haku löysi saman nimen tai otteluparin eikä POST lähtenyt. Ilman tuota riviä toinen ajo näyttäisi hiljaiselta onnistumiselta, vaikka seeder olisi luonut duplikaatin jota et huomannut.

**6. Miksi seederin loppukysely on integraatiotesti, eikä riitä että POST palautti 201?**

201 kertoo yhden pyynnön vastauksesta. Loppukysely tekee uudet GET:t elävään APIin ja vertaa specin nimet siihen, mitä kanta palauttaa nyt. Mukana on seeder, HTTP, controller, use case, domain ja SQLite. Yksikkötesti kutsuu `Team.Create`a ilman niitä. Jos tarkistus avaisi `leaguehub.db`-tiedoston itse, se ohittaisi rajan jota mitataan.

**7. Miksi reitissä lukee `/api/v1`, vaikka käytössä on vasta yksi versio?**

Polku on sopimus, jonka seeder ja myöhemmin selain on kirjoitettu vasten. Numero sanoo, että tämä muoto pysyy. Seuraava rikkova muutos (uusi kentän nimi, toinen polku) saa `/api/v2`:n, ja vanha asiakas saa jäädä v1:een kunnes se päivitetään tarkoituksella. Ilman numeroa jokainen muutos rikkoo kaikki asiakkaat kerralla, eikä vanhalle seederille ole osoitetta johon se voi jäädä. Versio asuu controllerin `[Route]`-attribuutissa, ei domainissa.

## Spec ja tekoäly

**8. Miksi spec kirjoitetaan ja commitoidaan ennen ensimmäistäkään riviä seederin koodia?**

Kaksi syytä. Osion 11 PR:ssä diffiä verrataan lupaukseen: spec kertoo mitä koodin piti tehdä, ja toteutuksen voi lukea sitä vasten — ilman speciä lukija arvaa. Ja myöhemmät promptit viittaavat speciin ("toteuta specs/001") sen sijaan, että sama sääntölista kirjoitettaisiin jokaiseen prompttiin uudestaan ja versiot ajautuisivat erilleen. Lisäksi väärään paikkaan merkitty sääntö on halvin korjata specissä: se on yksi rivi taulukossa, ei vielä koodia.

**9. Miksi koko tehtävää ei anneta agentille yhdellä promptilla?**

Repo, spec ja seeder tarkistetaan eri kohdista: repo osoitteesta ja koskemattomasta ensimmäisestä commitista, spec tiedostosta, seeder kahdella ajolla ja yhteenvedolla `3/3, 3/3, 2/2`. Yksi viesti sekoittaa ne — agentti ehtii pushata keskeneräisen koodin tai avata PR:n väärään baseen ennen kuin mitään on todettu. Rajattu prompti pitää myös diffin luettavana: yhden osion muutoksen ehtii lukea, koko tehtävän muutosta ei. Osioissa 1–4 ja 11–13 promptti rajaa gitin, osioissa 5–10 ja 14 tiedostot. Checkpoint sinun ajamanasi on menetelmän ydin, ja se on mahdollinen vain kun työ etenee osissa.

**10. Miksi agentti ei commitoi tässä tehtävässä, vaikka se osaisi?**

Commit on merkintä siitä, että kohta on todettu toimivaksi — ja toteaja on se, joka ajaa checkpointin. Agentti sanoo "valmis" myös silloin, kun ajo epäonnistuisi. Kun sinä commitoit checkpointin jälkeen, jokainen historian kohta on sellainen, jonka joku oikeasti tarkisti. Sama syy, miksi PR:n yhdistäminen jää ihmiselle.

**11. Miksi AGENTS.md ei luettele demodatan nimiä, vaikka ne ovat specissä?**

Kopio vanhenee. Kun demodata muuttuu, spec päivitetään — mutta kukaan ei muista päivittää toista listaa AGENTS.md:ssä, ja seuraava sessio lukee kahta eri totuutta. Siksi AGENTS.md viittaa speciin eikä toista sitä. Sama sääntö kuin koodissa: yksi tieto asuu yhdessä paikassa.

## Savutesti ja CLI

**12. Miksi savutestin tulos on paluukoodi eikä pelkkä tulosterivi?**

Tulosterivin lukee ihminen. Paluukoodin lukee ohjelma: skripti tai agentti pysähtyy nollasta poikkeavaan koodiin ilman, että kukaan tulkitsee tekstiä. Jos savutesti päättyy nollaan myös virhetilanteessa, se näyttää vihreältä kaikelle, mikä sen perään ketjutetaan — silloin komento ei kelpaa seuraavan askeleen pysäyttäjäksi. Sama syy, miksi seeder heittää poikkeuksen kun specin rivi puuttuu.

Osoite `/health` ei ole `/api/v1`:n alla, koska vointi ei ole osa domain-APIa vaan infrastruktuurin sopimus: se ei versioidu, ja sama osoite vastaa samalla tavalla silloinkin, kun domain-API saa joskus version v2.
