using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelService.Context.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pixels_PixelGroups_PixelGroupId",
                table: "Pixels");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Pixels");

            migrationBuilder.AlterColumn<Guid>(
                name: "PixelGroupId",
                table: "Pixels",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pixels_PixelGroups_PixelGroupId",
                table: "Pixels",
                column: "PixelGroupId",
                principalTable: "PixelGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pixels_PixelGroups_PixelGroupId",
                table: "Pixels");

            migrationBuilder.AlterColumn<Guid>(
                name: "PixelGroupId",
                table: "Pixels",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Pixels",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Pixels_PixelGroups_PixelGroupId",
                table: "Pixels",
                column: "PixelGroupId",
                principalTable: "PixelGroups",
                principalColumn: "Id");
        }
    }
}
