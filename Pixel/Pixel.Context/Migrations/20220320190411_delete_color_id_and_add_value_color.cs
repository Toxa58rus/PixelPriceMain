using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelService.Context.Migrations
{
    public partial class delete_color_id_and_add_value_color : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PixelColors");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Pixels");

            migrationBuilder.AlterDatabase(
                oldCollation: "en_US.UTF-8");

            migrationBuilder.AddColumn<int>(
                name: "Color",
                table: "Pixels",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Pixels");

            migrationBuilder.AlterDatabase(
                collation: "en_US.UTF-8");

            migrationBuilder.AddColumn<Guid>(
                name: "ColorId",
                table: "Pixels",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PixelColors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Color = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PixelColors", x => x.Id);
                });
        }
    }
}
