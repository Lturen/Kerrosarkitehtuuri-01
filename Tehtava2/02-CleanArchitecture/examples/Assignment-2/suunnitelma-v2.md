# Assignment 2 v2 — mallivastaus (suunnitelma)

Mallivastaus tehtävälle [README-v2.md](../../Assignment-2/README-v2.md). Avaa vasta, kun olet tehnyt omat neljä tehtävää.

Tämä ei ole koodia. Se on se suunnitelmatiedosto, jonka tehtävä pyysi.

---

## Tehtävä 1 — Käsitteet, tekemiset ja säännöt

### Käsitteet

| Käsite | Luokittelu | Miksi |
|--------|------------|-------|
| Väline | **Entiteetti** | Kaksi samannimistä palloa ovat eri pallot — toinen voi olla huollossa, toinen lainassa. Tarvitaan `Id`. |
| Laina | **Entiteetti** | Kaksi lainaa samalle välineelle eri viikkoina ovat eri lainoja. Tarvitaan `Id`. |
| Lainaajan nimi | **Ei kumpikaan** | Teksti lainan tiedoissa. Asiakas ei hallinnoi lainaajia. |
| Eräpäivä | **Ei kumpikaan** | Päivämäärä lainan tiedoissa. Lasketaan lainaushetkestä. |
| Huolto | **Ei kumpikaan** | Välineen oma tila, ei erillinen olio. |
| Lista välineistä / lainoista | **Ei kumpikaan** | Kysely, ei olio. |

Lainaamossa ei ole value objectia. LeagueHubin `Score` oli VO, koska kaksi lukua kuului yhteen. Täällä vastaavaa paria ei ole.

### Tekemiset

- lisää väline
- katso välineet
- laita väline huoltoon
- lainaa väline opiskelijalle
- palauta laina
- katso lainat

### Säännöt

1. Välineellä pitää olla nimi
2. Lainaan pitää kirjata lainaajan nimi
3. Eräpäivä on lainaushetki + 14 vuorokautta
4. Huollossa olevaa välinettä ei saa lainata
5. Samaa välinettä ei voi lainata kahdelle yhtä aikaa
6. Palautetun lainan voi palauttaa vain kerran
7. Lainattavan välineen pitää olla olemassa

---

## Tehtävä 2 — Kerrokset ja säännön paikka

### Asiat kerroksiin

| Asia | Kerros | Miksi |
|------|--------|-------|
| Väline (`Item`), laina (`Loan`) | Domain | Entiteetit ja niiden säännöt. Eivät tiedä HTTP:stä eivätkä tietokannasta. |
| Tekemiset (lainaa, palauta, …) | Application | Use caset: hae oliot, kutsu domainia, tallenna. |
| Listat (välineet, lainat) | Application | Lyhyitä use caseja. |
| Tietokanta (SQLite, `DbContext`) | Infrastructure | Tekniikka. Domain ei saa tietää tästä. |
| Reitit, statuskoodit, request-luokat | API | HTTP-kieli. Ei sääntöjä. |
| `Program.cs` | API | Composition root: kytkee rajapinnat toteutuksiin. |

### Säännöille koti

| Sääntö | Paikka | Miksi |
|--------|--------|-------|
| Välineellä pitää olla nimi | Entiteetti: `Item.Create` | Yhden olion oma data. Virheellistä välinettä ei voi luoda. |
| Lainaajan nimi kirjataan | Entiteetti: `Loan.Create` | Sama peruste. |
| Eräpäivä = lainaushetki + 14 vrk | Entiteetti: `Loan.Create` laskee | Factory takaa, ettei lainaa synny ilman eräpäivää. |
| Huollossa olevaa ei saa lainata | Entiteetti: `Item` | Välineen oma tila. Use case kutsuu metodia. |
| Ei kahta avointa lainaa | Use case: `BorrowItemUseCase` | Vaatii haun muista lainoista. |
| Palautus vain kerran | Entiteetti: `Loan.Return` | Yhden lainan oma tila. |
| Välineen pitää olla olemassa | Use case: haku + `NotFoundException` | Entiteetti ei kysy tietokannalta. |
| 400 / 404 / 201 | API: controller | HTTP-kieli. |

**Miksi "väline on olemassa" ei ole `Item`-luokassa:** olemassaolon tarkistus vaatii haun. Entiteetti ei kysy repositorylta.

**Miksi huolto ja avoin laina ovat eri paikoissa:** huolto katsoo välineen omaa kenttää → `Item`. Avoin laina katsoo muita rivejä → use case. Sama lopputulos, eri tieto.

---

## Tehtävä 3 — Tiedostopuu

```
Lainaamo.Clean.sln
├── Lainaamo.Domain/
│   ├── Entities/
│   │   ├── Item.cs          ← nimi ei tyhjä; huolto → ei lainaa
│   │   └── Loan.cs          ← lainaajan nimi; eräpäivä +14 vrk; palautus vain kerran
│   ├── Exceptions/
│   │   └── DomainException.cs
│   └── Interfaces/
│       ├── IItemRepository.cs
│       └── ILoanRepository.cs
├── Lainaamo.Application/
│   ├── Exceptions/
│   │   └── NotFoundException.cs
│   └── UseCases/
│       ├── Items/
│       │   ├── GetItemsUseCase.cs
│       │   ├── CreateItemUseCase.cs
│       │   └── MarkItemForMaintenanceUseCase.cs
│       └── Loans/
│           ├── GetLoansUseCase.cs
│           ├── BorrowItemUseCase.cs     ← väline olemassa; ei kahta avointa lainaa
│           └── ReturnLoanUseCase.cs
├── Lainaamo.Infrastructure/
│   ├── Persistence/
│   │   ├── LainaamoDbContext.cs
│   │   └── DbSeeder.cs
│   └── Repositories/
│       ├── ItemRepository.cs
│       └── LoanRepository.cs
└── Lainaamo.Api/
    ├── Controllers/
    │   ├── ItemsController.cs           ← 400 / 404 / 201
    │   └── LoansController.cs
    ├── Requests/
    │   ├── CreateItemRequest.cs
    │   └── BorrowItemRequest.cs
    └── Program.cs                       ← composition root
```

Repository-metodit tulevat tekemisistä, ei CRUD-paketista. Poistoa ei ole, koska asiakas ei pyytänyt sitä. `GetOpenByItemIdAsync` on haku — tulkinta on use casessa.

---

## Tehtävä 4 — Lainauksen polku

`POST /api/loans`, body `{ "itemId": 3, "borrowerName": "Maija" }`:

1. `LoansController` lukee pyynnön ja kutsuu `BorrowItemUseCasea`. Ei tarkista sääntöjä.
2. `BorrowItemUseCase` hakee välineen. Jos `null` → `NotFoundException`.
3. Use case kysyy `Item`iltä, saako lainata. Huollossa → `DomainException`.
4. Use case hakee avoimen lainan. Jos löytyi → `DomainException`.
5. `Loan.Create` tarkistaa nimen ja laskee eräpäivän.
6. `ILoanRepository.AddAsync` tallentaa.
7. Controller palauttaa 201.
