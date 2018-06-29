using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DBConnect.Migrations
{
    public partial class Updoot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WpnAttId",
                table: "WeaponAttribute",
                newName: "WeaponAttributeId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackedEventFreq",
                table: "TrackedEvent");

            migrationBuilder.DropColumn(
                name: "Option",
                table: "Skill");

            migrationBuilder.RenameColumn(
                name: "WeaponAttributeId",
                table: "WeaponAttribute",
                newName: "WpnAttId");
        }
    }
}
