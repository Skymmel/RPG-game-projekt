using Spectre.Console;
using static RPG_game.Core;

// ReSharper disable once CheckNamespace
namespace RPG_game;

public static class Prints
{
    public static void PromptValue<T>(string promptMessage, ref T targetVariable)
    {
        ChatToPlayer(promptMessage);
        Console.Write(" > ");
        string userInput = Console.ReadLine() ?? string.Empty;

        try
        {
            targetVariable = (T)Convert.ChangeType(userInput, typeof(T));
        }
        catch
        {
            ChatToPlayer("{RED}Neplatný vstup! Zkuste to znovu.");
            PromptValue(promptMessage, ref targetVariable);
        }
    }
    
    public static void ChatToPlayer(string msg)
    {
        ReplaceColorTagsAndPrint(msg);
    }
    
    public static void ClearConsole()
    {
        Console.Clear();
    }

    public static void WaitForKey(string message = "Press any key to continue...")
    {
        ChatToPlayer($"{{GREY}}{message}");
        Console.ReadKey();
    }

    public static string PromptSelection(string message, string ChoicesText, List<string> options)
    {
        ChatToPlayer(message);
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .PageSize(10)
                .MoreChoicesText(ChoicesText)
                .AddChoices(options));
    }

    private static void ReplaceColorTagsAndPrint(string input)
    {
        var colorMapping = new Dictionary<string, ConsoleColor>
        {
            { "{DEFAULT}", ConsoleColor.Gray },
            { "{DARKRED}", ConsoleColor.DarkRed },
            { "{LIGHTPURPLE}", ConsoleColor.Magenta },
            { "{GREEN}", ConsoleColor.Green },
            { "{OLIVE}", ConsoleColor.DarkYellow },
            { "{LIME}", ConsoleColor.Yellow },
            { "{RED}", ConsoleColor.Red },
            { "{GREY}", ConsoleColor.Gray },
            { "{YELLOW}", ConsoleColor.Yellow },
            { "{SILVER}", ConsoleColor.White },
            { "{BLUE}", ConsoleColor.Blue },
            { "{DARKBLUE}", ConsoleColor.DarkBlue },
            { "{ORANGE}", ConsoleColor.DarkYellow },
            { "{PURPLE}", ConsoleColor.DarkMagenta },
            { "{GRAY}", ConsoleColor.Gray }
        };

        bool isBold = false;

        // Zpracování barevných tagů
        foreach (var (tag, color) in colorMapping)
        {
            if (input.Contains(tag))
            {
                Console.ForegroundColor = color;
                input = input.Replace(tag, string.Empty); // Odstraň tag z textu
            }
        }

        // Zpracování {BOLD} tagu
        if (input.Contains("{BOLD}"))
        {
            isBold = true;
            input = input.Replace("{BOLD}", string.Empty); // Odstraň {BOLD} z textu
        }

        // Výstup textu
        if (isBold)
        {
            // Zachování emoji a tisk velkými písmeny
            var result = string.Concat(input.Select(c => char.IsLetter(c) ? char.ToUpper(c) : c));
            Console.WriteLine(result);
        }
        else
        {
            Console.WriteLine(input);
        }

        Console.ResetColor(); // Reset barvy na výchozí
    }
}