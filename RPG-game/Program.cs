// zadání - https://docs.google.com/presentation/d/188_5kibWX87MPohwD6y9G_sRTZcF_vcP/edit?usp=sharing&ouid=100446944888404563685&rtpof=true&sd=true
using System.Net;
using Spectre.Console;
namespace RPG_game;
class Program {
    private static Player _player = new("player");
    static void Main(string[] args) {
        Console.Clear();
        _player = new Player(AnsiConsole.Prompt(new TextPrompt<string>("What's your name?")));
        
        while (_player.Health > 0)
        {
            Console.Clear();
            PrintStats();
            switch (AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                            .AddChoices(_player.Actions)))
            {
                case "Run":
                    AddItem("Sword");
                    AnsiConsole.MarkupLine($"[yellow]get a sword[/]");
                    Console.ReadKey();
                    break;
                case "Inventory":
                    if (_player.Inventory.Count == 0)
                    {
                        AnsiConsole.MarkupLine($"You have nothing in your inventory");
                    }
                    else
                    {
                        foreach (var item in _player.Inventory)
                        {
                            AnsiConsole.Markup($"{item}");
                        }
                    }
                    Console.ReadKey();
                    break;
            }
        }
    }
    private static void PrintStats() {
        AnsiConsole.Markup($"[bold]{_player.Name.ToUpper()}[/]  ");
        AnsiConsole.Markup($"[red]{string.Join(" ", new string[_player.Health].Select(_ => "\u2665"))}[/]  ");
        AnsiConsole.MarkupLine($"[yellow]{string.Join(" ", new string[_player.Attack].Select(_ => "\u26a1"))}[/]");
    }

    public static void AddItem(string item)
    {
        _player.Inventory.Add(item);
    }
    
    public static void GetDefense(byte times = 1)
    {
        for (byte i = 0; i < times; i++)
        {
            _player.Defense++;
        }
    }
    public static void GetAttact(byte times = 1)
    {
        for (byte i = 0; i < times; i++)
        {
            _player.Attack++;
        }
    }
    public static void GetHealth(byte times = 1)
    {
        for (byte i = 0; i < times; i++)
        {
            _player.Health++;
        }
    }
}