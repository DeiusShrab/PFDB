namespace DBConnect.DBModels
{
  public partial class BestiaryMagic
  {
    public int BestiaryMagicId { get; set; }
    public int BestiaryId { get; set; }
    public int SpellId { get; set; }
    public string Notes { get; set; }
    public int CasterLevel { get; set; }
    public int UsesPerDay { get; set; }
    public int MagicTypeId { get; set; }
  }

  public enum MagicType
  {
    PreparedSpell = 1,
    SpontaneousSpellKnown = 2,
    SpellLikeAbility = 3
  }
}
