using Microsoft.EntityFrameworkCore;
using DBConnect.DBModels;

namespace DBConnect
{
  public partial class PFDBContext : DbContext, IPFDBContext
  {
    public virtual DbSet<Bestiary> Bestiary { get; set; }
    public virtual DbSet<BestiaryDetail> BestiaryDetail { get; set; }
    public virtual DbSet<BestiaryEnvironment> BestiaryEnvironment { get; set; }
    public virtual DbSet<BestiaryFeat> BestiaryFeat { get; set; }
    public virtual DbSet<BestiaryLanguage> BestiaryLanguage { get; set; }
    public virtual DbSet<BestiaryMagic> BestiaryMagic { get; set; }
    public virtual DbSet<BestiarySkill> BestiarySkill { get; set; }
    public virtual DbSet<BestiarySubType> BestiarySubType { get; set; }
    public virtual DbSet<BestiaryType> BestiaryType { get; set; }
    public virtual DbSet<Campaign> Campaign { get; set; }
    public virtual DbSet<CampaignData> CampaignData { get; set; }
    public virtual DbSet<Continent> Continent { get; set; }
    public virtual DbSet<ContinentWeather> ContinentWeather { get; set; }
    public virtual DbSet<Environment> Environment { get; set; }
    public virtual DbSet<Faction> Faction { get; set; }
    public virtual DbSet<Feat> Feat { get; set; }
    public virtual DbSet<Language> Language { get; set; }
    public virtual DbSet<Location> Location { get; set; }
    public virtual DbSet<MagicItem> MagicItem { get; set; }
    public virtual DbSet<MonsterSpawn> MonsterSpawn { get; set; }
    public virtual DbSet<Month> Month { get; set; }
    public virtual DbSet<Npc> Npc { get; set; }
    public virtual DbSet<Npcdetail> Npcdetail { get; set; }
    public virtual DbSet<Plane> Plane { get; set; }
    public virtual DbSet<Season> Season { get; set; }
    public virtual DbSet<Skill> Skill { get; set; }
    public virtual DbSet<Spell> Spell { get; set; }
    public virtual DbSet<SpellDetail> SpellDetail { get; set; }
    public virtual DbSet<SpellSchool> SpellSchool { get; set; }
    public virtual DbSet<SpellSubSchool> SpellSubSchool { get; set; }
    public virtual DbSet<Terrain> Terrain { get; set; }
    public virtual DbSet<Territory> Territory { get; set; }
    public virtual DbSet<Time> Time { get; set; }
    public virtual DbSet<TrackedEvent> TrackedEvent { get; set; }
    public virtual DbSet<Weather> Weather { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        //optionsBuilder.UseSqlServer("Server=192.168.1.150;Database=PFDB;User Id=PFDBSite;Password=ayy lmao ayy lmao AYY 1m40");
        //optionsBuilder.UseSqlServer("Server=localhost;Database=PFDB;User Id=PFDBSite;Password=ayy lmao ayy lmao AYY 1m40");
        optionsBuilder.UseSqlServer($"Server={PFConfig.DB_ADDR};Database={PFConfig.DB_DB};User Id={PFConfig.DB_USER};Password={PFConfig.DB_PASS}");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //modelBuilder.Properties()

      modelBuilder.Entity<Armor>(entity =>
      {
        entity.ToTable("Armor");
      });

      modelBuilder.Entity<Bestiary>(entity =>
      {
        entity.Property(e => e.AbilityScoreMods)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.AbilityScores)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Ac).HasColumnName("AC");

        entity.Property(e => e.Acflat).HasColumnName("ACFlat");

        entity.Property(e => e.Acmods)
            .HasColumnName("ACMods")
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.Actouch).HasColumnName("ACTouch");

        entity.Property(e => e.AgeCategory)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Alignment)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.AlternateNameForm)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Aura)
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.BaseStatistics)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.BeforeCombat)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Bloodline)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Cha).HasColumnName("CHA");

        entity.Property(e => e.Class)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.ClassArchetypes)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Cmb).HasColumnName("CMB");

        entity.Property(e => e.Cmd).HasColumnName("CMD");

        entity.Property(e => e.CompanionFamiliarLink)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Con).HasColumnName("CON");

        entity.Property(e => e.Cr).HasColumnName("CR");

        entity.Property(e => e.DefensiveAbilities)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.DescriptionVisual).IsUnicode(false);

        entity.Property(e => e.Dex).HasColumnName("DEX");

        entity.Property(e => e.DontUseRacialHd).HasColumnName("DontUseRacialHD");

        entity.Property(e => e.Dr)
            .HasColumnName("DR")
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.DuringCombat)
            .HasMaxLength(250)
            .IsUnicode(false);

        entity.Property(e => e.Environment)
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.ExtractsPrepared)
            .HasMaxLength(250)
            .IsUnicode(false);

        entity.Property(e => e.Feats).IsUnicode(false);

        entity.Property(e => e.FocusedSchool)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Gear)
            .HasMaxLength(250)
            .IsUnicode(false);

        entity.Property(e => e.Gender)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Group)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Hd)
            .HasColumnName("HD")
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Hp).HasColumnName("HP");

        entity.Property(e => e.Hpmods)
            .HasColumnName("HPMods")
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Immune)
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.Property(e => e.Int).HasColumnName("INT");

        entity.Property(e => e.Languages)
            .HasMaxLength(250)
            .IsUnicode(false);

        entity.Property(e => e.LinkText)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Melee)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Morale)
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.Mr).HasColumnName("MR");

        entity.Property(e => e.Mt).HasColumnName("MT");

        entity.Property(e => e.Mystery)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Note)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.OffenseNote)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Organization)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.OtherGear)
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.Property(e => e.Patron)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.ProhibitedSchools)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Race)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.RacialMods)
            .HasMaxLength(250)
            .IsUnicode(false);

        entity.Property(e => e.Ranged)
            .HasMaxLength(250)
            .IsUnicode(false);

        entity.Property(e => e.Reach)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Resist)
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.SaveMods)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Senses)
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.Property(e => e.Size)
            .HasMaxLength(20)
            .IsUnicode(false);

        entity.Property(e => e.Skills).IsUnicode(false);

        entity.Property(e => e.Space)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.SpecialAbilities).IsUnicode(false);

        entity.Property(e => e.SpecialAttacks)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Speed)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.SpeedMod)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.SpellDomains)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.SpellLikeAbilities).IsUnicode(false);

        entity.Property(e => e.SpellsKnown).IsUnicode(false);

        entity.Property(e => e.SpellsPrepared).IsUnicode(false);

        entity.Property(e => e.Sq)
            .HasColumnName("SQ")
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Sr).HasColumnName("SR");

        entity.Property(e => e.StatisticsNote)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Str).HasColumnName("STR");

        entity.Property(e => e.SubType)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.TemplatesApplied)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Traits)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Treasure).IsUnicode(false);

        entity.Property(e => e.Type)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.VariantParent)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Vulnerability)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Weaknesses)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Wis).HasColumnName("WIS");

        entity.Property(e => e.Xp).HasColumnName("XP");
      });

      modelBuilder.Entity<BestiaryDetail>(entity =>
      {
        entity.HasKey(e => e.BestiaryId);

        entity.Property(e => e.Description).IsUnicode(false);

        entity.Property(e => e.FullText).IsUnicode(false);

        entity.Property(e => e.MonsterSource)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Source)
            .HasMaxLength(50)
            .IsUnicode(false);
      });

      modelBuilder.Entity<BestiaryEnvironment>(entity =>
      {
        entity.Property(e => e.Notes)
            .HasMaxLength(500)
            .IsUnicode(false);
      });

      modelBuilder.Entity<BestiaryFeat>(entity =>
      {
        entity.Property(e => e.Notes)
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<BestiaryLanguage>(entity =>
      {
        entity.Property(e => e.Notes)
            .HasMaxLength(500)
            .IsUnicode(false);
      });

      modelBuilder.Entity<BestiaryMagic>(entity =>
      {
        entity.Property(e => e.Notes)
            .HasMaxLength(500)
            .IsUnicode(false);
      });

      modelBuilder.Entity<BestiarySkill>(entity =>
      {
        entity.Property(e => e.Notes)
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<BestiaryType>(entity =>
      {
        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<Campaign>(entity =>
      {
        entity.HasKey(e => e.CampaignId);

        entity.Property(e => e.CampaignName)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.CampaignNotes)
            .HasMaxLength(2000)
            .IsUnicode(false);
      });

      modelBuilder.Entity<CampaignData>(entity =>
      {
        entity.HasKey(e => new { e.CampaignId, e.Key });

        entity.Property(e => e.Value)
              .HasMaxLength(500)
              .IsUnicode(false);
        
      });

      modelBuilder.Entity<Continent>(entity =>
      {
        entity.Property(e => e.MapPath)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.PrimaryLanguage)
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<ContinentWeather>(entity =>
      {
        entity.HasKey(e => e.CWID);

        entity.Property(e => e.CWID)
            .HasColumnName("CWID")
            .ValueGeneratedNever();

        entity.Property(e => e.NextCWID).HasColumnName("NextCWID");

        entity.Property(e => e.ParentCWID).HasColumnName("ParentCWID");
      });

      modelBuilder.Entity<Environment>(entity =>
      {
        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Notes)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Temperature)
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<Faction>(entity =>
      {
        entity.Property(e => e.Language)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.PrimaryRace)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Type)
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<Feat>(entity =>
      {
        entity.Property(e => e.Benefit).IsUnicode(false);

        entity.Property(e => e.CompletionBenefit).IsUnicode(false);

        entity.Property(e => e.Description)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Fulltext).IsUnicode(false);

        entity.Property(e => e.Goal).IsUnicode(false);

        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Normal)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Note)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.PrerequisiteFeats)
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.Property(e => e.PrerequisiteSkills)
            .HasMaxLength(250)
            .IsUnicode(false);

        entity.Property(e => e.Prerequisites).IsUnicode(false);

        entity.Property(e => e.RaceName)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Source)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Special)
            .HasMaxLength(750)
            .IsUnicode(false);

        entity.Property(e => e.SuggestedTraits)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Type)
            .HasColumnName("Type_")
            .HasMaxLength(25)
            .IsUnicode(false);
      });

      modelBuilder.Entity<Language>(entity =>
      {
        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Notes)
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<Location>(entity =>
      {
        entity.Property(e => e.Name)
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<MagicItem>(entity =>
      {
        entity.Property(e => e.Al)
            .HasColumnName("AL")
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Aura)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.AuraStrength)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.BaseItem)
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.Cl).HasColumnName("CL");

        entity.Property(e => e.Communication)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Cost)
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.Description).IsUnicode(false);

        entity.Property(e => e.Destruction).IsUnicode(false);

        entity.Property(e => e.FullText).IsUnicode(false);

        entity.Property(e => e.Group)
            .HasColumnName("Group_")
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Languages)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.LinkText)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.MagicItems)
            .HasMaxLength(250)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Powers)
            .HasMaxLength(250)
            .IsUnicode(false);

        entity.Property(e => e.Price)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Requirements)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Scaling)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Senses)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Slot)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Source)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Weight)
            .HasMaxLength(50)
            .IsUnicode(false);
      });

      modelBuilder.Entity<MonsterSpawn>(entity =>
      {
        entity.HasKey(e => e.SpawnId);
      });

      modelBuilder.Entity<Month>(entity =>
      {
        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<Npc>(entity =>
      {
        entity.ToTable("NPC");

        entity.Property(e => e.Npcid).HasColumnName("NPCID");

        entity.Property(e => e.AbilityScoreMods)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.AbilityScores)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Ac)
            .HasColumnName("AC")
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Acmods)
            .HasColumnName("ACMods")
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.AgeCategory)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Alignment)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.AlternateNameForm)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Aura)
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.BaseStatistics).IsUnicode(false);

        entity.Property(e => e.BeforeCombat).IsUnicode(false);

        entity.Property(e => e.Bloodline)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Class)
            .HasColumnName("Class_")
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.ClassArchetypes)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Cmb).HasColumnName("CMB");

        entity.Property(e => e.Cmd).HasColumnName("CMD");

        entity.Property(e => e.CompanionFamiliarLink)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Cr).HasColumnName("CR");

        entity.Property(e => e.DefensiveAbilities)
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.Property(e => e.DescriptionVisual)
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.Dr)
            .HasColumnName("DR")
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.DuringCombat).IsUnicode(false);

        entity.Property(e => e.Environment)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.ExtractsPrepared)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Feats).IsUnicode(false);

        entity.Property(e => e.FocusedSchool)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Gear).IsUnicode(false);

        entity.Property(e => e.Gender)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Group)
            .HasColumnName("Group_")
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Hd)
            .HasColumnName("HD")
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Hp).HasColumnName("HP");

        entity.Property(e => e.Hpmods)
            .HasColumnName("HPMods")
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Immune)
            .HasMaxLength(250)
            .IsUnicode(false);

        entity.Property(e => e.Implements).IsUnicode(false);

        entity.Property(e => e.KineticistWildTalents)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Languages)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.LinkText)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Melee)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Morale).IsUnicode(false);

        entity.Property(e => e.Mr).HasColumnName("MR");

        entity.Property(e => e.Mt).HasColumnName("MT");

        entity.Property(e => e.Mystery)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Note)
            .HasMaxLength(250)
            .IsUnicode(false);

        entity.Property(e => e.OffenseNote)
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.Organization)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.OtherGear).IsUnicode(false);

        entity.Property(e => e.Patron)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.ProhibitedSchools)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.PsychicDiscipline)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.PsychicMagic)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Race)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.RacialMods)
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.Property(e => e.Ranged)
            .HasMaxLength(250)
            .IsUnicode(false);

        entity.Property(e => e.Reach)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Ref).HasColumnName("Ref_");

        entity.Property(e => e.Resist)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.SaveMods)
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.Saves)
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.Property(e => e.Senses)
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.Size)
            .HasColumnName("Size_")
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Skills).IsUnicode(false);

        entity.Property(e => e.Source)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Space)
            .HasColumnName("Space_")
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.SpecialAbilities).IsUnicode(false);

        entity.Property(e => e.SpecialAttacks)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Speed)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.SpeedMod)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.SpellDomains)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.SpellLikeAbilities)
            .HasMaxLength(700)
            .IsUnicode(false);

        entity.Property(e => e.SpellsKnown).IsUnicode(false);

        entity.Property(e => e.SpellsPrepared).IsUnicode(false);

        entity.Property(e => e.Spirit)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Sq)
            .HasColumnName("SQ")
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Sr)
            .HasColumnName("SR")
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.SubType)
            .HasColumnName("SubType_")
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.TemplatesApplied)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.ThassilonianSpecialization)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Traits)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Treasure)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Type)
            .HasColumnName("Type_")
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Vulnerability)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Weaknesses)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Xp).HasColumnName("XP");
      });

      modelBuilder.Entity<Npcdetail>(entity =>
      {
        entity.HasKey(e => e.Npcid);

        entity.ToTable("NPCDetail");

        entity.Property(e => e.Npcid).HasColumnName("NPCID");

        entity.Property(e => e.Description).IsUnicode(false);

        entity.Property(e => e.FullText).IsUnicode(false);

        entity.Property(e => e.MonsterSource)
            .HasMaxLength(250)
            .IsUnicode(false);
      });

      modelBuilder.Entity<Plane>(entity =>
      {
        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<Season>(entity =>
      {
        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<Skill>(entity =>
      {
        entity.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<Spell>(entity =>
      {
        entity.Property(e => e.Area)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Augmented)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Bloodline)
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.CastingTime)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Components)
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.Property(e => e.Deity)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Descriptor)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Domain)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Duration)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Effect)
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.Property(e => e.HauntStatistics)
            .HasMaxLength(250)
            .IsUnicode(false);

        entity.Property(e => e.LinkText)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.MythicText).IsUnicode(false);

        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Patron)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Range)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.SavingThrow)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.SchoolId)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.ShortDescription)
            .HasMaxLength(250)
            .IsUnicode(false);

        entity.Property(e => e.Slalevel).HasColumnName("SLALevel");

        entity.Property(e => e.Source)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.SpellLevel)
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.Property(e => e.SpellResistance)
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.SubSchoolId)
            .HasMaxLength(25)
            .IsUnicode(false);

        entity.Property(e => e.Targets)
            .HasMaxLength(200)
            .IsUnicode(false);
      });

      modelBuilder.Entity<SpellDetail>(entity =>
      {
        entity.HasKey(e => e.SpellId);

        entity.Property(e => e.Description).IsUnicode(false);

        entity.Property(e => e.DescriptionFormatted).IsUnicode(false);

        entity.Property(e => e.FullText).IsUnicode(false);
      });

      modelBuilder.Entity<SpellSchool>(entity =>
      {
        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<SpellSubSchool>(entity =>
      {
        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<Terrain>(entity =>
      {
        entity.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<Territory>(entity =>
      {
        entity.Property(e => e.Name)
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<Time>(entity =>
      {
        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<TrackedEvent>(entity =>
      {
        entity.Property(e => e.DateOccurring)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Notes)
            .IsRequired()
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.ReoccurFreq)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
      });

      modelBuilder.Entity<Weapon>(entity =>
      {
        entity.ToTable("Weapon");
      });

      modelBuilder.Entity<Weather>(entity =>
      {
        entity.Property(e => e.Description)
            .HasMaxLength(1000)
            .IsUnicode(false);

        entity.Property(e => e.Effects)
            .HasMaxLength(1000)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
      });
    }
  }
}
