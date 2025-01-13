namespace RPG_game;

public class Room
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Item { get; set; }
    public Enemy Enemy { get; set; }

    public Room(string name, string description, string item, Enemy enemy)
    {
        Name = name;
        Description = description;
        Item = item;
        Enemy = enemy;
    }
}