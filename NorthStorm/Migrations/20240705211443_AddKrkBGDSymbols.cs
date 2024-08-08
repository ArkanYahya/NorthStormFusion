using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthStorm.Migrations
{
    /// <inheritdoc />
    public partial class AddKrkBGDSymbols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BaghdadSymbol",
                table: "Religions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KirkukSymbol",
                table: "Religions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BaghdadSymbol",
                table: "Races",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KirkukSymbol",
                table: "Races",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BaghdadSymbol",
                table: "Nationalities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KirkukSymbol",
                table: "Nationalities",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaghdadSymbol",
                table: "Religions");

            migrationBuilder.DropColumn(
                name: "KirkukSymbol",
                table: "Religions");

            migrationBuilder.DropColumn(
                name: "BaghdadSymbol",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "KirkukSymbol",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "BaghdadSymbol",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "KirkukSymbol",
                table: "Nationalities");
        }
    }
}
