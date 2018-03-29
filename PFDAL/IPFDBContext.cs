using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PFDAL.Models;

namespace PFDAL
{
  public interface IPFDBContext
  {
    DbSet<Bestiary> Bestiary { get; set; }
    DbSet<BestiaryDetail> BestiaryDetail { get; set; }
    DbSet<BestiaryEnvironment> BestiaryEnvironment { get; set; }
    DbSet<BestiaryFeat> BestiaryFeat { get; set; }
    DbSet<BestiaryLanguage> BestiaryLanguage { get; set; }
    DbSet<BestiarySkill> BestiarySkill { get; set; }
    DbSet<BestiarySubType> BestiarySubType { get; set; }
    DbSet<BestiaryType> BestiaryType { get; set; }
    DbSet<Continent> Continent { get; set; }
    DbSet<ContinentWeather> ContinentWeather { get; set; }
    DbSet<Environment> Environment { get; set; }
    DbSet<Faction> Faction { get; set; }
    DbSet<Feat> Feat { get; set; }
    DbSet<Language> Language { get; set; }
    DbSet<Location> Location { get; set; }
    DbSet<MagicItem> MagicItem { get; set; }
    DbSet<MonsterSpawn> MonsterSpawn { get; set; }
    DbSet<Month> Month { get; set; }
    DbSet<Npc> Npc { get; set; }
    DbSet<Npcdetail> Npcdetail { get; set; }
    DbSet<Season> Season { get; set; }
    DbSet<Spell> Spell { get; set; }
    DbSet<SpellDetail> SpellDetail { get; set; }
    DbSet<Territory> Territory { get; set; }
    DbSet<Weather> Weather { get; set; }
    DbSet<Terrain> Terrain { get; set; }
    DbSet<Time> Time { get; set; }
    DbSet<Plane> Plane { get; set; }
    DbSet<Skill> Skill { get; set; }
  }
}
