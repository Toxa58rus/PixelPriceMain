﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelService.Context.Migrations
{
    public partial class ChengePixelColorDeletePixelId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PixelId",
                table: "PixelColors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PixelId",
                table: "PixelColors",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
