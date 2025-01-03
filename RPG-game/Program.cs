using Spectre.Console;
namespace RPG_game;
class Program {
    private static Character _player = new("player");
    static void Main(string[] args) {
        Console.Clear();
        _player = new Character(AnsiConsole.Prompt(new TextPrompt<string>("What's your name?")));
        PrintStats();
    }
    private static void PrintStats() {
        AnsiConsole.Markup($"[bold]{_player.Name.ToUpper()}[/]  ");
        AnsiConsole.Markup($"[red]{string.Join(" ", new string[_player.Health].Select(_ => "\u2665"))}[/]  ");
        AnsiConsole.MarkupLine($"[yellow]{string.Join(" ", new string[_player.Attack].Select(_ => "\u26a1"))}[/]");
    }
}