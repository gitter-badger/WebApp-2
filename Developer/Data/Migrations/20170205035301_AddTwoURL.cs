using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Developer.Data.Migrations
{
    public partial class AddTwoURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LicenseUrl",
                table: "Apps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrivacyStatementUrl",
                table: "Apps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenseUrl",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "PrivacyStatementUrl",
                table: "Apps");
        }
    }
}
