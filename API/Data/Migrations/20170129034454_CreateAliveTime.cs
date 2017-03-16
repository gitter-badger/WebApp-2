using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class CreateAliveTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "AliveTime",
                table: "AccessToken",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 20, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "AccessToken",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AliveTime",
                table: "AccessToken");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "AccessToken");
        }
    }
}
