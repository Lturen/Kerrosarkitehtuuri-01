1a.
**käsitteet**
-Mitkä asiat toimeksiannossa esiintyvät?
--Välineet.
    --Nimi
    --Huollossa true or false.
    --Id
--Lainat.
    --Väline
    --Lainaushetki.
    --Viimeinen palatutus pvm.
    --Palautettu pvm.
--Lainaaja
    --Etunimi
    --Sukunimi
    --Puhelinnumero
    --Lainat

**Tekemiset**
-Mitä järjestelmässä tehdään?

-Henkilökunta voi luoda uuden välineen.
-Opiskelija lainaa välineen.
-Opiskelija palauttaa lainatun välineen.
-Tulostetaan lista lainoista.
-Tulostetaan lista välineistä.
-Tulostetaan lainaajan lainat.
-Laita väline huoltoon.


**Säännöt**
-Mitkä lauseet rajoittivat sitä, mikä on sallittua?

-Väline voi olla lainassa vain yhdelle opiskelijalle kerrallaan.
-Laina aika on aina 2 viikkoa, eräpäivä lasketaan aina lainaushetkestä.
-Väline voi olla rikki ja huollossa, huollossa olevaa välinettä ei voi lainata.
-Välineellä pitää olla nimi.
-Lainaan pitää kirjata aina lainaajan nimi.
-Lainaajan etunimi & sukunimi pitää olla unique
-Lainattavan välineen on oltava olemassa.

1b.
DDD-nimet käsitteille

2. Suunnittele rakenne neljään kerrokseen.

2a

program.cs DI injektointi, täällä rekisteröidään....

controller:
Täällä on htps pyynnöt get, post, put, delete try catch lohkon sisällä. Jos ottaa catch, palauttaa virheviestin käyttäen expection luokkaa.

Services: Täällä on logiikka, esim onko esine jo olemessa? tarkasta onko nimi null?
Onko väline jo lainassa?
Bisness logiikka voi olla tosi specifiä tuolla. Services luokka perii sitten alemman kerroksen rajapinnan, jonka funktioita se voi käyttää.

Repositories:
Tämä on kerros datalle, täällä data asuu. Tämä kerros piti sisällään seed datan. Rajapinta kertoo mitä voidaan tehdä, itse luokka kertoo miten. Repository luokassa oli funktioit GetItems() joka palautti listan. Tottakai luokassa oli myös seed datassa luotu lista.

| Asia | Kerros | Miksi |
|Väline|Repository||
| | … | … |
