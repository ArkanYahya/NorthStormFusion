using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthStorm.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeToJobTransfers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeJobTransfer_JobTransfers_JobTransfersId",
                table: "EmployeeJobTransfer");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeJobTransfer_JobTransfers_JobTransfersId",
                table: "EmployeeJobTransfer",
                column: "JobTransfersId",
                principalTable: "JobTransfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeJobTransfer_JobTransfers_JobTransfersId",
                table: "EmployeeJobTransfer");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeJobTransfer_JobTransfers_JobTransfersId",
                table: "EmployeeJobTransfer",
                column: "JobTransfersId",
                principalTable: "JobTransfers",
                principalColumn: "Id");
        }
    }
}
