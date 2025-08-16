# Hash Demo - Med och utan Salt

En .NET C# konsolapplikation som demonstrerar skillnaden mellan hashning med och utan salt. Applikationen ger användaren en interaktiv meny för att testa båda metoderna.

## Funktioner

- **Hashning utan salt**: Visar hur samma input alltid ger samma hash
- **Hashning med salt**: Visar hur salt gör att samma input ger olika hash-värden
- **Prestandatest**: Jämför prestanda mellan hashning med och utan salt över tusentals iterationer
- **Färgkodad konsol**: Tydlig visuell separation mellan labels, värden och felmeddelanden
- **SHA-256 algoritm**: Använder säker hash-algoritm
- **Säker saltgenerering**: Använder RandomNumberGenerator för kryptografiskt säkra salt-värden

## Krav

- .NET 9.0 eller senare
- Windows, macOS eller Linux

## Installation

1. Klona eller ladda ner projektet
2. Navigera till projektmappen
3. Kör följande kommando:

```bash
dotnet restore
```

## Användning

### Steg-för-steg instruktioner

1. **Öppna en terminal/kommandotolk**
   - Windows: PowerShell, Command Prompt eller Windows Terminal
   - macOS: Terminal
   - Linux: Terminal

2. **Navigera till projektmappen**
   ```bash
   cd sökväg/till/HashDemo
   ```

3. **Kör applikationen**
   ```bash
   dotnet run
   ```

### Alternativa sätt att köra programmet

#### Metod 1: Direkt körning (rekommenderat)
```bash
dotnet run
```

#### Metod 2: Bygga först, sedan köra
```bash
dotnet build
dotnet run
```

#### Metod 3: Köra kompilerad exe-fil (Windows)
```bash
dotnet build
.\bin\Debug\net9.0\HashDemo.exe
```

#### Metod 4: Köra via dotnet med dll-fil
```bash
dotnet build
dotnet .\bin\Debug\net9.0\HashDemo.dll
```

### Menyalternativ

1. **Hasha text UTAN salt**
   - Mata in text som ska hashas
   - Samma input ger alltid samma hash-värde
   - Lämpligt för att verifiera integritet

2. **Hasha text MED salt**
   - Mata in text som ska hashas
   - Genererar automatiskt ett unikt salt
   - Samma input ger olika hash-värden varje gång
   - Lämpligt för lösenordslagring

3. **Prestandatest - Jämför metoder**
   - Jämför prestanda mellan hashning med och utan salt
   - Kör samma test över tusentals iterationer
   - Visar tidsåtgång och procentuell skillnad
   - Demonstrerar kostnad för ökad säkerhet

4. **Avsluta**
   - Avslutar applikationen

## Exempel på användning

```
=================================
| Hash Demo - Med och utan Salt |
=================================

Välj ett alternativ:
  1. Hasha text UTAN salt
  2. Hasha text MED salt
  3. Prestandatest - Jämför metoder
  4. Avsluta

Ditt val: 1

Ange text att hasha: lösenord123

Hash (utan salt): 8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92
```

### Exempel på prestandatest

```
Ditt val: 3

Ange text att testa: lösenord123
Antal iterationer (rekommenderat: 10000): 10000

Startar prestandatest...

Resultat:
Utan salt (10 000 iterationer): 125.43 ms
Med salt (10 000 iterationer): 178.92 ms
Skillnad: 53.49 ms (42.6% långsammare med salt)
Genomsnitt per hash utan salt: 0.0125 ms
Genomsnitt per hash med salt: 0.0179 ms

💡 Salt gör hashning långsammare men betydligt säkrare!
```

## Varför använda salt?

- **Utan salt**: Samma lösenord = samma hash. Sårbart för rainbow table-attacker
- **Med salt**: Samma lösenord = olika hash varje gång. Mycket säkrare för lösenordslagring

## Prestandaanalys

Prestandatestet visar den verkliga kostnaden för ökad säkerhet:

### Vad testet mäter
- **Utan salt**: Tid för att hasha samma text upprepade gånger
- **Med salt**: Tid för att generera nytt salt + hasha text för varje iteration
- **Skillnad**: Procentuell ökning i bearbetningstid

### Typiska resultat
- Salt ökar bearbetningstiden med **30-50%**
- På moderna datorer: **~0.01-0.02 ms per hash**
- För 10 000 hashningar: **~50-200 ms extra**

### Praktisk betydelse
- **Acceptabel kostnad** för lösenordslagring
- **Obetydlig påverkan** för vanliga applikationer
- **Dramatisk säkerhetsförbättring** väger upp prestandan

### Rekommendationer
- **Använd alltid salt** för lösenordslagring
- **Kör prestandatest** för att förstå påverkan i din miljö
- **Överväg bcrypt/scrypt** för ännu säkrare lösenordslagring

## Teknisk information

- **Hash-algoritm**: SHA-256
- **Saltlängd**: 16 bytes (Base64-kodad)
- **Randomgenerator**: RandomNumberGenerator (kryptografiskt säker)
- **Kodning**: UTF-8 för textinput
- **Prestandamätning**: Stopwatch-klass för högprecisionsmätningar
- **Standarditerationer**: 10 000 (anpassningsbar)

## Färgkodning

- 🔵 **Cyan**: Huvudrubrik
- 🟡 **Gul**: Labels och menytitlar
- 🟢 **Grön**: Input-prompter
- 🟣 **Magenta**: Resultat-labels
- ⚪ **Vit**: Resultat-värden
- 🔴 **Röd**: Felmeddelanden

## Felsökning

### Vanliga problem och lösningar

**Problem**: `dotnet: kommandot hittades inte`
- **Lösning**: Installera .NET SDK från https://dotnet.microsoft.com/download

**Problem**: `Projektet kan inte byggas`
- **Lösning**: Kontrollera att du är i rätt mapp och kör `dotnet restore`

**Problem**: `Inga färger visas i konsolen`
- **Lösning**: Använd en modern terminal som stöder ANSI-färger (Windows Terminal rekommenderas)

**Problem**: `Access denied` eller behörighetsproblem
- **Lösning**: Kör terminalen som administratör (endast Windows)

### Systemkrav för körning

- **.NET Runtime**: 9.0 eller senare
- **Operativsystem**: Windows 10+, macOS 10.15+, eller Linux
- **Terminal**: Stöd för ANSI-färger (rekommenderat)

## Kompilera projektet

```bash
dotnet build
```

## Körning i release-läge

```bash
dotnet run --configuration Release
```

## Utvecklare

Skapad som en demonstration av hashning med och utan salt i .NET C#.
