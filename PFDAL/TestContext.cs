﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Moq;
using PFDAL.Models;

namespace PFDAL
{
  class TestContext : IPFDBContext
  {
    #region DBSets
    public DbSet<Bestiary> Bestiary { get; set; }
    public DbSet<BestiaryDetail> BestiaryDetail { get; set; }
    public DbSet<BestiaryEnvironment> BestiaryEnvironment { get; set; }
    public DbSet<BestiaryFeat> BestiaryFeat { get; set; }
    public DbSet<BestiaryLanguage> BestiaryLanguage { get; set; }
    public DbSet<BestiarySkill> BestiarySkill { get; set; }
    public DbSet<BestiarySubType> BestiarySubType { get; set; }
    public DbSet<BestiaryType> BestiaryType { get; set; }
    public DbSet<Continent> Continent { get; set; }
    public DbSet<ContinentWeather> ContinentWeather { get; set; }
    public DbSet<Models.Environment> Environment { get; set; }
    public DbSet<Faction> Faction { get; set; }
    public DbSet<Feat> Feat { get; set; }
    public DbSet<Language> Language { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<MagicItem> MagicItem { get; set; }
    public DbSet<MonsterSpawn> MonsterSpawn { get; set; }
    public DbSet<Month> Month { get; set; }
    public DbSet<Npc> Npc { get; set; }
    public DbSet<Npcdetail> Npcdetail { get; set; }
    public DbSet<Season> Season { get; set; }
    public DbSet<Spell> Spell { get; set; }
    public DbSet<SpellDetail> SpellDetail { get; set; }
    public DbSet<Territory> Territory { get; set; }
    public DbSet<Weather> Weather { get; set; }
    public DbSet<Terrain> Terrain { get; set; }
    public DbSet<Time> Time { get; set; }
    public DbSet<Plane> Plane { get; set; }
    public DbSet<Skill> Skill { get; set; }
    #endregion

    #region Lists

    private List<Bestiary> _Bestiary = new List<Bestiary>();
    private List<BestiaryDetail> _BestiaryDetail = new List<BestiaryDetail>();
    private List<BestiaryEnvironment> _BestiaryEnvironment = new List<BestiaryEnvironment>();
    private List<BestiaryFeat> _BestiaryFeat = new List<BestiaryFeat>();
    private List<BestiaryLanguage> _BestiaryLanguage = new List<BestiaryLanguage>();
    private List<BestiarySkill> _BestiarySkill = new List<BestiarySkill>();
    private List<BestiarySubType> _BestiarySubType = new List<BestiarySubType>();
    private List<BestiaryType> _BestiaryType = new List<BestiaryType>();
    private List<Continent> _Continent = new List<Continent>();
    private List<ContinentWeather> _ContinentWeather = new List<ContinentWeather>();
    private List<Models.Environment> _Environment = new List<Models.Environment>();
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
    private List<Plane> _Plane = new List<Plane>();
    private List<Skill> _Skill = new List<Skill>();
    #endregion

    private int _changes;

    public TestContext()
    {
      Bestiary = GetQueryableMockDbSet(_Bestiary);
      BestiaryDetail = GetQueryableMockDbSet(_BestiaryDetail);
      BestiaryEnvironment = GetQueryableMockDbSet(_BestiaryEnvironment);
      BestiaryFeat = GetQueryableMockDbSet(_BestiaryFeat);
      BestiaryLanguage = GetQueryableMockDbSet(_BestiaryLanguage);
      BestiarySkill = GetQueryableMockDbSet(_BestiarySkill);
      BestiarySubType = GetQueryableMockDbSet(_BestiarySubType);
      BestiaryType = GetQueryableMockDbSet(_BestiaryType);
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
      Plane = GetQueryableMockDbSet(_Plane);
      Skill = GetQueryableMockDbSet(_Skill);
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

      return dbSet.Object;
    }

    public void Dispose()
    {

    }

    public int SaveChanges()
    {
      var i = _changes;
      _changes = 0;
      return i;
    }

    public int SaveChanges(bool acceptAllChangesOnSuccess)
    {
      var i = _changes;
      _changes = 0;
      return i;
    }
  }
}
