using System.Collections.Generic;
using PFDBCommon.DBModels;

namespace PFDBCommon.ConnectModels
{
  public class SpellLookupRequest
  {
    public bool Sor { get; set; }
    public bool Wiz { get; set; }
    public bool Cleric { get; set; }
    public bool Druid { get; set; }
    public bool Ranger { get; set; }
    public bool Bard { get; set; }
    public bool Paladin { get; set; }
    public bool Alchemist { get; set; }
    public bool Summoner { get; set; }
    public bool Witch { get; set; }
    public bool Inquisitor { get; set; }
    public bool Oracle { get; set; }
    public bool Antipaladin { get; set; }
    public bool Magus { get; set; }
    public bool Adept { get; set; }
    public bool BloodRager { get; set; }
    public bool Shaman { get; set; }
    public bool Psychic { get; set; }
    public bool Medium { get; set; }
    public bool Mesmerist { get; set; }
    public bool Occultist { get; set; }
    public bool Spiritualist { get; set; }
    public bool Skald { get; set; }
    public bool Investigator { get; set; }
    public bool Hunter { get; set; }
    public bool Lv0 { get; set; }
    public bool Lv1 { get; set; }
    public bool Lv2 { get; set; }
    public bool Lv3 { get; set; }
    public bool Lv4 { get; set; }
    public bool Lv5 { get; set; }
    public bool Lv6 { get; set; }
    public bool Lv7 { get; set; }
    public bool Lv8 { get; set; }
    public bool Lv9 { get; set; }
  }

  public class SpellLookupResponse
  {
    public List<Spell> Spells { get; set; }
    public List<SpellDetail> SpellDetails { get; set; }
  }
}
