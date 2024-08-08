using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthStorm.Migrations
{
    /// <inheritdoc />
    public partial class correctRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobTitles_GovernmentalInstituteClassification_GovernmentalInstituteClassificationId",
                table: "JobTitles");

            migrationBuilder.DropIndex(
                name: "IX_JobTitles_GovernmentalInstituteClassificationId",
                table: "JobTitles");

            migrationBuilder.DropColumn(
                name: "GovernmentalInstituteClassificationId",
                table: "JobTitles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GovernmentalInstituteClassificationId",
                table: "JobTitles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_GovernmentalInstituteClassificationId",
                table: "JobTitles",
                column: "GovernmentalInstituteClassificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTitles_GovernmentalInstituteClassification_GovernmentalInstituteClassificationId",
                table: "JobTitles",
                column: "GovernmentalInstituteClassificationId",
                principalTable: "GovernmentalInstituteClassification",
                principalColumn: "Id");
        }
    }
}
