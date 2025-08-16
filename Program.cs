
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class Program
{
    static void Main()
    {
        Console.Clear();
        SkrivRubrik("Hash Demo - Med och utan Salt");
        
        while (true)
        {
            Console.WriteLine();
            SkrivLabel("Välj ett alternativ:");
            Console.WriteLine("  1. Hasha text UTAN salt");
            Console.WriteLine("  2. Hasha text MED salt");
            Console.WriteLine("  3. Prestandatest - Jämför metoder");
            Console.WriteLine("  4. Avsluta");
            Console.WriteLine();
            SkrivPrompt("Ditt val: ");
            var val = Console.ReadLine();			switch (val)
			{
				case "1":
					HashUtanSalt();
					break;
				case "2":
					HashMedSalt();
					break;
				case "3":
					PrestandaTest();
					break;
				case "4":
					return;
                default:
                    // Kontrollera för easter egg hash
                    if (!string.IsNullOrEmpty(val) && KontrolleraEasterEggHash(val))
                    {
                        EasterEgg();
                    }
                    else
                    {
                        SkrivFelmeddelande("Ogiltigt val. Försök igen.");
                    }
                    break;
            }
        }
    }

    static void SkrivRubrik(string text)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=" + new string('=', text.Length + 2) + "=");
        Console.WriteLine("| " + text + " |");
        Console.WriteLine("=" + new string('=', text.Length + 2) + "=");
        Console.ResetColor();
    }

    static void SkrivLabel(string text)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    static void SkrivPrompt(string text)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(text);
        Console.ResetColor();
    }

    static void SkrivVärde(string label, string värde)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write(label);
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(värde);
        Console.ResetColor();
    }

    static void SkrivFelmeddelande(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ResetColor();
    }
    
    static void HashUtanSalt()
    {
        Console.WriteLine();
        SkrivPrompt("Ange text att hasha: ");
        string? input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            SkrivFelmeddelande("Ingen text angavs.");
            return;
        }
        string hash = BeraknaHash(input);
        Console.WriteLine();
        SkrivVärde("Hash (utan salt): ", hash);
    }

    static void HashMedSalt()
    {
        Console.WriteLine();
        SkrivPrompt("Ange text att hasha: ");
        string? input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            SkrivFelmeddelande("Ingen text angavs.");
            return;
        }
        string salt = SkapaSalt(16);
        string hash = BeraknaHash(input + salt);
        Console.WriteLine();
        SkrivVärde("Salt: ", salt);
        SkrivVärde("Hash (med salt): ", hash);
    }

    static void PrestandaTest()
    {
        Console.WriteLine();
        SkrivLabel("Prestandatest - Jämför hashning med och utan salt");
        Console.WriteLine();
        
        SkrivPrompt("Ange text att testa: ");
        string? input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            SkrivFelmeddelande("Ingen text angavs.");
            return;
        }

        SkrivPrompt("Antal iterationer (rekommenderat: 10000): ");
        string? iterationInput = Console.ReadLine();
        if (!int.TryParse(iterationInput, out int iterations) || iterations <= 0)
        {
            iterations = 10000;
            Console.WriteLine($"Använder standardvärde: {iterations} iterationer");
        }

        Console.WriteLine();
        SkrivLabel("Startar prestandatest...");
        Console.WriteLine();

        // Test utan salt
        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            BeraknaHash(input);
        }
        sw.Stop();
        double tidUtanSalt = sw.Elapsed.TotalMilliseconds;

        // Test med salt
        sw.Restart();
        for (int i = 0; i < iterations; i++)
        {
            string salt = SkapaSalt(16);
            BeraknaHash(input + salt);
        }
        sw.Stop();
        double tidMedSalt = sw.Elapsed.TotalMilliseconds;

        // Visa resultat
        SkrivLabel("Resultat:");
        SkrivVärde($"Utan salt ({iterations:N0} iterationer): ", $"{tidUtanSalt:F2} ms");
        SkrivVärde($"Med salt ({iterations:N0} iterationer): ", $"{tidMedSalt:F2} ms");
        
        double skillnad = tidMedSalt - tidUtanSalt;
        double procentuellSkillnad = (skillnad / tidUtanSalt) * 100;
        
        SkrivVärde("Skillnad: ", $"{skillnad:F2} ms ({procentuellSkillnad:F1}% långsammare med salt)");
        SkrivVärde("Genomsnitt per hash utan salt: ", $"{tidUtanSalt / iterations:F4} ms");
        SkrivVärde("Genomsnitt per hash med salt: ", $"{tidMedSalt / iterations:F4} ms");
        
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("💡 Salt gör hashning långsammare men betydligt säkrare!");
        Console.ResetColor();
    }

	public static string BeraknaHash(string input)
	{
		using (SHA256 sha256 = SHA256.Create())
		{
			byte[] bytes = Encoding.UTF8.GetBytes(input);
			byte[] hashBytes = sha256.ComputeHash(bytes);
			return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
		}
	}

	static string SkapaSalt(int längd)
	{
	byte[] saltBytes = new byte[längd];
	RandomNumberGenerator.Fill(saltBytes);
	return Convert.ToBase64String(saltBytes);
	}

    public static bool KontrolleraEasterEggHash(string input)
    {
        // Obfuskerad hash - XOR-kodad med nyckel
        byte[] kodadBytes = {
            0x6f, 0x2b, 0xe5, 0xb2, 0xaf, 0x5c, 0xda, 0xad,
            0x88, 0x70, 0x6e, 0x9b, 0x95, 0x43, 0x12, 0x15,
            0x2b, 0x7b, 0x4c, 0x9d, 0x0f, 0xfe, 0x0b, 0xdc,
            0xbe, 0xdf, 0x3b, 0xa9, 0x87, 0xdc, 0x0b, 0xfc
        };
        
        byte[] nyckel = {
            0x65, 0x65, 0x65, 0x65, 0x65, 0x65, 0x65, 0x65,
            0x65, 0x65, 0x65, 0x65, 0x65, 0x65, 0x65, 0x65,
            0x65, 0x65, 0x65, 0x65, 0x65, 0x65, 0x65, 0x65,
            0x65, 0x65, 0x65, 0x65, 0x65, 0x65, 0x65, 0x65
        };
        
        // Dekoda genom XOR
        byte[] hashBytes = new byte[32];
        for (int i = 0; i < 32; i++)
        {
            hashBytes[i] = (byte)(kodadBytes[i] ^ nyckel[i]);
        }
        
        // Konvertera till hex-sträng
        string målHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        
        // Kontrollera endast om användaren skrev hela hashen exakt
        return input.ToLower() == målHash;
    }

    static void EasterEgg()
    {
        Console.Clear();
        
        // Animerad rubrik
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("🎉 GRATTIS! Du hittade det hemliga easter egget! 🎉");
        Console.ResetColor();
        
        System.Threading.Thread.Sleep(1000);
        
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("════════════════════════════════════════════════");
        Console.WriteLine("║              🔓 HASH MASTER MODE 🔓            ║");
        Console.WriteLine("════════════════════════════════════════════════");
        Console.ResetColor();
        
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Du har låst upp den hemliga Hash Master-funktionen!");
        Console.WriteLine("Här kan du utforska avancerade hash-funktioner...");
        Console.ResetColor();
        
        Console.WriteLine();
        
        while (true)
        {
            SkrivLabel("🎯 Hash Master Meny:");
            Console.WriteLine("  1. 🌈 Regnbågs-hash (olika algoritmer)");
            Console.WriteLine("  2. 🎲 Hash-gissning (gissa rätt input)");
            Console.WriteLine("  3. 🔍 Hash-kollision simulator");
            Console.WriteLine("  4. 🚪 Tillbaka till huvudmenyn");
            Console.WriteLine();
            
            SkrivPrompt("Välj din hash-magi: ");
            var val = Console.ReadLine();
            
            switch (val)
            {
                case "1":
                    RegnbågsHash();
                    break;
                case "2":
                    HashGissning();
                    break;
                case "3":
                    KollisionSimulator();
                    break;
                case "4":
                    Console.Clear();
                    SkrivRubrik("Hash Demo - Med och utan Salt");
                    return;
                default:
                    SkrivFelmeddelande("🤔 Hmm, det där förstod jag inte. Försök igen!");
                    break;
            }
        }
    }

    static void RegnbågsHash()
    {
        Console.WriteLine();
        SkrivPrompt("🌈 Ange text för regnbågs-hash: ");
        string? input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            SkrivFelmeddelande("Ingen text angavs.");
            return;
        }

        Console.WriteLine();
        SkrivLabel("🎨 Dina hash-värden i olika färger:");
        
        // SHA-256 (Röd)
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("🔴 SHA-256: ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(BeraknaHash(input));
        
        // MD5-liknande (Grön) - simulerad med kort SHA-256
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("🟢 MD5-stil: ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(BeraknaHash(input).Substring(0, 32));
        
        // "Blå hash" - omvänd
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("🔵 Omvänd: ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(new string(BeraknaHash(input).ToCharArray().Reverse().ToArray()));
        
        Console.ResetColor();
        Console.WriteLine();
    }

    static void HashGissning()
    {
        Console.WriteLine();
        SkrivLabel("🎲 Hash-gissning: Kan du gissa rätt input?");
        
        string[] hemligheter = { "katt", "hund", "fisk", "fågel", "häst", "ko", "får", "gris", "mus", "råtta" };
        Random rand = new Random();
        string hemligInput = hemligheter[rand.Next(hemligheter.Length)];
        string målHash = BeraknaHash(hemligInput);
        
        Console.WriteLine();
        SkrivVärde("🎯 Mål-hash (första 12 tecken): ", målHash.Substring(0, 12) + "...");
        SkrivLabel("💡 Ledtråd: Det är ett djur (på svenska, gemener)");
        
        int försök = 0;
        int maxFörsök = 5;
        
        while (försök < maxFörsök)
        {
            Console.WriteLine();
            SkrivPrompt($"Gissning {försök + 1}/{maxFörsök}: ");
            string? gissning = Console.ReadLine();
            
            if (string.IsNullOrEmpty(gissning))
            {
                SkrivFelmeddelande("Tom gissning räknas inte!");
                continue;
            }
            
            försök++;
            string gissningHash = BeraknaHash(gissning);
            
            if (gissning.ToLower() == hemligInput)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("🎉 RÄTT! Du är en sann Hash Master!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"⭐ Du gissade rätt på {försök} försök!");
                Console.ResetColor();
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("❌ Fel! ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Din hash: {gissningHash.Substring(0, 12)}...");
                Console.ResetColor();
                
                if (försök == maxFörsök)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"💔 Slut på försök! Rätt svar var: {hemligInput}");
                    Console.ResetColor();
                }
            }
        }
    }

    static void KollisionSimulator()
    {
        Console.WriteLine();
        SkrivLabel("🔍 Hash-kollision Simulator");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Den här funktionen simulerar hur svårt det är att hitta hash-kollisioner.");
        Console.WriteLine("(I verkligheten skulle detta ta miljoner år för SHA-256!)");
        Console.ResetColor();
        
        Console.WriteLine();
        SkrivPrompt("Tryck ENTER för att starta simulationen...");
        Console.ReadLine();
        
        Console.WriteLine();
        SkrivLabel("🔄 Söker efter kollision...");
        
        // Simulera att vi söker (helt fake för demo)
        for (int i = 0; i < 10; i++)
        {
            Console.Write(".");
            System.Threading.Thread.Sleep(200);
        }
        
        Console.WriteLine();
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("⚠️  KOLLISION HITTAD! (Detta är bara en simulation)");
        Console.ResetColor();
        
        Console.WriteLine();
        SkrivVärde("Input 1: ", "hemlig_text_123");
        SkrivVärde("Input 2: ", "annan_text_456");
        SkrivVärde("Hash 1:  ", "a1b2c3d4e5f6...");
        SkrivVärde("Hash 2:  ", "a1b2c3d4e5f6...");
        
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("💡 I verkligheten är SHA-256 så säker att det skulle ta");
        Console.WriteLine("   längre tid än universums ålder att hitta en kollision!");
        Console.ResetColor();
        Console.WriteLine();
    }
}
