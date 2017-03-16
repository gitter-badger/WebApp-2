using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Developer.Data.Migrations
{
    public partial class RenameUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sex",
                table: "AspNetUsers",
                newName: "Sex");

            migrationBuilder.RenameColumn(
                name: "preferedLanguage",
                table: "AspNetUsers",
                newName: "PreferedLanguage");

            migrationBuilder.RenameColumn(
                name: "nickname",
                table: "AspNetUsers",
                newName: "NickName");

            migrationBuilder.RenameColumn(
                name: "headimgurl",
                table: "AspNetUsers",
                newName: "HeadImgUrl");

            migrationBuilder.RenameColumn(
                name: "accountCreateTime",
                table: "AspNetUsers",
                newName: "AccountCreateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "AspNetUsers",
                newName: "sex");

            migrationBuilder.RenameColumn(
                name: "PreferedLanguage",
                table: "AspNetUsers",
                newName: "preferedLanguage");

            migrationBuilder.RenameColumn(
                name: "NickName",
                table: "AspNetUsers",
                newName: "nickname");

            migrationBuilder.RenameColumn(
                name: "HeadImgUrl",
                table: "AspNetUsers",
                newName: "headimgurl");

            migrationBuilder.RenameColumn(
                name: "AccountCreateTime",
                table: "AspNetUsers",
                newName: "accountCreateTime");
        }
    }
}
