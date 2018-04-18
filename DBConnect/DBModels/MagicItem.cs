namespace DBConnect.DBModels
{
  public partial class MagicItem
    {
        public int MagicItemId { get; set; }
        public string Name { get; set; }
        public string Aura { get; set; }
        public int? Cl { get; set; }
        public string Slot { get; set; }
        public string Price { get; set; }
        public string Weight { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Cost { get; set; }
        public string Group { get; set; }
        public string Source { get; set; }
        public string Al { get; set; }
        public int? Int { get; set; }
        public int? Wis { get; set; }
        public int? Cha { get; set; }
        public int? Ego { get; set; }
        public string Communication { get; set; }
        public string Senses { get; set; }
        public string Powers { get; set; }
        public string MagicItems { get; set; }
        public string FullText { get; set; }
        public string Destruction { get; set; }
        public bool? MinorArtifact { get; set; }
        public bool? MajorArtifact { get; set; }
        public bool? Abjuration { get; set; }
        public bool? Conjuration { get; set; }
        public bool? Divination { get; set; }
        public bool? Enchantment { get; set; }
        public bool? Evocation { get; set; }
        public bool? Necromancy { get; set; }
        public bool? Transmutation { get; set; }
        public string AuraStrength { get; set; }
        public double? WeightValue { get; set; }
        public int? PriceValue { get; set; }
        public int? CostValue { get; set; }
        public string Languages { get; set; }
        public string BaseItem { get; set; }
        public string LinkText { get; set; }
        public bool? Mythic { get; set; }
        public bool? LegendaryWeapon { get; set; }
        public bool? Illusion { get; set; }
        public bool? Universal { get; set; }
        public string Scaling { get; set; }
    }
}
