﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelService.Context.Migrations
{
    public partial class add_Color_Id_in_Pixel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ColorId",
                table: "Pixels",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Pixels");
        }
    }
}
