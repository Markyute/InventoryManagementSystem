namespace ShoeInventory.Services
{
  
    public static class ConsoleHelper
    {
        private const int Width = 68;

       
        public static void PrintHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("╔" + new string('═', Width) + "╗");
            Console.WriteLine("║" + CenterText(title, Width) + "║");
            Console.WriteLine("╚" + new string('═', Width) + "╝");
            Console.ResetColor();
        }

        public static void PrintDivider()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  " + new string('─', Width - 2));
            Console.ResetColor();
        }

        public static void PrintSubHeader(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  ── {text} ──");
            Console.ResetColor();
        }

     
        public static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ✔  {message}");
            Console.ResetColor();
        }

        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  ✘  ERROR: {message}");
            Console.ResetColor();
        }

        public static void PrintWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  ⚠  {message}");
            Console.ResetColor();
        }

        public static void PrintInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"     {message}");
            Console.ResetColor();
        }

        public static void PrintLabel(string label, string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"     {label,-18}: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(value);
            Console.ResetColor();
        }

   
        public static string PromptString(string label, bool allowEmpty = false)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"  > {label}: ");
                Console.ResetColor();
                string? input = Console.ReadLine()?.Trim();
                if (!allowEmpty && string.IsNullOrWhiteSpace(input))
                {
                    PrintError("Input cannot be empty. Please try again.");
                    continue;
                }
                return input ?? string.Empty;
            }
        }

        public static int PromptInt(string label, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"  > {label}: ");
                Console.ResetColor();
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int result) && result >= min && result <= max)
                    return result;
                PrintError($"Please enter a valid integer between {min} and {max}.");
            }
        }

        public static decimal PromptDecimal(string label, decimal min = 0)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"  > {label}: ");
                Console.ResetColor();
                string? input = Console.ReadLine();
                if (decimal.TryParse(input, out decimal result) && result >= min)
                    return result;
                PrintError($"Please enter a valid decimal number (min: {min}).");
            }
        }

        public static bool Confirm(string question)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\n  ? {question} (y/n): ");
            Console.ResetColor();
            string? input = Console.ReadLine()?.Trim().ToLower();
            return input == "y" || input == "yes";
        }

        public static void PressAnyKey()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\n  Press any key to continue...");
            Console.ResetColor();
            Console.ReadKey(true);
        }

     
        private static string CenterText(string text, int width)
        {
            if (text.Length >= width) return text;
            int totalPadding = width - text.Length;
            int leftPad = totalPadding / 2;
            int rightPad = totalPadding - leftPad;
            return new string(' ', leftPad) + text + new string(' ', rightPad);
        }

        public static void ClearAndHeader(string title)
        {
            Console.Clear();
            PrintHeader(title);
        }
    }
}
