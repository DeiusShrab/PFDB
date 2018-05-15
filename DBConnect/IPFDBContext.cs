
using Microsoft.EntityFrameworkCore;
using DBConnect.DBModels;

namespace DBConnect
{
  public interface IPFDBContext : System.IDisposable
  {
    #region DBSets

    DbSet<Armor> Armor { get; set; }
    DbSet<Bestiary> Bestiary { get; set; }
    DbSet<BestiaryDetail> BestiaryDetail { get; set; }
    DbSet<BestiaryEnvironment> BestiaryEnvironment { get; set; }
    DbSet<BestiaryFeat> BestiaryFeat { get; set; }
    DbSet<BestiaryLanguage> BestiaryLanguage { get; set; }
    DbSet<BestiaryMagic> BestiaryMagic { get; set; }
    DbSet<BestiarySkill> BestiarySkill { get; set; }
    DbSet<BestiarySubType> BestiarySubType { get; set; }
    DbSet<BestiaryType> BestiaryType { get; set; }
    DbSet<Campaign> Campaign { get; set; }
    DbSet<CampaignData> CampaignData { get; set; }
    DbSet<Character> Character { get; set; }
    DbSet<CharacterClass> CharacterClass { get; set; }
    DbSet<CharacterGear> CharacterGear { get; set; }
    DbSet<CharacterRace> CharacterRace { get; set; }
    DbSet<ClassAbility> ClassAbility { get; set; }
    DbSet<Continent> Continent { get; set; }
    DbSet<ContinentWeather> ContinentWeather { get; set; }
    DbSet<Enchantment> Enchantment { get; set; }
    DbSet<Environment> Environment { get; set; }
    DbSet<Faction> Faction { get; set; }
    DbSet<FavoredClass> FavoredClass { get; set; }
    DbSet<Feat> Feat { get; set; }
    DbSet<Gear> Gear { get; set; }
    DbSet<Language> Language { get; set; }
    DbSet<Location> Location { get; set; }
    DbSet<MagicItem> MagicItem { get; set; }
    DbSet<MonsterSpawn> MonsterSpawn { get; set; }
    DbSet<Month> Month { get; set; }
    DbSet<Npc> Npc { get; set; }
    DbSet<Npcdetail> Npcdetail { get; set; }
    DbSet<Plane> Plane { get; set; }
    DbSet<Player> Player { get; set; }
    DbSet<Season> Season { get; set; }
    DbSet<Skill> Skill { get; set; }
    DbSet<Spell> Spell { get; set; }
    DbSet<SpellDetail> SpellDetail { get; set; }
    DbSet<SpellSchool> SpellSchool { get; set; }
    DbSet<SpellSubSchool> SpellSubSchool { get; set; }
    DbSet<Terrain> Terrain { get; set; }
    DbSet<Territory> Territory { get; set; }
    DbSet<Time> Time { get; set; }
    DbSet<TrackedEvent> TrackedEvent { get; set; }
    DbSet<Weapon> Weapon { get; set; }
    DbSet<WeaponAttribute> WeaponAttribute { get; set; }
    DbSet<Weather> Weather { get; set; }

    #endregion

    #region DB Functions

    int SaveChanges();
    int SaveChanges(bool acceptAllChangesOnSuccess);

    #endregion
  }
}
