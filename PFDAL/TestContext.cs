using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Moq;
using PFDAL.Models;

namespace PFDAL
{
  class TestContext : IPFDBContext
  {
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
    public DbSet<Environment> Environment { get; set; }
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

    public TestContext()
    {
      
    }
  }
}
