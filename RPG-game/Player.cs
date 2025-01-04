namespace RPG_game;
public class Player(string name, int health = 3, int attack = 0, int defense = 0, int level = 0) {
    public string Name { get; set; } = name;
    public int Health { get; set; } = health;
    public int Attack { get; set; } = attack;
    public int Defense { get; set; } = defense;
    public int Level { get; set; } = level;
    public List<string> Inventory { get; set; } = new List<string>();
    public List<string> Actions { get; set; } = new List<string>{"Run", "Inventory"};

}