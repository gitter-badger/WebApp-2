using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Data.Migrations
{
    public partial class changeappgrant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppGrant");

            migrationBuilder.CreateTable(
                name: "LocalAppGrant",
                columns: table => new
                {
                    LocalAppGrantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    APIUserId = table.Column<string>(nullable: true),
                    AppID = table.Column<string>(nullable: true),
                    GrantTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalAppGrant", x => x.LocalAppGrantId);
                    table.ForeignKey(
                        name: "FK_LocalAppGrant_AspNetUsers_APIUserId",
                        column: x => x.APIUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalAppGrant_APIUserId",
                table: "LocalAppGrant",
                column: "APIUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalAppGrant");

            migrationBuilder.CreateTable(
                name: "AppGrant",
                columns: table => new
                {
                    AppGrantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    APIUserId = table.Column<string>(nullable: true),
                    AppID = table.Column<string>(nullable: true),
                    GrantTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppGrant", x => x.AppGrantId);
                    table.ForeignKey(
                        name: "FK_AppGrant_AspNetUsers_APIUserId",
                        column: x => x.APIUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppGrant_APIUserId",
                table: "AppGrant",
                column: "APIUserId");
        }
    }
}
