using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class ChangeAccessTokenMeaning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessToken_OAuthPack_PackId",
                table: "AccessToken");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AccessToken_PackId",
                table: "AccessToken");

            migrationBuilder.DropColumn(
                name: "PackId",
                table: "AccessToken");

            migrationBuilder.AddColumn<string>(
                name: "ApplyAppId",
                table: "AccessToken",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "ApplyAppId",
                table: "AccessToken");

            migrationBuilder.AddColumn<int>(
                name: "PackId",
                table: "AccessToken",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_AccessToken_PackId",
                table: "AccessToken",
                column: "PackId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessToken_OAuthPack_PackId",
                table: "AccessToken",
                column: "PackId",
                principalTable: "OAuthPack",
                principalColumn: "OAuthPackId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
