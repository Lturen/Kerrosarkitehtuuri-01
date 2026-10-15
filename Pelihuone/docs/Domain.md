# Pelihuone – Domain

Domain-malli kuvaa luokat ja liiketoimintasäännöt riippumatta tallennustavasta.
Kielen (FI/EN), ulkoasun (Xamkin värit, logot) ja fyysisten asioiden (puhdistusliinat, wifi) kohdat kuuluvat UI:hin, eivät tähän.

## Sanasto

| Sana | Koodissa | Yksi lause |
|---|---|---|
| **Käyttäjä** | `User` | Henkilö, joka kirjautuu koulun sähköpostilla ja jolla on rooli (opiskelija, henkilökunta, admin, superadmin). |
| **Kampus** | `Campus` | Koulun toimipiste, jolla on pelihuone, pelipisteet ja aukioloajat. |
| **Pelipiste** | `GameStation` | Yksittäinen varattava piste (PC, Switch tai PlayStation), jolla on numero ja lista peleistä. |
| **Peli** | `Game` | Pelipisteellä pelattavissa oleva peli, jolla on nimi ja alusta. |
| **Varaus** | `Booking` | Yhden käyttäjän aikaväli yhdellä pelipisteellä, jonka voi perua ennen alkua. |
| **Tapahtuma** | `Event` | Henkilökunnan järjestämä tilaisuus, joka varaa koko kampuksen tilan eikä salli yksittäisvarauksia. |
| **Aikaväli** | `TimeSlot` | Alkamis- ja päättymisaika, jonka kesto on 30 min – 4 h ja joka mahtuu aukioloaikojen sisään. |
| **Aukioloaika** | `OpeningHours` | Kampuksen viikonpäiväkohtainen aukeamis- ja sulkeutumisaika. |
| **Sähköposti** | `Email` | Koulun sähköpostiosoite (`@xamk.fi` tai `@edu.xamk.fi`), jolla käyttäjä tunnistetaan. |
| **Ilmoitus** | `Notification` | Käyttäjälle lähetetty viesti varauksen peruuntumisesta tai koko päivän varauksesta. |
| **Uutinen** | `NewsItem` | Ylläpitäjän etusivulle julkaisema pelitilan uutinen. |
| **Toivepyyntö** | `GameRequest` | Käyttäjän ylläpitäjille esittämä toive uudesta pelistä. |
| **Vapaana** | `IsFree` | Laskettu tieto: kampus auki, ei Tapahtumaa eikä aktiivista Varausta nykyhetkellä. Ei tallenneta. |

Valinnat: asiakas puhuu "pelipisteestä", joten `Pelikone`-nimi vaihdettiin siihen, koska tilassa on myös Switch ja PlayStation. Tunnisteet ovat koodissa englanniksi, kurssin tapaan.

## Luokat

### Käyttäjä
- Id
- Nimi
- Sähköposti (uniikki)
- Puhelinnumero
- KulkulätkäId
- Rooli (Opiskelija | Henkilökunta | Admin | SuperAdmin)

### Kampus
- Id
- Kaupunki
- Koulu
- Osoitetiedot
- Pelihuoneen numero
- Aukioloajat (lista: viikonpäivä, aukeaa, sulkeutuu)

### Pelipiste
Yksittäinen varattava piste (esim. kone 1–6, Nintendo Switch, PlayStation).
- Id
- Numero (näytetään varauksessa)
- Malli
- Tyyppi (PC | Switch | PlayStation)
- Kampus
- Pelit (lista: Peli)

### Peli
- Id
- Nimi
- Alusta

### Varaus
Yhden käyttäjän varaus yhdelle pelipisteelle.
- Id
- Käyttäjä
- Pelipiste (kampus tulee pelipisteen kautta)
- Alkaa
- Päättyy
- Kesto (laskettu: Päättyy − Alkaa)
- Tila (Aktiivinen | Peruttu | Käytetty | Käyttämättä)

### Tapahtuma
Henkilökunnan järjestämä tapahtuma, joka varaa koko tilan.
- Id
- Järjestäjä (Käyttäjä, rooli Henkilökunta tai ylempi)
- Kampus
- Nimi
- Alkaa
- Päättyy

### Ilmoitus
- Id
- Vastaanottaja (Käyttäjä)
- Tyyppi (VarausPeruttuTapahtumanVuoksi | TilaVarattuKokoPäiväksi)
- Viesti
- Lähetetty

### Uutinen
- Id
- Otsikko
- Sisältö
- Julkaistu
- Kirjoittaja (Käyttäjä, Admin)

### Toivepyyntö
Pyyntö uudesta pelistä ylläpitäjille.
- Id
- Käyttäjä
- Pelin nimi
- Alusta
- Luotu

## Liiketoimintasäännöt

Paikka-sarake kertoo, missä kerroksessa sääntö valvotaan:
- **Domain** = entiteetti tai value object (sääntö pätee aina, tallennustavasta riippumatta)
- **Service** = sovelluslogiikka (tarvitsee dataa useammasta entiteetistä tai repositorysta)
- **UI** = vain käyttöliittymän validointi (ei ainoa suoja)
- **DB** = lisäksi tietokannan constraint (esim. uniikki-indeksi) turvaverkkona

### Käyttäjä
| # | Sääntö | Paikka | Syy |
|---|---|---|---|
| K1 | Sähköposti on uniikki. | Service + DB | Tarvitsee haun kaikista käyttäjistä; DB-indeksi estää kilpailutilanteen. |
| K2 | Sähköpostin pääte on `@xamk.fi` tai `@edu.xamk.fi`. Tarkistus päätteestä, ei `contains`. | Domain (VO `Sahkoposti`) | Asiakas: kirjautuminen koulun sähköpostilla. `contains` hyväksyisi myös vääriä osoitteita. |
| K3 | Vain SuperAdmin voi antaa admin-oikeuksia. | Service | Tarvitsee tiedon toimijasta ja kohteesta; asiakkaan vaatimus. |
| K4 | Vain Henkilökunta tai ylempi voi luoda Tapahtuman. | Service | Asiakas: henkilökunta järjestää tapahtumat. |

### Varaus
| # | Sääntö | Paikka | Syy |
|---|---|---|---|
| V1 | Samaa pelipistettä ei voi varata päällekkäisiin aikoihin. | Service (+ DB-tarkistus) | Asiakas: ehdoton sääntö. Tarvitsee muut varaukset repositorystä; kilpailutilanne vaatii DB-tason suojan. |
| V2 | Varaus on kampuksen aukioloaikojen sisällä. | Domain (Kampus tai VO `Aukioloaika` tarkistaa Aikavälin) | Asiakas: ehdoton sääntö. Aukioloajat kuuluvat kampukselle, ei tallennukseen. |
| V3 | Kesto 30 min – 4 h. | Domain (VO `Aikavali`) | Asiakas: ehdoton sääntö (yläraja). Tarvitsee vain varauksen omat tiedot. |
| V4 | Käyttäjällä ei kahta päällekkäistä varausta. Eri päiville saa varata useita. | Service | Asiakas: kahta konetta ei voi varata samaan aikaan. Tarvitsee käyttäjän muut varaukset. |
| V5 | Ketjutusta ei sallita. | Service | Asiakas. Tarkka määritelmä auki (ks. avoimet kysymykset). |
| V6 | Peruutus vain ennen varauksen alkua. | Domain (`Varaus.Peru(nyt)`) | Asiakas: ehdoton sääntö. Tarvitsee vain varauksen alkuajan ja kellonajan. |
| V7 | Varausta ei voi tehdä Tapahtuman ajalle. | Service | Asiakas: tapahtuman aikana ei yksittäisvarauksia. Tarvitsee tapahtumat repositorystä. |
| V8 | Varauksessa on ilmoitettava pelipisteen numero. | Domain + UI | Havainto: koneet numeroitu. Ilman pelipistettä varaus ei ole kelvollinen. |

### Tapahtuma
| # | Sääntö | Paikka | Syy |
|---|---|---|---|
| T1 | Tapahtuma varaa koko kampuksen tilan. | Domain | Asiakas: henkilökunta voi varata koko tilan. |
| T2 | Tapahtuman luonti peruu päällekkäiset varaukset ja lähettää ilmoituksen (VarausPeruttuTapahtumanVuoksi). | Service | Asiakas: opiskelijalle ilmoitus. Koskee useaa entiteettiä ja ilmoituspalvelua. |
| T3 | Koko päivän tapahtumasta ilmoitus kaikille koulun sähköpostin käyttäjille (TilaVarattuKokoPäiväksi). | Service | Asiakas. Tarvitsee käyttäjälistan ja ilmoituspalvelun. |
| T4 | Tapahtuma on aukioloaikojen sisällä. | Domain (kuten V2) | Sama syy kuin V2: varaus ei voi ylittää aukioloaikoja. |

## Value objectit

| Käsite | Päätös | Perustelu |
|---|---|---|
| `Sahkoposti` | **VO** | Sisältää validoinnin (K2) ja vertailu on arvon perusteella. Kapseloi säännön yhteen paikkaan. |
| `Aikavali` (Alkaa + Päättyy) | **VO** | Alku ja loppu kuuluvat aina yhteen. Sisältää keston (V3), päällekkäisyystarkistuksen (V1, V4, V7) ja aukioloaikavertailun (V2). Käytetään sekä Varauksessa että Tapahtumassa. |
| `Aukioloaika` (viikonpäivä, aukeaa, sulkeutuu) | **VO** | Ei omaa identiteettiä, vertailu arvon perusteella. Kampus omistaa listan niistä. |
| `PelipisteenNumero` | **Primitiivi (int)** | Ei omia sääntöjä toistaiseksi. VO vasta, jos numeroinnille tulee sääntöjä. |
| `KulkulatkaId` | **Primitiivi (string)** | Vain tunniste, ei logiikkaa tässä vaiheessa. |
| `Kesto` | **Johdettu** (`Aikavali.Kesto`) | Ei tallenneta erikseen, jottei se voi ristiriitaistua aikavälin kanssa. |

Entiteetit (oma identiteetti, elinkaari): Käyttäjä, Kampus, Pelipiste, Peli, Varaus, Tapahtuma, Ilmoitus, Uutinen, Toivepyyntö.

## Johdetut näkymät

Näitä ei tallenneta, vaan ne lasketaan kyselyhetkellä.

### Pelipiste vapaana nyt
Syöte: kampus, nykyhetki. Pelipiste on **vapaa**, jos kaikki ovat tosia:
1. Kampus on auki nykyhetkellä (Aukioloaika).
2. Kampuksella ei ole aktiivista Tapahtumaa, joka kattaa nykyhetken.
3. Pelipisteellä ei ole aktiivista Varausta (Tila = Aktiivinen), jonka Aikavali kattaa nykyhetken.

Muuten pelipiste on **varattu** (varaus tai tapahtuma) tai **suljettu** (kampus kiinni).

### Pelipiste vapaana tänään
Syöte: kampus, päivä. Palauttaa pelipisteittäin vapaat aikavälit: aukioloaika vähennettynä aktiivisilla varauksilla ja tapahtumilla. Jos päivälle osuu koko päivän Tapahtuma, kaikki pelipisteet ovat varattuja koko päivän.

### Kalenterinäkymä
Päivän/viikon aktiiviset Varaukset ja Tapahtumat kampuksittain. Peruttuja ei näytetä.

**Miksi laskettu:** tila muuttuu ajan kuluessa ilman tapahtumaa (varaus alkaa tai päättyy), joten tallennettu "vapaa"-lippu vanhenisi. Näin sääntö ei myöskään ole kiinni tallennustavasta, mikä oli asiakkaan vaatimus.

## Avoimet kysymykset (asiakkaalle)
- Mitkä ovat aukioloajat? Ovatko ne päiväkohtaisia ja kampuskohtaisia?
- Mitä ketjutus tarkoittaa tarkasti (peräkkäiset varaukset samalle henkilölle/koneelle)?
- Kuinka pitkälle eteenpäin voi varata?
- Miten tunnistetaan, onko varaus käytetty (kulkulätkä, kuittaus)? Mikä on sanktio tyhjistä varauksista?
- Varaako tapahtuma aina kaikki kampuksen pelipisteet?
- Kuuluuko oven pääsynhallinta (kulkulätkä) tähän järjestelmään vai vain aukioloaikojen noudattaminen?
