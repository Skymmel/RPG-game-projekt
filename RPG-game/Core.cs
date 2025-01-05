using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console;

namespace RPG_game;

public partial class Core
{
    // Instance hráče
    private static Core _player;

    static void Main(string[] args)
    {
        // Vytvoření instance hráče
        _player = new Core();

        // Spuštění hlavní logiky
        _player.CoreVoid();
        Prints.WaitForKey();
    }

    private void CoreVoid()
    {
        string localName = Name;
        Prints.PromptValue("What's your name?", ref localName);
        Name = localName;
        
        Prints.ClearConsole();
        Prints.ChatToPlayer($"{{YELLOW}} {Name}");

        Health = 100;
        
        while (Health > 0)
        {
            Prints.ClearConsole();
            PrintStats();

            switch (Prints.PromptSelection("Choose an action:", "[grey](Move up and down to reveal more options)[/]", Actions))
            {
                case "Run":
                    AddItem("Sword");
                    Prints.ChatToPlayer($"{{YELLOW}}You found a sword!");
                    Prints.WaitForKey(); // Nahrazuje Console.ReadKey()
                    break;
                case "Inventory":
                    if (Inventory.Count == 0)
                    {
                        Prints.ChatToPlayer($"{{RED}}You have nothing in your inventory.");
                    }
                    else
                    {
                        Prints.ChatToPlayer($"{{GREEN}}Your inventory:");
                        foreach (var item in Inventory)
                        {
                            Prints.ChatToPlayer($"{{YELLOW}}{item}");
                        }
                    }
                    Prints.WaitForKey();
                    break;
            }
        }

        Prints.ChatToPlayer($"{{GREY}}Game over!");
    }

    private static void PrintStats()
    {
        // Vypisuje jméno hráče
        Prints.ChatToPlayer($"{{BOLD}}{{YELLOW}}{_player.Name.ToUpper()}");

        // Vypisuje zdraví hráče jako srdce
        string healthIcons = string.Join(" ", new string[_player.Health].Select(_ => "\u2665"));
        Prints.ChatToPlayer($"{{RED}}{healthIcons}");

        // Vypisuje útok hráče jako blesky
        string attackIcons = string.Join(" ", new string[_player.Attack].Select(_ => "\u26a1"));
        Prints.ChatToPlayer($"{{YELLOW}}{attackIcons}");
    }


    public void AddItem(string item)
    {
        Inventory.Add(item);
    }
    
    public void GetDefense(byte times = 1)
    {
        for (byte i = 0; i < times; i++)
        {
            Defense++;
        }
    }

    public void GetAttack(byte times = 1)
    {
        for (byte i = 0; i < times; i++)
        {
            Attack++;
        }
    }

    public void GetHealth(byte times = 1)
    {
        for (byte i = 0; i < times; i++)
        {
            Health++;
        }
    }
}
