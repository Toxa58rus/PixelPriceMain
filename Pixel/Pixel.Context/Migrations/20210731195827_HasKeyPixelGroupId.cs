using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PixelService.Context.Migrations
{
    public partial class HasKeyPixelGroupId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PixelGroup",
                newName: "PixelGroups");

            migrationBuilder.RenameTable(
                name: "PixelColor",
                newName: "PixelColors");


            migrationBuilder.AddPrimaryKey(
                name: "PK_PixelGroups",
                table: "PixelGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PixelColors",
                table: "PixelColors",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PixelGroups",
                table: "PixelGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PixelColors",
                table: "PixelColors");

            migrationBuilder.RenameTable(
                name: "PixelGroups",
                newName: "PixelGroup");

            migrationBuilder.RenameTable(
                name: "PixelColors",
                newName: "PixelColor");


        }
    }
}
