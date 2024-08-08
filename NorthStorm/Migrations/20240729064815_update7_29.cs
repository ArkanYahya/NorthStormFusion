using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthStorm.Migrations
{
    /// <inheritdoc />
    public partial class update7_29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GovernmentalInstitutes_GovernmentalInstituteClassification_ClassificationId",
                table: "GovernmentalInstitutes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GovernmentalInstituteClassification",
                table: "GovernmentalInstituteClassification");

            migrationBuilder.RenameTable(
                name: "GovernmentalInstituteClassification",
                newName: "GovernmentalInstituteClassifications");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GovernmentalInstituteClassifications",
                table: "GovernmentalInstituteClassifications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GovernmentalInstitutes_GovernmentalInstituteClassifications_ClassificationId",
                table: "GovernmentalInstitutes",
                column: "ClassificationId",
                principalTable: "GovernmentalInstituteClassifications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GovernmentalInstitutes_GovernmentalInstituteClassifications_ClassificationId",
                table: "GovernmentalInstitutes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GovernmentalInstituteClassifications",
                table: "GovernmentalInstituteClassifications");

            migrationBuilder.RenameTable(
                name: "GovernmentalInstituteClassifications",
                newName: "GovernmentalInstituteClassification");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GovernmentalInstituteClassification",
                table: "GovernmentalInstituteClassification",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GovernmentalInstitutes_GovernmentalInstituteClassification_ClassificationId",
                table: "GovernmentalInstitutes",
                column: "ClassificationId",
                principalTable: "GovernmentalInstituteClassification",
                principalColumn: "Id");
        }
    }
}
