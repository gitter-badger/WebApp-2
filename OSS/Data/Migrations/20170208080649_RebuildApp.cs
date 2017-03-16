using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OSS.Data.Migrations
{
    public partial class RebuildApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bucket_AspNetUsers_CreaterId",
                table: "Bucket");

            migrationBuilder.DropIndex(
                name: "IX_Bucket_CreaterId",
                table: "Bucket");

            migrationBuilder.DropColumn(
                name: "CreaterId",
                table: "Bucket");

            migrationBuilder.AddColumn<int>(
                name: "BelongingAppId",
                table: "Bucket",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    AppId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "CreaterId",
                table: "Bucket",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bucket_CreaterId",
                table: "Bucket",
                column: "CreaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bucket_AspNetUsers_CreaterId",
                table: "Bucket",
                column: "CreaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
