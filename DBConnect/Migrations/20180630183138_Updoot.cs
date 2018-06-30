using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DBConnect.Migrations
{
    public partial class Updoot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_Race_RaceId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_Prerequisite_Feat_FeatId",
                table: "Prerequisite");

            migrationBuilder.DropForeignKey(
                name: "FK_Prerequisite_Skill_SkillId",
                table: "Prerequisite");

            migrationBuilder.DropIndex(
                name: "IX_Prerequisite_FeatId",
                table: "Prerequisite");

            migrationBuilder.DropIndex(
                name: "IX_Prerequisite_SkillId",
                table: "Prerequisite");

            migrationBuilder.DropColumn(
                name: "FeatId",
                table: "Prerequisite");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "Prerequisite");

            migrationBuilder.DropColumn(
                name: "DamageType",
                table: "Gear");

            migrationBuilder.DropColumn(
                name: "ReplacesAbilityId",
                table: "ClassAbility");

            migrationBuilder.RenameColumn(
                name: "WpnAttId",
                table: "WeaponAttribute",
                newName: "WeaponAttributeId");

            migrationBuilder.AddColumn<int>(
                name: "ContinentId",
                table: "TrackedEvent",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrackedEventFreq",
                table: "TrackedEvent",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Option",
                table: "Skill",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ForId",
                table: "Prerequisite",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NeedId",
                table: "Prerequisite",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityType",
                table: "ClassAbility",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReplacesAbilities",
                table: "ClassAbility",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassType",
                table: "Class",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Class",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HPCurrent",
                table: "Character",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HPMax",
                table: "Character",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HPSubdual",
                table: "Character",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LiveDisplayMapPath",
                table: "Campaign",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContinentEnvironment",
                columns: table => new
                {
                    ContinentId = table.Column<int>(nullable: false),
                    EnvironmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContinentEnvironment", x => new { x.ContinentId, x.EnvironmentId });
                    table.ForeignKey(
                        name: "FK_ContinentEnvironment_Continent_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continent",
                        principalColumn: "ContinentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContinentEnvironment_Environment_EnvironmentId",
                        column: x => x.EnvironmentId,
                        principalTable: "Environment",
                        principalColumn: "EnvironmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OverridePrerequisite",
                columns: table => new
                {
                    ClassAbilityId = table.Column<int>(nullable: false),
                    PrerequisiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverridePrerequisite", x => new { x.ClassAbilityId, x.PrerequisiteId });
                    table.ForeignKey(
                        name: "FK_OverridePrerequisite_ClassAbility_ClassAbilityId",
                        column: x => x.ClassAbilityId,
                        principalTable: "ClassAbility",
                        principalColumn: "ClassAbilityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OverridePrerequisite_Prerequisite_PrerequisiteId",
                        column: x => x.PrerequisiteId,
                        principalTable: "Prerequisite",
                        principalColumn: "PrerequisiteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceAbility",
                columns: table => new
                {
                    RaceAbilityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    RaceId = table.Column<int>(nullable: false),
                    ReplacesRaceAbilities = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceAbility", x => x.RaceAbilityId);
                    table.ForeignKey(
                        name: "FK_RaceAbility_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterRaceAbility",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    RaceAbilityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterRaceAbility", x => new { x.CharacterId, x.RaceAbilityId });
                    table.ForeignKey(
                        name: "FK_CharacterRaceAbility_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterRaceAbility_RaceAbility_RaceAbilityId",
                        column: x => x.RaceAbilityId,
                        principalTable: "RaceAbility",
                        principalColumn: "RaceAbilityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackedEvent_ContinentId",
                table: "TrackedEvent",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterRaceAbility_RaceAbilityId",
                table: "CharacterRaceAbility",
                column: "RaceAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ContinentEnvironment_EnvironmentId",
                table: "ContinentEnvironment",
                column: "EnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OverridePrerequisite_PrerequisiteId",
                table: "OverridePrerequisite",
                column: "PrerequisiteId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceAbility_RaceId",
                table: "RaceAbility",
                column: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Race_RaceId",
                table: "Character",
                column: "RaceId",
                principalTable: "Race",
                principalColumn: "RaceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackedEvent_Continent_ContinentId",
                table: "TrackedEvent",
                column: "ContinentId",
                principalTable: "Continent",
                principalColumn: "ContinentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_Race_RaceId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackedEvent_Continent_ContinentId",
                table: "TrackedEvent");

            migrationBuilder.DropTable(
                name: "CharacterRaceAbility");

            migrationBuilder.DropTable(
                name: "ContinentEnvironment");

            migrationBuilder.DropTable(
                name: "OverridePrerequisite");

            migrationBuilder.DropTable(
                name: "RaceAbility");

            migrationBuilder.DropIndex(
                name: "IX_TrackedEvent_ContinentId",
                table: "TrackedEvent");

            migrationBuilder.DropColumn(
                name: "ContinentId",
                table: "TrackedEvent");

            migrationBuilder.DropColumn(
                name: "TrackedEventFreq",
                table: "TrackedEvent");

            migrationBuilder.DropColumn(
                name: "Option",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "ForId",
                table: "Prerequisite");

            migrationBuilder.DropColumn(
                name: "NeedId",
                table: "Prerequisite");

            migrationBuilder.DropColumn(
                name: "AbilityType",
                table: "ClassAbility");

            migrationBuilder.DropColumn(
                name: "ReplacesAbilities",
                table: "ClassAbility");

            migrationBuilder.DropColumn(
                name: "ClassType",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "HPCurrent",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "HPMax",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "HPSubdual",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "LiveDisplayMapPath",
                table: "Campaign");

            migrationBuilder.RenameColumn(
                name: "WeaponAttributeId",
                table: "WeaponAttribute",
                newName: "WpnAttId");

            migrationBuilder.AddColumn<int>(
                name: "FeatId",
                table: "Prerequisite",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SkillId",
                table: "Prerequisite",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DamageType",
                table: "Gear",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReplacesAbilityId",
                table: "ClassAbility",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prerequisite_FeatId",
                table: "Prerequisite",
                column: "FeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Prerequisite_SkillId",
                table: "Prerequisite",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Race_RaceId",
                table: "Character",
                column: "RaceId",
                principalTable: "Race",
                principalColumn: "RaceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prerequisite_Feat_FeatId",
                table: "Prerequisite",
                column: "FeatId",
                principalTable: "Feat",
                principalColumn: "FeatId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prerequisite_Skill_SkillId",
                table: "Prerequisite",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
