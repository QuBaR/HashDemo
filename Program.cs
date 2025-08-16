
using System;
using System.Security.Cryptography;
using System.Text;

class Program
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
            Console.WriteLine("  3. Avsluta");
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
					return;
                default:
                    SkrivFelmeddelande("Ogiltigt val. Försök igen.");
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

	static string BeraknaHash(string input)
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
}
