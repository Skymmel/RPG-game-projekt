namespace RPG_game;
public class Character
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public Character(string name, int health = 3, int attack = 0, int defense = 0)
    {
        Name = name;
        Health = health;
        Attack = attack;
        Defense = defense;
    }
}