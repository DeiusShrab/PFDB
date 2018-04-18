namespace DBConnect.DBModels
{
  public partial class Terrain
    {
        public int TerrainId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MovementModifier { get; set; }
        public bool IsWater { get; set; }
        public bool IsUnderground { get; set; }
        public bool IsRough { get; set; } // Rough terrain cannot be traversed by land vehicles, and typically imposes a movement penalty
        public bool IsBroken { get; set; } // Broken terrain can only be traversed on foot, and usually with a movement penalty
    }
}
