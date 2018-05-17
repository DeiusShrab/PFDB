using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
  public partial class Spell
  {
    public int SpellId { get; set; }
    public string Name { get; set; }
    public int SchoolId { get; set; }
    public int SubSchoolId { get; set; }
    public string Descriptor { get; set; }
    public string SpellLevel { get; set; }
    public string CastingTime { get; set; }
    public string Components { get; set; }
    public bool CostlyComponents { get; set; }
    public string Range { get; set; }
    public string Area { get; set; }
    public string Effect { get; set; }
    public string Targets { get; set; }
    public string Duration { get; set; }
    public bool Dismissable { get; set; }
    public bool Shapeable { get; set; }
    public string SavingThrow { get; set; }
    public string SpellResistance { get; set; }
    public string Source { get; set; }
    public bool Verbal { get; set; }
    public bool Somatic { get; set; }
    public bool Material { get; set; }
    public bool Focus { get; set; }
    public bool DivineFocus { get; set; }
    public int? Sor { get; set; }
    public int? Wiz { get; set; }
    public int? Cleric { get; set; }
    public int? Druid { get; set; }
    public int? Ranger { get; set; }
    public int? Bard { get; set; }
    public int? Paladin { get; set; }
    public int? Alchemist { get; set; }
    public int? Summoner { get; set; }
    public int? Witch { get; set; }
    public int? Inquisitor { get; set; }
    public int? Oracle { get; set; }
    public int? Antipaladin { get; set; }
    public int? Magus { get; set; }
    public int? Adept { get; set; }
    public string Deity { get; set; }
    public int Slalevel { get; set; }
    public string Domain { get; set; }
    public string ShortDescription { get; set; }
    public bool Acid { get; set; }
    public bool Air { get; set; }
    public bool Chaotic { get; set; }
    public bool Cold { get; set; }
    public bool Curse { get; set; }
    public bool Darkness { get; set; }
    public bool Death { get; set; }
    public bool Disease { get; set; }
    public bool Earth { get; set; }
    public bool Electricity { get; set; }
    public bool Emotion { get; set; }
    public bool Evil { get; set; }
    public bool Fear { get; set; }
    public bool Fire { get; set; }
    public bool Force { get; set; }
    public bool Good { get; set; }
    public bool LanguageDependent { get; set; }
    public bool Lawful { get; set; }
    public bool Light { get; set; }
    public bool MindAffecting { get; set; }
    public bool Pain { get; set; }
    public bool Poison { get; set; }
    public bool Shadow { get; set; }
    public bool Sonic { get; set; }
    public bool Water { get; set; }
    public string LinkText { get; set; }
    public int MaterialCost { get; set; }
    public string Bloodline { get; set; }
    public string Patron { get; set; }
    public string MythicText { get; set; }
    public string Augmented { get; set; }
    public bool Mythic { get; set; }
    public int? BloodRager { get; set; }
    public int? Shaman { get; set; }
    public int? Psychic { get; set; }
    public int? Medium { get; set; }
    public int? Mesmerist { get; set; }
    public int? Occultist { get; set; }
    public int? Spiritualist { get; set; }
    public int? Skald { get; set; }
    public int? Investigator { get; set; }
    public int? Hunter { get; set; }
    public string HauntStatistics { get; set; }

    public virtual SpellDetail SpellDetail { get; set; }
    public virtual SpellSchool SpellSchool { get; set; }
    public virtual SpellSubSchool SpellSubSchool { get; set; }
    public virtual ICollection<BestiaryMagic> BestiaryMagics { get; } = new List<BestiaryMagic>();
    public virtual ICollection<CharacterMagic> CharacterMagics { get; } = new List<CharacterMagic>();
  }
}
