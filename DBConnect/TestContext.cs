using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using PFDBCommon.DBModels;

namespace DBConnect
{
  class TestContext : PFDBContext
  {
    #region Lists

    private List<Armor> _Armor = new List<Armor>();
    private List<Bestiary> _Bestiary = new List<Bestiary>();
    private List<BestiaryDetail> _BestiaryDetail = new List<BestiaryDetail>();
    private List<BestiaryEnvironment> _BestiaryEnvironment = new List<BestiaryEnvironment>();
    private List<BestiaryFeat> _BestiaryFeat = new List<BestiaryFeat>();
    private List<BestiaryLanguage> _BestiaryLanguage = new List<BestiaryLanguage>();
    private List<BestiaryMagic> _BestiaryMagic = new List<BestiaryMagic>();
    private List<BestiarySkill> _BestiarySkill = new List<BestiarySkill>();
    private List<BestiarySubType> _BestiarySubType = new List<BestiarySubType>();
    private List<BestiaryType> _BestiaryType = new List<BestiaryType>();
    private List<Campaign> _Campaign = new List<Campaign>();
    private List<CampaignData> _CampaignData = new List<CampaignData>();
    private List<Character> _Character = new List<Character>();
    private List<CharacterClassAbility> _CharacterClassAbility = new List<CharacterClassAbility>();
    private List<CharacterClassLevel> _CharacterClassLevel = new List<CharacterClassLevel>();
    private List<CharacterFeat> _CharacterFeat = new List<CharacterFeat>();
    private List<CharacterGear> _CharacterGear = new List<CharacterGear>();
    private List<CharacterGearEnchantment> _CharacterGearEnchantment = new List<CharacterGearEnchantment>();
    private List<CharacterLanguage> _CharacterLanguage = new List<CharacterLanguage>();
    private List<CharacterMagic> _CharacterMagic = new List<CharacterMagic>();
    private List<CharacterSkill> _CharacterSkill = new List<CharacterSkill>();
    private List<Class> _Class = new List<Class>();
    private List<ClassAbility> _ClassAbility = new List<ClassAbility>();
    private List<ClassSkill> _ClassSkill = new List<ClassSkill>();
    private List<Continent> _Continent = new List<Continent>();
    private List<ContinentWeather> _ContinentWeather = new List<ContinentWeather>();
    private List<Enchantment> _Enchantment = new List<Enchantment>();
    private List<Environment> _Environment = new List<Environment>();
    private List<Faction> _Faction = new List<Faction>();
    private List<FavoredClass> _FavoredClass = new List<FavoredClass>();
    private List<Feat> _Feat = new List<Feat>();
    private List<Gear> _Gear = new List<Gear>();
    private List<Language> _Language = new List<Language>();
    private List<Location> _Location = new List<Location>();
    private List<MagicItem> _MagicItem = new List<MagicItem>();
    private List<MonsterSpawn> _MonsterSpawn = new List<MonsterSpawn>();
    private List<Month> _Month = new List<Month>();
    private List<Npc> _Npc = new List<Npc>();
    private List<Npcdetail> _Npcdetail = new List<Npcdetail>();
    private List<Plane> _Plane = new List<Plane>();
    private List<Player> _Player = new List<Player>();
    private List<PlayerCampaign> _PlayerCampaign = new List<PlayerCampaign>();
    private List<Prerequisite> _Prerequisite = new List<Prerequisite>();
    private List<Race> _Race = new List<Race>();
    private List<RaceSubType> _RaceSubType = new List<RaceSubType>();
    private List<Season> _Season = new List<Season>();
    private List<Skill> _Skill = new List<Skill>();
    private List<Spell> _Spell = new List<Spell>();
    private List<SpellDetail> _SpellDetail = new List<SpellDetail>();
    private List<SpellSchool> _SpellSchool = new List<SpellSchool>();
    private List<SpellSubSchool> _SpellSubSchool = new List<SpellSubSchool>();
    private List<Terrain> _Terrain = new List<Terrain>();
    private List<Territory> _Territory = new List<Territory>();
    private List<Time> _Time = new List<Time>();
    private List<TrackedEvent> _TrackedEvent = new List<TrackedEvent>();
    private List<Weapon> _Weapon = new List<Weapon>();
    private List<WeaponAttribute> _WeaponAttribute = new List<WeaponAttribute>();
    private List<WeaponAttributeApplied> _WeaponAttributeApplied = new List<WeaponAttributeApplied>();
    private List<Weather> _Weather = new List<Weather>();


    #endregion

    private int _changes;

    public TestContext()
    {
      Armor = GetQueryableMockDbSet(_Armor);
      Bestiary = GetQueryableMockDbSet(_Bestiary);
      BestiaryDetail = GetQueryableMockDbSet(_BestiaryDetail);
      BestiaryEnvironment = GetQueryableMockDbSet(_BestiaryEnvironment);
      BestiaryFeat = GetQueryableMockDbSet(_BestiaryFeat);
      BestiaryLanguage = GetQueryableMockDbSet(_BestiaryLanguage);
      BestiaryMagic = GetQueryableMockDbSet(_BestiaryMagic);
      BestiarySkill = GetQueryableMockDbSet(_BestiarySkill);
      BestiarySubType = GetQueryableMockDbSet(_BestiarySubType);
      BestiaryType = GetQueryableMockDbSet(_BestiaryType);
      Campaign = GetQueryableMockDbSet(_Campaign);
      CampaignData = GetQueryableMockDbSet(_CampaignData);
      Character = GetQueryableMockDbSet(_Character);
      CharacterClassAbility = GetQueryableMockDbSet(_CharacterClassAbility);
      CharacterClassLevel = GetQueryableMockDbSet(_CharacterClassLevel);
      CharacterFeat = GetQueryableMockDbSet(_CharacterFeat);
      CharacterGear = GetQueryableMockDbSet(_CharacterGear);
      CharacterGearEnchantment = GetQueryableMockDbSet(_CharacterGearEnchantment);
      CharacterLanguage = GetQueryableMockDbSet(_CharacterLanguage);
      CharacterMagic = GetQueryableMockDbSet(_CharacterMagic);
      CharacterSkill = GetQueryableMockDbSet(_CharacterSkill);
      Class = GetQueryableMockDbSet(_Class);
      ClassAbility = GetQueryableMockDbSet(_ClassAbility);
      ClassSkill = GetQueryableMockDbSet(_ClassSkill);
      Continent = GetQueryableMockDbSet(_Continent);
      ContinentWeather = GetQueryableMockDbSet(_ContinentWeather);
      Enchantment = GetQueryableMockDbSet(_Enchantment);
      Environment = GetQueryableMockDbSet(_Environment);
      Faction = GetQueryableMockDbSet(_Faction);
      FavoredClass = GetQueryableMockDbSet(_FavoredClass);
      Feat = GetQueryableMockDbSet(_Feat);
      Gear = GetQueryableMockDbSet(_Gear);
      Language = GetQueryableMockDbSet(_Language);
      Location = GetQueryableMockDbSet(_Location);
      MagicItem = GetQueryableMockDbSet(_MagicItem);
      MonsterSpawn = GetQueryableMockDbSet(_MonsterSpawn);
      Month = GetQueryableMockDbSet(_Month);
      Npc = GetQueryableMockDbSet(_Npc);
      Npcdetail = GetQueryableMockDbSet(_Npcdetail);
      Plane = GetQueryableMockDbSet(_Plane);
      Player = GetQueryableMockDbSet(_Player);
      PlayerCampaign = GetQueryableMockDbSet(_PlayerCampaign);
      Prerequisite = GetQueryableMockDbSet(_Prerequisite);
      Race = GetQueryableMockDbSet(_Race);
      RaceSubType = GetQueryableMockDbSet(_RaceSubType);
      Season = GetQueryableMockDbSet(_Season);
      Skill = GetQueryableMockDbSet(_Skill);
      Spell = GetQueryableMockDbSet(_Spell);
      SpellDetail = GetQueryableMockDbSet(_SpellDetail);
      SpellSchool = GetQueryableMockDbSet(_SpellSchool);
      SpellSubSchool = GetQueryableMockDbSet(_SpellSubSchool);
      Terrain = GetQueryableMockDbSet(_Terrain);
      Territory = GetQueryableMockDbSet(_Territory);
      Time = GetQueryableMockDbSet(_Time);
      TrackedEvent = GetQueryableMockDbSet(_TrackedEvent);
      Weapon = GetQueryableMockDbSet(_Weapon);
      WeaponAttribute = GetQueryableMockDbSet(_WeaponAttribute);
      WeaponAttributeApplied = GetQueryableMockDbSet(_WeaponAttributeApplied);
      Weather = GetQueryableMockDbSet(_Weather);

    }

    private DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
    {
      var queryable = sourceList.AsQueryable();

      var dbSet = new Mock<DbSet<T>>();
      dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
      dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
      dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
      dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
      dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => { sourceList.Add(s); _changes++; });
      dbSet.Setup(d => d.Remove(It.IsAny<T>())).Callback<T>((s) => { sourceList.Remove(s); _changes++; });

      return dbSet.Object;
    }

    public override void Dispose()
    {
      _changes = 0;
    }

    public override int SaveChanges()
    {
      var i = _changes;
      _changes = 0;
      return i;
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
      var i = _changes;
      _changes = 0;
      return i;
    }
  }
}
