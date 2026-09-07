# Esimerkkikoodit — kerta 1

Opiskelijan tehtävä on rakentaa itse. Nämä ovat mallivastaukset, samalla jaolla kuin tehtäväkansiot.

| Kansio | Mitä se on |
|--------|------------|
| [Assignment-1/spaghetti](Assignment-1/spaghetti/) | Assignment 1: vaiheet 1–3, kaikki yhdessä controllerissa |
| [Assignment-1/layered](Assignment-1/layered/) | Assignment 1: kerrokset + tiketit LH-1–LH-4 |
| [Assignment-2/layered](Assignment-2/layered/) | Assignment 2: Lainaamo kerroksissa + testi *väline on jo lainassa* |
| [kertauskysymykset-vastaukset.md](kertauskysymykset-vastaukset.md) | Esimerkkivastaukset kertauskysymyksiin |

Assignment 2:n **lähtökoodi** ei ole tässä. Se on `../Assignment-2/starter/Lainaamo/`.

```bash
cd Assignment-1/spaghetti/LeagueHub && dotnet run
cd Assignment-1/layered && dotnet test && dotnet run --project LeagueHub
cd Assignment-2/layered && dotnet test && dotnet run --project Lainaamo
```
