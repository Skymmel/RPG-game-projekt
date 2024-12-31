using Spectre.Console;

namespace RPG_game;

public class Game
{
    public static string? Name;
    public static int HP = 3;
    public static void Run()
    {
        Console.Clear();
        
        Name = AnsiConsole.Prompt(
            new TextPrompt<string>("What's your name?"));
        PrintStats();
    }
    private static void PrintStats()
    {
        AnsiConsole.Markup($"[bold]{Name?.ToUpper()}[/]  ");
        AnsiConsole.Markup($"[red]{String.Join(" ", new string[HP].Select(_ => "\u2665"))}[/]");
        
    }
}