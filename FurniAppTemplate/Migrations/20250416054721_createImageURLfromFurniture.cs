﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurniAppTemplate.Migrations
{
    /// <inheritdoc />
    public partial class createImageURLfromFurniture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Furnitures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Furnitures");
        }
    }
}
