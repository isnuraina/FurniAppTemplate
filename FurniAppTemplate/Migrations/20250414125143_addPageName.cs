using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurniAppTemplate.Migrations
{
    /// <inheritdoc />
    public partial class addPageName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PageName",
                table: "MainChairAndContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageName",
                table: "MainChairAndContents");
        }
    }
}
