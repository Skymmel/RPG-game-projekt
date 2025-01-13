namespace RPG_game;

public class Dungeon
{
    public Room CurrentRoom { get; set; }
    private List<Room> Rooms;

    public Dungeon()
    {
        Rooms = new List<Room>
        {
            new Room("Entrance Hall", "A dimly lit room with an eerie silence.", null!, new Enemy("Goblin", 10, 2)),
            new Room("Treasure Room", "A glittering room filled with gold.", "Potion", null!),
            new Room("Dark Cave", "A pitch-black cave with strange noises.", null!, new Enemy("Orc", 20, 5))
        };

        CurrentRoom = Rooms[0];
    }

    public Room GetNextRoom()
    {
        int currentIndex = Rooms.IndexOf(CurrentRoom);
        return Rooms[(currentIndex + 1) % Rooms.Count];
    }
}