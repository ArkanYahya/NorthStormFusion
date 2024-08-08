using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthStorm.Migrations
{
    /// <inheritdoc />
    public partial class AddNewRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Certificates_CertificateId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTitles_Employees_EmployeeId",
                table: "JobTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTitles_Levels_LevelId",
                table: "JobTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Employees_EmployeeId",
                table: "Levels");

            migrationBuilder.DropForeignKey(
                name: "FK_Salary_Employees_EmployeeId",
                table: "Salary");

            migrationBuilder.DropIndex(
                name: "IX_Salary_EmployeeId",
                table: "Salary");

            migrationBuilder.DropIndex(
                name: "IX_Levels_EmployeeId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_JobTitles_EmployeeId",
                table: "JobTitles");

            migrationBuilder.DropIndex(
                name: "IX_JobTitles_LevelId",
                table: "JobTitles");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CertificateId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Salary");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "JobTitles");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "JobTitles");

            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "JobTransfers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CertificateEmployee",
                columns: table => new
                {
                    CertificatesId = table.Column<int>(type: "int", nullable: false),
                    EmployeesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateEmployee", x => new { x.CertificatesId, x.EmployeesId });
                    table.ForeignKey(
                        name: "FK_CertificateEmployee_Certificates_CertificatesId",
                        column: x => x.CertificatesId,
                        principalTable: "Certificates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CertificateEmployee_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeJobTitle",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    JobTitlesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeJobTitle", x => new { x.EmployeesId, x.JobTitlesId });
                    table.ForeignKey(
                        name: "FK_EmployeeJobTitle_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeJobTitle_JobTitles_JobTitlesId",
                        column: x => x.JobTitlesId,
                        principalTable: "JobTitles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLevel",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    LevelsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLevel", x => new { x.EmployeesId, x.LevelsId });
                    table.ForeignKey(
                        name: "FK_EmployeeLevel_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeLevel_Levels_LevelsId",
                        column: x => x.LevelsId,
                        principalTable: "Levels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalary",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    SalariesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalary", x => new { x.EmployeesId, x.SalariesId });
                    table.ForeignKey(
                        name: "FK_EmployeeSalary_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeSalary_Salary_SalariesId",
                        column: x => x.SalariesId,
                        principalTable: "Salary",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobTitleLevel",
                columns: table => new
                {
                    JobTitlesId = table.Column<int>(type: "int", nullable: false),
                    LevelsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitleLevel", x => new { x.JobTitlesId, x.LevelsId });
                    table.ForeignKey(
                        name: "FK_JobTitleLevel_JobTitles_JobTitlesId",
                        column: x => x.JobTitlesId,
                        principalTable: "JobTitles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobTitleLevel_Levels_LevelsId",
                        column: x => x.LevelsId,
                        principalTable: "Levels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobTransfers_LevelId",
                table: "JobTransfers",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateEmployee_EmployeesId",
                table: "CertificateEmployee",
                column: "EmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeJobTitle_JobTitlesId",
                table: "EmployeeJobTitle",
                column: "JobTitlesId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLevel_LevelsId",
                table: "EmployeeLevel",
                column: "LevelsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalary_SalariesId",
                table: "EmployeeSalary",
                column: "SalariesId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitleLevel_LevelsId",
                table: "JobTitleLevel",
                column: "LevelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTransfers_Levels_LevelId",
                table: "JobTransfers",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobTransfers_Levels_LevelId",
                table: "JobTransfers");

            migrationBuilder.DropTable(
                name: "CertificateEmployee");

            migrationBuilder.DropTable(
                name: "EmployeeJobTitle");

            migrationBuilder.DropTable(
                name: "EmployeeLevel");

            migrationBuilder.DropTable(
                name: "EmployeeSalary");

            migrationBuilder.DropTable(
                name: "JobTitleLevel");

            migrationBuilder.DropIndex(
                name: "IX_JobTransfers_LevelId",
                table: "JobTransfers");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "JobTransfers");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Salary",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Levels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "JobTitles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "JobTitles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CertificateId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salary_EmployeeId",
                table: "Salary",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_EmployeeId",
                table: "Levels",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_EmployeeId",
                table: "JobTitles",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_LevelId",
                table: "JobTitles",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CertificateId",
                table: "Employees",
                column: "CertificateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Certificates_CertificateId",
                table: "Employees",
                column: "CertificateId",
                principalTable: "Certificates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTitles_Employees_EmployeeId",
                table: "JobTitles",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTitles_Levels_LevelId",
                table: "JobTitles",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Employees_EmployeeId",
                table: "Levels",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Salary_Employees_EmployeeId",
                table: "Salary",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
