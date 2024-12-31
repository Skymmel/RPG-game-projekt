using Spectre.Console;
namespace RPG_game;
class Program
{
    private static Character _player = new("player");
    static void Main(string[] args)
    {
        Console.Clear();
        _player = new Character("Skymmel");
        PrintStats();
    }
    private static void PrintStats()
    {
        AnsiConsole.Markup($"[bold]{_player.Name.ToUpper()}[/]  ");
        AnsiConsole.Markup($"[red]{String.Join(" ", new string[_player.Health].Select(_ => "\u2665"))}[/]");
        AnsiConsole.Markup($"  [yellow]{String.Join(" ", new string[_player.Attack].Select(_ => "\u26a1"))}[/]");
    }
}