namespace RPG_game;
public class Character(string name, int health = 3, int attack = 0, int defense = 0, List<int>? inventory = null) {
    public string Name { get; set; } = name;
    public int Health { get; set; } = health;
    public int Attack { get; set; } = attack;
    public int Defense { get; set; } = defense;
    public List<int>? Inventory { get; set; } = inventory;
}