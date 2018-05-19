using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DBConnect.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BestiaryType",
                columns: table => new
                {
                    BestiaryTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestiaryType", x => x.BestiaryTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Campaign",
                columns: table => new
                {
                    CampaignId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CampaignName = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    CampaignNotes = table.Column<string>(unicode: false, maxLength: 2000, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign", x => x.CampaignId);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    ClassId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Alignment = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ArchetypeForClassId = table.Column<int>(nullable: false),
                    CastingStat = table.Column<int>(nullable: false),
                    HD = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    SkillPts = table.Column<int>(nullable: false),
                    StartingGold = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "Enchantment",
                columns: table => new
                {
                    EnchantmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bonus = table.Column<int>(nullable: false),
                    BonusType = table.Column<int>(nullable: false),
                    Effect = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    EnchantmentType = table.Column<int>(nullable: false),
                    EnhancementCost = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enchantment", x => x.EnchantmentId);
                });

            migrationBuilder.CreateTable(
                name: "Environment",
                columns: table => new
                {
                    EnvironmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Notes = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Temperature = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    TravelSpeed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Environment", x => x.EnvironmentId);
                });

            migrationBuilder.CreateTable(
                name: "Feat",
                columns: table => new
                {
                    FeatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArmorMastery = table.Column<bool>(nullable: false),
                    Benefit = table.Column<string>(unicode: false, nullable: true),
                    Betrayal = table.Column<bool>(nullable: false),
                    BloodHex = table.Column<bool>(nullable: false),
                    CompanionFamiliar = table.Column<bool>(nullable: false),
                    CompletionBenefit = table.Column<string>(unicode: false, nullable: true),
                    Critical = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Esoteric = table.Column<bool>(nullable: false),
                    Fulltext = table.Column<string>(unicode: false, nullable: true),
                    Goal = table.Column<string>(unicode: false, nullable: true),
                    Grit = table.Column<bool>(nullable: false),
                    ItemMastery = table.Column<bool>(nullable: false),
                    Multiples = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Normal = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Note = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Panache = table.Column<bool>(nullable: false),
                    Performance = table.Column<bool>(nullable: false),
                    PrerequisiteFeats = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    PrerequisiteSkills = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Prerequisites = table.Column<string>(unicode: false, nullable: true),
                    RaceName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Racial = table.Column<bool>(nullable: false),
                    ShieldMastery = table.Column<bool>(nullable: false),
                    Source = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Special = table.Column<string>(unicode: false, maxLength: 750, nullable: true),
                    Stare = table.Column<bool>(nullable: false),
                    Style = table.Column<bool>(nullable: false),
                    SuggestedTraits = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Targeting = table.Column<bool>(nullable: false),
                    Teamwork = table.Column<bool>(nullable: false),
                    Type_ = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    WeaponMastery = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feat", x => x.FeatId);
                });

            migrationBuilder.CreateTable(
                name: "Gear",
                columns: table => new
                {
                    ArmTyp = table.Column<int>(nullable: true),
                    ArmorBonus = table.Column<int>(nullable: true),
                    CheckPenalty = table.Column<int>(nullable: true),
                    MaxDex = table.Column<int>(nullable: true),
                    SpellFailure = table.Column<int>(nullable: true),
                    GearId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cost = table.Column<decimal>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    CritMin = table.Column<int>(nullable: true),
                    CritMult = table.Column<int>(nullable: true),
                    DamageType = table.Column<int>(nullable: true),
                    DmgDiceNum = table.Column<int>(nullable: true),
                    DmgDiceType = table.Column<int>(nullable: true),
                    Range = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gear", x => x.GearId);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    LanguageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Notes = table.Column<string>(unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.LanguageId);
                });

            migrationBuilder.CreateTable(
                name: "MagicItem",
                columns: table => new
                {
                    MagicItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Abjuration = table.Column<bool>(nullable: true),
                    AL = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Aura = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AuraStrength = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    BaseItem = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    Cha = table.Column<int>(nullable: true),
                    CL = table.Column<int>(nullable: true),
                    Communication = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Conjuration = table.Column<bool>(nullable: true),
                    Cost = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    CostValue = table.Column<int>(nullable: true),
                    Description = table.Column<string>(unicode: false, nullable: true),
                    Destruction = table.Column<string>(unicode: false, nullable: true),
                    Divination = table.Column<bool>(nullable: true),
                    Ego = table.Column<int>(nullable: true),
                    Enchantment = table.Column<bool>(nullable: true),
                    Evocation = table.Column<bool>(nullable: true),
                    FullText = table.Column<string>(unicode: false, nullable: true),
                    Group_ = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Illusion = table.Column<bool>(nullable: true),
                    Int = table.Column<int>(nullable: true),
                    Languages = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    LegendaryWeapon = table.Column<bool>(nullable: true),
                    LinkText = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    MagicItems = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    MajorArtifact = table.Column<bool>(nullable: true),
                    MinorArtifact = table.Column<bool>(nullable: true),
                    Mythic = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Necromancy = table.Column<bool>(nullable: true),
                    Powers = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Price = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    PriceValue = table.Column<int>(nullable: true),
                    Requirements = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Scaling = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Senses = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Slot = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Source = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Transmutation = table.Column<bool>(nullable: true),
                    Universal = table.Column<bool>(nullable: true),
                    Weight = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    WeightValue = table.Column<double>(nullable: true),
                    Wis = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagicItem", x => x.MagicItemId);
                });

            migrationBuilder.CreateTable(
                name: "NPC",
                columns: table => new
                {
                    NPCID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AbilityScoreMods = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    AbilityScores = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AC = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ACMods = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    AgeCategory = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Alignment = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    AlternateNameForm = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Aura = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    BaseAtk = table.Column<int>(nullable: true),
                    BaseStatistics = table.Column<string>(unicode: false, nullable: true),
                    BeforeCombat = table.Column<string>(unicode: false, nullable: true),
                    Bloodline = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Burrow = table.Column<bool>(nullable: true),
                    CharacterFlag = table.Column<bool>(nullable: true),
                    Class_ = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ClassArchetypes = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Climb = table.Column<bool>(nullable: true),
                    CMB = table.Column<int>(nullable: true),
                    CMD = table.Column<int>(nullable: true),
                    CompanionFamiliarLink = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    CompanionFlag = table.Column<bool>(nullable: true),
                    CR = table.Column<int>(nullable: true),
                    DefensiveAbilities = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    DescriptionVisual = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    DR = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    DuringCombat = table.Column<string>(unicode: false, nullable: true),
                    Environment = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    ExtractsPrepared = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Feats = table.Column<string>(unicode: false, nullable: true),
                    Fly = table.Column<bool>(nullable: true),
                    FocusedSchool = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Fort = table.Column<int>(nullable: true),
                    Gear = table.Column<string>(unicode: false, nullable: true),
                    Gender = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Group_ = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    HD = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    HP = table.Column<int>(nullable: true),
                    HPMods = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Immune = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Implements = table.Column<string>(unicode: false, nullable: true),
                    Init = table.Column<int>(nullable: true),
                    IsTemplate = table.Column<bool>(nullable: true),
                    KineticistWildTalents = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Land = table.Column<bool>(nullable: true),
                    Languages = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    LinkText = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Melee = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Morale = table.Column<string>(unicode: false, nullable: true),
                    MR = table.Column<int>(nullable: true),
                    MT = table.Column<int>(nullable: true),
                    Mystery = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Mythic = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Note = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    OffenseNote = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    Organization = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    OtherGear = table.Column<string>(unicode: false, nullable: true),
                    Patron = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ProhibitedSchools = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    PsychicDiscipline = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    PsychicMagic = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Race = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    RacialMods = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Ranged = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Reach = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ref_ = table.Column<int>(nullable: true),
                    Resist = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    SaveMods = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    Saves = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Senses = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    Size_ = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Skills = table.Column<string>(unicode: false, nullable: true),
                    Source = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Space_ = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    SpecialAbilities = table.Column<string>(unicode: false, nullable: true),
                    SpecialAttacks = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Speed = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    SpeedMod = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    SpellDomains = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    SpellLikeAbilities = table.Column<string>(unicode: false, maxLength: 700, nullable: true),
                    SpellsKnown = table.Column<string>(unicode: false, nullable: true),
                    SpellsPrepared = table.Column<string>(unicode: false, nullable: true),
                    Spirit = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    SQ = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    SR = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SubType_ = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Swim = table.Column<bool>(nullable: true),
                    TemplatesApplied = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ThassilonianSpecialization = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Traits = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Treasure = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Type_ = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    UniqueMonster = table.Column<bool>(nullable: true),
                    Variant = table.Column<bool>(nullable: true),
                    Vulnerability = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Weaknesses = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Will = table.Column<int>(nullable: true),
                    XP = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPC", x => x.NPCID);
                });

            migrationBuilder.CreateTable(
                name: "Plane",
                columns: table => new
                {
                    PlaneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plane", x => x.PlaneId);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisplayName = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    SeasonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    SeasonOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.SeasonId);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SkillId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Stat = table.Column<int>(nullable: false),
                    TrainedOnly = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SkillId);
                });

            migrationBuilder.CreateTable(
                name: "SpellSchool",
                columns: table => new
                {
                    SpellSchoolId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellSchool", x => x.SpellSchoolId);
                });

            migrationBuilder.CreateTable(
                name: "Terrain",
                columns: table => new
                {
                    TerrainId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    IsBroken = table.Column<bool>(nullable: false),
                    IsRough = table.Column<bool>(nullable: false),
                    IsUnderground = table.Column<bool>(nullable: false),
                    IsWater = table.Column<bool>(nullable: false),
                    MovementModifier = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terrain", x => x.TerrainId);
                });

            migrationBuilder.CreateTable(
                name: "Time",
                columns: table => new
                {
                    TimeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsNight = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    TimeOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Time", x => x.TimeId);
                });

            migrationBuilder.CreateTable(
                name: "WeaponAttribute",
                columns: table => new
                {
                    WpnAttId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Effect = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponAttribute", x => x.WpnAttId);
                });

            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    WeatherId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColdDanger = table.Column<bool>(nullable: false),
                    ColdLethal = table.Column<bool>(nullable: false),
                    Deadly = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    Effects = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    Flooding = table.Column<bool>(nullable: false),
                    HeatDanger = table.Column<bool>(nullable: false),
                    HeatLethal = table.Column<bool>(nullable: false),
                    HighWind = table.Column<bool>(nullable: false),
                    Magical = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    VisionObscured = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.WeatherId);
                });

            migrationBuilder.CreateTable(
                name: "Bestiary",
                columns: table => new
                {
                    BestiaryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AbilityScoreMods = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    AbilityScores = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AC = table.Column<int>(nullable: false),
                    ACFlat = table.Column<int>(nullable: false),
                    ACMods = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    ACTouch = table.Column<int>(nullable: false),
                    AgeCategory = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Alignment = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    AlternateNameForm = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Aura = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    BaseAtk = table.Column<int>(nullable: false),
                    BaseStatistics = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    BeforeCombat = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Bloodline = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Burrow = table.Column<int>(nullable: false),
                    CHA = table.Column<int>(nullable: false),
                    CharacterFlag = table.Column<bool>(nullable: false),
                    Class = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ClassArchetypes = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Climb = table.Column<int>(nullable: false),
                    CMB = table.Column<int>(nullable: false),
                    CMD = table.Column<int>(nullable: false),
                    CompanionFamiliarLink = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    CompanionFlag = table.Column<bool>(nullable: false),
                    CON = table.Column<int>(nullable: false),
                    CR = table.Column<int>(nullable: false),
                    DefensiveAbilities = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    DescriptionVisual = table.Column<string>(unicode: false, nullable: true),
                    DEX = table.Column<int>(nullable: false),
                    DontUseRacialHD = table.Column<bool>(nullable: false),
                    DR = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    DuringCombat = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Environment = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    ExtractsPrepared = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Feats = table.Column<string>(unicode: false, nullable: true),
                    Fly = table.Column<int>(nullable: false),
                    FocusedSchool = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Fortitude = table.Column<int>(nullable: false),
                    Gear = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Gender = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Group = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    HD = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    HP = table.Column<int>(nullable: false),
                    HPMods = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Immune = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Init = table.Column<int>(nullable: false),
                    INT = table.Column<int>(nullable: false),
                    IsTemplate = table.Column<bool>(nullable: false),
                    Land = table.Column<int>(nullable: false),
                    Languages = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    LinkText = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Melee = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Morale = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    MR = table.Column<int>(nullable: false),
                    MT = table.Column<int>(nullable: false),
                    Mystery = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Mythic = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Note = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    OffenseNote = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Organization = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    OtherGear = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Patron = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    ProhibitedSchools = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Race = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    RacialMods = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Ranged = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Reach = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Reflex = table.Column<int>(nullable: false),
                    Resist = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    SaveMods = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Senses = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Size = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Skills = table.Column<string>(unicode: false, nullable: true),
                    Space = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SpecialAbilities = table.Column<string>(unicode: false, nullable: true),
                    SpecialAttacks = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Speed = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    SpeedMod = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    SpellDomains = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SpellLikeAbilities = table.Column<string>(unicode: false, nullable: true),
                    SpellsKnown = table.Column<string>(unicode: false, nullable: true),
                    SpellsPrepared = table.Column<string>(unicode: false, nullable: true),
                    SQ = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    SR = table.Column<int>(nullable: false),
                    StatisticsNote = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    STR = table.Column<int>(nullable: false),
                    SubType = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Swim = table.Column<int>(nullable: false),
                    TemplatesApplied = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Traits = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Treasure = table.Column<string>(unicode: false, nullable: true),
                    Type = table.Column<int>(unicode: false, maxLength: 100, nullable: true),
                    UniqueMonster = table.Column<bool>(nullable: false),
                    VariantParent = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Vulnerability = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Weaknesses = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Will = table.Column<int>(nullable: false),
                    WIS = table.Column<int>(nullable: false),
                    XP = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestiary", x => x.BestiaryId);
                    table.ForeignKey(
                        name: "FK_Bestiary_BestiaryType_Type",
                        column: x => x.Type,
                        principalTable: "BestiaryType",
                        principalColumn: "BestiaryTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CampaignData",
                columns: table => new
                {
                    CampaignId = table.Column<int>(nullable: false),
                    Key = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    Value = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignData", x => new { x.CampaignId, x.Key });
                    table.ForeignKey(
                        name: "FK_CampaignData_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrackedEvent",
                columns: table => new
                {
                    TrackedEventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CampaignId = table.Column<int>(nullable: false),
                    DateLastOccurred = table.Column<string>(nullable: true),
                    DateOccurring = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Notes = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    ReoccurFreq = table.Column<int>(unicode: false, maxLength: 100, nullable: false),
                    TrackedEventData = table.Column<string>(nullable: true),
                    TrackedEventType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackedEvent", x => x.TrackedEventId);
                    table.ForeignKey(
                        name: "FK_TrackedEvent_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassAbility",
                columns: table => new
                {
                    ClassAbilityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClassId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(unicode: false, nullable: true),
                    LevelRequirement = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ReplacesAbilityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassAbility", x => x.ClassAbilityId);
                    table.ForeignKey(
                        name: "FK_ClassAbility_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Continent",
                columns: table => new
                {
                    ContinentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsUnderground = table.Column<bool>(nullable: false),
                    IsWater = table.Column<bool>(nullable: false),
                    MapPath = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    PrimaryLanguageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continent", x => x.ContinentId);
                    table.ForeignKey(
                        name: "FK_Continent_Language_PrimaryLanguageId",
                        column: x => x.PrimaryLanguageId,
                        principalTable: "Language",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "NPCDetail",
                columns: table => new
                {
                    NPCID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(unicode: false, nullable: true),
                    FullText = table.Column<string>(unicode: false, nullable: true),
                    MonsterSource = table.Column<string>(unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPCDetail", x => x.NPCID);
                    table.ForeignKey(
                        name: "FK_NPCDetail_NPC_NPCID",
                        column: x => x.NPCID,
                        principalTable: "NPC",
                        principalColumn: "NPCID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerCampaign",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    CampaignId = table.Column<int>(nullable: false),
                    IsDM = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCampaign", x => new { x.PlayerId, x.CampaignId });
                    table.ForeignKey(
                        name: "FK_PlayerCampaign_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCampaign_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Month",
                columns: table => new
                {
                    MonthId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Days = table.Column<int>(nullable: false),
                    MonthOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    SeasonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Month", x => x.MonthId);
                    table.ForeignKey(
                        name: "FK_Month_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ClassSkill",
                columns: table => new
                {
                    ClassId = table.Column<int>(nullable: false),
                    SkillId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSkill", x => new { x.ClassId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_ClassSkill_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prerequisite",
                columns: table => new
                {
                    PrerequisiteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    FeatId = table.Column<int>(nullable: true),
                    SkillId = table.Column<int>(nullable: true),
                    Stat = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prerequisite", x => x.PrerequisiteId);
                    table.ForeignKey(
                        name: "FK_Prerequisite_Feat_FeatId",
                        column: x => x.FeatId,
                        principalTable: "Feat",
                        principalColumn: "FeatId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prerequisite_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpellSubSchool",
                columns: table => new
                {
                    SpellSubSchoolId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    SpellSchoolId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellSubSchool", x => x.SpellSubSchoolId);
                    table.ForeignKey(
                        name: "FK_SpellSubSchool_SpellSchool_SpellSchoolId",
                        column: x => x.SpellSchoolId,
                        principalTable: "SpellSchool",
                        principalColumn: "SpellSchoolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeaponAttributeApplied",
                columns: table => new
                {
                    GearId = table.Column<int>(nullable: false),
                    WeaponAttributeId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponAttributeApplied", x => new { x.GearId, x.WeaponAttributeId });
                    table.ForeignKey(
                        name: "FK_WeaponAttributeApplied_Gear_GearId",
                        column: x => x.GearId,
                        principalTable: "Gear",
                        principalColumn: "GearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeaponAttributeApplied_WeaponAttribute_WeaponAttributeId",
                        column: x => x.WeaponAttributeId,
                        principalTable: "WeaponAttribute",
                        principalColumn: "WpnAttId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BestiaryDetail",
                columns: table => new
                {
                    BestiaryId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(unicode: false, nullable: true),
                    FullText = table.Column<string>(unicode: false, nullable: true),
                    MonsterSource = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Source = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestiaryDetail", x => x.BestiaryId);
                    table.ForeignKey(
                        name: "FK_BestiaryDetail_Bestiary_BestiaryId",
                        column: x => x.BestiaryId,
                        principalTable: "Bestiary",
                        principalColumn: "BestiaryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BestiaryEnvironment",
                columns: table => new
                {
                    BestiaryEnvironmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BestiaryId = table.Column<int>(nullable: false),
                    EnvironmentId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestiaryEnvironment", x => x.BestiaryEnvironmentId);
                    table.ForeignKey(
                        name: "FK_BestiaryEnvironment_Bestiary_BestiaryId",
                        column: x => x.BestiaryId,
                        principalTable: "Bestiary",
                        principalColumn: "BestiaryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestiaryEnvironment_Environment_EnvironmentId",
                        column: x => x.EnvironmentId,
                        principalTable: "Environment",
                        principalColumn: "EnvironmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BestiaryFeat",
                columns: table => new
                {
                    BestiaryFeatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BestiaryId = table.Column<int>(nullable: false),
                    FeatId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestiaryFeat", x => x.BestiaryFeatId);
                    table.ForeignKey(
                        name: "FK_BestiaryFeat_Bestiary_BestiaryId",
                        column: x => x.BestiaryId,
                        principalTable: "Bestiary",
                        principalColumn: "BestiaryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestiaryFeat_Feat_FeatId",
                        column: x => x.FeatId,
                        principalTable: "Feat",
                        principalColumn: "FeatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BestiaryLanguage",
                columns: table => new
                {
                    BestiaryLanguageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BestiaryId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestiaryLanguage", x => x.BestiaryLanguageId);
                    table.ForeignKey(
                        name: "FK_BestiaryLanguage_Bestiary_BestiaryId",
                        column: x => x.BestiaryId,
                        principalTable: "Bestiary",
                        principalColumn: "BestiaryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestiaryLanguage_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BestiarySkill",
                columns: table => new
                {
                    BestiarySkillId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BestiaryId = table.Column<int>(nullable: false),
                    Bonus = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    SkillId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestiarySkill", x => x.BestiarySkillId);
                    table.ForeignKey(
                        name: "FK_BestiarySkill_Bestiary_BestiaryId",
                        column: x => x.BestiaryId,
                        principalTable: "Bestiary",
                        principalColumn: "BestiaryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestiarySkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BestiarySubType",
                columns: table => new
                {
                    BestiarySubTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BestiaryId = table.Column<int>(nullable: false),
                    BestiaryTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestiarySubType", x => x.BestiarySubTypeId);
                    table.ForeignKey(
                        name: "FK_BestiarySubType_Bestiary_BestiaryId",
                        column: x => x.BestiaryId,
                        principalTable: "Bestiary",
                        principalColumn: "BestiaryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestiarySubType_BestiaryType_BestiaryTypeId",
                        column: x => x.BestiaryTypeId,
                        principalTable: "BestiaryType",
                        principalColumn: "BestiaryTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    RaceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BestiaryId = table.Column<int>(nullable: true),
                    ModCHA = table.Column<int>(nullable: false),
                    ModCON = table.Column<int>(nullable: false),
                    ModDEX = table.Column<int>(nullable: false),
                    ModINT = table.Column<int>(nullable: false),
                    ModSTR = table.Column<int>(nullable: false),
                    ModWIS = table.Column<int>(nullable: false),
                    RP = table.Column<int>(nullable: false),
                    RaceTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.RaceId);
                    table.ForeignKey(
                        name: "FK_Race_Bestiary_RaceTypeId",
                        column: x => x.RaceTypeId,
                        principalTable: "Bestiary",
                        principalColumn: "BestiaryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContinentWeather",
                columns: table => new
                {
                    CWID = table.Column<int>(nullable: false),
                    CWName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ContinentId = table.Column<int>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    NextCWID = table.Column<int>(nullable: true),
                    ParentCWID = table.Column<int>(nullable: true),
                    RandomDuration = table.Column<bool>(nullable: false),
                    SeasonId = table.Column<int>(nullable: false),
                    WeatherId = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContinentWeather", x => x.CWID);
                    table.ForeignKey(
                        name: "FK_ContinentWeather_Continent_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continent",
                        principalColumn: "ContinentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContinentWeather_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContinentWeather_Weather_WeatherId",
                        column: x => x.WeatherId,
                        principalTable: "Weather",
                        principalColumn: "WeatherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonsterSpawn",
                columns: table => new
                {
                    SpawnId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BestiaryId = table.Column<int>(nullable: false),
                    ContinentId = table.Column<int>(nullable: false),
                    PlaneId = table.Column<int>(nullable: false),
                    SeasonId = table.Column<int>(nullable: false),
                    TimeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonsterSpawn", x => x.SpawnId);
                    table.ForeignKey(
                        name: "FK_MonsterSpawn_Bestiary_BestiaryId",
                        column: x => x.BestiaryId,
                        principalTable: "Bestiary",
                        principalColumn: "BestiaryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonsterSpawn_Continent_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continent",
                        principalColumn: "ContinentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonsterSpawn_Plane_PlaneId",
                        column: x => x.PlaneId,
                        principalTable: "Plane",
                        principalColumn: "PlaneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonsterSpawn_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonsterSpawn_Time_TimeId",
                        column: x => x.TimeId,
                        principalTable: "Time",
                        principalColumn: "TimeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spell",
                columns: table => new
                {
                    SpellId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Acid = table.Column<bool>(nullable: false),
                    Adept = table.Column<int>(nullable: true),
                    Air = table.Column<bool>(nullable: false),
                    Alchemist = table.Column<int>(nullable: true),
                    Antipaladin = table.Column<int>(nullable: true),
                    Area = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Augmented = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Bard = table.Column<int>(nullable: true),
                    BloodRager = table.Column<int>(nullable: true),
                    Bloodline = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    CastingTime = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Chaotic = table.Column<bool>(nullable: false),
                    Cleric = table.Column<int>(nullable: true),
                    Cold = table.Column<bool>(nullable: false),
                    Components = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    CostlyComponents = table.Column<bool>(nullable: false),
                    Curse = table.Column<bool>(nullable: false),
                    Darkness = table.Column<bool>(nullable: false),
                    Death = table.Column<bool>(nullable: false),
                    Deity = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    Descriptor = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Disease = table.Column<bool>(nullable: false),
                    Dismissable = table.Column<bool>(nullable: false),
                    DivineFocus = table.Column<bool>(nullable: false),
                    Domain = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Druid = table.Column<int>(nullable: true),
                    Duration = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Earth = table.Column<bool>(nullable: false),
                    Effect = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Electricity = table.Column<bool>(nullable: false),
                    Emotion = table.Column<bool>(nullable: false),
                    Evil = table.Column<bool>(nullable: false),
                    Fear = table.Column<bool>(nullable: false),
                    Fire = table.Column<bool>(nullable: false),
                    Focus = table.Column<bool>(nullable: false),
                    Force = table.Column<bool>(nullable: false),
                    Good = table.Column<bool>(nullable: false),
                    HauntStatistics = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Hunter = table.Column<int>(nullable: true),
                    Inquisitor = table.Column<int>(nullable: true),
                    Investigator = table.Column<int>(nullable: true),
                    LanguageDependent = table.Column<bool>(nullable: false),
                    Lawful = table.Column<bool>(nullable: false),
                    Light = table.Column<bool>(nullable: false),
                    LinkText = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Magus = table.Column<int>(nullable: true),
                    Material = table.Column<bool>(nullable: false),
                    MaterialCost = table.Column<int>(nullable: false),
                    Medium = table.Column<int>(nullable: true),
                    Mesmerist = table.Column<int>(nullable: true),
                    MindAffecting = table.Column<bool>(nullable: false),
                    Mythic = table.Column<bool>(nullable: false),
                    MythicText = table.Column<string>(unicode: false, nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Occultist = table.Column<int>(nullable: true),
                    Oracle = table.Column<int>(nullable: true),
                    Pain = table.Column<bool>(nullable: false),
                    Paladin = table.Column<int>(nullable: true),
                    Patron = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Poison = table.Column<bool>(nullable: false),
                    Psychic = table.Column<int>(nullable: true),
                    Range = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Ranger = table.Column<int>(nullable: true),
                    SavingThrow = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    SchoolId = table.Column<int>(unicode: false, maxLength: 25, nullable: true),
                    Shadow = table.Column<bool>(nullable: false),
                    Shaman = table.Column<int>(nullable: true),
                    Shapeable = table.Column<bool>(nullable: false),
                    ShortDescription = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Skald = table.Column<int>(nullable: true),
                    SLALevel = table.Column<int>(nullable: false),
                    Somatic = table.Column<bool>(nullable: false),
                    Sonic = table.Column<bool>(nullable: false),
                    Sor = table.Column<int>(nullable: true),
                    Source = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SpellLevel = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    SpellResistance = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Spiritualist = table.Column<int>(nullable: true),
                    SubSchoolId = table.Column<int>(unicode: false, maxLength: 25, nullable: true),
                    Summoner = table.Column<int>(nullable: true),
                    Targets = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Verbal = table.Column<bool>(nullable: false),
                    Water = table.Column<bool>(nullable: false),
                    Witch = table.Column<int>(nullable: true),
                    Wiz = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spell", x => x.SpellId);
                    table.ForeignKey(
                        name: "FK_Spell_SpellSchool_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "SpellSchool",
                        principalColumn: "SpellSchoolId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Spell_SpellSubSchool_SubSchoolId",
                        column: x => x.SubSchoolId,
                        principalTable: "SpellSubSchool",
                        principalColumn: "SpellSubSchoolId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CampaignId = table.Column<int>(nullable: true),
                    Deity = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    PlayerId = table.Column<int>(nullable: true),
                    RaceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_Character_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Character_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Character_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faction",
                columns: table => new
                {
                    FactionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ParentFactionId = table.Column<int>(nullable: true),
                    PrimaryLanguageId = table.Column<int>(nullable: true),
                    PrimaryRaceId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faction", x => x.FactionId);
                    table.ForeignKey(
                        name: "FK_Faction_Language_PrimaryLanguageId",
                        column: x => x.PrimaryLanguageId,
                        principalTable: "Language",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Faction_Race_PrimaryRaceId",
                        column: x => x.PrimaryRaceId,
                        principalTable: "Race",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "FavoredClass",
                columns: table => new
                {
                    FavoredClassId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClassId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    RaceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoredClass", x => x.FavoredClassId);
                    table.ForeignKey(
                        name: "FK_FavoredClass_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoredClass_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceSubType",
                columns: table => new
                {
                    BestiaryTypeId = table.Column<int>(nullable: false),
                    RaceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceSubType", x => new { x.BestiaryTypeId, x.RaceId });
                    table.ForeignKey(
                        name: "FK_RaceSubType_BestiaryType_BestiaryTypeId",
                        column: x => x.BestiaryTypeId,
                        principalTable: "BestiaryType",
                        principalColumn: "BestiaryTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceSubType_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BestiaryMagic",
                columns: table => new
                {
                    BestiaryMagicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BestiaryId = table.Column<int>(nullable: false),
                    CasterLevel = table.Column<int>(nullable: false),
                    MagicTypeId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    SpellId = table.Column<int>(nullable: false),
                    UsesPerDay = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestiaryMagic", x => x.BestiaryMagicId);
                    table.ForeignKey(
                        name: "FK_BestiaryMagic_Bestiary_BestiaryId",
                        column: x => x.BestiaryId,
                        principalTable: "Bestiary",
                        principalColumn: "BestiaryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestiaryMagic_Spell_SpellId",
                        column: x => x.SpellId,
                        principalTable: "Spell",
                        principalColumn: "SpellId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpellDetail",
                columns: table => new
                {
                    SpellId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(unicode: false, nullable: true),
                    DescriptionFormatted = table.Column<string>(unicode: false, nullable: true),
                    FullText = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellDetail", x => x.SpellId);
                    table.ForeignKey(
                        name: "FK_SpellDetail_Spell_SpellId",
                        column: x => x.SpellId,
                        principalTable: "Spell",
                        principalColumn: "SpellId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterClassAbility",
                columns: table => new
                {
                    CharacterClassAbilityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CharacterId = table.Column<int>(nullable: false),
                    ClassAbilityId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterClassAbility", x => x.CharacterClassAbilityId);
                    table.ForeignKey(
                        name: "FK_CharacterClassAbility_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterClassAbility_ClassAbility_ClassAbilityId",
                        column: x => x.ClassAbilityId,
                        principalTable: "ClassAbility",
                        principalColumn: "ClassAbilityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterClassLevel",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    ClassId = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterClassLevel", x => new { x.CharacterId, x.ClassId });
                    table.ForeignKey(
                        name: "FK_CharacterClassLevel_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterClassLevel_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterFeat",
                columns: table => new
                {
                    CharacterFeatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CharacterId = table.Column<int>(nullable: false),
                    FeatId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterFeat", x => x.CharacterFeatId);
                    table.ForeignKey(
                        name: "FK_CharacterFeat_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterFeat_Feat_FeatId",
                        column: x => x.FeatId,
                        principalTable: "Feat",
                        principalColumn: "FeatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterGear",
                columns: table => new
                {
                    CharacterGearId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CharacterId = table.Column<int>(nullable: false),
                    GearId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterGear", x => x.CharacterGearId);
                    table.ForeignKey(
                        name: "FK_CharacterGear_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterGear_Gear_GearId",
                        column: x => x.GearId,
                        principalTable: "Gear",
                        principalColumn: "GearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterLanguage",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterLanguage", x => new { x.CharacterId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_CharacterLanguage_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterLanguage_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterMagic",
                columns: table => new
                {
                    CharacterMagicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CharacterId = table.Column<int>(nullable: false),
                    MagicType = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    SpellId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMagic", x => x.CharacterMagicId);
                    table.ForeignKey(
                        name: "FK_CharacterMagic_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMagic_Spell_SpellId",
                        column: x => x.SpellId,
                        principalTable: "Spell",
                        principalColumn: "SpellId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterSkill",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    SkillId = table.Column<int>(nullable: false),
                    Ranks = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkill", x => new { x.CharacterId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_CharacterSkill_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Territory",
                columns: table => new
                {
                    TerritoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContinentId = table.Column<int>(nullable: true),
                    FactionId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Territory", x => x.TerritoryId);
                    table.ForeignKey(
                        name: "FK_Territory_Continent_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continent",
                        principalColumn: "ContinentId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Territory_Faction_FactionId",
                        column: x => x.FactionId,
                        principalTable: "Faction",
                        principalColumn: "FactionId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CharacterGearEnchantment",
                columns: table => new
                {
                    CharacterGearId = table.Column<int>(nullable: false),
                    EnchantmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterGearEnchantment", x => new { x.CharacterGearId, x.EnchantmentId });
                    table.ForeignKey(
                        name: "FK_CharacterGearEnchantment_CharacterGear_CharacterGearId",
                        column: x => x.CharacterGearId,
                        principalTable: "CharacterGear",
                        principalColumn: "CharacterGearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterGearEnchantment_Enchantment_EnchantmentId",
                        column: x => x.EnchantmentId,
                        principalTable: "Enchantment",
                        principalColumn: "EnchantmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContinentId = table.Column<int>(nullable: true),
                    EnvironmentId = table.Column<int>(nullable: true),
                    FactionId = table.Column<int>(nullable: true),
                    GridX = table.Column<int>(nullable: false),
                    GridY = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    TerrainId = table.Column<int>(nullable: true),
                    TerritoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Location_Continent_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continent",
                        principalColumn: "ContinentId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Location_Environment_EnvironmentId",
                        column: x => x.EnvironmentId,
                        principalTable: "Environment",
                        principalColumn: "EnvironmentId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Location_Faction_FactionId",
                        column: x => x.FactionId,
                        principalTable: "Faction",
                        principalColumn: "FactionId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Location_Terrain_TerrainId",
                        column: x => x.TerrainId,
                        principalTable: "Terrain",
                        principalColumn: "TerrainId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Location_Territory_TerritoryId",
                        column: x => x.TerritoryId,
                        principalTable: "Territory",
                        principalColumn: "TerritoryId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bestiary_Type",
                table: "Bestiary",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_BestiaryEnvironment_BestiaryId",
                table: "BestiaryEnvironment",
                column: "BestiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_BestiaryEnvironment_EnvironmentId",
                table: "BestiaryEnvironment",
                column: "EnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BestiaryFeat_BestiaryId",
                table: "BestiaryFeat",
                column: "BestiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_BestiaryFeat_FeatId",
                table: "BestiaryFeat",
                column: "FeatId");

            migrationBuilder.CreateIndex(
                name: "IX_BestiaryLanguage_BestiaryId",
                table: "BestiaryLanguage",
                column: "BestiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_BestiaryLanguage_LanguageId",
                table: "BestiaryLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_BestiaryMagic_BestiaryId",
                table: "BestiaryMagic",
                column: "BestiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_BestiaryMagic_SpellId",
                table: "BestiaryMagic",
                column: "SpellId");

            migrationBuilder.CreateIndex(
                name: "IX_BestiarySkill_BestiaryId",
                table: "BestiarySkill",
                column: "BestiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_BestiarySkill_SkillId",
                table: "BestiarySkill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_BestiarySubType_BestiaryId",
                table: "BestiarySubType",
                column: "BestiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_BestiarySubType_BestiaryTypeId",
                table: "BestiarySubType",
                column: "BestiaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_CampaignId",
                table: "Character",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_PlayerId",
                table: "Character",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_RaceId",
                table: "Character",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterClassAbility_CharacterId",
                table: "CharacterClassAbility",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterClassAbility_ClassAbilityId",
                table: "CharacterClassAbility",
                column: "ClassAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterClassLevel_ClassId",
                table: "CharacterClassLevel",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterFeat_CharacterId",
                table: "CharacterFeat",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterFeat_FeatId",
                table: "CharacterFeat",
                column: "FeatId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterGear_CharacterId",
                table: "CharacterGear",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterGear_GearId",
                table: "CharacterGear",
                column: "GearId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterGearEnchantment_EnchantmentId",
                table: "CharacterGearEnchantment",
                column: "EnchantmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterLanguage_LanguageId",
                table: "CharacterLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMagic_CharacterId",
                table: "CharacterMagic",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMagic_SpellId",
                table: "CharacterMagic",
                column: "SpellId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkill_SkillId",
                table: "CharacterSkill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassAbility_ClassId",
                table: "ClassAbility",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSkill_SkillId",
                table: "ClassSkill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Continent_PrimaryLanguageId",
                table: "Continent",
                column: "PrimaryLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ContinentWeather_ContinentId",
                table: "ContinentWeather",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContinentWeather_SeasonId",
                table: "ContinentWeather",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ContinentWeather_WeatherId",
                table: "ContinentWeather",
                column: "WeatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Faction_PrimaryLanguageId",
                table: "Faction",
                column: "PrimaryLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Faction_PrimaryRaceId",
                table: "Faction",
                column: "PrimaryRaceId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoredClass_ClassId",
                table: "FavoredClass",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoredClass_RaceId",
                table: "FavoredClass",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_ContinentId",
                table: "Location",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_EnvironmentId",
                table: "Location",
                column: "EnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_FactionId",
                table: "Location",
                column: "FactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_TerrainId",
                table: "Location",
                column: "TerrainId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_TerritoryId",
                table: "Location",
                column: "TerritoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MonsterSpawn_BestiaryId",
                table: "MonsterSpawn",
                column: "BestiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_MonsterSpawn_ContinentId",
                table: "MonsterSpawn",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_MonsterSpawn_PlaneId",
                table: "MonsterSpawn",
                column: "PlaneId");

            migrationBuilder.CreateIndex(
                name: "IX_MonsterSpawn_SeasonId",
                table: "MonsterSpawn",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_MonsterSpawn_TimeId",
                table: "MonsterSpawn",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Month_SeasonId",
                table: "Month",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCampaign_CampaignId",
                table: "PlayerCampaign",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Prerequisite_FeatId",
                table: "Prerequisite",
                column: "FeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Prerequisite_SkillId",
                table: "Prerequisite",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Race_RaceTypeId",
                table: "Race",
                column: "RaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceSubType_RaceId",
                table: "RaceSubType",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Spell_SchoolId",
                table: "Spell",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Spell_SubSchoolId",
                table: "Spell",
                column: "SubSchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellSubSchool_SpellSchoolId",
                table: "SpellSubSchool",
                column: "SpellSchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Territory_ContinentId",
                table: "Territory",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_Territory_FactionId",
                table: "Territory",
                column: "FactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackedEvent_CampaignId",
                table: "TrackedEvent",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_WeaponAttributeApplied_WeaponAttributeId",
                table: "WeaponAttributeApplied",
                column: "WeaponAttributeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestiaryDetail");

            migrationBuilder.DropTable(
                name: "BestiaryEnvironment");

            migrationBuilder.DropTable(
                name: "BestiaryFeat");

            migrationBuilder.DropTable(
                name: "BestiaryLanguage");

            migrationBuilder.DropTable(
                name: "BestiaryMagic");

            migrationBuilder.DropTable(
                name: "BestiarySkill");

            migrationBuilder.DropTable(
                name: "BestiarySubType");

            migrationBuilder.DropTable(
                name: "CampaignData");

            migrationBuilder.DropTable(
                name: "CharacterClassAbility");

            migrationBuilder.DropTable(
                name: "CharacterClassLevel");

            migrationBuilder.DropTable(
                name: "CharacterFeat");

            migrationBuilder.DropTable(
                name: "CharacterGearEnchantment");

            migrationBuilder.DropTable(
                name: "CharacterLanguage");

            migrationBuilder.DropTable(
                name: "CharacterMagic");

            migrationBuilder.DropTable(
                name: "CharacterSkill");

            migrationBuilder.DropTable(
                name: "ClassSkill");

            migrationBuilder.DropTable(
                name: "ContinentWeather");

            migrationBuilder.DropTable(
                name: "FavoredClass");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "MagicItem");

            migrationBuilder.DropTable(
                name: "MonsterSpawn");

            migrationBuilder.DropTable(
                name: "Month");

            migrationBuilder.DropTable(
                name: "NPCDetail");

            migrationBuilder.DropTable(
                name: "PlayerCampaign");

            migrationBuilder.DropTable(
                name: "Prerequisite");

            migrationBuilder.DropTable(
                name: "RaceSubType");

            migrationBuilder.DropTable(
                name: "SpellDetail");

            migrationBuilder.DropTable(
                name: "TrackedEvent");

            migrationBuilder.DropTable(
                name: "WeaponAttributeApplied");

            migrationBuilder.DropTable(
                name: "ClassAbility");

            migrationBuilder.DropTable(
                name: "CharacterGear");

            migrationBuilder.DropTable(
                name: "Enchantment");

            migrationBuilder.DropTable(
                name: "Weather");

            migrationBuilder.DropTable(
                name: "Environment");

            migrationBuilder.DropTable(
                name: "Terrain");

            migrationBuilder.DropTable(
                name: "Territory");

            migrationBuilder.DropTable(
                name: "Plane");

            migrationBuilder.DropTable(
                name: "Time");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "NPC");

            migrationBuilder.DropTable(
                name: "Feat");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "Spell");

            migrationBuilder.DropTable(
                name: "WeaponAttribute");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Gear");

            migrationBuilder.DropTable(
                name: "Continent");

            migrationBuilder.DropTable(
                name: "Faction");

            migrationBuilder.DropTable(
                name: "SpellSubSchool");

            migrationBuilder.DropTable(
                name: "Campaign");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Race");

            migrationBuilder.DropTable(
                name: "SpellSchool");

            migrationBuilder.DropTable(
                name: "Bestiary");

            migrationBuilder.DropTable(
                name: "BestiaryType");
        }
    }
}
