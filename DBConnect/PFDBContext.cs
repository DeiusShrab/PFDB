using Microsoft.EntityFrameworkCore;
using PFDBCommon.DBModels;

namespace DBConnect
{
  public class PFDBContext : DbContext
  {
    public DbSet<Armor> Armor { get; set; }
    public DbSet<Bestiary> Bestiary { get; set; }
    public DbSet<BestiaryDetail> BestiaryDetail { get; set; }
    public DbSet<BestiaryEnvironment> BestiaryEnvironment { get; set; }
    public DbSet<BestiaryFeat> BestiaryFeat { get; set; }
    public DbSet<BestiaryLanguage> BestiaryLanguage { get; set; }
    public DbSet<BestiaryMagic> BestiaryMagic { get; set; }
    public DbSet<BestiarySkill> BestiarySkill { get; set; }
    public DbSet<BestiarySubType> BestiarySubType { get; set; }
    public DbSet<BestiaryType> BestiaryType { get; set; }
    public DbSet<Campaign> Campaign { get; set; }
    public DbSet<CampaignData> CampaignData { get; set; }
    public DbSet<Character> Character { get; set; }
    public DbSet<CharacterClassAbility> CharacterClassAbility { get; set; }
    public DbSet<CharacterClassLevel> CharacterClassLevel { get; set; }
    public DbSet<CharacterFeat> CharacterFeat { get; set; }
    public DbSet<CharacterGear> CharacterGear { get; set; }
    public DbSet<CharacterGearEnchantment> CharacterGearEnchantment { get; set; }
    public DbSet<CharacterLanguage> CharacterLanguage { get; set; }
    public DbSet<CharacterMagic> CharacterMagic { get; set; }
    public DbSet<CharacterRaceAbility> CharacterRaceAbility { get; set; }
    public DbSet<CharacterSkill> CharacterSkill { get; set; }
    public DbSet<Class> Class { get; set; }
    public DbSet<ClassAbility> ClassAbility { get; set; }
    public DbSet<ClassSkill> ClassSkill { get; set; }
    public DbSet<Continent> Continent { get; set; }
    public DbSet<ContinentEnvironment> ContinentEnvironment { get; set; }
    public DbSet<ContinentWeather> ContinentWeather { get; set; }
    public DbSet<Enchantment> Enchantment { get; set; }
    public DbSet<Environment> Environment { get; set; }
    public DbSet<Faction> Faction { get; set; }
    public DbSet<FavoredClass> FavoredClass { get; set; }
    public DbSet<Feat> Feat { get; set; }
    public DbSet<Gear> Gear { get; set; }
    public DbSet<Language> Language { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<MagicItem> MagicItem { get; set; }
    public DbSet<MonsterSpawn> MonsterSpawn { get; set; }
    public DbSet<Month> Month { get; set; }
    public DbSet<Npc> Npc { get; set; }
    public DbSet<Npcdetail> Npcdetail { get; set; }
    public DbSet<OverridePrerequisite> OverridePrerequisite { get; set; }
    public DbSet<Plane> Plane { get; set; }
    public DbSet<Player> Player { get; set; }
    public DbSet<PlayerCampaign> PlayerCampaign { get; set; }
    public DbSet<Prerequisite> Prerequisite { get; set; }
    public DbSet<Race> Race { get; set; }
    public DbSet<RaceAbility> RaceAbility { get; set; }
    public DbSet<RaceSubType> RaceSubType { get; set; }
    public DbSet<Season> Season { get; set; }
    public DbSet<Skill> Skill { get; set; }
    public DbSet<Spell> Spell { get; set; }
    public DbSet<SpellDetail> SpellDetail { get; set; }
    public DbSet<SpellSchool> SpellSchool { get; set; }
    public DbSet<SpellSubSchool> SpellSubSchool { get; set; }
    public DbSet<Terrain> Terrain { get; set; }
    public DbSet<Territory> Territory { get; set; }
    public DbSet<Time> Time { get; set; }
    public DbSet<TrackedEvent> TrackedEvent { get; set; }
    public DbSet<Weapon> Weapon { get; set; }
    public DbSet<WeaponAttribute> WeaponAttribute { get; set; }
    public DbSet<WeaponAttributeApplied> WeaponAttributeApplied { get; set; }
    public DbSet<Weather> Weather { get; set; }

    public PFDBContext() { }

    public PFDBContext(DbContextOptions<PFDBContext> contextOptions) : base(contextOptions) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        //optionsBuilder.UseSqlServer("Server=192.168.1.150;Database=PFDB;User Id=PFDBSite;Password=ayy lmao ayy lmao AYY 1m40");
        //optionsBuilder.UseSqlServer("Server=localhost;Database=PFDB;User Id=PFDBSite;Password=ayy lmao ayy lmao AYY 1m40");
        optionsBuilder.UseSqlServer($"Server={PFConfig.DB_ADDR};Database={PFConfig.DB_DB};User Id={PFConfig.DB_USER};Password={PFConfig.DB_PASS}");
#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging();
#endif
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        entity.HasOne(e => e.BestiaryType)
              .WithMany(e => e.Bestiaries)
              .HasForeignKey(e => e.Type)
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(e => e.BestiaryEnvironments)
              .WithOne(e => e.Bestiary)
              .HasForeignKey(e => e.BestiaryId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.BestiaryFeats)
              .WithOne(e => e.Bestiary)
              .HasForeignKey(e => e.BestiaryId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.BestiaryLanguages)
              .WithOne(e => e.Bestiary)
              .HasForeignKey(e => e.BestiaryId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.BestiaryMagics)
              .WithOne(e => e.Bestiary)
              .HasForeignKey(e => e.BestiaryId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.BestiarySkills)
              .WithOne(e => e.Bestiary)
              .HasForeignKey(e => e.BestiaryId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.BestiarySubTypes)
              .WithOne(e => e.Bestiary)
              .HasForeignKey(e => e.BestiaryId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.MonsterSpawns)
              .WithOne(e => e.Bestiary)
              .HasForeignKey(e => e.BestiaryId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.BestiaryRaces)
              .WithOne(e => e.RaceBestiary)
              .HasForeignKey(e => e.BestiaryId)
              .OnDelete(DeleteBehavior.SetNull);
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

        entity.HasOne(e => e.Bestiary)
              .WithOne(e => e.BestiaryDetail)
              .IsRequired()
              .HasForeignKey("BestiaryDetail", "BestiaryId")
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<BestiaryEnvironment>(entity =>
      {
        entity.HasKey(e => e.BestiaryEnvironmentId);

        entity.HasOne(e => e.Bestiary)
              .WithMany(e => e.BestiaryEnvironments)
              .IsRequired()
              .HasForeignKey(e => e.BestiaryId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Environment)
              .WithMany(e => e.BestiaryEnvironments)
              .IsRequired()
              .HasForeignKey(e => e.EnvironmentId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.Property(e => e.Notes)
            .HasMaxLength(500)
            .IsUnicode(false);
      });

      modelBuilder.Entity<BestiaryFeat>(entity =>
      {
        entity.HasKey(e => e.BestiaryFeatId);

        entity.HasOne(e => e.Bestiary)
              .WithMany(e => e.BestiaryFeats)
              .IsRequired()
              .HasForeignKey(e => e.BestiaryId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Feat)
              .WithMany(e => e.BestiaryFeats)
              .IsRequired()
              .HasForeignKey(e => e.FeatId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.Property(e => e.Notes)
            .HasMaxLength(500)
            .IsUnicode(false);
      });

      modelBuilder.Entity<BestiaryLanguage>(entity =>
      {
        entity.HasKey(e => e.BestiaryLanguageId);

        entity.HasOne(e => e.Bestiary)
              .WithMany(e => e.BestiaryLanguages)
              .IsRequired()
              .HasForeignKey(e => e.BestiaryId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Language)
              .WithMany(e => e.BestiaryLanguages)
              .IsRequired()
              .HasForeignKey(e => e.LanguageId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.Property(e => e.Notes)
            .HasMaxLength(500)
            .IsUnicode(false);
      });

      modelBuilder.Entity<BestiaryMagic>(entity =>
      {
        entity.HasKey(e => e.BestiaryMagicId);

        entity.HasOne(e => e.Bestiary)
              .WithMany(e => e.BestiaryMagics)
              .IsRequired()
              .HasForeignKey(e => e.BestiaryId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Spell)
              .WithMany(e => e.BestiaryMagics)
              .IsRequired()
              .HasForeignKey(e => e.SpellId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.Property(e => e.Notes)
            .HasMaxLength(500)
            .IsUnicode(false);
      });

      modelBuilder.Entity<BestiarySkill>(entity =>
      {
        entity.HasKey(e => e.BestiarySkillId);

        entity.HasOne(e => e.Bestiary)
              .WithMany(e => e.BestiarySkills)
              .IsRequired()
              .HasForeignKey(e => e.BestiaryId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Skill)
              .WithMany(e => e.BestiarySkills)
              .IsRequired()
              .HasForeignKey(e => e.SkillId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.Property(e => e.Notes)
            .HasMaxLength(500)
            .IsUnicode(false);
      });

      modelBuilder.Entity<BestiarySubType>(entity =>
      {
        entity.HasKey(e => e.BestiarySubTypeId);

        entity.HasOne(e => e.Bestiary)
              .WithMany(e => e.BestiarySubTypes)
              .IsRequired()
              .HasForeignKey(e => e.BestiaryId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.BestiaryType)
              .WithMany(e => e.BestiarySubTypes)
              .IsRequired()
              .HasForeignKey(e => e.BestiaryTypeId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<BestiaryType>(entity =>
      {
        entity.HasKey(e => e.BestiaryTypeId);

        entity.HasMany(e => e.Bestiaries)
              .WithOne(e => e.BestiaryType)
              .HasForeignKey(e => e.Type)
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(e => e.BestiarySubTypes)
              .WithOne(e => e.BestiaryType)
              .HasForeignKey(e => e.BestiaryTypeId)
              .OnDelete(DeleteBehavior.Cascade);

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

        entity.HasMany(e => e.CampaignData)
              .WithOne(e => e.Campaign)
              .HasForeignKey(e => e.CampaignId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<CampaignData>(entity =>
      {
        entity.HasKey(e => new { e.CampaignId, e.Key });

        entity.Property(e => e.Key)
              .HasMaxLength(500)
              .IsUnicode(false);

        entity.Property(e => e.Value)
              .HasColumnType("varchar(MAX)");

        entity.HasOne(e => e.Campaign)
              .WithMany(e => e.CampaignData)
              .IsRequired()
              .HasForeignKey(e => e.CampaignId)
              .OnDelete(DeleteBehavior.Cascade);

      });

      modelBuilder.Entity<Character>(entity =>
      {
        entity.HasKey(e => e.CharacterId);

        entity.HasOne(e => e.Player)
              .WithMany(e => e.Characters)
              .HasForeignKey(e => e.PlayerId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasOne(e => e.Campaign)
              .WithMany(e => e.Characters)
              .HasForeignKey(e => e.CampaignId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasOne(e => e.Race)
              .WithMany(e => e.Characters)
              .HasForeignKey(e => e.RaceId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasMany(e => e.CharacterClassLevels)
              .WithOne(e => e.Character)
              .HasForeignKey(e => e.CharacterId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.CharacterClassAbilities)
              .WithOne(e => e.Character)
              .HasForeignKey(e => e.CharacterId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.CharacterGear)
              .WithOne(e => e.Character)
              .HasForeignKey(e => e.CharacterId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.CharacterFeats)
              .WithOne(e => e.Character)
              .HasForeignKey(e => e.CharacterId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.CharacterSkills)
              .WithOne(e => e.Character)
              .HasForeignKey(e => e.CharacterId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.CharacterMagics)
              .WithOne(e => e.Character)
              .HasForeignKey(e => e.CharacterId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.CharacterRaceAbilities)
              .WithOne(e => e.Character)
              .HasForeignKey(e => e.CharacterId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.Property(e => e.Name)
              .HasMaxLength(100)
              .IsUnicode(false);

        entity.Property(e => e.Deity)
              .HasMaxLength(100)
              .IsUnicode(false);
      });

      modelBuilder.Entity<Class>(entity =>
      {
        entity.HasKey(e => e.ClassId);

        entity.Property(e => e.Name)
              .HasMaxLength(100)
              .IsUnicode(false);

        entity.Property(e => e.StartingGold)
              .HasMaxLength(100)
              .IsUnicode(false);

        entity.Property(e => e.Alignment)
              .HasMaxLength(100)
              .IsUnicode(false);

        entity.HasMany(e => e.ClassAbilities)
              .WithOne(e => e.Class)
              .HasForeignKey(e => e.ClassId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.ClassSkills)
              .WithOne(e => e.Class)
              .HasForeignKey(e => e.ClassId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.CharacterClassLevels)
              .WithOne(e => e.Class)
              .HasForeignKey(e => e.ClassId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.FavoredClasses)
              .WithOne(e => e.Class)
              .HasForeignKey(e => e.ClassId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<CharacterClassAbility>(entity =>
      {
        entity.HasKey(e => e.CharacterClassAbilityId);

        entity.Property(e => e.Note)
              .HasMaxLength(500)
              .IsUnicode(false);

        entity.HasOne(e => e.Character)
              .WithMany(e => e.CharacterClassAbilities)
              .IsRequired()
              .HasForeignKey(e => e.CharacterId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.ClassAbility)
              .WithMany(e => e.CharacterClassAbilities)
              .IsRequired()
              .HasForeignKey(e => e.ClassAbilityId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<CharacterClassLevel>(entity =>
      {
        entity.HasKey(e => new { e.CharacterId, e.ClassId });

        entity.HasOne(e => e.Character)
              .WithMany(e => e.CharacterClassLevels)
              .IsRequired()
              .HasForeignKey(e => e.CharacterId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Class)
              .WithMany(e => e.CharacterClassLevels)
              .IsRequired()
              .HasForeignKey(e => e.ClassId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<CharacterFeat>(entity =>
      {
        entity.HasKey(e => e.CharacterFeatId);

        entity.Property(e => e.Notes)
              .HasMaxLength(500)
              .IsUnicode(false);

        entity.HasOne(e => e.Character)
              .WithMany(e => e.CharacterFeats)
              .IsRequired()
              .HasForeignKey(e => e.CharacterId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Feat)
              .WithMany(e => e.CharacterFeats)
              .IsRequired()
              .HasForeignKey(e => e.FeatId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<CharacterGear>(entity =>
      {
        entity.HasKey(e => e.CharacterGearId);

        entity.Property(e => e.Notes)
              .HasMaxLength(500)
              .IsUnicode(false);

        entity.HasOne(e => e.Character)
              .WithMany(e => e.CharacterGear)
              .IsRequired()
              .HasForeignKey(e => e.CharacterId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Gear)
              .WithMany(e => e.CharacterGear)
              .IsRequired()
              .HasForeignKey(e => e.GearId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.CharacterGearEnchantments)
              .WithOne(e => e.CharacterGear)
              .HasForeignKey(e => e.CharacterGearId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<CharacterGearEnchantment>(entity =>
      {
        entity.HasKey(key => new { key.CharacterGearId, key.EnchantmentId });

        entity.HasOne(e => e.CharacterGear)
              .WithMany(e => e.CharacterGearEnchantments)
              .IsRequired()
              .HasForeignKey(e => e.CharacterGearId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Enchantment)
              .WithMany(e => e.CharacterGearEnchantments)
              .IsRequired()
              .HasForeignKey(e => e.EnchantmentId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<CharacterLanguage>(entity =>
      {
        entity.HasKey(e => new { e.CharacterId, e.LanguageId });

        entity.HasOne(e => e.Character)
              .WithMany(e => e.CharacterLanguages)
              .IsRequired()
              .HasForeignKey(e => e.CharacterId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Language)
              .WithMany(e => e.CharacterLanguages)
              .IsRequired()
              .HasForeignKey(e => e.LanguageId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<CharacterMagic>(entity =>
      {
        entity.HasKey(e => e.CharacterMagicId);

        entity.HasOne(e => e.Character)
              .WithMany(e => e.CharacterMagics)
              .IsRequired()
              .HasForeignKey(e => e.CharacterId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Spell)
              .WithMany(e => e.CharacterMagics)
              .IsRequired()
              .HasForeignKey(e => e.SpellId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<CharacterRaceAbility>(entity =>
      {
        entity.HasKey(e => new { e.CharacterId, e.RaceAbilityId });

        entity.HasOne(e => e.Character)
              .WithMany(e => e.CharacterRaceAbilities)
              .IsRequired()
              .HasForeignKey(e => e.CharacterId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.RaceAbility)
              .WithMany(e => e.CharacterRaceAbilities)
              .IsRequired()
              .HasForeignKey(e => e.RaceAbilityId)
              .OnDelete(DeleteBehavior.Restrict);
      });

      modelBuilder.Entity<Race>(entity =>
      {
        entity.HasKey(e => e.RaceId);

        entity.HasOne(e => e.RaceBestiary)
              .WithMany(e => e.BestiaryRaces)
              .HasForeignKey(e => e.RaceTypeId)
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(e => e.RaceSubTypes)
              .WithOne(e => e.Race)
              .HasForeignKey(e => e.RaceId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.Characters)
              .WithOne(e => e.Race)
              .HasForeignKey(e => e.RaceId)
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(e => e.FavoredClasses)
              .WithOne(e => e.Race)
              .HasForeignKey(e => e.RaceId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<RaceAbility>(entity =>
      {
        entity.HasKey(e => e.RaceAbilityId);

        entity.HasOne(e => e.Race)
              .WithMany(e => e.RaceAbilities)
              .HasForeignKey(e => e.RaceId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.CharacterRaceAbilities)
              .WithOne(e => e.RaceAbility)
              .HasForeignKey(e => e.RaceAbilityId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<RaceSubType>(entity =>
      {
        entity.HasKey(e => new { e.BestiaryTypeId, e.RaceId });

        entity.HasOne(e => e.Race)
              .WithMany(e => e.RaceSubTypes)
              .IsRequired()
              .HasForeignKey(e => e.RaceId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.BestiaryType)
              .WithMany(e => e.RaceSubTypes)
              .IsRequired()
              .HasForeignKey(e => e.BestiaryTypeId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<CharacterSkill>(entity =>
      {
        entity.HasKey(e => new { e.CharacterId, e.SkillId });

        entity.HasOne(e => e.Character)
              .WithMany(e => e.CharacterSkills)
              .IsRequired()
              .HasForeignKey(e => e.CharacterId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Skill)
              .WithMany(e => e.CharacterSkills)
              .IsRequired()
              .HasForeignKey(e => e.SkillId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<ClassAbility>(entity =>
      {
        entity.HasKey(e => e.ClassAbilityId);

        entity.Property(e => e.Name)
              .HasMaxLength(100)
              .IsUnicode(false);

        entity.Property(e => e.Description)
              .IsUnicode(false);

        entity.HasOne(e => e.Class)
              .WithMany(e => e.ClassAbilities)
              .IsRequired()
              .HasForeignKey(e => e.ClassId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<ClassSkill>(entity =>
      {
        entity.HasKey(e => new { e.ClassId, e.SkillId });

        entity.HasOne(e => e.Class)
              .WithMany(e => e.ClassSkills)
              .IsRequired()
              .HasForeignKey(e => e.ClassId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Skill)
              .WithMany(e => e.ClassSkills)
              .IsRequired()
              .HasForeignKey(e => e.SkillId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Continent>(entity =>
      {
        entity.HasKey(e => e.ContinentId);

        entity.Property(e => e.MapPath)
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.HasOne(e => e.PrimaryLanguage)
              .WithMany(e => e.ContinentPrimaryLanguages)
              .HasForeignKey(e => e.PrimaryLanguageId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasMany(e => e.ContinentWeathers)
              .WithOne(e => e.Continent)
              .HasForeignKey(e => e.ContinentId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.Locations)
              .WithOne(e => e.Continent)
              .HasForeignKey(e => e.ContinentId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasMany(e => e.MonsterSpawns)
              .WithOne(e => e.Continent)
              .HasForeignKey(e => e.ContinentId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.Territories)
              .WithOne(e => e.Continent)
              .HasForeignKey(e => e.ContinentId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.ContinentEvents)
              .WithOne(e => e.Continent)
              .HasForeignKey(e => e.ContinentId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<ContinentEnvironment>(entity =>
      {
        entity.HasKey(e => new { e.ContinentId, e.EnvironmentId });

        entity.HasOne(e => e.Continent)
              .WithMany(e => e.ContinentEnvironments)
              .IsRequired()
              .HasForeignKey(e => e.ContinentId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Environment)
              .WithMany(e => e.ContinentEnvironments)
              .IsRequired()
              .HasForeignKey(e => e.EnvironmentId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<ContinentWeather>(entity =>
      {
        entity.HasKey(e => e.CWID);

        entity.Property(e => e.CWID)
            .HasColumnName("CWID")
            .ValueGeneratedNever();

        entity.Property(e => e.NextCWID).HasColumnName("NextCWID");

        entity.Property(e => e.ParentCWID).HasColumnName("ParentCWID");

        entity.Property(e => e.CWName)
              .HasColumnName("CWName")
              .HasMaxLength(100)
              .IsUnicode(false);

        entity.HasOne(e => e.Continent)
              .WithMany(e => e.ContinentWeathers)
              .IsRequired()
              .HasForeignKey(e => e.ContinentId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Weather)
              .WithMany(e => e.ContinentWeathers)
              .IsRequired()
              .HasForeignKey(e => e.WeatherId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Season)
              .WithMany(e => e.ContinentWeathers)
              .IsRequired()
              .HasForeignKey(e => e.SeasonId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Enchantment>(entity =>
      {
        entity.HasKey(e => e.EnchantmentId);

        entity.Property(e => e.Name)
              .HasMaxLength(100)
              .IsUnicode(false);

        entity.Property(e => e.Effect)
              .HasMaxLength(1000)
              .IsUnicode(false);

        entity.HasMany(e => e.CharacterGearEnchantments)
              .WithOne(e => e.Enchantment)
              .HasForeignKey(e => e.EnchantmentId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Environment>(entity =>
      {
        entity.HasKey(e => e.EnvironmentId);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Notes)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Temperature)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.HasMany(e => e.BestiaryEnvironments)
              .WithOne(e => e.Environment)
              .HasForeignKey(e => e.EnvironmentId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.Locations)
              .WithOne(e => e.Environment)
              .HasForeignKey(e => e.EnvironmentId)
              .OnDelete(DeleteBehavior.SetNull);
      });

      modelBuilder.Entity<Faction>(entity =>
      {
        entity.HasKey(e => e.FactionId);

        entity.Property(e => e.Name)
              .HasMaxLength(100)
              .IsUnicode(false);

        entity.Property(e => e.Type)
              .HasMaxLength(200)
              .IsUnicode(false);

        entity.HasOne(e => e.PrimaryLanguage)
              .WithMany(e => e.FactionPrimaryLanguages)
              .HasForeignKey(e => e.PrimaryLanguageId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasOne(e => e.PrimaryRace)
              .WithMany(e => e.FactionPrimaryRaces)
              .HasForeignKey(e => e.PrimaryRaceId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasMany(e => e.Locations)
              .WithOne(e => e.Faction)
              .HasForeignKey(e => e.FactionId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasMany(e => e.Territories)
              .WithOne(e => e.Faction)
              .HasForeignKey(e => e.FactionId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<FavoredClass>(entity =>
      {
        entity.HasKey(e => e.FavoredClassId);

        entity.Property(e => e.Description)
              .HasMaxLength(500)
              .IsUnicode(false);

        entity.HasOne(e => e.Class)
              .WithMany(e => e.FavoredClasses)
              .IsRequired()
              .HasForeignKey(e => e.ClassId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Race)
              .WithMany(e => e.FavoredClasses)
              .IsRequired()
              .HasForeignKey(e => e.RaceId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Feat>(entity =>
      {
        entity.HasKey(e => e.FeatId);

        entity.Property(e => e.Benefit)
              .IsUnicode(false);

        entity.Property(e => e.CompletionBenefit)
              .IsUnicode(false);

        entity.Property(e => e.Description)
              .HasMaxLength(500)
              .IsUnicode(false);

        entity.Property(e => e.Fulltext)
              .IsUnicode(false);

        entity.Property(e => e.Goal)
              .IsUnicode(false);

        entity.Property(e => e.Name)
              .HasMaxLength(100)
              .IsUnicode(false);

        entity.Property(e => e.Normal)
              .HasMaxLength(500)
              .IsUnicode(false);

        entity.Property(e => e.Note)
              .HasMaxLength(500)
              .IsUnicode(false);

        entity.Property(e => e.Prerequisites)
              .IsUnicode(false);

        entity.Property(e => e.PrerequisiteFeats)
              .HasMaxLength(200)
              .IsUnicode(false);

        entity.Property(e => e.PrerequisiteSkills)
              .HasMaxLength(250)
              .IsUnicode(false);

        entity.Property(e => e.Prerequisites)
              .IsUnicode(false);

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

        entity.HasMany(e => e.BestiaryFeats)
              .WithOne(e => e.Feat)
              .HasForeignKey(e => e.FeatId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.CharacterFeats)
              .WithOne(e => e.Feat)
              .HasForeignKey(e => e.FeatId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Gear>(entity =>
      {
        entity.HasKey(e => e.GearId);

        entity.Property(e => e.Name)
              .HasMaxLength(100)
              .IsUnicode(false);

        entity.Property(e => e.Cost)
              .HasColumnType("decimal(10,2)");

        entity.HasMany(e => e.CharacterGear)
              .WithOne(e => e.Gear)
              .HasForeignKey(e => e.GearId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Language>(entity =>
      {
        entity.HasKey(e => e.LanguageId);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.Notes)
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.HasMany(e => e.BestiaryLanguages)
              .WithOne(e => e.Language)
              .HasForeignKey(e => e.LanguageId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.CharacterLanguages)
              .WithOne(e => e.Language)
              .HasForeignKey(e => e.LanguageId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.ContinentPrimaryLanguages)
              .WithOne(e => e.PrimaryLanguage)
              .HasForeignKey(e => e.PrimaryLanguageId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasMany(e => e.FactionPrimaryLanguages)
              .WithOne(e => e.PrimaryLanguage)
              .HasForeignKey(e => e.PrimaryLanguageId)
              .OnDelete(DeleteBehavior.SetNull);
      });

      modelBuilder.Entity<Location>(entity =>
      {
        entity.HasKey(e => e.LocationId);

        entity.Property(e => e.Name)
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.HasOne(e => e.Continent)
              .WithMany(e => e.Locations)
              .HasForeignKey(e => e.ContinentId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasOne(e => e.Faction)
              .WithMany(e => e.Locations)
              .HasForeignKey(e => e.FactionId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasOne(e => e.Territory)
              .WithMany(e => e.Locations)
              .HasForeignKey(e => e.TerritoryId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasOne(e => e.Terrain)
              .WithMany(e => e.Locations)
              .HasForeignKey(e => e.TerrainId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasOne(e => e.Environment)
              .WithMany(e => e.Locations)
              .HasForeignKey(e => e.EnvironmentId)
              .OnDelete(DeleteBehavior.SetNull);
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

        entity.HasOne(e => e.Continent)
              .WithMany(e => e.MonsterSpawns)
              .IsRequired()
              .HasForeignKey(e => e.ContinentId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Season)
              .WithMany(e => e.MonsterSpawns)
              .IsRequired()
              .HasForeignKey(e => e.SeasonId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Bestiary)
              .WithMany(e => e.MonsterSpawns)
              .IsRequired()
              .HasForeignKey(e => e.BestiaryId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Time)
              .WithMany(e => e.MonsterSpawns)
              .IsRequired()
              .HasForeignKey(e => e.TimeId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Plane)
              .WithMany(e => e.MonsterSpawns)
              .IsRequired()
              .HasForeignKey(e => e.PlaneId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Month>(entity =>
      {
        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.HasOne(e => e.Season)
              .WithMany(e => e.Months)
              .HasForeignKey(e => e.SeasonId)
              .OnDelete(DeleteBehavior.SetNull);
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

        entity.HasOne(e => e.Npc)
              .WithOne(e => e.Npcdetail)
              .IsRequired()
              .HasForeignKey("Npcdetail", "Npcid")
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<OverridePrerequisite>(entity =>
      {
        entity.HasKey(e => new { e.ClassAbilityId, e.PrerequisiteId });

        entity.HasOne(e => e.ClassAbility)
              .WithMany(e => e.OverridePrerequisites)
              .IsRequired()
              .HasForeignKey(e => e.ClassAbilityId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Prerequisite)
              .WithMany(e => e.OverridePrerequisites)
              .IsRequired()
              .HasForeignKey(e => e.PrerequisiteId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Plane>(entity =>
      {
        entity.HasKey(e => e.PlaneId);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.HasMany(e => e.MonsterSpawns)
              .WithOne(e => e.Plane)
              .HasForeignKey(e => e.PlaneId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Player>(entity =>
      {
        entity.HasKey(e => e.PlayerId);

        entity.Property(e => e.DisplayName)
              .HasMaxLength(100)
              .IsUnicode(false)
              .IsRequired();

        entity.HasMany(e => e.PlayerCampaigns)
              .WithOne(e => e.Player)
              .HasForeignKey(e => e.PlayerId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.Characters)
              .WithOne(e => e.Player)
              .HasForeignKey(e => e.PlayerId)
              .OnDelete(DeleteBehavior.SetNull);
      });

      modelBuilder.Entity<PlayerCampaign>(entity =>
      {
        entity.HasKey(e => new { e.PlayerId, e.CampaignId });

        entity.HasOne(e => e.Player)
              .WithMany(e => e.PlayerCampaigns)
              .IsRequired()
              .HasForeignKey(e => e.PlayerId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Campaign)
              .WithMany(e => e.PlayerCampaigns)
              .IsRequired()
              .HasForeignKey(e => e.CampaignId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Prerequisite>(entity =>
      {
        entity.HasKey(e => e.PrerequisiteId);

        entity.HasMany(e => e.OverridePrerequisites)
              .WithOne(e => e.Prerequisite)
              .HasForeignKey(e => e.PrerequisiteId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Season>(entity =>
      {
        entity.HasKey(e => e.SeasonId);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.HasMany(e => e.MonsterSpawns)
              .WithOne(e => e.Season)
              .HasForeignKey(e => e.SeasonId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.Months)
              .WithOne(e => e.Season)
              .HasForeignKey(e => e.SeasonId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasMany(e => e.ContinentWeathers)
              .WithOne(e => e.Season)
              .HasForeignKey(e => e.SeasonId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Skill>(entity =>
      {
        entity.HasKey(e => e.SkillId);

        entity.Property(e => e.Option)
              .HasMaxLength(100)
              .IsUnicode(false);

        entity.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.HasMany(e => e.BestiarySkills)
              .WithOne(e => e.Skill)
              .HasForeignKey(e => e.SkillId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.CharacterSkills)
              .WithOne(e => e.Skill)
              .HasForeignKey(e => e.SkillId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.ClassSkills)
              .WithOne(e => e.Skill)
              .HasForeignKey(e => e.SkillId)
              .OnDelete(DeleteBehavior.Cascade);
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

        entity.HasOne(e => e.SpellSchool)
              .WithMany(e => e.Spells)
              .HasForeignKey(e => e.SchoolId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasOne(e => e.SpellSubSchool)
              .WithMany(e => e.Spells)
              .HasForeignKey(e => e.SubSchoolId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasMany(e => e.BestiaryMagics)
              .WithOne(e => e.Spell)
              .HasForeignKey(e => e.SpellId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.CharacterMagics)
              .WithOne(e => e.Spell)
              .HasForeignKey(e => e.SpellId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<SpellDetail>(entity =>
      {
        entity.HasKey(e => e.SpellId);

        entity.Property(e => e.Description).IsUnicode(false);

        entity.Property(e => e.DescriptionFormatted).IsUnicode(false);

        entity.Property(e => e.FullText).IsUnicode(false);

        entity.HasOne(e => e.Spell)
              .WithOne(e => e.SpellDetail)
              .IsRequired()
              .HasForeignKey("SpellDetail", "SpellId")
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<SpellSchool>(entity =>
      {
        entity.HasKey(e => e.SpellSchoolId);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.HasMany(e => e.Spells)
              .WithOne(e => e.SpellSchool)
              .HasForeignKey(e => e.SchoolId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasMany(e => e.SpellSubSchools)
              .WithOne(e => e.SpellSchool)
              .HasForeignKey(e => e.SpellSchoolId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<SpellSubSchool>(entity =>
      {
        entity.HasKey(e => e.SpellSubSchoolId);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.HasMany(e => e.Spells)
              .WithOne(e => e.SpellSubSchool)
              .HasForeignKey(e => e.SubSchoolId)
              .OnDelete(DeleteBehavior.SetNull);
      });

      modelBuilder.Entity<Terrain>(entity =>
      {
        entity.HasKey(e => e.TerrainId);

        entity.Property(e => e.Description)
            .HasMaxLength(500)
            .IsUnicode(false);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.Property(e => e.MovementModifier)
              .HasColumnType("decimal(5,2)");

        entity.HasMany(e => e.Locations)
              .WithOne(e => e.Terrain)
              .HasForeignKey(e => e.TerrainId)
              .OnDelete(DeleteBehavior.SetNull);
      });

      modelBuilder.Entity<Territory>(entity =>
      {
        entity.HasKey(e => e.TerritoryId);

        entity.Property(e => e.Name)
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

        entity.HasOne(e => e.Continent)
              .WithMany(e => e.Territories)
              .HasForeignKey(e => e.ContinentId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasOne(e => e.Faction)
              .WithMany(e => e.Territories)
              .HasForeignKey(e => e.FactionId)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasMany(e => e.Locations)
              .WithOne(e => e.Territory)
              .HasForeignKey(e => e.TerritoryId)
              .OnDelete(DeleteBehavior.SetNull);
      });

      modelBuilder.Entity<Time>(entity =>
      {
        entity.HasKey(e => e.TimeId);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        entity.HasMany(e => e.MonsterSpawns)
              .WithOne(e => e.Time)
              .HasForeignKey(e => e.TimeId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<TrackedEvent>(entity =>
      {
        entity.HasKey(e => e.TrackedEventId);

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

        entity.HasOne(e => e.Campaign)
              .WithMany(e => e.TrackedEvents)
              .HasForeignKey(e => e.CampaignId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Continent)
              .WithMany(e => e.ContinentEvents)
              .HasForeignKey(e => e.ContinentId)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Weapon>(entity =>
      {
        entity.ToTable("Weapon");

        entity.HasMany(e => e.WeaponAttributesApplied)
              .WithOne(e => e.Weapon)
              .HasForeignKey(e => e.GearId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<WeaponAttribute>(entity =>
      {
        entity.HasKey(e => e.WeaponAttributeId);

        entity.Property(e => e.Name)
              .IsRequired()
              .HasMaxLength(100)
              .IsUnicode(false);

        entity.Property(e => e.Effect)
              .HasMaxLength(200)
              .IsUnicode(false);

        entity.HasMany(e => e.WeaponAttributesApplied)
              .WithOne(e => e.Attribute)
              .HasForeignKey(e => e.WeaponAttributeId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<WeaponAttributeApplied>(entity =>
      {
        entity.HasKey(e => new { e.GearId, e.WeaponAttributeId });

        entity.HasOne(e => e.Weapon)
              .WithMany(e => e.WeaponAttributesApplied)
              .IsRequired()
              .HasForeignKey(e => e.GearId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Attribute)
              .WithMany(e => e.WeaponAttributesApplied)
              .IsRequired()
              .HasForeignKey(e => e.WeaponAttributeId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Weather>(entity =>
      {
        entity.HasKey(e => e.WeatherId);

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

        entity.HasMany(e => e.ContinentWeathers)
              .WithOne(e => e.Weather)
              .HasForeignKey(e => e.WeatherId)
              .OnDelete(DeleteBehavior.Cascade);
      });
    }
  }
}
