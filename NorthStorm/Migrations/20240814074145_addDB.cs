using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthStorm.Migrations
{
    /// <inheritdoc />
    public partial class addDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TmpAppreciationId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TmpBonusId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TmpLeaveId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TmpLeaveRequestId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TmpPromotionId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TmpAppreciations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Cause = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TmpAppreciations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TmpBonuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    NextDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TmpBonuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TmpLeaveRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endtDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TmpLeaveRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TmpLeaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TmpLeaves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TmpPromotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    NextDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TmpPromotions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TmpAppreciationId",
                table: "Employees",
                column: "TmpAppreciationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TmpBonusId",
                table: "Employees",
                column: "TmpBonusId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TmpLeaveId",
                table: "Employees",
                column: "TmpLeaveId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TmpLeaveRequestId",
                table: "Employees",
                column: "TmpLeaveRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TmpPromotionId",
                table: "Employees",
                column: "TmpPromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_TmpAppreciations_TmpAppreciationId",
                table: "Employees",
                column: "TmpAppreciationId",
                principalTable: "TmpAppreciations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_TmpBonuses_TmpBonusId",
                table: "Employees",
                column: "TmpBonusId",
                principalTable: "TmpBonuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_TmpLeaveRequests_TmpLeaveRequestId",
                table: "Employees",
                column: "TmpLeaveRequestId",
                principalTable: "TmpLeaveRequests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_TmpLeaves_TmpLeaveId",
                table: "Employees",
                column: "TmpLeaveId",
                principalTable: "TmpLeaves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_TmpPromotions_TmpPromotionId",
                table: "Employees",
                column: "TmpPromotionId",
                principalTable: "TmpPromotions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_TmpAppreciations_TmpAppreciationId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_TmpBonuses_TmpBonusId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_TmpLeaveRequests_TmpLeaveRequestId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_TmpLeaves_TmpLeaveId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_TmpPromotions_TmpPromotionId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "TmpAppreciations");

            migrationBuilder.DropTable(
                name: "TmpBonuses");

            migrationBuilder.DropTable(
                name: "TmpLeaveRequests");

            migrationBuilder.DropTable(
                name: "TmpLeaves");

            migrationBuilder.DropTable(
                name: "TmpPromotions");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TmpAppreciationId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TmpBonusId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TmpLeaveId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TmpLeaveRequestId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TmpPromotionId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TmpAppreciationId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TmpBonusId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TmpLeaveId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TmpLeaveRequestId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TmpPromotionId",
                table: "Employees");
        }
    }
}
