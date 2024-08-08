using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthStorm.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GovernmentalInstituteClassification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovernmentalInstituteClassification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeNumber = table.Column<int>(type: "int", nullable: false),
                    GradeAsWriting = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Stage01 = table.Column<int>(type: "int", nullable: false),
                    Stage02 = table.Column<int>(type: "int", nullable: false),
                    Stage03 = table.Column<int>(type: "int", nullable: false),
                    Stage04 = table.Column<int>(type: "int", nullable: false),
                    Stage05 = table.Column<int>(type: "int", nullable: false),
                    Stage06 = table.Column<int>(type: "int", nullable: false),
                    Stage07 = table.Column<int>(type: "int", nullable: false),
                    Stage08 = table.Column<int>(type: "int", nullable: false),
                    Stage09 = table.Column<int>(type: "int", nullable: false),
                    Stage10 = table.Column<int>(type: "int", nullable: false),
                    Stage11 = table.Column<int>(type: "int", nullable: false),
                    AnnualBonus = table.Column<int>(type: "int", nullable: false),
                    MinimumDuration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobTitleClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitleClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recruitments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruitments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Religiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religiones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentLocationId = table.Column<int>(type: "int", nullable: true),
                    LocationClassificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_LocationClassifications_LocationClassificationId",
                        column: x => x.LocationClassificationId,
                        principalTable: "LocationClassifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Locations_Locations_ParentLocationId",
                        column: x => x.ParentLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GovernmentalInstitutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassificationId = table.Column<int>(type: "int", nullable: true),
                    ParentGovernmentalInstituteId = table.Column<int>(type: "int", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovernmentalInstitutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GovernmentalInstitutes_GovernmentalInstituteClassification_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "GovernmentalInstituteClassification",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GovernmentalInstitutes_GovernmentalInstitutes_ParentGovernmentalInstituteId",
                        column: x => x.ParentGovernmentalInstituteId,
                        principalTable: "GovernmentalInstitutes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GovernmentalInstitutes_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GlobalSpecialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccurateSpecialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_GovernmentalInstitutes_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "GovernmentalInstitutes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FourthName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CivilNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: true),
                    ReligionId = table.Column<int>(type: "int", nullable: true),
                    RaceId = table.Column<int>(type: "int", nullable: true),
                    NationalityId = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    RecruitmentId = table.Column<int>(type: "int", nullable: true),
                    CertificateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Certificates_CertificateId",
                        column: x => x.CertificateId,
                        principalTable: "Certificates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationalities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Recruitments_RecruitmentId",
                        column: x => x.RecruitmentId,
                        principalTable: "Recruitments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Religiones_ReligionId",
                        column: x => x.ReligionId,
                        principalTable: "Religiones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentLevelId = table.Column<int>(type: "int", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Levels_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Levels_Levels_ParentLevelId",
                        column: x => x.ParentLevelId,
                        principalTable: "Levels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Levels_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Salary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasicSalary = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salary_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentJobTitleId = table.Column<int>(type: "int", nullable: true),
                    ClassificationId = table.Column<int>(type: "int", nullable: true),
                    GradeId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    GovernmentalInstituteClassificationId = table.Column<int>(type: "int", nullable: true),
                    JobTitleClassificationId = table.Column<int>(type: "int", nullable: true),
                    JobTitleId = table.Column<int>(type: "int", nullable: true),
                    LevelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobTitles_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobTitles_GovernmentalInstituteClassification_GovernmentalInstituteClassificationId",
                        column: x => x.GovernmentalInstituteClassificationId,
                        principalTable: "GovernmentalInstituteClassification",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobTitles_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobTitles_JobTitleClassifications_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "JobTitleClassifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobTitles_JobTitleClassifications_JobTitleClassificationId",
                        column: x => x.JobTitleClassificationId,
                        principalTable: "JobTitleClassifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobTitles_JobTitles_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "JobTitles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobTitles_JobTitles_ParentJobTitleId",
                        column: x => x.ParentJobTitleId,
                        principalTable: "JobTitles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobTitles_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobTransfers_Levels_DestinationLevelId",
                        column: x => x.DestinationLevelId,
                        principalTable: "Levels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeJobTransfer",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    JobTransfersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeJobTransfer", x => new { x.EmployeesId, x.JobTransfersId });
                    table.ForeignKey(
                        name: "FK_EmployeeJobTransfer_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeJobTransfer_JobTransfers_JobTransfersId",
                        column: x => x.JobTransfersId,
                        principalTable: "JobTransfers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_UniversityId",
                table: "Certificates",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeJobTransfer_JobTransfersId",
                table: "EmployeeJobTransfer",
                column: "JobTransfersId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CertificateId",
                table: "Employees",
                column: "CertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GenderId",
                table: "Employees",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_NationalityId",
                table: "Employees",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RaceId",
                table: "Employees",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RecruitmentId",
                table: "Employees",
                column: "RecruitmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ReligionId",
                table: "Employees",
                column: "ReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StatusId",
                table: "Employees",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_GovernmentalInstitutes_ClassificationId",
                table: "GovernmentalInstitutes",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_GovernmentalInstitutes_LocationId",
                table: "GovernmentalInstitutes",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_GovernmentalInstitutes_ParentGovernmentalInstituteId",
                table: "GovernmentalInstitutes",
                column: "ParentGovernmentalInstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_ClassificationId",
                table: "JobTitles",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_EmployeeId",
                table: "JobTitles",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_GovernmentalInstituteClassificationId",
                table: "JobTitles",
                column: "GovernmentalInstituteClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_GradeId",
                table: "JobTitles",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_JobTitleClassificationId",
                table: "JobTitles",
                column: "JobTitleClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_JobTitleId",
                table: "JobTitles",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_LevelId",
                table: "JobTitles",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_ParentJobTitleId",
                table: "JobTitles",
                column: "ParentJobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTransfers_DestinationLevelId",
                table: "JobTransfers",
                column: "DestinationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_EmployeeId",
                table: "Levels",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_LocationId",
                table: "Levels",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_ParentLevelId",
                table: "Levels",
                column: "ParentLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_LocationClassificationId",
                table: "Locations",
                column: "LocationClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ParentLocationId",
                table: "Locations",
                column: "ParentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Salary_EmployeeId",
                table: "Salary",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeJobTransfer");

            migrationBuilder.DropTable(
                name: "JobTitles");

            migrationBuilder.DropTable(
                name: "Salary");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "JobTransfers");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "JobTitleClassifications");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Recruitments");

            migrationBuilder.DropTable(
                name: "Religiones");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "GovernmentalInstitutes");

            migrationBuilder.DropTable(
                name: "GovernmentalInstituteClassification");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "LocationClassifications");
        }
    }
}
