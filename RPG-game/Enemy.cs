namespace RPG_game;

public class Enemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }

    public Enemy(string name, int health, int attack)
    {
        Name = name;
        Health = health;
        Attack = attack;
    }
}