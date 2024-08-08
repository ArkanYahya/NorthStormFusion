using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthStorm.Migrations
{
    /// <inheritdoc />
    public partial class update8_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BaghdadSymbol",
                table: "GovernmentalInstituteClassifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KirkukSymbol",
                table: "GovernmentalInstituteClassifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GovernmentalInstituteClassificationId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GovernmentalInstituteClassificationId",
                table: "Employees",
                column: "GovernmentalInstituteClassificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_GovernmentalInstituteClassifications_GovernmentalInstituteClassificationId",
                table: "Employees",
                column: "GovernmentalInstituteClassificationId",
                principalTable: "GovernmentalInstituteClassifications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_GovernmentalInstituteClassifications_GovernmentalInstituteClassificationId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_GovernmentalInstituteClassificationId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BaghdadSymbol",
                table: "GovernmentalInstituteClassifications");

            migrationBuilder.DropColumn(
                name: "KirkukSymbol",
                table: "GovernmentalInstituteClassifications");

            migrationBuilder.DropColumn(
                name: "GovernmentalInstituteClassificationId",
                table: "Employees");
        }
    }
}
