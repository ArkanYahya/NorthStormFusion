using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthStorm.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_LocationClassifications_LocationClassificationId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "LocationClassificationId",
                table: "Locations",
                newName: "ClassificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_LocationClassificationId",
                table: "Locations",
                newName: "IX_Locations_ClassificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_LocationClassifications_ClassificationId",
                table: "Locations",
                column: "ClassificationId",
                principalTable: "LocationClassifications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_LocationClassifications_ClassificationId",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "ClassificationId",
                table: "Locations",
                newName: "LocationClassificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_ClassificationId",
                table: "Locations",
                newName: "IX_Locations_LocationClassificationId");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_LocationClassifications_LocationClassificationId",
                table: "Locations",
                column: "LocationClassificationId",
                principalTable: "LocationClassifications",
                principalColumn: "Id");
        }
    }
}
