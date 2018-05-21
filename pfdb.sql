IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [BestiaryType] (
    [BestiaryTypeId] int NOT NULL IDENTITY,
    [Name] varchar(100) NOT NULL,
    CONSTRAINT [PK_BestiaryType] PRIMARY KEY ([BestiaryTypeId])
);

GO

CREATE TABLE [Campaign] (
    [CampaignId] int NOT NULL IDENTITY,
    [CampaignName] varchar(100) NOT NULL,
    [CampaignNotes] varchar(2000) NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Campaign] PRIMARY KEY ([CampaignId])
);

GO

CREATE TABLE [Class] (
    [ClassId] int NOT NULL IDENTITY,
    [Alignment] varchar(100) NULL,
    [ArchetypeForClassId] int NOT NULL,
    [CastingStat] int NOT NULL,
    [HD] int NOT NULL,
    [Name] varchar(100) NULL,
    [SkillPts] int NOT NULL,
    [StartingGold] varchar(100) NULL,
    CONSTRAINT [PK_Class] PRIMARY KEY ([ClassId])
);

GO

CREATE TABLE [Enchantment] (
    [EnchantmentId] int NOT NULL IDENTITY,
    [Bonus] int NOT NULL,
    [BonusType] int NOT NULL,
    [Effect] varchar(1000) NULL,
    [EnchantmentType] int NOT NULL,
    [EnhancementCost] int NOT NULL,
    [Name] varchar(100) NULL,
    CONSTRAINT [PK_Enchantment] PRIMARY KEY ([EnchantmentId])
);

GO

CREATE TABLE [Environment] (
    [EnvironmentId] int NOT NULL IDENTITY,
    [Name] varchar(100) NOT NULL,
    [Notes] varchar(500) NULL,
    [Temperature] varchar(100) NULL,
    [TravelSpeed] int NOT NULL,
    CONSTRAINT [PK_Environment] PRIMARY KEY ([EnvironmentId])
);

GO

CREATE TABLE [Feat] (
    [FeatId] int NOT NULL IDENTITY,
    [ArmorMastery] bit NOT NULL,
    [Benefit] varchar(max) NULL,
    [Betrayal] bit NOT NULL,
    [BloodHex] bit NOT NULL,
    [CompanionFamiliar] bit NOT NULL,
    [CompletionBenefit] varchar(max) NULL,
    [Critical] bit NOT NULL,
    [Description] varchar(500) NULL,
    [Esoteric] bit NOT NULL,
    [Fulltext] varchar(max) NULL,
    [Goal] varchar(max) NULL,
    [Grit] bit NOT NULL,
    [ItemMastery] bit NOT NULL,
    [Multiples] bit NOT NULL,
    [Name] varchar(100) NULL,
    [Normal] varchar(500) NULL,
    [Note] varchar(500) NULL,
    [Panache] bit NOT NULL,
    [Performance] bit NOT NULL,
    [PrerequisiteFeats] varchar(200) NULL,
    [PrerequisiteSkills] varchar(250) NULL,
    [Prerequisites] varchar(max) NULL,
    [RaceName] varchar(50) NULL,
    [Racial] bit NOT NULL,
    [ShieldMastery] bit NOT NULL,
    [Source] varchar(50) NULL,
    [Special] varchar(750) NULL,
    [Stare] bit NOT NULL,
    [Style] bit NOT NULL,
    [SuggestedTraits] varchar(100) NULL,
    [Targeting] bit NOT NULL,
    [Teamwork] bit NOT NULL,
    [Type_] varchar(25) NULL,
    [WeaponMastery] bit NOT NULL,
    CONSTRAINT [PK_Feat] PRIMARY KEY ([FeatId])
);

GO

CREATE TABLE [Gear] (
    [ArmTyp] int NULL,
    [ArmorBonus] int NULL,
    [CheckPenalty] int NULL,
    [MaxDex] int NULL,
    [SpellFailure] int NULL,
    [GearId] int NOT NULL IDENTITY,
    [Cost] decimal(18, 2) NOT NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [Name] varchar(100) NULL,
    [Type] int NOT NULL,
    [Weight] int NOT NULL,
    [CritMin] int NULL,
    [CritMult] int NULL,
    [DamageType] int NULL,
    [DmgDiceNum] int NULL,
    [DmgDiceType] int NULL,
    [Range] int NULL,
    CONSTRAINT [PK_Gear] PRIMARY KEY ([GearId])
);

GO

CREATE TABLE [Language] (
    [LanguageId] int NOT NULL IDENTITY,
    [Name] varchar(100) NOT NULL,
    [Notes] varchar(200) NULL,
    CONSTRAINT [PK_Language] PRIMARY KEY ([LanguageId])
);

GO

CREATE TABLE [MagicItem] (
    [MagicItemId] int NOT NULL IDENTITY,
    [Abjuration] bit NULL,
    [AL] varchar(50) NULL,
    [Aura] varchar(100) NULL,
    [AuraStrength] varchar(25) NULL,
    [BaseItem] varchar(150) NULL,
    [Cha] int NULL,
    [CL] int NULL,
    [Communication] varchar(50) NULL,
    [Conjuration] bit NULL,
    [Cost] varchar(150) NULL,
    [CostValue] int NULL,
    [Description] varchar(max) NULL,
    [Destruction] varchar(max) NULL,
    [Divination] bit NULL,
    [Ego] int NULL,
    [Enchantment] bit NULL,
    [Evocation] bit NULL,
    [FullText] varchar(max) NULL,
    [Group_] varchar(25) NULL,
    [Illusion] bit NULL,
    [Int] int NULL,
    [Languages] varchar(50) NULL,
    [LegendaryWeapon] bit NULL,
    [LinkText] varchar(50) NULL,
    [MagicItems] varchar(250) NULL,
    [MajorArtifact] bit NULL,
    [MinorArtifact] bit NULL,
    [Mythic] bit NULL,
    [Name] varchar(100) NULL,
    [Necromancy] bit NULL,
    [Powers] varchar(250) NULL,
    [Price] varchar(50) NULL,
    [PriceValue] int NULL,
    [Requirements] varchar(500) NULL,
    [Scaling] varchar(25) NULL,
    [Senses] varchar(100) NULL,
    [Slot] varchar(50) NULL,
    [Source] varchar(50) NULL,
    [Transmutation] bit NULL,
    [Universal] bit NULL,
    [Weight] varchar(50) NULL,
    [WeightValue] float NULL,
    [Wis] int NULL,
    CONSTRAINT [PK_MagicItem] PRIMARY KEY ([MagicItemId])
);

GO

CREATE TABLE [NPC] (
    [NPCID] int NOT NULL IDENTITY,
    [AbilityScoreMods] varchar(25) NULL,
    [AbilityScores] varchar(100) NULL,
    [AC] varchar(50) NULL,
    [ACMods] varchar(150) NULL,
    [AgeCategory] varchar(25) NULL,
    [Alignment] varchar(25) NULL,
    [AlternateNameForm] varchar(50) NULL,
    [Aura] varchar(150) NULL,
    [BaseAtk] int NULL,
    [BaseStatistics] varchar(max) NULL,
    [BeforeCombat] varchar(max) NULL,
    [Bloodline] varchar(50) NULL,
    [Burrow] bit NULL,
    [CharacterFlag] bit NULL,
    [Class_] varchar(100) NULL,
    [ClassArchetypes] varchar(100) NULL,
    [Climb] bit NULL,
    [CMB] int NULL,
    [CMD] int NULL,
    [CompanionFamiliarLink] varchar(25) NULL,
    [CompanionFlag] bit NULL,
    [CR] int NULL,
    [DefensiveAbilities] varchar(200) NULL,
    [DescriptionVisual] varchar(150) NULL,
    [DR] varchar(100) NULL,
    [DuringCombat] varchar(max) NULL,
    [Environment] varchar(25) NULL,
    [ExtractsPrepared] varchar(500) NULL,
    [Feats] varchar(max) NULL,
    [Fly] bit NULL,
    [FocusedSchool] varchar(25) NULL,
    [Fort] int NULL,
    [Gear] varchar(max) NULL,
    [Gender] varchar(25) NULL,
    [Group_] varchar(25) NULL,
    [HD] varchar(100) NULL,
    [HP] int NULL,
    [HPMods] varchar(100) NULL,
    [Immune] varchar(250) NULL,
    [Implements] varchar(max) NULL,
    [Init] int NULL,
    [IsTemplate] bit NULL,
    [KineticistWildTalents] varchar(500) NULL,
    [Land] bit NULL,
    [Languages] varchar(500) NULL,
    [LinkText] varchar(50) NULL,
    [Melee] varchar(500) NULL,
    [Morale] varchar(max) NULL,
    [MR] int NULL,
    [MT] int NULL,
    [Mystery] varchar(100) NULL,
    [Mythic] bit NULL,
    [Name] varchar(100) NULL,
    [Note] varchar(250) NULL,
    [OffenseNote] varchar(150) NULL,
    [Organization] varchar(25) NULL,
    [OtherGear] varchar(max) NULL,
    [Patron] varchar(50) NULL,
    [ProhibitedSchools] varchar(50) NULL,
    [PsychicDiscipline] varchar(25) NULL,
    [PsychicMagic] varchar(25) NULL,
    [Race] varchar(100) NULL,
    [RacialMods] varchar(200) NULL,
    [Ranged] varchar(250) NULL,
    [Reach] varchar(50) NULL,
    [Ref_] int NULL,
    [Resist] varchar(100) NULL,
    [SaveMods] varchar(150) NULL,
    [Saves] varchar(200) NULL,
    [Senses] varchar(150) NULL,
    [Size_] varchar(25) NULL,
    [Skills] varchar(max) NULL,
    [Source] varchar(50) NULL,
    [Space_] varchar(25) NULL,
    [SpecialAbilities] varchar(max) NULL,
    [SpecialAttacks] varchar(500) NULL,
    [Speed] varchar(100) NULL,
    [SpeedMod] varchar(100) NULL,
    [SpellDomains] varchar(100) NULL,
    [SpellLikeAbilities] varchar(700) NULL,
    [SpellsKnown] varchar(max) NULL,
    [SpellsPrepared] varchar(max) NULL,
    [Spirit] varchar(25) NULL,
    [SQ] varchar(500) NULL,
    [SR] varchar(50) NULL,
    [SubType_] varchar(100) NULL,
    [Swim] bit NULL,
    [TemplatesApplied] varchar(50) NULL,
    [ThassilonianSpecialization] varchar(50) NULL,
    [Traits] varchar(100) NULL,
    [Treasure] varchar(25) NULL,
    [Type_] varchar(50) NULL,
    [UniqueMonster] bit NULL,
    [Variant] bit NULL,
    [Vulnerability] varchar(25) NULL,
    [Weaknesses] varchar(100) NULL,
    [Will] int NULL,
    [XP] int NULL,
    CONSTRAINT [PK_NPC] PRIMARY KEY ([NPCID])
);

GO

CREATE TABLE [Plane] (
    [PlaneId] int NOT NULL IDENTITY,
    [Name] varchar(100) NOT NULL,
    CONSTRAINT [PK_Plane] PRIMARY KEY ([PlaneId])
);

GO

CREATE TABLE [Player] (
    [PlayerId] int NOT NULL IDENTITY,
    [DisplayName] varchar(100) NOT NULL,
    CONSTRAINT [PK_Player] PRIMARY KEY ([PlayerId])
);

GO

CREATE TABLE [Season] (
    [SeasonId] int NOT NULL IDENTITY,
    [Name] varchar(100) NOT NULL,
    [SeasonOrder] int NOT NULL,
    CONSTRAINT [PK_Season] PRIMARY KEY ([SeasonId])
);

GO

CREATE TABLE [Skill] (
    [SkillId] int NOT NULL IDENTITY,
    [Description] varchar(500) NOT NULL,
    [Name] varchar(100) NOT NULL,
    [Stat] int NOT NULL,
    [TrainedOnly] bit NOT NULL,
    CONSTRAINT [PK_Skill] PRIMARY KEY ([SkillId])
);

GO

CREATE TABLE [SpellSchool] (
    [SpellSchoolId] int NOT NULL IDENTITY,
    [Name] varchar(100) NOT NULL,
    CONSTRAINT [PK_SpellSchool] PRIMARY KEY ([SpellSchoolId])
);

GO

CREATE TABLE [Terrain] (
    [TerrainId] int NOT NULL IDENTITY,
    [Description] varchar(500) NULL,
    [IsBroken] bit NOT NULL,
    [IsRough] bit NOT NULL,
    [IsUnderground] bit NOT NULL,
    [IsWater] bit NOT NULL,
    [MovementModifier] decimal(18, 2) NOT NULL,
    [Name] varchar(100) NOT NULL,
    CONSTRAINT [PK_Terrain] PRIMARY KEY ([TerrainId])
);

GO

CREATE TABLE [Time] (
    [TimeId] int NOT NULL IDENTITY,
    [IsNight] bit NOT NULL,
    [Name] varchar(100) NOT NULL,
    [TimeOrder] int NOT NULL,
    CONSTRAINT [PK_Time] PRIMARY KEY ([TimeId])
);

GO

CREATE TABLE [WeaponAttribute] (
    [WpnAttId] int NOT NULL IDENTITY,
    [Effect] varchar(200) NULL,
    [Name] varchar(100) NOT NULL,
    CONSTRAINT [PK_WeaponAttribute] PRIMARY KEY ([WpnAttId])
);

GO

CREATE TABLE [Weather] (
    [WeatherId] int NOT NULL IDENTITY,
    [ColdDanger] bit NOT NULL,
    [ColdLethal] bit NOT NULL,
    [Deadly] bit NOT NULL,
    [Description] varchar(1000) NULL,
    [Effects] varchar(1000) NULL,
    [Flooding] bit NOT NULL,
    [HeatDanger] bit NOT NULL,
    [HeatLethal] bit NOT NULL,
    [HighWind] bit NOT NULL,
    [Magical] bit NOT NULL,
    [Name] varchar(100) NOT NULL,
    [VisionObscured] bit NOT NULL,
    CONSTRAINT [PK_Weather] PRIMARY KEY ([WeatherId])
);

GO

CREATE TABLE [Bestiary] (
    [BestiaryId] int NOT NULL IDENTITY,
    [AbilityScoreMods] varchar(50) NULL,
    [AbilityScores] varchar(100) NULL,
    [AC] int NOT NULL,
    [ACFlat] int NOT NULL,
    [ACMods] varchar(150) NULL,
    [ACTouch] int NOT NULL,
    [AgeCategory] varchar(25) NULL,
    [Alignment] varchar(50) NULL,
    [AlternateNameForm] varchar(50) NULL,
    [Aura] varchar(150) NULL,
    [BaseAtk] int NOT NULL,
    [BaseStatistics] varchar(500) NULL,
    [BeforeCombat] varchar(25) NULL,
    [Bloodline] varchar(25) NULL,
    [Burrow] int NOT NULL,
    [CHA] int NOT NULL,
    [CharacterFlag] bit NOT NULL,
    [Class] varchar(50) NULL,
    [ClassArchetypes] varchar(25) NULL,
    [Climb] int NOT NULL,
    [CMB] int NOT NULL,
    [CMD] int NOT NULL,
    [CompanionFamiliarLink] varchar(25) NULL,
    [CompanionFlag] bit NOT NULL,
    [CON] int NOT NULL,
    [CR] int NOT NULL,
    [DefensiveAbilities] varchar(100) NULL,
    [DescriptionVisual] varchar(max) NULL,
    [DEX] int NOT NULL,
    [DontUseRacialHD] bit NOT NULL,
    [DR] varchar(100) NULL,
    [DuringCombat] varchar(250) NULL,
    [Environment] varchar(150) NULL,
    [ExtractsPrepared] varchar(250) NULL,
    [Feats] varchar(max) NULL,
    [Fly] int NOT NULL,
    [FocusedSchool] varchar(25) NULL,
    [Fortitude] int NOT NULL,
    [Gear] varchar(250) NULL,
    [Gender] varchar(25) NULL,
    [Group] varchar(50) NULL,
    [HD] varchar(50) NULL,
    [HP] int NOT NULL,
    [HPMods] varchar(100) NULL,
    [Immune] varchar(200) NULL,
    [Init] int NOT NULL,
    [INT] int NOT NULL,
    [IsTemplate] bit NOT NULL,
    [Land] int NOT NULL,
    [Languages] varchar(250) NULL,
    [LinkText] varchar(50) NULL,
    [Melee] varchar(500) NULL,
    [Morale] varchar(150) NULL,
    [MR] int NOT NULL,
    [MT] int NOT NULL,
    [Mystery] varchar(25) NULL,
    [Mythic] bit NOT NULL,
    [Name] varchar(50) NULL,
    [Note] varchar(100) NULL,
    [OffenseNote] varchar(100) NULL,
    [Organization] varchar(500) NULL,
    [OtherGear] varchar(200) NULL,
    [Patron] varchar(25) NULL,
    [ProhibitedSchools] varchar(50) NULL,
    [Race] varchar(50) NULL,
    [RacialMods] varchar(250) NULL,
    [Ranged] varchar(250) NULL,
    [Reach] varchar(100) NULL,
    [Reflex] int NOT NULL,
    [Resist] varchar(150) NULL,
    [SaveMods] varchar(100) NULL,
    [Senses] varchar(200) NULL,
    [Size] varchar(20) NULL,
    [Skills] varchar(max) NULL,
    [Space] varchar(50) NULL,
    [SpecialAbilities] varchar(max) NULL,
    [SpecialAttacks] varchar(500) NULL,
    [Speed] varchar(100) NULL,
    [SpeedMod] varchar(100) NULL,
    [SpellDomains] varchar(50) NULL,
    [SpellLikeAbilities] varchar(max) NULL,
    [SpellsKnown] varchar(max) NULL,
    [SpellsPrepared] varchar(max) NULL,
    [SQ] varchar(500) NULL,
    [SR] int NOT NULL,
    [StatisticsNote] varchar(100) NULL,
    [STR] int NOT NULL,
    [SubType] varchar(100) NULL,
    [Swim] int NOT NULL,
    [TemplatesApplied] varchar(50) NULL,
    [Traits] varchar(25) NULL,
    [Treasure] varchar(max) NULL,
    [Type] int NULL,
    [UniqueMonster] bit NOT NULL,
    [VariantParent] varchar(25) NULL,
    [Vulnerability] varchar(25) NULL,
    [Weaknesses] varchar(100) NULL,
    [Will] int NOT NULL,
    [WIS] int NOT NULL,
    [XP] int NOT NULL,
    CONSTRAINT [PK_Bestiary] PRIMARY KEY ([BestiaryId]),
    CONSTRAINT [FK_Bestiary_BestiaryType_Type] FOREIGN KEY ([Type]) REFERENCES [BestiaryType] ([BestiaryTypeId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [CampaignData] (
    [CampaignId] int NOT NULL,
    [Key] varchar(500) NOT NULL,
    [Value] varchar(500) NULL,
    CONSTRAINT [PK_CampaignData] PRIMARY KEY ([CampaignId], [Key]),
    CONSTRAINT [FK_CampaignData_Campaign_CampaignId] FOREIGN KEY ([CampaignId]) REFERENCES [Campaign] ([CampaignId]) ON DELETE CASCADE
);

GO

CREATE TABLE [TrackedEvent] (
    [TrackedEventId] int NOT NULL IDENTITY,
    [CampaignId] int NOT NULL,
    [DateLastOccurred] nvarchar(max) NULL,
    [DateOccurring] varchar(100) NOT NULL,
    [Location] nvarchar(max) NULL,
    [Name] varchar(100) NOT NULL,
    [Notes] varchar(500) NOT NULL,
    [ReoccurFreq] int NOT NULL,
    [TrackedEventData] nvarchar(max) NULL,
    [TrackedEventType] int NOT NULL,
    CONSTRAINT [PK_TrackedEvent] PRIMARY KEY ([TrackedEventId]),
    CONSTRAINT [FK_TrackedEvent_Campaign_CampaignId] FOREIGN KEY ([CampaignId]) REFERENCES [Campaign] ([CampaignId]) ON DELETE CASCADE
);

GO

CREATE TABLE [ClassAbility] (
    [ClassAbilityId] int NOT NULL IDENTITY,
    [ClassId] int NOT NULL,
    [Description] varchar(max) NULL,
    [LevelRequirement] int NOT NULL,
    [Name] varchar(100) NULL,
    [ReplacesAbilityId] int NOT NULL,
    CONSTRAINT [PK_ClassAbility] PRIMARY KEY ([ClassAbilityId]),
    CONSTRAINT [FK_ClassAbility_Class_ClassId] FOREIGN KEY ([ClassId]) REFERENCES [Class] ([ClassId]) ON DELETE CASCADE
);

GO

CREATE TABLE [Continent] (
    [ContinentId] int NOT NULL IDENTITY,
    [IsUnderground] bit NOT NULL,
    [IsWater] bit NOT NULL,
    [MapPath] varchar(200) NULL,
    [Name] varchar(100) NULL,
    [PrimaryLanguageId] int NULL,
    CONSTRAINT [PK_Continent] PRIMARY KEY ([ContinentId]),
    CONSTRAINT [FK_Continent_Language_PrimaryLanguageId] FOREIGN KEY ([PrimaryLanguageId]) REFERENCES [Language] ([LanguageId]) ON DELETE SET NULL
);

GO

CREATE TABLE [NPCDetail] (
    [NPCID] int NOT NULL,
    [Description] varchar(max) NULL,
    [FullText] varchar(max) NULL,
    [MonsterSource] varchar(250) NULL,
    CONSTRAINT [PK_NPCDetail] PRIMARY KEY ([NPCID]),
    CONSTRAINT [FK_NPCDetail_NPC_NPCID] FOREIGN KEY ([NPCID]) REFERENCES [NPC] ([NPCID]) ON DELETE CASCADE
);

GO

CREATE TABLE [PlayerCampaign] (
    [PlayerId] int NOT NULL,
    [CampaignId] int NOT NULL,
    [IsDM] bit NOT NULL,
    CONSTRAINT [PK_PlayerCampaign] PRIMARY KEY ([PlayerId], [CampaignId]),
    CONSTRAINT [FK_PlayerCampaign_Campaign_CampaignId] FOREIGN KEY ([CampaignId]) REFERENCES [Campaign] ([CampaignId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PlayerCampaign_Player_PlayerId] FOREIGN KEY ([PlayerId]) REFERENCES [Player] ([PlayerId]) ON DELETE CASCADE
);

GO

CREATE TABLE [Month] (
    [MonthId] int NOT NULL IDENTITY,
    [Days] int NOT NULL,
    [MonthOrder] int NOT NULL,
    [Name] varchar(100) NOT NULL,
    [SeasonId] int NULL,
    CONSTRAINT [PK_Month] PRIMARY KEY ([MonthId]),
    CONSTRAINT [FK_Month_Season_SeasonId] FOREIGN KEY ([SeasonId]) REFERENCES [Season] ([SeasonId]) ON DELETE SET NULL
);

GO

CREATE TABLE [ClassSkill] (
    [ClassId] int NOT NULL,
    [SkillId] int NOT NULL,
    CONSTRAINT [PK_ClassSkill] PRIMARY KEY ([ClassId], [SkillId]),
    CONSTRAINT [FK_ClassSkill_Class_ClassId] FOREIGN KEY ([ClassId]) REFERENCES [Class] ([ClassId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ClassSkill_Skill_SkillId] FOREIGN KEY ([SkillId]) REFERENCES [Skill] ([SkillId]) ON DELETE CASCADE
);

GO

CREATE TABLE [Prerequisite] (
    [PrerequisiteId] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NULL,
    [FeatId] int NULL,
    [SkillId] int NULL,
    [Stat] int NOT NULL,
    [Value] int NOT NULL,
    CONSTRAINT [PK_Prerequisite] PRIMARY KEY ([PrerequisiteId]),
    CONSTRAINT [FK_Prerequisite_Feat_FeatId] FOREIGN KEY ([FeatId]) REFERENCES [Feat] ([FeatId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Prerequisite_Skill_SkillId] FOREIGN KEY ([SkillId]) REFERENCES [Skill] ([SkillId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [SpellSubSchool] (
    [SpellSubSchoolId] int NOT NULL IDENTITY,
    [Name] varchar(100) NOT NULL,
    [SpellSchoolId] int NOT NULL,
    CONSTRAINT [PK_SpellSubSchool] PRIMARY KEY ([SpellSubSchoolId]),
    CONSTRAINT [FK_SpellSubSchool_SpellSchool_SpellSchoolId] FOREIGN KEY ([SpellSchoolId]) REFERENCES [SpellSchool] ([SpellSchoolId]) ON DELETE CASCADE
);

GO

CREATE TABLE [WeaponAttributeApplied] (
    [GearId] int NOT NULL,
    [WeaponAttributeId] int NOT NULL,
    [Note] nvarchar(max) NULL,
    CONSTRAINT [PK_WeaponAttributeApplied] PRIMARY KEY ([GearId], [WeaponAttributeId]),
    CONSTRAINT [FK_WeaponAttributeApplied_Gear_GearId] FOREIGN KEY ([GearId]) REFERENCES [Gear] ([GearId]) ON DELETE CASCADE,
    CONSTRAINT [FK_WeaponAttributeApplied_WeaponAttribute_WeaponAttributeId] FOREIGN KEY ([WeaponAttributeId]) REFERENCES [WeaponAttribute] ([WpnAttId]) ON DELETE CASCADE
);

GO

CREATE TABLE [BestiaryDetail] (
    [BestiaryId] int NOT NULL,
    [Description] varchar(max) NULL,
    [FullText] varchar(max) NULL,
    [MonsterSource] varchar(100) NULL,
    [Source] varchar(50) NULL,
    CONSTRAINT [PK_BestiaryDetail] PRIMARY KEY ([BestiaryId]),
    CONSTRAINT [FK_BestiaryDetail_Bestiary_BestiaryId] FOREIGN KEY ([BestiaryId]) REFERENCES [Bestiary] ([BestiaryId]) ON DELETE CASCADE
);

GO

CREATE TABLE [BestiaryEnvironment] (
    [BestiaryEnvironmentId] int NOT NULL IDENTITY,
    [BestiaryId] int NOT NULL,
    [EnvironmentId] int NOT NULL,
    [Notes] varchar(500) NULL,
    CONSTRAINT [PK_BestiaryEnvironment] PRIMARY KEY ([BestiaryEnvironmentId]),
    CONSTRAINT [FK_BestiaryEnvironment_Bestiary_BestiaryId] FOREIGN KEY ([BestiaryId]) REFERENCES [Bestiary] ([BestiaryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BestiaryEnvironment_Environment_EnvironmentId] FOREIGN KEY ([EnvironmentId]) REFERENCES [Environment] ([EnvironmentId]) ON DELETE CASCADE
);

GO

CREATE TABLE [BestiaryFeat] (
    [BestiaryFeatId] int NOT NULL IDENTITY,
    [BestiaryId] int NOT NULL,
    [FeatId] int NOT NULL,
    [Notes] varchar(500) NULL,
    CONSTRAINT [PK_BestiaryFeat] PRIMARY KEY ([BestiaryFeatId]),
    CONSTRAINT [FK_BestiaryFeat_Bestiary_BestiaryId] FOREIGN KEY ([BestiaryId]) REFERENCES [Bestiary] ([BestiaryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BestiaryFeat_Feat_FeatId] FOREIGN KEY ([FeatId]) REFERENCES [Feat] ([FeatId]) ON DELETE CASCADE
);

GO

CREATE TABLE [BestiaryLanguage] (
    [BestiaryLanguageId] int NOT NULL IDENTITY,
    [BestiaryId] int NOT NULL,
    [LanguageId] int NOT NULL,
    [Notes] varchar(500) NULL,
    CONSTRAINT [PK_BestiaryLanguage] PRIMARY KEY ([BestiaryLanguageId]),
    CONSTRAINT [FK_BestiaryLanguage_Bestiary_BestiaryId] FOREIGN KEY ([BestiaryId]) REFERENCES [Bestiary] ([BestiaryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BestiaryLanguage_Language_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [Language] ([LanguageId]) ON DELETE CASCADE
);

GO

CREATE TABLE [BestiarySkill] (
    [BestiarySkillId] int NOT NULL IDENTITY,
    [BestiaryId] int NOT NULL,
    [Bonus] int NOT NULL,
    [Notes] varchar(500) NULL,
    [SkillId] int NOT NULL,
    CONSTRAINT [PK_BestiarySkill] PRIMARY KEY ([BestiarySkillId]),
    CONSTRAINT [FK_BestiarySkill_Bestiary_BestiaryId] FOREIGN KEY ([BestiaryId]) REFERENCES [Bestiary] ([BestiaryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BestiarySkill_Skill_SkillId] FOREIGN KEY ([SkillId]) REFERENCES [Skill] ([SkillId]) ON DELETE CASCADE
);

GO

CREATE TABLE [BestiarySubType] (
    [BestiarySubTypeId] int NOT NULL IDENTITY,
    [BestiaryId] int NOT NULL,
    [BestiaryTypeId] int NOT NULL,
    CONSTRAINT [PK_BestiarySubType] PRIMARY KEY ([BestiarySubTypeId]),
    CONSTRAINT [FK_BestiarySubType_Bestiary_BestiaryId] FOREIGN KEY ([BestiaryId]) REFERENCES [Bestiary] ([BestiaryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BestiarySubType_BestiaryType_BestiaryTypeId] FOREIGN KEY ([BestiaryTypeId]) REFERENCES [BestiaryType] ([BestiaryTypeId]) ON DELETE CASCADE
);

GO

CREATE TABLE [Race] (
    [RaceId] int NOT NULL IDENTITY,
    [BestiaryId] int NULL,
    [ModCHA] int NOT NULL,
    [ModCON] int NOT NULL,
    [ModDEX] int NOT NULL,
    [ModINT] int NOT NULL,
    [ModSTR] int NOT NULL,
    [ModWIS] int NOT NULL,
    [RP] int NOT NULL,
    [RaceTypeId] int NULL,
    CONSTRAINT [PK_Race] PRIMARY KEY ([RaceId]),
    CONSTRAINT [FK_Race_Bestiary_RaceTypeId] FOREIGN KEY ([RaceTypeId]) REFERENCES [Bestiary] ([BestiaryId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ContinentWeather] (
    [CWID] int NOT NULL,
    [CWName] varchar(100) NULL,
    [ContinentId] int NOT NULL,
    [Duration] int NOT NULL,
    [NextCWID] int NULL,
    [ParentCWID] int NULL,
    [RandomDuration] bit NOT NULL,
    [SeasonId] int NOT NULL,
    [WeatherId] int NOT NULL,
    [Weight] int NOT NULL,
    CONSTRAINT [PK_ContinentWeather] PRIMARY KEY ([CWID]),
    CONSTRAINT [FK_ContinentWeather_Continent_ContinentId] FOREIGN KEY ([ContinentId]) REFERENCES [Continent] ([ContinentId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ContinentWeather_Season_SeasonId] FOREIGN KEY ([SeasonId]) REFERENCES [Season] ([SeasonId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ContinentWeather_Weather_WeatherId] FOREIGN KEY ([WeatherId]) REFERENCES [Weather] ([WeatherId]) ON DELETE CASCADE
);

GO

CREATE TABLE [MonsterSpawn] (
    [SpawnId] int NOT NULL IDENTITY,
    [BestiaryId] int NOT NULL,
    [ContinentId] int NOT NULL,
    [PlaneId] int NOT NULL,
    [SeasonId] int NOT NULL,
    [TimeId] int NOT NULL,
    CONSTRAINT [PK_MonsterSpawn] PRIMARY KEY ([SpawnId]),
    CONSTRAINT [FK_MonsterSpawn_Bestiary_BestiaryId] FOREIGN KEY ([BestiaryId]) REFERENCES [Bestiary] ([BestiaryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_MonsterSpawn_Continent_ContinentId] FOREIGN KEY ([ContinentId]) REFERENCES [Continent] ([ContinentId]) ON DELETE CASCADE,
    CONSTRAINT [FK_MonsterSpawn_Plane_PlaneId] FOREIGN KEY ([PlaneId]) REFERENCES [Plane] ([PlaneId]) ON DELETE CASCADE,
    CONSTRAINT [FK_MonsterSpawn_Season_SeasonId] FOREIGN KEY ([SeasonId]) REFERENCES [Season] ([SeasonId]) ON DELETE CASCADE,
    CONSTRAINT [FK_MonsterSpawn_Time_TimeId] FOREIGN KEY ([TimeId]) REFERENCES [Time] ([TimeId]) ON DELETE CASCADE
);

GO

CREATE TABLE [Spell] (
    [SpellId] int NOT NULL IDENTITY,
    [Acid] bit NOT NULL,
    [Adept] int NULL,
    [Air] bit NOT NULL,
    [Alchemist] int NULL,
    [Antipaladin] int NULL,
    [Area] varchar(100) NULL,
    [Augmented] varchar(500) NULL,
    [Bard] int NULL,
    [BloodRager] int NULL,
    [Bloodline] varchar(150) NULL,
    [CastingTime] varchar(50) NULL,
    [Chaotic] bit NOT NULL,
    [Cleric] int NULL,
    [Cold] bit NOT NULL,
    [Components] varchar(200) NULL,
    [CostlyComponents] bit NOT NULL,
    [Curse] bit NOT NULL,
    [Darkness] bit NOT NULL,
    [Death] bit NOT NULL,
    [Deity] varchar(25) NULL,
    [Descriptor] varchar(50) NULL,
    [Disease] bit NOT NULL,
    [Dismissable] bit NOT NULL,
    [DivineFocus] bit NOT NULL,
    [Domain] varchar(100) NULL,
    [Druid] int NULL,
    [Duration] varchar(100) NULL,
    [Earth] bit NOT NULL,
    [Effect] varchar(200) NULL,
    [Electricity] bit NOT NULL,
    [Emotion] bit NOT NULL,
    [Evil] bit NOT NULL,
    [Fear] bit NOT NULL,
    [Fire] bit NOT NULL,
    [Focus] bit NOT NULL,
    [Force] bit NOT NULL,
    [Good] bit NOT NULL,
    [HauntStatistics] varchar(250) NULL,
    [Hunter] int NULL,
    [Inquisitor] int NULL,
    [Investigator] int NULL,
    [LanguageDependent] bit NOT NULL,
    [Lawful] bit NOT NULL,
    [Light] bit NOT NULL,
    [LinkText] varchar(50) NULL,
    [Magus] int NULL,
    [Material] bit NOT NULL,
    [MaterialCost] int NOT NULL,
    [Medium] int NULL,
    [Mesmerist] int NULL,
    [MindAffecting] bit NOT NULL,
    [Mythic] bit NOT NULL,
    [MythicText] varchar(max) NULL,
    [Name] varchar(50) NULL,
    [Occultist] int NULL,
    [Oracle] int NULL,
    [Pain] bit NOT NULL,
    [Paladin] int NULL,
    [Patron] varchar(100) NULL,
    [Poison] bit NOT NULL,
    [Psychic] int NULL,
    [Range] varchar(100) NULL,
    [Ranger] int NULL,
    [SavingThrow] varchar(100) NULL,
    [SchoolId] int NULL,
    [Shadow] bit NOT NULL,
    [Shaman] int NULL,
    [Shapeable] bit NOT NULL,
    [ShortDescription] varchar(250) NULL,
    [Skald] int NULL,
    [SLALevel] int NOT NULL,
    [Somatic] bit NOT NULL,
    [Sonic] bit NOT NULL,
    [Sor] int NULL,
    [Source] varchar(50) NULL,
    [SpellLevel] varchar(200) NULL,
    [SpellResistance] varchar(50) NULL,
    [Spiritualist] int NULL,
    [SubSchoolId] int NULL,
    [Summoner] int NULL,
    [Targets] varchar(200) NULL,
    [Verbal] bit NOT NULL,
    [Water] bit NOT NULL,
    [Witch] int NULL,
    [Wiz] int NULL,
    CONSTRAINT [PK_Spell] PRIMARY KEY ([SpellId]),
    CONSTRAINT [FK_Spell_SpellSchool_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [SpellSchool] ([SpellSchoolId]) ON DELETE SET NULL,
    CONSTRAINT [FK_Spell_SpellSubSchool_SubSchoolId] FOREIGN KEY ([SubSchoolId]) REFERENCES [SpellSubSchool] ([SpellSubSchoolId]) ON DELETE SET NULL
);

GO

CREATE TABLE [Character] (
    [CharacterId] int NOT NULL IDENTITY,
    [CampaignId] int NULL,
    [Deity] varchar(100) NULL,
    [Name] varchar(100) NULL,
    [PlayerId] int NULL,
    [RaceId] int NULL,
    CONSTRAINT [PK_Character] PRIMARY KEY ([CharacterId]),
    CONSTRAINT [FK_Character_Campaign_CampaignId] FOREIGN KEY ([CampaignId]) REFERENCES [Campaign] ([CampaignId]) ON DELETE SET NULL,
    CONSTRAINT [FK_Character_Player_PlayerId] FOREIGN KEY ([PlayerId]) REFERENCES [Player] ([PlayerId]) ON DELETE SET NULL,
    CONSTRAINT [FK_Character_Race_RaceId] FOREIGN KEY ([RaceId]) REFERENCES [Race] ([RaceId]) ON DELETE CASCADE
);

GO

CREATE TABLE [Faction] (
    [FactionId] int NOT NULL IDENTITY,
    [Name] varchar(100) NULL,
    [ParentFactionId] int NULL,
    [PrimaryLanguageId] int NULL,
    [PrimaryRaceId] int NULL,
    [Type] varchar(200) NULL,
    CONSTRAINT [PK_Faction] PRIMARY KEY ([FactionId]),
    CONSTRAINT [FK_Faction_Language_PrimaryLanguageId] FOREIGN KEY ([PrimaryLanguageId]) REFERENCES [Language] ([LanguageId]) ON DELETE SET NULL,
    CONSTRAINT [FK_Faction_Race_PrimaryRaceId] FOREIGN KEY ([PrimaryRaceId]) REFERENCES [Race] ([RaceId]) ON DELETE SET NULL
);

GO

CREATE TABLE [FavoredClass] (
    [FavoredClassId] int NOT NULL IDENTITY,
    [ClassId] int NOT NULL,
    [Description] varchar(500) NULL,
    [RaceId] int NOT NULL,
    CONSTRAINT [PK_FavoredClass] PRIMARY KEY ([FavoredClassId]),
    CONSTRAINT [FK_FavoredClass_Class_ClassId] FOREIGN KEY ([ClassId]) REFERENCES [Class] ([ClassId]) ON DELETE CASCADE,
    CONSTRAINT [FK_FavoredClass_Race_RaceId] FOREIGN KEY ([RaceId]) REFERENCES [Race] ([RaceId]) ON DELETE CASCADE
);

GO

CREATE TABLE [RaceSubType] (
    [BestiaryTypeId] int NOT NULL,
    [RaceId] int NOT NULL,
    CONSTRAINT [PK_RaceSubType] PRIMARY KEY ([BestiaryTypeId], [RaceId]),
    CONSTRAINT [FK_RaceSubType_BestiaryType_BestiaryTypeId] FOREIGN KEY ([BestiaryTypeId]) REFERENCES [BestiaryType] ([BestiaryTypeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_RaceSubType_Race_RaceId] FOREIGN KEY ([RaceId]) REFERENCES [Race] ([RaceId]) ON DELETE CASCADE
);

GO

CREATE TABLE [BestiaryMagic] (
    [BestiaryMagicId] int NOT NULL IDENTITY,
    [BestiaryId] int NOT NULL,
    [CasterLevel] int NOT NULL,
    [MagicTypeId] int NOT NULL,
    [Notes] varchar(500) NULL,
    [SpellId] int NOT NULL,
    [UsesPerDay] int NOT NULL,
    CONSTRAINT [PK_BestiaryMagic] PRIMARY KEY ([BestiaryMagicId]),
    CONSTRAINT [FK_BestiaryMagic_Bestiary_BestiaryId] FOREIGN KEY ([BestiaryId]) REFERENCES [Bestiary] ([BestiaryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BestiaryMagic_Spell_SpellId] FOREIGN KEY ([SpellId]) REFERENCES [Spell] ([SpellId]) ON DELETE CASCADE
);

GO

CREATE TABLE [SpellDetail] (
    [SpellId] int NOT NULL,
    [Description] varchar(max) NULL,
    [DescriptionFormatted] varchar(max) NULL,
    [FullText] varchar(max) NULL,
    CONSTRAINT [PK_SpellDetail] PRIMARY KEY ([SpellId]),
    CONSTRAINT [FK_SpellDetail_Spell_SpellId] FOREIGN KEY ([SpellId]) REFERENCES [Spell] ([SpellId]) ON DELETE CASCADE
);

GO

CREATE TABLE [CharacterClassAbility] (
    [CharacterClassAbilityId] int NOT NULL IDENTITY,
    [CharacterId] int NOT NULL,
    [ClassAbilityId] int NOT NULL,
    [Note] varchar(500) NULL,
    CONSTRAINT [PK_CharacterClassAbility] PRIMARY KEY ([CharacterClassAbilityId]),
    CONSTRAINT [FK_CharacterClassAbility_Character_CharacterId] FOREIGN KEY ([CharacterId]) REFERENCES [Character] ([CharacterId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CharacterClassAbility_ClassAbility_ClassAbilityId] FOREIGN KEY ([ClassAbilityId]) REFERENCES [ClassAbility] ([ClassAbilityId]) ON DELETE CASCADE
);

GO

CREATE TABLE [CharacterClassLevel] (
    [CharacterId] int NOT NULL,
    [ClassId] int NOT NULL,
    [Level] int NOT NULL,
    CONSTRAINT [PK_CharacterClassLevel] PRIMARY KEY ([CharacterId], [ClassId]),
    CONSTRAINT [FK_CharacterClassLevel_Character_CharacterId] FOREIGN KEY ([CharacterId]) REFERENCES [Character] ([CharacterId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CharacterClassLevel_Class_ClassId] FOREIGN KEY ([ClassId]) REFERENCES [Class] ([ClassId]) ON DELETE CASCADE
);

GO

CREATE TABLE [CharacterFeat] (
    [CharacterFeatId] int NOT NULL IDENTITY,
    [CharacterId] int NOT NULL,
    [FeatId] int NOT NULL,
    [Notes] varchar(500) NULL,
    CONSTRAINT [PK_CharacterFeat] PRIMARY KEY ([CharacterFeatId]),
    CONSTRAINT [FK_CharacterFeat_Character_CharacterId] FOREIGN KEY ([CharacterId]) REFERENCES [Character] ([CharacterId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CharacterFeat_Feat_FeatId] FOREIGN KEY ([FeatId]) REFERENCES [Feat] ([FeatId]) ON DELETE CASCADE
);

GO

CREATE TABLE [CharacterGear] (
    [CharacterGearId] int NOT NULL IDENTITY,
    [CharacterId] int NOT NULL,
    [GearId] int NOT NULL,
    [Notes] varchar(500) NULL,
    [Quantity] int NOT NULL,
    CONSTRAINT [PK_CharacterGear] PRIMARY KEY ([CharacterGearId]),
    CONSTRAINT [FK_CharacterGear_Character_CharacterId] FOREIGN KEY ([CharacterId]) REFERENCES [Character] ([CharacterId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CharacterGear_Gear_GearId] FOREIGN KEY ([GearId]) REFERENCES [Gear] ([GearId]) ON DELETE CASCADE
);

GO

CREATE TABLE [CharacterLanguage] (
    [CharacterId] int NOT NULL,
    [LanguageId] int NOT NULL,
    CONSTRAINT [PK_CharacterLanguage] PRIMARY KEY ([CharacterId], [LanguageId]),
    CONSTRAINT [FK_CharacterLanguage_Character_CharacterId] FOREIGN KEY ([CharacterId]) REFERENCES [Character] ([CharacterId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CharacterLanguage_Language_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [Language] ([LanguageId]) ON DELETE CASCADE
);

GO

CREATE TABLE [CharacterMagic] (
    [CharacterMagicId] int NOT NULL IDENTITY,
    [CharacterId] int NOT NULL,
    [MagicType] int NOT NULL,
    [Notes] nvarchar(max) NULL,
    [SpellId] int NOT NULL,
    CONSTRAINT [PK_CharacterMagic] PRIMARY KEY ([CharacterMagicId]),
    CONSTRAINT [FK_CharacterMagic_Character_CharacterId] FOREIGN KEY ([CharacterId]) REFERENCES [Character] ([CharacterId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CharacterMagic_Spell_SpellId] FOREIGN KEY ([SpellId]) REFERENCES [Spell] ([SpellId]) ON DELETE CASCADE
);

GO

CREATE TABLE [CharacterSkill] (
    [CharacterId] int NOT NULL,
    [SkillId] int NOT NULL,
    [Ranks] int NOT NULL,
    CONSTRAINT [PK_CharacterSkill] PRIMARY KEY ([CharacterId], [SkillId]),
    CONSTRAINT [FK_CharacterSkill_Character_CharacterId] FOREIGN KEY ([CharacterId]) REFERENCES [Character] ([CharacterId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CharacterSkill_Skill_SkillId] FOREIGN KEY ([SkillId]) REFERENCES [Skill] ([SkillId]) ON DELETE CASCADE
);

GO

CREATE TABLE [Territory] (
    [TerritoryId] int NOT NULL IDENTITY,
    [ContinentId] int NULL,
    [FactionId] int NULL,
    [Name] varchar(100) NOT NULL,
    [Notes] nvarchar(max) NULL,
    CONSTRAINT [PK_Territory] PRIMARY KEY ([TerritoryId]),
    CONSTRAINT [FK_Territory_Continent_ContinentId] FOREIGN KEY ([ContinentId]) REFERENCES [Continent] ([ContinentId]) ON DELETE SET NULL,
    CONSTRAINT [FK_Territory_Faction_FactionId] FOREIGN KEY ([FactionId]) REFERENCES [Faction] ([FactionId]) ON DELETE SET NULL
);

GO

CREATE TABLE [CharacterGearEnchantment] (
    [CharacterGearId] int NOT NULL,
    [EnchantmentId] int NOT NULL,
    CONSTRAINT [PK_CharacterGearEnchantment] PRIMARY KEY ([CharacterGearId], [EnchantmentId]),
    CONSTRAINT [FK_CharacterGearEnchantment_CharacterGear_CharacterGearId] FOREIGN KEY ([CharacterGearId]) REFERENCES [CharacterGear] ([CharacterGearId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CharacterGearEnchantment_Enchantment_EnchantmentId] FOREIGN KEY ([EnchantmentId]) REFERENCES [Enchantment] ([EnchantmentId]) ON DELETE CASCADE
);

GO

CREATE TABLE [Location] (
    [LocationId] int NOT NULL IDENTITY,
    [ContinentId] int NULL,
    [EnvironmentId] int NULL,
    [FactionId] int NULL,
    [GridX] int NOT NULL,
    [GridY] int NOT NULL,
    [Name] varchar(100) NULL,
    [TerrainId] int NULL,
    [TerritoryId] int NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY ([LocationId]),
    CONSTRAINT [FK_Location_Continent_ContinentId] FOREIGN KEY ([ContinentId]) REFERENCES [Continent] ([ContinentId]) ON DELETE SET NULL,
    CONSTRAINT [FK_Location_Environment_EnvironmentId] FOREIGN KEY ([EnvironmentId]) REFERENCES [Environment] ([EnvironmentId]) ON DELETE SET NULL,
    CONSTRAINT [FK_Location_Faction_FactionId] FOREIGN KEY ([FactionId]) REFERENCES [Faction] ([FactionId]) ON DELETE SET NULL,
    CONSTRAINT [FK_Location_Terrain_TerrainId] FOREIGN KEY ([TerrainId]) REFERENCES [Terrain] ([TerrainId]) ON DELETE SET NULL,
    CONSTRAINT [FK_Location_Territory_TerritoryId] FOREIGN KEY ([TerritoryId]) REFERENCES [Territory] ([TerritoryId]) ON DELETE SET NULL
);

GO

CREATE INDEX [IX_Bestiary_Type] ON [Bestiary] ([Type]);

GO

CREATE INDEX [IX_BestiaryEnvironment_BestiaryId] ON [BestiaryEnvironment] ([BestiaryId]);

GO

CREATE INDEX [IX_BestiaryEnvironment_EnvironmentId] ON [BestiaryEnvironment] ([EnvironmentId]);

GO

CREATE INDEX [IX_BestiaryFeat_BestiaryId] ON [BestiaryFeat] ([BestiaryId]);

GO

CREATE INDEX [IX_BestiaryFeat_FeatId] ON [BestiaryFeat] ([FeatId]);

GO

CREATE INDEX [IX_BestiaryLanguage_BestiaryId] ON [BestiaryLanguage] ([BestiaryId]);

GO

CREATE INDEX [IX_BestiaryLanguage_LanguageId] ON [BestiaryLanguage] ([LanguageId]);

GO

CREATE INDEX [IX_BestiaryMagic_BestiaryId] ON [BestiaryMagic] ([BestiaryId]);

GO

CREATE INDEX [IX_BestiaryMagic_SpellId] ON [BestiaryMagic] ([SpellId]);

GO

CREATE INDEX [IX_BestiarySkill_BestiaryId] ON [BestiarySkill] ([BestiaryId]);

GO

CREATE INDEX [IX_BestiarySkill_SkillId] ON [BestiarySkill] ([SkillId]);

GO

CREATE INDEX [IX_BestiarySubType_BestiaryId] ON [BestiarySubType] ([BestiaryId]);

GO

CREATE INDEX [IX_BestiarySubType_BestiaryTypeId] ON [BestiarySubType] ([BestiaryTypeId]);

GO

CREATE INDEX [IX_Character_CampaignId] ON [Character] ([CampaignId]);

GO

CREATE INDEX [IX_Character_PlayerId] ON [Character] ([PlayerId]);

GO

CREATE INDEX [IX_Character_RaceId] ON [Character] ([RaceId]);

GO

CREATE INDEX [IX_CharacterClassAbility_CharacterId] ON [CharacterClassAbility] ([CharacterId]);

GO

CREATE INDEX [IX_CharacterClassAbility_ClassAbilityId] ON [CharacterClassAbility] ([ClassAbilityId]);

GO

CREATE INDEX [IX_CharacterClassLevel_ClassId] ON [CharacterClassLevel] ([ClassId]);

GO

CREATE INDEX [IX_CharacterFeat_CharacterId] ON [CharacterFeat] ([CharacterId]);

GO

CREATE INDEX [IX_CharacterFeat_FeatId] ON [CharacterFeat] ([FeatId]);

GO

CREATE INDEX [IX_CharacterGear_CharacterId] ON [CharacterGear] ([CharacterId]);

GO

CREATE INDEX [IX_CharacterGear_GearId] ON [CharacterGear] ([GearId]);

GO

CREATE INDEX [IX_CharacterGearEnchantment_EnchantmentId] ON [CharacterGearEnchantment] ([EnchantmentId]);

GO

CREATE INDEX [IX_CharacterLanguage_LanguageId] ON [CharacterLanguage] ([LanguageId]);

GO

CREATE INDEX [IX_CharacterMagic_CharacterId] ON [CharacterMagic] ([CharacterId]);

GO

CREATE INDEX [IX_CharacterMagic_SpellId] ON [CharacterMagic] ([SpellId]);

GO

CREATE INDEX [IX_CharacterSkill_SkillId] ON [CharacterSkill] ([SkillId]);

GO

CREATE INDEX [IX_ClassAbility_ClassId] ON [ClassAbility] ([ClassId]);

GO

CREATE INDEX [IX_ClassSkill_SkillId] ON [ClassSkill] ([SkillId]);

GO

CREATE INDEX [IX_Continent_PrimaryLanguageId] ON [Continent] ([PrimaryLanguageId]);

GO

CREATE INDEX [IX_ContinentWeather_ContinentId] ON [ContinentWeather] ([ContinentId]);

GO

CREATE INDEX [IX_ContinentWeather_SeasonId] ON [ContinentWeather] ([SeasonId]);

GO

CREATE INDEX [IX_ContinentWeather_WeatherId] ON [ContinentWeather] ([WeatherId]);

GO

CREATE INDEX [IX_Faction_PrimaryLanguageId] ON [Faction] ([PrimaryLanguageId]);

GO

CREATE INDEX [IX_Faction_PrimaryRaceId] ON [Faction] ([PrimaryRaceId]);

GO

CREATE INDEX [IX_FavoredClass_ClassId] ON [FavoredClass] ([ClassId]);

GO

CREATE INDEX [IX_FavoredClass_RaceId] ON [FavoredClass] ([RaceId]);

GO

CREATE INDEX [IX_Location_ContinentId] ON [Location] ([ContinentId]);

GO

CREATE INDEX [IX_Location_EnvironmentId] ON [Location] ([EnvironmentId]);

GO

CREATE INDEX [IX_Location_FactionId] ON [Location] ([FactionId]);

GO

CREATE INDEX [IX_Location_TerrainId] ON [Location] ([TerrainId]);

GO

CREATE INDEX [IX_Location_TerritoryId] ON [Location] ([TerritoryId]);

GO

CREATE INDEX [IX_MonsterSpawn_BestiaryId] ON [MonsterSpawn] ([BestiaryId]);

GO

CREATE INDEX [IX_MonsterSpawn_ContinentId] ON [MonsterSpawn] ([ContinentId]);

GO

CREATE INDEX [IX_MonsterSpawn_PlaneId] ON [MonsterSpawn] ([PlaneId]);

GO

CREATE INDEX [IX_MonsterSpawn_SeasonId] ON [MonsterSpawn] ([SeasonId]);

GO

CREATE INDEX [IX_MonsterSpawn_TimeId] ON [MonsterSpawn] ([TimeId]);

GO

CREATE INDEX [IX_Month_SeasonId] ON [Month] ([SeasonId]);

GO

CREATE INDEX [IX_PlayerCampaign_CampaignId] ON [PlayerCampaign] ([CampaignId]);

GO

CREATE INDEX [IX_Prerequisite_FeatId] ON [Prerequisite] ([FeatId]);

GO

CREATE INDEX [IX_Prerequisite_SkillId] ON [Prerequisite] ([SkillId]);

GO

CREATE INDEX [IX_Race_RaceTypeId] ON [Race] ([RaceTypeId]);

GO

CREATE INDEX [IX_RaceSubType_RaceId] ON [RaceSubType] ([RaceId]);

GO

CREATE INDEX [IX_Spell_SchoolId] ON [Spell] ([SchoolId]);

GO

CREATE INDEX [IX_Spell_SubSchoolId] ON [Spell] ([SubSchoolId]);

GO

CREATE INDEX [IX_SpellSubSchool_SpellSchoolId] ON [SpellSubSchool] ([SpellSchoolId]);

GO

CREATE INDEX [IX_Territory_ContinentId] ON [Territory] ([ContinentId]);

GO

CREATE INDEX [IX_Territory_FactionId] ON [Territory] ([FactionId]);

GO

CREATE INDEX [IX_TrackedEvent_CampaignId] ON [TrackedEvent] ([CampaignId]);

GO

CREATE INDEX [IX_WeaponAttributeApplied_WeaponAttributeId] ON [WeaponAttributeApplied] ([WeaponAttributeId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180519061140_Initial', N'2.0.3-rtm-10026');

GO

