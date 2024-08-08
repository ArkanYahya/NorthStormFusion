using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthStorm.Migrations
{
    /// <inheritdoc />
    public partial class update_7_4_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobTitles_JobTitles_JobTitleId",
                table: "JobTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTitles_JobTitles_ParentJobTitleId",
                table: "JobTitles");

            migrationBuilder.DropIndex(
                name: "IX_JobTitles_JobTitleId",
                table: "JobTitles");

            migrationBuilder.DropColumn(
                name: "JobTitleId",
                table: "JobTitles");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTitles_JobTitles_ParentJobTitleId",
                table: "JobTitles",
                column: "ParentJobTitleId",
                principalTable: "JobTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobTitles_JobTitles_ParentJobTitleId",
                table: "JobTitles");

            migrationBuilder.AddColumn<int>(
                name: "JobTitleId",
                table: "JobTitles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_JobTitleId",
                table: "JobTitles",
                column: "JobTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTitles_JobTitles_JobTitleId",
                table: "JobTitles",
                column: "JobTitleId",
                principalTable: "JobTitles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTitles_JobTitles_ParentJobTitleId",
                table: "JobTitles",
                column: "ParentJobTitleId",
                principalTable: "JobTitles",
                principalColumn: "Id");
        }
    }
}
