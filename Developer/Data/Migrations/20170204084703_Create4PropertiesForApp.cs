using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Developer.Data.Migrations
{
    public partial class Create4PropertiesForApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DebugMode",
                table: "Apps",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnableOAuth",
                table: "Apps",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ForceConfirmation",
                table: "Apps",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ForceInputPassword",
                table: "Apps",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DebugMode",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "EnableOAuth",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "ForceConfirmation",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "ForceInputPassword",
                table: "Apps");
        }
    }
}
