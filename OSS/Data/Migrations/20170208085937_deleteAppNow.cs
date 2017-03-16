using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OSS.Data.Migrations
{
    public partial class deleteAppNow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bucket_Apps_BelongingAppId",
                table: "Bucket");

            migrationBuilder.DropTable(
                name: "Apps");

            migrationBuilder.DropIndex(
                name: "IX_Bucket_BelongingAppId",
                table: "Bucket");

            migrationBuilder.DropColumn(
                name: "BelongingAppId",
                table: "Bucket");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BelongingAppId",
                table: "Bucket",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    AppId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.AppId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bucket_BelongingAppId",
                table: "Bucket",
                column: "BelongingAppId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bucket_Apps_BelongingAppId",
                table: "Bucket",
                column: "BelongingAppId",
                principalTable: "Apps",
                principalColumn: "AppId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
