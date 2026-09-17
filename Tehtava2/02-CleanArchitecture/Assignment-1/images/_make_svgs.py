"""Kerta 2 Assignment 1 -diagrammit. Aja kerran; SVG:t commitoitaan UTF-8:na."""
from pathlib import Path

OUT = Path(__file__).parent
FONT = "Segoe UI, Calibri, sans-serif"
MONO = "Segoe UI, Consolas, monospace"

HEAD = """<?xml version="1.0" encoding="UTF-8"?>
<svg xmlns="http://www.w3.org/2000/svg" width="{w}" height="{h}" viewBox="0 0 {w} {h}" role="img" aria-label="{aria}">
  <rect width="{w}" height="{h}" fill="#f7f5f2"/>
  <text x="24" y="36" font-family="FONT" font-size="20" font-weight="700" fill="#1d3557">{title}</text>
""".replace("FONT", FONT)
FOOT = "</svg>\n"


def txt(x, y, s, size=13, fill="#1d3557", weight=None, font=None):
    w = f' font-weight="{weight}"' if weight else ""
    fam = font or FONT
    return f'<text x="{x}" y="{y}" font-family="{fam}" font-size="{size}" fill="{fill}"{w}>{s}</text>'


def write(name, w, h, title, body, aria=None):
    (OUT / name).write_text(
        HEAD.format(w=w, h=h, title=title, aria=aria or title) + body + FOOT,
        encoding="utf-8",
    )
    print("wrote", name)


write(
    "01-folders-vs-projects.svg",
    960,
    420,
    "Kansiot eivät valvo — projektit valvovat",
    f"""
  <rect x="40" y="70" width="420" height="280" rx="10" fill="#fff1ee" stroke="#c1121f" stroke-width="2"/>
  {txt(56, 100, "Kerta 1 — yksi projekti", 16, "#1d3557", 700)}
  {txt(56, 128, "LeagueHub/", 14, font=MONO)}
  {txt(72, 152, "Controllers/", 13, font=MONO)}
  {txt(72, 176, "Services/", 13, font=MONO)}
  {txt(72, 200, "Repositories/", 13, font=MONO)}
  {txt(56, 236, "Controller voi kutsua repositorya.", 13, "#9b2226")}
  {txt(56, 258, "Kääntäjä ei estä. Vain kuri estää.", 13, "#9b2226")}

  <rect x="500" y="70" width="420" height="280" rx="10" fill="#e9f5f3" stroke="#2a9d8f" stroke-width="2"/>
  {txt(516, 100, "Kerta 2 — neljä projektia", 16, "#1d3557", 700)}
  {txt(516, 128, "LeagueHub.Api", 13, font=MONO)}
  {txt(516, 152, "LeagueHub.Application", 13, font=MONO)}
  {txt(516, 176, "LeagueHub.Infrastructure", 13, font=MONO)}
  {txt(516, 200, "LeagueHub.Domain", 13, font=MONO)}
  {txt(516, 236, "Domainissa ei ole viittausta muualle.")}
  {txt(516, 258, "Väärä using ei käänny.")}

  {txt(40, 384, "Sama tuote. Ero on siinä, kuka valvoo riippuvuussääntöä: ihminen vai kääntäjä.", 14)}
""",
    aria="Kerrokset kansioina vastaan erillisinä projekteina",
)

write(
    "02-anemic-vs-rich.svg",
    960,
    430,
    "Aneeminen malli vs. rikas malli",
    f"""
  <rect x="40" y="70" width="420" height="290" rx="10" fill="#fff1ee" stroke="#c1121f" stroke-width="2"/>
  {txt(56, 100, "Aneeminen — kerta 1", 16, "#1d3557", 700)}
  {txt(56, 130, "public int? HomeGoals {{ get; set; }}", 13, font=MONO)}
  {txt(56, 162, "Match on tietosäiliö.", 14)}
  {txt(56, 186, "Säännöt ovat MatchServicessä.", 14)}
  {txt(56, 222, "new Match {{ HomeGoals = -1 }}", 13, "#9b2226", font=MONO)}
  {txt(56, 246, "kääntyy ja menee listaan.", 13, "#9b2226")}
  {txt(56, 280, "Kuka tahansa voi ohittaa säännön.", 13)}

  <rect x="500" y="70" width="420" height="290" rx="10" fill="#e9f5f3" stroke="#2a9d8f" stroke-width="2"/>
  {txt(516, 100, "Rikas — kerta 2", 16, "#1d3557", 700)}
  {txt(516, 130, "public int? HomeGoals {{ get; private set; }}", 13, font=MONO)}
  {txt(516, 162, "Match suojaa omat sääntönsä.", 14)}
  {txt(516, 186, "Tila muuttuu vain metodeilla.", 14)}
  {txt(516, 222, "Match.Create(...)", 13, "#2a9d8f", font=MONO)}
  {txt(516, 246, "match.RecordResult(score, now)", 13, "#2a9d8f", font=MONO)}
  {txt(516, 280, "Virheellistä oliota ei voi luoda.", 13)}

  {txt(40, 396, "Invariantti = sääntö, jonka pitää olla voimassa aina kun olio on olemassa.", 14)}
""",
    aria="Aneeminen Match vastaan rikas Match",
)

write(
    "03-ca-layers.svg",
    960,
    560,
    "Clean Architecture: Domain on keskellä",
    f"""
  <defs>
    <marker id="caArrow" viewBox="0 0 10 10" refX="8" refY="5" markerWidth="8" markerHeight="8" orient="auto-start-reverse">
      <path d="M 0 0 L 10 5 L 0 10 z" fill="#1d3557"/>
    </marker>
  </defs>

  <rect x="80" y="64" width="800" height="78" rx="12" fill="#1d3557"/>
  {txt(108, 96, "API", 18, "#ffffff", 700)}
  {txt(108, 120, "Controllerit, requestit, Program.cs — composition root", 13, "#a8dadc")}

  <line x1="280" y1="142" x2="280" y2="168" stroke="#1d3557" stroke-width="2.5" marker-end="url(#caArrow)"/>
  <line x1="680" y1="142" x2="680" y2="168" stroke="#1d3557" stroke-width="2.5" marker-end="url(#caArrow)"/>

  <rect x="80" y="172" width="380" height="100" rx="12" fill="#457b9d"/>
  {txt(100, 208, "Application", 18, "#ffffff", 700)}
  {txt(100, 234, "Use caset, Standing, NotFound", 13, "#a8dadc")}
  {txt(100, 254, "Orkestroi — ei invariantteja", 13, "#a8dadc")}

  <rect x="500" y="172" width="380" height="100" rx="12" fill="#2a9d8f"/>
  {txt(520, 208, "Infrastructure", 18, "#ffffff", 700)}
  {txt(520, 234, "EF Core, DbContext, SQLite", 13, "#ffffff")}
  {txt(520, 254, "Repository-toteutukset", 13, "#ffffff")}

  <line x1="270" y1="272" x2="400" y2="318" stroke="#1d3557" stroke-width="2.5" marker-end="url(#caArrow)"/>
  <line x1="690" y1="272" x2="560" y2="318" stroke="#1d3557" stroke-width="2.5" marker-end="url(#caArrow)"/>

  <rect x="200" y="322" width="560" height="110" rx="12" fill="#3d5a80"/>
  {txt(228, 360, "Domain — ei viittaa mihinkään", 18, "#ffffff", 700)}
  {txt(228, 386, "Team, Match, Score, DomainException", 13, "#a8dadc")}
  {txt(228, 408, "ITeamRepository, IMatchRepository — rajapinnat, ei EF:ää", 13, "#a8dadc")}

  {txt(24, 476, "Dependency Rule: nuolet osoittavat sisäänpäin. Tietokanta riippuu domainista — ei toisinpäin.", 14)}
  {txt(24, 502, "API viittaa myös Infrastructureen, koska Program.cs kytkee rajapinnan toteutukseen.", 14, "#457b9d")}
  {txt(24, 528, "Sama jako kuin MyLeaguessa — tällä kurssilla ilman CQRS:ää ja Reactia.", 13)}
""",
    aria="Clean Architecture: riippuvuudet osoittavat Domainiin",
)

write(
    "04-rule-placement.svg",
    960,
    420,
    "Mihin sääntö kuuluu?",
    f"""
  <rect x="40" y="70" width="420" height="280" rx="10" fill="#3d5a80"/>
  {txt(56, 104, "Entiteetti", 16, "#ffffff", 700)}
  {txt(56, 132, "Koskee yhden olion omaa dataa.", 14, "#a8dadc")}
  {txt(56, 168, "Koti ja vieras eivät ole sama", 14, "#ffffff")}
  {txt(56, 192, "Tulos vain kerran", 14, "#ffffff")}
  {txt(56, 216, "Ottelu on jo pelattu", 14, "#ffffff")}
  {txt(56, 240, "Maalit eivät ole negatiivisia (Score)", 14, "#ffffff")}
  {txt(56, 276, "Match.Create / RecordResult", 13, "#a8dadc", font=MONO)}

  <rect x="500" y="70" width="420" height="280" rx="10" fill="#457b9d"/>
  {txt(516, 104, "Use case", 16, "#ffffff", 700)}
  {txt(516, 132, "Tarvitaan haku tai toinen olio.", 14, "#a8dadc")}
  {txt(516, 168, "Molemmat joukkueet ovat olemassa", 14, "#ffffff")}
  {txt(516, 192, "Ottelua ei löydy", 14, "#ffffff")}
  {txt(516, 216, "Pelinumero uniikki joukkueessa", 14, "#ffffff")}
  {txt(516, 240, "(soveltava Player)", 13, "#a8dadc")}
  {txt(516, 276, "CreateMatchUseCase", 13, "#a8dadc", font=MONO)}

  {txt(40, 384, "return BadRequest(...) ei ole kumpikaan — se on HTTP ja kuuluu API-kerrokseen.", 14)}
""",
    aria="Säännön paikka: entiteetti tai use case",
)

write(
    "05-request-flow-ca.svg",
    960,
    420,
    "POST /api/matches/1/result — nyt CA",
    f"""
  <rect x="24" y="70" width="170" height="200" rx="10" fill="#e9edc9" stroke="#606c38" stroke-width="2"/>
  {txt(40, 100, "1. HTTP", 15, "#1d3557", 700)}
  {txt(40, 126, "Swagger")}
  {txt(40, 148, "homeGoals")}
  {txt(40, 168, "awayGoals")}

  <rect x="210" y="70" width="170" height="200" rx="10" fill="#1d3557"/>
  {txt(226, 100, "2. API", 15, "#ffffff", 700)}
  {txt(226, 126, "Controller", 13, "#ffffff")}
  {txt(226, 148, "Kutsuu use casea", 13, "#ffffff")}
  {txt(226, 168, "400 / 404 / 200", 13, "#ffffff")}
  {txt(226, 198, "Ei sääntöjä", 12, "#a8dadc")}

  <rect x="396" y="70" width="170" height="200" rx="10" fill="#457b9d"/>
  {txt(412, 100, "3. Use case", 15, "#ffffff", 700)}
  {txt(412, 126, "Hae ottelu", 13, "#ffffff")}
  {txt(412, 148, "Score.Create", 13, "#ffffff")}
  {txt(412, 168, "RecordResult", 13, "#ffffff")}
  {txt(412, 188, "UpdateAsync", 13, "#ffffff")}
  {txt(412, 218, "Orkestroi", 12, "#a8dadc")}

  <rect x="582" y="70" width="170" height="200" rx="10" fill="#3d5a80"/>
  {txt(598, 100, "4. Domain", 15, "#ffffff", 700)}
  {txt(598, 126, "Tulos jo?", 13, "#ffffff")}
  {txt(598, 148, "Ajankohta ohi?", 13, "#ffffff")}
  {txt(598, 168, "Maalit &gt;= 0", 13, "#ffffff")}
  {txt(598, 198, "Invariantit", 12, "#a8dadc")}

  <rect x="768" y="70" width="168" height="200" rx="10" fill="#2a9d8f"/>
  {txt(784, 100, "5. Infra", 15, "#ffffff", 700)}
  {txt(784, 126, "EF Core", 13, "#ffffff")}
  {txt(784, 148, "SQLite", 13, "#ffffff")}
  {txt(784, 168, "SaveChanges", 13, "#ffffff")}
  {txt(784, 198, "Ei sääntöjä", 12, "#ffffff")}

  {txt(24, 310, "Kerralla 1 vaiheet 3 ja 4 olivat sama Service. Nyt orkestrointi ja invariantti ovat eri projekteissa.", 14)}
  {txt(24, 336, "Use case ei toista if (HomeGoals is not null) — sen tekee Match.RecordResult.", 14, "#457b9d")}
  {txt(24, 372, "Välitehtävässä käy tämä polku koodista osoittaen, ei kaaviota lukien.", 13)}
""",
    aria="POST result Clean Architecturessa",
)

write(
    "06-project-structure.svg",
    960,
    520,
    "Projektit ja viittaukset",
    f"""
  <defs>
    <marker id="refArrow" viewBox="0 0 10 10" refX="8" refY="5" markerWidth="8" markerHeight="8" orient="auto-start-reverse">
      <path d="M 0 0 L 10 5 L 0 10 z" fill="#1d3557"/>
    </marker>
  </defs>

  <rect x="300" y="60" width="360" height="70" rx="10" fill="#1d3557"/>
  {txt(320, 90, "LeagueHub.Api", 16, "#ffffff", 700)}
  {txt(320, 112, "viittaa Application + Infrastructure", 12, "#a8dadc")}

  <line x1="380" y1="130" x2="220" y2="168" stroke="#1d3557" stroke-width="2" marker-end="url(#refArrow)"/>
  <line x1="580" y1="130" x2="740" y2="168" stroke="#1d3557" stroke-width="2" marker-end="url(#refArrow)"/>

  <rect x="40" y="172" width="360" height="80" rx="10" fill="#457b9d"/>
  {txt(60, 204, "LeagueHub.Application", 16, "#ffffff", 700)}
  {txt(60, 228, "viittaa vain Domainiin", 12, "#a8dadc")}

  <rect x="560" y="172" width="360" height="80" rx="10" fill="#2a9d8f"/>
  {txt(580, 204, "LeagueHub.Infrastructure", 16, "#ffffff", 700)}
  {txt(580, 228, "viittaa vain Domainiin", 12, "#ffffff")}

  <line x1="220" y1="252" x2="400" y2="300" stroke="#1d3557" stroke-width="2" marker-end="url(#refArrow)"/>
  <line x1="740" y1="252" x2="560" y2="300" stroke="#1d3557" stroke-width="2" marker-end="url(#refArrow)"/>

  <rect x="260" y="304" width="440" height="90" rx="10" fill="#3d5a80"/>
  {txt(280, 340, "LeagueHub.Domain", 16, "#ffffff", 700)}
  {txt(280, 364, "dotnet list LeagueHub.Domain reference — tyhjä lista", 13, "#a8dadc")}
  {txt(280, 384, "Tests viittaa Domain + Application", 12, "#a8dadc")}

  {txt(24, 436, "Mitä ei lisätä: Domain ei viittaa kehenkään. Application ei viittaa Infrastructureen.", 14)}
  {txt(24, 464, "Vanha LeagueHub-projekti jää viereen referenssiksi. Älä siirrä sen tiedostoja näihin.", 14, "#457b9d")}
  {txt(24, 492, "LeagueHub.Tests ei näy kaaviossa ylhäällä — se ei ole kerros, se on tarkastaja.", 13)}
""",
    aria="LeagueHub Clean Architecture -projektit",
)

write(
    "07-tests-two-levels.svg",
    960,
    400,
    "Kaksi testitasoa — ei HTTP:tä kummallakaan",
    f"""
  <rect x="40" y="70" width="420" height="250" rx="10" fill="#e9f5f3" stroke="#2a9d8f" stroke-width="2"/>
  {txt(56, 104, "Domain-testi", 16, "#1d3557", 700)}
  {txt(56, 136, "Ei fakea, ei repositorya.", 14)}
  {txt(56, 160, "new Match + RecordResult(now)", 14)}
  {txt(56, 196, "Create_Throws_WhenSameTeam", 13, font=MONO)}
  {txt(56, 220, "RecordResult_Throws_WhenAlready", 13, font=MONO)}
  {txt(56, 256, "Aika parametrina — tuleva ottelu helppo.", 13, "#2a9d8f")}

  <rect x="500" y="70" width="420" height="250" rx="10" fill="#e8f1f6" stroke="#457b9d" stroke-width="2"/>
  {txt(516, 104, "Use case -testi", 16, "#1d3557", 700)}
  {txt(516, 136, "Fake toteuttaa ITeamRepositoryn.", 14)}
  {txt(516, 160, "Testaa orkestrointia: puuttuva joukkue.", 14)}
  {txt(516, 196, "ExecuteAsync_Throws_WhenTeamMissing", 13, font=MONO)}
  {txt(516, 220, "Fake ei tiedä SQLitestä.", 13)}
  {txt(516, 256, "Tallennus vaihtui — testit eivät.", 13, "#457b9d")}

  {txt(40, 356, "Kerralla 1 oli vain oikea laatikko (Service + fake). Vasen on uusi: entiteettiä voi testata yksin.", 14)}
""",
    aria="Kaksi testitasoa: domain ja use case",
)

write(
    "08-scoped-vs-singleton.svg",
    960,
    380,
    "Elinkaari seuraa datan kotia",
    f"""
  <rect x="40" y="70" width="420" height="220" rx="10" fill="#1d3557"/>
  {txt(56, 104, "Kerta 1 — Singleton", 16, "#ffffff", 700)}
  {txt(56, 136, "Data elää repository-oliossa.", 14, "#ffffff")}
  {txt(56, 160, "Olio kuolee — lista tyhjenee.", 14, "#ffffff")}
  {txt(56, 196, "AddSingleton IMatchRepository", 13, "#a8dadc", font=MONO)}
  {txt(56, 236, "Scoped-repository = seuraava GET tyhjä.", 13, "#a8dadc")}

  <rect x="500" y="70" width="420" height="220" rx="10" fill="#2a9d8f"/>
  {txt(516, 104, "Kerta 2 — Scoped", 16, "#ffffff", 700)}
  {txt(516, 136, "Data elää SQLite-tiedostossa.", 14, "#ffffff")}
  {txt(516, 160, "DbContext on Scoped (yksi / pyyntö).", 14, "#ffffff")}
  {txt(516, 196, "AddScoped IMatchRepository", 13, "#ffffff", font=MONO)}
  {txt(516, 236, "Singleton + DbContext = captive dependency.", 13, "#ffffff")}

  {txt(40, 328, "Use caset ovat Scoped molemmilla kerroilla. Vaihtui vain se, jonka sisällä tila asui.", 14)}
""",
    aria="Repositoryn elinkaari seuraa siitä missä data elää",
)
