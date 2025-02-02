namespace RPG_game
{
    public partial class Core
    {
        public string Name { get; set; } = null!;
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Level { get; set; }
        public List<string> Inventory { get; set; } = new List<string>();
        public List<string> Actions { get; set; } = new List<string>
        {
            "Inventory",
            "Explore", // Prozkoumání místnosti
            "Move"    // Přesun do další místnosti
        };
    }
}