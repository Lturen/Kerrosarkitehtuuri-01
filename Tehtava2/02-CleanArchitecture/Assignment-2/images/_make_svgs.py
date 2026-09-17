"""Kerta 2 Assignment 2 -diagrammit. Aja kerran; SVG:t commitoitaan UTF-8:na."""
from pathlib import Path
import shutil

OUT = Path(__file__).parent
A1 = OUT.parent.parent / "Assignment-1" / "images"
FONT = "Segoe UI, Calibri, sans-serif"

HEAD = """<?xml version="1.0" encoding="UTF-8"?>
<svg xmlns="http://www.w3.org/2000/svg" width="{w}" height="{h}" viewBox="0 0 {w} {h}" role="img" aria-label="{aria}">
  <rect width="{w}" height="{h}" fill="#f7f5f2"/>
  <text x="24" y="36" font-family="FONT" font-size="20" font-weight="700" fill="#1d3557">{title}</text>
""".replace("FONT", FONT)
FOOT = "</svg>\n"


def txt(x, y, s, size=13, fill="#1d3557", weight=None):
    w = f' font-weight="{weight}"' if weight else ""
    return f'<text x="{x}" y="{y}" font-family="{FONT}" font-size="{size}" fill="{fill}"{w}>{s}</text>'


def write(name, w, h, title, body, aria=None):
    (OUT / name).write_text(
        HEAD.format(w=w, h=h, title=title, aria=aria or title) + body + FOOT,
        encoding="utf-8",
    )
    print("wrote", name)


write(
    "01-lainaamo-overview.svg",
    960,
    400,
    "Mitä rakennamme: Lainaamo",
    f"""
  <rect x="40" y="70" width="280" height="210" rx="10" fill="#1d3557"/>
  {txt(56, 102, "Välineet", 16, "#ffffff", 700)}
  {txt(56, 130, "Nimi", 13, "#ffffff")}
  {txt(56, 152, "Huollossa? (uusi)", 13, "#ffffff")}
  {txt(56, 186, "GET/POST /api/items", 13, "#a8dadc")}
  {txt(56, 208, "POST .../maintenance", 13, "#a8dadc")}

  <rect x="340" y="70" width="280" height="210" rx="10" fill="#457b9d"/>
  {txt(356, 102, "Lainat", 16, "#ffffff", 700)}
  {txt(356, 130, "Väline, lainaaja", 13, "#ffffff")}
  {txt(356, 152, "BorrowedAt, DueAt +14 vrk", 13, "#ffffff")}
  {txt(356, 174, "ReturnedAt tai avoin", 13, "#ffffff")}
  {txt(356, 208, "POST /api/loans", 13, "#a8dadc")}
  {txt(356, 230, "POST .../return", 13, "#a8dadc")}

  <rect x="640" y="70" width="280" height="210" rx="10" fill="#e07a5f" stroke="#9b2226" stroke-width="2"/>
  {txt(656, 102, "Säännöt", 16, "#1d3557", 700)}
  {txt(656, 130, "Nimi ei tyhjä")}
  {txt(656, 152, "Ei avointa lainaa kahdesti")}
  {txt(656, 174, "Huolto → ei lainaa")}
  {txt(656, 196, "DueAt = +14 vuorokautta")}
  {txt(656, 230, "Sijoitus: vaihe 3")}

  {txt(40, 322, "Sama tuote kuin kerralla 1, plus huolto ja eräpäivä — jotta aneemista mallia ei voi kopioida.", 15)}
  {txt(40, 350, "Älä avaa kerran 1 Models-luokkia pohjaksi. Suunnittele ensin, koodaa sitten.", 14, "#457b9d")}
""",
    aria="Lainaamo: välineet, lainat ja uudet säännöt",
)

write(
    "02-ddd-order.svg",
    960,
    280,
    "Tee tässä järjestyksessä",
    f"""
  <rect x="24" y="70" width="140" height="120" rx="10" fill="#3d5a80"/>
  {txt(40, 108, "1", 22, "#ffffff", 700)}
  {txt(40, 136, "Sanasto", 14, "#ffffff")}
  {txt(40, 158, "paperille", 12, "#a8dadc")}

  <rect x="180" y="70" width="140" height="120" rx="10" fill="#3d5a80"/>
  {txt(196, 108, "2-3", 22, "#ffffff", 700)}
  {txt(196, 136, "Invariantit", 14, "#ffffff")}
  {txt(196, 158, "entiteetti / use case", 12, "#a8dadc")}

  <rect x="336" y="70" width="140" height="120" rx="10" fill="#457b9d"/>
  {txt(352, 108, "4-5", 22, "#ffffff", 700)}
  {txt(352, 136, "Projektit", 14, "#ffffff")}
  {txt(352, 158, "viittaukset", 12, "#a8dadc")}

  <rect x="492" y="70" width="140" height="120" rx="10" fill="#2a9d8f"/>
  {txt(508, 108, "6", 22, "#ffffff", 700)}
  {txt(508, 136, "Domain", 14, "#ffffff")}
  {txt(508, 158, "+ domain-testit", 12, "#ffffff")}

  <rect x="648" y="70" width="140" height="120" rx="10" fill="#2a9d8f"/>
  {txt(664, 108, "7", 22, "#ffffff", 700)}
  {txt(664, 136, "Application", 14, "#ffffff")}
  {txt(664, 158, "use caset", 12, "#ffffff")}

  <rect x="804" y="70" width="132" height="120" rx="10" fill="#1d3557"/>
  {txt(820, 108, "8-9", 22, "#ffffff", 700)}
  {txt(820, 136, "Infra, API", 14, "#ffffff")}
  {txt(820, 158, "testit, Swagger", 12, "#a8dadc")}

  {txt(24, 230, "Älä hyppää Swaggeriin ennen vaihetta 6. Jos et tiedä mihin if kuuluu, palaa vaiheeseen 3.", 14)}
  {txt(24, 256, "Sama järjestys kuin ohjatussa LeagueHubissa — nyt ilman valmista koodia.", 13, "#457b9d")}
""",
    aria="DDD-järjestys: kieli ensin, koodi viimeksi",
)

# Sama sisältö kuin Assignment 1:ssä — paikallinen kopio, ei ristiinviittausta.
shutil.copyfile(A1 / "04-rule-placement.svg", OUT / "03-rule-placement.svg")
shutil.copyfile(A1 / "03-ca-layers.svg", OUT / "04-ca-layers.svg")
print("copied 03-rule-placement.svg and 04-ca-layers.svg from Assignment-1")
