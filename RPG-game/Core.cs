using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Spectre.Console;

namespace RPG_game;

public partial class Core
{
        // Instance hráče
    private static Core _player = null!;

    // Instance dungeon mapy
    private static Dungeon _dungeon = null!;

    static void Main(string[] args)
    {
        // Vytvoření instance hráče
        _player = new Core();

        // Vytvoření dungeon mapy
        _dungeon = new Dungeon();

        // Spuštění hlavní logiky
        _player.CoreVoid();
        Prints.WaitForKey();
    }

    private void CoreVoid()
    {
        string localName = Name;
        Prints.PromptValue("What's your name?", ref localName);
        Name = localName;

        if (Name == null)
        {
            Prints.ChatToPlayer($"{{RED}} Name cannot be null.");
            Prints.WaitForKey();
        }

        Prints.ClearConsole();
        Prints.ChatToPlayer($"{{YELLOW}} Welcome, {Name}! Your adventure begins.");

        Health = 50;

        while (Health > 0)
        {
            Prints.ClearConsole();
            PrintStats();

            // Zobrazení aktuální místnosti
            Prints.ChatToPlayer($"{{GREEN}}You are in {_dungeon.CurrentRoom.Name}.");
            Prints.ChatToPlayer(_dungeon.CurrentRoom.Description);

            // Volba akce
            switch (Prints.PromptSelection("Choose an action:", "[grey](Move up and down to reveal more options)[/]", Actions))
            {
                case "Explore":
                    ExploreRoom();
                    break;
                case "Move":
                    MoveToNextRoom();
                    break;
                case "Inventory":
                    ShowInventory();
                    break;
            }
        }

        Prints.ChatToPlayer($"{{RED}}You have died! Game over!");
    }

    private void ExploreRoom()
    {
        var enemy = _dungeon.CurrentRoom.Enemy;

        // Kontrola, zda je v místnosti nepřítel
        if (enemy != null && enemy.Health > 0)
        {
            Prints.ChatToPlayer($"{{RED}}A {enemy.Name} appears! Prepare to fight.");

            while (enemy.Health > 0 && Health > 0)
            {
                var action = Prints.PromptSelection("What do you want to do?", "[grey](Choose your action)[/]", new List<string> { "Attack", "Run" });

                if (action == "Attack")
                {
                    FightEnemy(enemy);
                }
                else if (action == "Run")
                {
                    Prints.ChatToPlayer($"{{YELLOW}}You decided to flee!");
                    return;
                }

                if (enemy.Health <= 0)
                {
                    Prints.ChatToPlayer($"{{GREEN}}You defeated the {enemy.Name}!");
                    _dungeon.CurrentRoom.Enemy = null!;
                }

                if (Health <= 0)
                {
                    Prints.ChatToPlayer($"{{RED}}You have been defeated!");
                    return;
                }
            }
        }
        else
        {
            Prints.ChatToPlayer($"{{YELLOW}}You found {_dungeon.CurrentRoom.Item ?? "nothing"}.");
            if (_dungeon.CurrentRoom.Item != null)
            {
                AddItem(_dungeon.CurrentRoom.Item);
                Prints.ChatToPlayer($"{{GREEN}}You added {_dungeon.CurrentRoom.Item} to your inventory.");
                _dungeon.CurrentRoom.Item = null!;
            }
        }
    }
    
    public void AddItem(string item)
    {
        Inventory.Add(item);
    }

    private void MoveToNextRoom()
    {
        Prints.ChatToPlayer("Where do you want to go?");
        var nextRoom = _dungeon.GetNextRoom();
        _dungeon.CurrentRoom = nextRoom;
        Prints.ChatToPlayer($"{{GREEN}}You moved to {nextRoom.Name}.");
    }

    private void ShowInventory()
    {
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
    }

    private void FightEnemy(Enemy enemy)
    {
        Random rand = new Random();

        while (Health > 0 && enemy.Health > 0)
        {
            // Nepřítel zaútočí s náhodnou silou v rozmezí
            int enemyAttack = rand.Next(enemy.Attack - 1, enemy.Attack + 2);
            Prints.ChatToPlayer($"{{RED}}{enemy.Name} attacks you!");
            Health -= enemyAttack;
            Prints.ChatToPlayer($"{{RED}}You took {enemyAttack} damage. Health: {Health}.");

            // Hráč zaútočí s náhodnou silou v rozmezí
            int playerAttack = rand.Next(Attack - 2, Attack + 3);
            Prints.ChatToPlayer("You strike back!");
            enemy.Health -= playerAttack;
            Prints.ChatToPlayer($"{{YELLOW}}You dealt {playerAttack} damage. {enemy.Name} health: {enemy.Health}.");

            // Kontrola, zda byl nepřítel poražen
            if (enemy.Health <= 0)
            {
                Prints.ChatToPlayer($"{{GREEN}}You defeated the {enemy.Name}!");
                break;
            }

            // Kontrola, zda byl hráč poražen
            if (Health <= 0)
            {
                Prints.ChatToPlayer($"{{RED}}You have been defeated!");
                break;
            }

            // Náhodná šance, že nepřítel uprchne
            if (rand.Next(0, 100) < 10) // 10% šance na útěk
            {
                Prints.ChatToPlayer($"{{YELLOW}}The {enemy.Name} fled!");
                enemy.Health = 0; // Nepřítel uprchne a je považován za poraženého
                break;
            }
        }
    }

    private static void PrintStats()
    {
        Prints.ChatToPlayer($"{{BOLD}}{{YELLOW}}{_player.Name.ToUpper()}");

        string healthIcons = $"{_player.Health} HP";
        Prints.ChatToPlayer($"{{RED}}{healthIcons}");

        string attackIcons = $"{_player.Attack} AP";
        Prints.ChatToPlayer($"{{YELLOW}}{attackIcons}");
    }
}
