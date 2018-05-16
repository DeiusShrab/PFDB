using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using DBConnect.DBModels;

namespace DBConnect
{
  class TestContext : PFDBContext
  {
    #region Lists

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
    private List<Continent> _Continent = new List<Continent>();
    private List<ContinentWeather> _ContinentWeather = new List<ContinentWeather>();
    private List<Environment> _Environment = new List<Environment>();
    private List<Faction> _Faction = new List<Faction>();
    private List<Feat> _Feat = new List<Feat>();
    private List<Language> _Language = new List<Language>();
    private List<Location> _Location = new List<Location>();
    private List<MagicItem> _MagicItem = new List<MagicItem>();
    private List<MonsterSpawn> _MonsterSpawn = new List<MonsterSpawn>();
    private List<Month> _Month = new List<Month>();
    private List<Npc> _Npc = new List<Npc>();
    private List<Npcdetail> _Npcdetail = new List<Npcdetail>();
    private List<Season> _Season = new List<Season>();
    private List<Spell> _Spell = new List<Spell>();
    private List<SpellDetail> _SpellDetail = new List<SpellDetail>();
    private List<Territory> _Territory = new List<Territory>();
    private List<Weather> _Weather = new List<Weather>();
    private List<Terrain> _Terrain = new List<Terrain>();
    private List<Time> _Time = new List<Time>();
    private List<TrackedEvent> _TrackedEvent = new List<TrackedEvent>();
    private List<Plane> _Plane = new List<Plane>();
    private List<Skill> _Skill = new List<Skill>();
    private List<SpellSchool> _SpellSchool = new List<SpellSchool>();
    private List<SpellSubSchool> _SpellSubSchool = new List<SpellSubSchool>();
    private List<Armor> _Armor = new List<Armor>();
    private List<CharacterClass> _CharacterClass = new List<CharacterClass>();
    private List<CharacterGear> _CharacterGear = new List<CharacterGear>();
    private List<CharacterRace> _CharacterRace = new List<CharacterRace>();
    private List<ClassAbility> _ClassAbility = new List<ClassAbility>();
    private List<Enchantment> _Enchantment = new List<Enchantment>();
    private List<FavoredClass> _FavoredClass = new List<FavoredClass>();
    private List<Gear> _Gear = new List<Gear>();
    private List<Weapon> _Weapon = new List<Weapon>();
    private List<WeaponAttribute> _WeaponAttribute = new List<WeaponAttribute>();
    private List<Character> _Character = new List<Character>();
    private List<Player> _Player = new List<Player>();
    private List<PlayerCampaign> _PlayerCampaign = new List<PlayerCampaign>();

    #endregion

    private int _changes;

    public TestContext()
    {
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
      Continent = GetQueryableMockDbSet(_Continent);
      ContinentWeather = GetQueryableMockDbSet(_ContinentWeather);
      Environment = GetQueryableMockDbSet(_Environment);
      Faction = GetQueryableMockDbSet(_Faction);
      Feat = GetQueryableMockDbSet(_Feat);
      Language = GetQueryableMockDbSet(_Language);
      Location = GetQueryableMockDbSet(_Location);
      MagicItem = GetQueryableMockDbSet(_MagicItem);
      MonsterSpawn = GetQueryableMockDbSet(_MonsterSpawn);
      Month = GetQueryableMockDbSet(_Month);
      Npc = GetQueryableMockDbSet(_Npc);
      Npcdetail = GetQueryableMockDbSet(_Npcdetail);
      Season = GetQueryableMockDbSet(_Season);
      Spell = GetQueryableMockDbSet(_Spell);
      SpellDetail = GetQueryableMockDbSet(_SpellDetail);
      Territory = GetQueryableMockDbSet(_Territory);
      Weather = GetQueryableMockDbSet(_Weather);
      Terrain = GetQueryableMockDbSet(_Terrain);
      Time = GetQueryableMockDbSet(_Time);
      TrackedEvent = GetQueryableMockDbSet(_TrackedEvent);
      Plane = GetQueryableMockDbSet(_Plane);
      Skill = GetQueryableMockDbSet(_Skill);
      SpellSchool = GetQueryableMockDbSet(_SpellSchool);
      SpellSubSchool = GetQueryableMockDbSet(_SpellSubSchool);
      Armor = GetQueryableMockDbSet(_Armor);
      CharacterClass = GetQueryableMockDbSet(_CharacterClass);
      CharacterGear = GetQueryableMockDbSet(_CharacterGear);
      CharacterRace = GetQueryableMockDbSet(_CharacterRace);
      ClassAbility = GetQueryableMockDbSet(_ClassAbility);
      Enchantment = GetQueryableMockDbSet(_Enchantment);
      FavoredClass = GetQueryableMockDbSet(_FavoredClass);
      Gear = GetQueryableMockDbSet(_Gear);
      Weapon = GetQueryableMockDbSet(_Weapon);
      WeaponAttribute = GetQueryableMockDbSet(_WeaponAttribute);
      Character = GetQueryableMockDbSet(_Character);
      Player = GetQueryableMockDbSet(_Player);
      PlayerCampaign = GetQueryableMockDbSet(_PlayerCampaign);
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
