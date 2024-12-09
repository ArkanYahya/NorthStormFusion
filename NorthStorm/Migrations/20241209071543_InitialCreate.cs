using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthStorm.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Absences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AbsenceReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AbsenceInDays = table.Column<int>(type: "int", nullable: false),
                    OnAbsenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnrollDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Absences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BeneficiaryClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficiaryClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CareerClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    InflueritialCertificate = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GradeStart = table.Column<int>(type: "int", nullable: false),
                    StageStart = table.Column<int>(type: "int", nullable: false),
                    GradeEnd = table.Column<int>(type: "int", nullable: false),
                    AllocationPercentage = table.Column<int>(type: "int", nullable: false),
                    FirstPromotionDuration = table.Column<int>(type: "int", nullable: false),
                    CertificateOldCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollegeNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CollegeOldCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeSPSS1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegeNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComptenceClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KirkukSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaghdadSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComptenceClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditedServiceClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCountedForPromotion = table.Column<bool>(type: "bit", nullable: false),
                    IsCountedForBouns = table.Column<bool>(type: "bit", nullable: false),
                    IsCountedForRetirement = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditedServiceClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeputyClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeputyClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationalInstituteClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalInstituteClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EngilshLanguges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngilshLanguges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LowEvaluationLimit = table.Column<int>(type: "int", nullable: false),
                    UpperEvaluationLimit = table.Column<int>(type: "int", nullable: false),
                    PromotionEffect = table.Column<bool>(type: "bit", nullable: false),
                    MotivationEffect = table.Column<bool>(type: "bit", nullable: false),
                    BonusEffect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KirkukSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaghdadSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralAndPrecises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralAndPrecises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GovernmentalInstituteClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KirkukSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaghdadSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovernmentalInstituteClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobTitleChangeClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitleChangeClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobTitleClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KirkukSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaghdadSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitleClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LevelClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    KirkukSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaghdadSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KirkukSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaghdadSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatusClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaritalStatusAllowance = table.Column<int>(type: "int", nullable: false),
                    MaritalStatusSPSS = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatusClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MilitaryClassification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitaryClassification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KirkukSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaghdadSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivilegeClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExtendingRetirementAge = table.Column<int>(type: "int", nullable: false),
                    PromotionBenefites = table.Column<bool>(type: "bit", nullable: false),
                    OtherBenefites = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivilegeClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PromotionMinutesYear = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PunishmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PunishmentSeverity = table.Column<int>(type: "int", nullable: false),
                    PunishmentWriting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PunishmentEffect = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PunishmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KirkukSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaghdadSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RankOthers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RankAsNumber = table.Column<int>(type: "int", nullable: false),
                    RankAsWriting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RankAsFemale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RankAsMale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RankAsDistinction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RankAsGeneral = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankOthers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Religions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KirkukSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaghdadSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResponsibleClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponsiblityPercentage = table.Column<int>(type: "int", nullable: false),
                    ResponsiblSPSS1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsiblSPSS2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsiblityInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsibleClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RewardClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasicSalary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShiftClasifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftClasifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffPeriod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffClassifications", x => x.Id);
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
                name: "ThankClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThankSeniority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThankClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comptences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentComptenceId = table.Column<int>(type: "int", nullable: true),
                    ClassificationId = table.Column<int>(type: "int", nullable: true),
                    ComptenceSPSS = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comptences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comptences_ComptenceClassifications_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "ComptenceClassifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comptences_Comptences_ParentComptenceId",
                        column: x => x.ParentComptenceId,
                        principalTable: "Comptences",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CreditedServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnCreditedServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UntilDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreditedServiceInDays = table.Column<int>(type: "int", nullable: false),
                    CreditedServiceInMonthes = table.Column<int>(type: "int", nullable: false),
                    CreditedServiceInYears = table.Column<int>(type: "int", nullable: false),
                    CreditedServiceForPromotionInDays = table.Column<int>(type: "int", nullable: false),
                    CreditedServiceForPromotionInMonthes = table.Column<int>(type: "int", nullable: false),
                    CreditedServiceForPromotionInYears = table.Column<int>(type: "int", nullable: false),
                    CreditedServiceForBounsInDays = table.Column<int>(type: "int", nullable: false),
                    CreditedServiceForBounsInMonthes = table.Column<int>(type: "int", nullable: false),
                    CreditedServiceForBounsInYears = table.Column<int>(type: "int", nullable: false),
                    CreditedServiceForRetirementInDays = table.Column<int>(type: "int", nullable: false),
                    CreditedServiceForRetirementInMonthes = table.Column<int>(type: "int", nullable: false),
                    CreditedServiceForRetirementInYears = table.Column<int>(type: "int", nullable: false),
                    CreditedServiceClassificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditedServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditedServices_CreditedServiceClassifications_CreditedServiceClassificationId",
                        column: x => x.CreditedServiceClassificationId,
                        principalTable: "CreditedServiceClassifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Evaluation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvaluationInNumber = table.Column<int>(type: "int", nullable: false),
                    EvaluationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalaryGradeNumber = table.Column<int>(type: "int", nullable: false),
                    StageNumber = table.Column<int>(type: "int", nullable: false),
                    JobGradeNumber = table.Column<int>(type: "int", nullable: false),
                    EvaluationClassificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluation_EvaluationClassifications_EvaluationClassificationId",
                        column: x => x.EvaluationClassificationId,
                        principalTable: "EvaluationClassifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LeaveClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaveIndays = table.Column<int>(type: "int", nullable: false),
                    ServiceCharged = table.Column<bool>(type: "bit", nullable: false),
                    PromotionCharged = table.Column<bool>(type: "bit", nullable: false),
                    OutSideLaborForce = table.Column<bool>(type: "bit", nullable: false),
                    SalaryCharged = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    OtherNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveClassifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveClassifications_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassificationId = table.Column<int>(type: "int", nullable: true),
                    ParentLocationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_LocationClassifications_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "LocationClassifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Locations_Locations_ParentLocationId",
                        column: x => x.ParentLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Privileges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrivilegeClassificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privileges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Privileges_PrivilegeClassifications_PrivilegeClassificationId",
                        column: x => x.PrivilegeClassificationId,
                        principalTable: "PrivilegeClassifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Punishments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PunishmentReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PunishmentTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Punishments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Punishments_PunishmentTypes_PunishmentTypeId",
                        column: x => x.PunishmentTypeId,
                        principalTable: "PunishmentTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeNumber = table.Column<int>(type: "int", nullable: false),
                    GradeAsWriting = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    MinimumDuration = table.Column<int>(type: "int", nullable: false),
                    RankAsFemale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rankOtherId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_RankOthers_rankOtherId",
                        column: x => x.rankOtherId,
                        principalTable: "RankOthers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartingFromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponsibleClassificationId = table.Column<int>(type: "int", nullable: true),
                    DeputyClassificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobPositions_DeputyClassifications_DeputyClassificationId",
                        column: x => x.DeputyClassificationId,
                        principalTable: "DeputyClassifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobPositions_ResponsibleClassifications_ResponsibleClassificationId",
                        column: x => x.ResponsibleClassificationId,
                        principalTable: "ResponsibleClassifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnrollDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftClassificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shifts_ShiftClasifications_ShiftClassificationId",
                        column: x => x.ShiftClassificationId,
                        principalTable: "ShiftClasifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Recruitments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffClassificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruitments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recruitments_StaffClassifications_StaffClassificationId",
                        column: x => x.StaffClassificationId,
                        principalTable: "StaffClassifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DismissClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DismissSPSSCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DismissClassifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DismissClassifications_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ThankAndAppreciations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThankAndAppreciationReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThankSeniority = table.Column<int>(type: "int", nullable: false),
                    ThankClassificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThankAndAppreciations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThankAndAppreciations_ThankClassifications_ThankClassificationId",
                        column: x => x.ThankClassificationId,
                        principalTable: "ThankClassifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Leaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaveReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaveInDays = table.Column<int>(type: "int", nullable: false),
                    LeaveInMonthes = table.Column<int>(type: "int", nullable: false),
                    LeaveInYears = table.Column<int>(type: "int", nullable: false),
                    OnLeaveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupposedEnrollDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnrollDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LeaveClassificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leaves_LeaveClassifications_LeaveClassificationId",
                        column: x => x.LeaveClassificationId,
                        principalTable: "LeaveClassifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumintsAndComminications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RationIdNumber = table.Column<int>(type: "int", nullable: false),
                    RationCenterNumber = table.Column<int>(type: "int", nullable: false),
                    HouseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<int>(type: "int", nullable: false),
                    OilPhoneNumber = table.Column<int>(type: "int", nullable: false),
                    EmployeeEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    RationCenterId = table.Column<int>(type: "int", nullable: true),
                    CountryAddressId = table.Column<int>(type: "int", nullable: true),
                    GovernorateAddressId = table.Column<int>(type: "int", nullable: true),
                    CityAddressId = table.Column<int>(type: "int", nullable: true),
                    TownAddressId = table.Column<int>(type: "int", nullable: true),
                    NeighbrohoodAddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumintsAndComminications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumintsAndComminications_Locations_CityAddressId",
                        column: x => x.CityAddressId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumintsAndComminications_Locations_CountryAddressId",
                        column: x => x.CountryAddressId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumintsAndComminications_Locations_GovernorateAddressId",
                        column: x => x.GovernorateAddressId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumintsAndComminications_Locations_NeighbrohoodAddressId",
                        column: x => x.NeighbrohoodAddressId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumintsAndComminications_Locations_RationCenterId",
                        column: x => x.RationCenterId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumintsAndComminications_Locations_TownAddressId",
                        column: x => x.TownAddressId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EducationalInstitutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentEducationalInstitutionId = table.Column<int>(type: "int", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    ClassificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalInstitutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationalInstitutions_EducationalInstituteClassifications_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "EducationalInstituteClassifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EducationalInstitutions_EducationalInstitutions_ParentEducationalInstitutionId",
                        column: x => x.ParentEducationalInstitutionId,
                        principalTable: "EducationalInstitutions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EducationalInstitutions_Locations_LocationId",
                        column: x => x.LocationId,
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
                        name: "FK_GovernmentalInstitutes_GovernmentalInstituteClassifications_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "GovernmentalInstituteClassifications",
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
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentLevelId = table.Column<int>(type: "int", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    ClassificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Levels_LevelClassifications_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "LevelClassifications",
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
                    JobTitleClassificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitles", x => x.Id);
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
                        name: "FK_JobTitles_JobTitles_ParentJobTitleId",
                        column: x => x.ParentJobTitleId,
                        principalTable: "JobTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Retirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisengagementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DismissClassificationId = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Retirements_DismissClassifications_DismissClassificationId",
                        column: x => x.DismissClassificationId,
                        principalTable: "DismissClassifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Retirements_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
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
                    DestinationLevelId = table.Column<int>(type: "int", nullable: true),
                    LevelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobTransfers_Levels_DestinationLevelId",
                        column: x => x.DestinationLevelId,
                        principalTable: "Levels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobTransfers_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
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

            migrationBuilder.CreateTable(
                name: "Staffings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StaffingCount = table.Column<int>(type: "int", nullable: false),
                    EmployeeCounted = table.Column<int>(type: "int", nullable: false),
                    VacantStaffing = table.Column<int>(type: "int", nullable: false),
                    StaffingJobTitleId = table.Column<int>(type: "int", nullable: true),
                    StaffingUnitId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staffings_JobTitles_StaffingJobTitleId",
                        column: x => x.StaffingJobTitleId,
                        principalTable: "JobTitles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Staffings_Levels_StaffingUnitId",
                        column: x => x.StaffingUnitId,
                        principalTable: "Levels",
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
                    AbsenceId = table.Column<int>(type: "int", nullable: true),
                    CareerClassificationId = table.Column<int>(type: "int", nullable: true),
                    CreditedServiceId = table.Column<int>(type: "int", nullable: true),
                    DismissClassificationId = table.Column<int>(type: "int", nullable: true),
                    EngilshLangugeId = table.Column<int>(type: "int", nullable: true),
                    EvaluationId = table.Column<int>(type: "int", nullable: true),
                    GeneralAndPreciseId = table.Column<int>(type: "int", nullable: true),
                    GovernmentalInstituteClassificationId = table.Column<int>(type: "int", nullable: true),
                    JobPositionId = table.Column<int>(type: "int", nullable: true),
                    LeaveId = table.Column<int>(type: "int", nullable: true),
                    MaritalStatusClassificationId = table.Column<int>(type: "int", nullable: true),
                    MilitaryClassificationId = table.Column<int>(type: "int", nullable: true),
                    PrivilegeId = table.Column<int>(type: "int", nullable: true),
                    PunishmentId = table.Column<int>(type: "int", nullable: true),
                    ResponsibleClassificationId = table.Column<int>(type: "int", nullable: true),
                    RetirementId = table.Column<int>(type: "int", nullable: true),
                    ShiftClassificationId = table.Column<int>(type: "int", nullable: true),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    StaffClassificationId = table.Column<int>(type: "int", nullable: true),
                    StaffingId = table.Column<int>(type: "int", nullable: true),
                    ThankAndAppreciationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Absences_AbsenceId",
                        column: x => x.AbsenceId,
                        principalTable: "Absences",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_CareerClassifications_CareerClassificationId",
                        column: x => x.CareerClassificationId,
                        principalTable: "CareerClassifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_CreditedServices_CreditedServiceId",
                        column: x => x.CreditedServiceId,
                        principalTable: "CreditedServices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_DismissClassifications_DismissClassificationId",
                        column: x => x.DismissClassificationId,
                        principalTable: "DismissClassifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_EngilshLanguges_EngilshLangugeId",
                        column: x => x.EngilshLangugeId,
                        principalTable: "EngilshLanguges",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Evaluation_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_GeneralAndPrecises_GeneralAndPreciseId",
                        column: x => x.GeneralAndPreciseId,
                        principalTable: "GeneralAndPrecises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_GovernmentalInstituteClassifications_GovernmentalInstituteClassificationId",
                        column: x => x.GovernmentalInstituteClassificationId,
                        principalTable: "GovernmentalInstituteClassifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_JobPositions_JobPositionId",
                        column: x => x.JobPositionId,
                        principalTable: "JobPositions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Leaves_LeaveId",
                        column: x => x.LeaveId,
                        principalTable: "Leaves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_MaritalStatusClassifications_MaritalStatusClassificationId",
                        column: x => x.MaritalStatusClassificationId,
                        principalTable: "MaritalStatusClassifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_MilitaryClassification_MilitaryClassificationId",
                        column: x => x.MilitaryClassificationId,
                        principalTable: "MilitaryClassification",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationalities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Privileges_PrivilegeId",
                        column: x => x.PrivilegeId,
                        principalTable: "Privileges",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Punishments_PunishmentId",
                        column: x => x.PunishmentId,
                        principalTable: "Punishments",
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
                        name: "FK_Employees_Religions_ReligionId",
                        column: x => x.ReligionId,
                        principalTable: "Religions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_ResponsibleClassifications_ResponsibleClassificationId",
                        column: x => x.ResponsibleClassificationId,
                        principalTable: "ResponsibleClassifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Retirements_RetirementId",
                        column: x => x.RetirementId,
                        principalTable: "Retirements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_ShiftClasifications_ShiftClassificationId",
                        column: x => x.ShiftClassificationId,
                        principalTable: "ShiftClasifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_StaffClassifications_StaffClassificationId",
                        column: x => x.StaffClassificationId,
                        principalTable: "StaffClassifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Staffings_StaffingId",
                        column: x => x.StaffingId,
                        principalTable: "Staffings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_ThankAndAppreciations_ThankAndAppreciationId",
                        column: x => x.ThankAndAppreciationId,
                        principalTable: "ThankAndAppreciations",
                        principalColumn: "Id");
                });

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
                name: "EmployeeJobTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    JobTitleId = table.Column<int>(type: "int", nullable: false),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobTitleAssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeJobTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeJobTitles_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeJobTitles_JobTitles_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "JobTitles",
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "EmployeePromotion",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    PromotionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePromotion", x => new { x.EmployeesId, x.PromotionsId });
                    table.ForeignKey(
                        name: "FK_EmployeePromotion_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeePromotion_Promotions_PromotionsId",
                        column: x => x.PromotionsId,
                        principalTable: "Promotions",
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
                name: "NationalCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NationalCardNumber = table.Column<int>(type: "int", nullable: false),
                    CivilStatusIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalIdReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NationalIdExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlaceOfBirthId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NationalCards_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NationalCards_Locations_PlaceOfBirthId",
                        column: x => x.PlaceOfBirthId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CertificateEmployee_EmployeesId",
                table: "CertificateEmployee",
                column: "EmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_Comptences_ClassificationId",
                table: "Comptences",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comptences_ParentComptenceId",
                table: "Comptences",
                column: "ParentComptenceId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditedServices_CreditedServiceClassificationId",
                table: "CreditedServices",
                column: "CreditedServiceClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_DismissClassifications_StatusId",
                table: "DismissClassifications",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumintsAndComminications_CityAddressId",
                table: "DocumintsAndComminications",
                column: "CityAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumintsAndComminications_CountryAddressId",
                table: "DocumintsAndComminications",
                column: "CountryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumintsAndComminications_GovernorateAddressId",
                table: "DocumintsAndComminications",
                column: "GovernorateAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumintsAndComminications_NeighbrohoodAddressId",
                table: "DocumintsAndComminications",
                column: "NeighbrohoodAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumintsAndComminications_RationCenterId",
                table: "DocumintsAndComminications",
                column: "RationCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumintsAndComminications_TownAddressId",
                table: "DocumintsAndComminications",
                column: "TownAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_ClassificationId",
                table: "EducationalInstitutions",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_LocationId",
                table: "EducationalInstitutions",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_ParentEducationalInstitutionId",
                table: "EducationalInstitutions",
                column: "ParentEducationalInstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeJobTitles_EmployeeId",
                table: "EmployeeJobTitles",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeJobTitles_JobTitleId",
                table: "EmployeeJobTitles",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeJobTransfer_JobTransfersId",
                table: "EmployeeJobTransfer",
                column: "JobTransfersId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLevel_LevelsId",
                table: "EmployeeLevel",
                column: "LevelsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePromotion_PromotionsId",
                table: "EmployeePromotion",
                column: "PromotionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AbsenceId",
                table: "Employees",
                column: "AbsenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CareerClassificationId",
                table: "Employees",
                column: "CareerClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CreditedServiceId",
                table: "Employees",
                column: "CreditedServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DismissClassificationId",
                table: "Employees",
                column: "DismissClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EngilshLangugeId",
                table: "Employees",
                column: "EngilshLangugeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EvaluationId",
                table: "Employees",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GenderId",
                table: "Employees",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GeneralAndPreciseId",
                table: "Employees",
                column: "GeneralAndPreciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GovernmentalInstituteClassificationId",
                table: "Employees",
                column: "GovernmentalInstituteClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobPositionId",
                table: "Employees",
                column: "JobPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_LeaveId",
                table: "Employees",
                column: "LeaveId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_MaritalStatusClassificationId",
                table: "Employees",
                column: "MaritalStatusClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_MilitaryClassificationId",
                table: "Employees",
                column: "MilitaryClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_NationalityId",
                table: "Employees",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PrivilegeId",
                table: "Employees",
                column: "PrivilegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PunishmentId",
                table: "Employees",
                column: "PunishmentId");

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
                name: "IX_Employees_ResponsibleClassificationId",
                table: "Employees",
                column: "ResponsibleClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RetirementId",
                table: "Employees",
                column: "RetirementId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ShiftClassificationId",
                table: "Employees",
                column: "ShiftClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ShiftId",
                table: "Employees",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StaffClassificationId",
                table: "Employees",
                column: "StaffClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StaffingId",
                table: "Employees",
                column: "StaffingId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StatusId",
                table: "Employees",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ThankAndAppreciationId",
                table: "Employees",
                column: "ThankAndAppreciationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalary_SalariesId",
                table: "EmployeeSalary",
                column: "SalariesId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluation_EvaluationClassificationId",
                table: "Evaluation",
                column: "EvaluationClassificationId");

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
                name: "IX_Grades_rankOtherId",
                table: "Grades",
                column: "rankOtherId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPositions_DeputyClassificationId",
                table: "JobPositions",
                column: "DeputyClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPositions_ResponsibleClassificationId",
                table: "JobPositions",
                column: "ResponsibleClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitleLevel_LevelsId",
                table: "JobTitleLevel",
                column: "LevelsId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_ClassificationId",
                table: "JobTitles",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_GradeId",
                table: "JobTitles",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_JobTitleClassificationId",
                table: "JobTitles",
                column: "JobTitleClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_ParentJobTitleId",
                table: "JobTitles",
                column: "ParentJobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTransfers_DestinationLevelId",
                table: "JobTransfers",
                column: "DestinationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTransfers_LevelId",
                table: "JobTransfers",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveClassifications_GenderId",
                table: "LeaveClassifications",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_LeaveClassificationId",
                table: "Leaves",
                column: "LeaveClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_ClassificationId",
                table: "Levels",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_LocationId",
                table: "Levels",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_ParentLevelId",
                table: "Levels",
                column: "ParentLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ClassificationId",
                table: "Locations",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ParentLocationId",
                table: "Locations",
                column: "ParentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_NationalCards_EmployeeId",
                table: "NationalCards",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_NationalCards_PlaceOfBirthId",
                table: "NationalCards",
                column: "PlaceOfBirthId");

            migrationBuilder.CreateIndex(
                name: "IX_Privileges_PrivilegeClassificationId",
                table: "Privileges",
                column: "PrivilegeClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Punishments_PunishmentTypeId",
                table: "Punishments",
                column: "PunishmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitments_StaffClassificationId",
                table: "Recruitments",
                column: "StaffClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Retirements_DismissClassificationId",
                table: "Retirements",
                column: "DismissClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Retirements_StatusId",
                table: "Retirements",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ShiftClassificationId",
                table: "Shifts",
                column: "ShiftClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffings_StaffingJobTitleId",
                table: "Staffings",
                column: "StaffingJobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffings_StaffingUnitId",
                table: "Staffings",
                column: "StaffingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ThankAndAppreciations_ThankClassificationId",
                table: "ThankAndAppreciations",
                column: "ThankClassificationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeneficiaryClassifications");

            migrationBuilder.DropTable(
                name: "CertificateEmployee");

            migrationBuilder.DropTable(
                name: "CollegeNames");

            migrationBuilder.DropTable(
                name: "Comptences");

            migrationBuilder.DropTable(
                name: "DocumintsAndComminications");

            migrationBuilder.DropTable(
                name: "EducationalInstitutions");

            migrationBuilder.DropTable(
                name: "EmployeeJobTitles");

            migrationBuilder.DropTable(
                name: "EmployeeJobTransfer");

            migrationBuilder.DropTable(
                name: "EmployeeLevel");

            migrationBuilder.DropTable(
                name: "EmployeePromotion");

            migrationBuilder.DropTable(
                name: "EmployeeSalary");

            migrationBuilder.DropTable(
                name: "GovernmentalInstitutes");

            migrationBuilder.DropTable(
                name: "JobTitleChangeClassifications");

            migrationBuilder.DropTable(
                name: "JobTitleLevel");

            migrationBuilder.DropTable(
                name: "NationalCards");

            migrationBuilder.DropTable(
                name: "RewardClassifications");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "ComptenceClassifications");

            migrationBuilder.DropTable(
                name: "EducationalInstituteClassifications");

            migrationBuilder.DropTable(
                name: "JobTransfers");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "Salary");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Absences");

            migrationBuilder.DropTable(
                name: "CareerClassifications");

            migrationBuilder.DropTable(
                name: "CreditedServices");

            migrationBuilder.DropTable(
                name: "EngilshLanguges");

            migrationBuilder.DropTable(
                name: "Evaluation");

            migrationBuilder.DropTable(
                name: "GeneralAndPrecises");

            migrationBuilder.DropTable(
                name: "GovernmentalInstituteClassifications");

            migrationBuilder.DropTable(
                name: "JobPositions");

            migrationBuilder.DropTable(
                name: "Leaves");

            migrationBuilder.DropTable(
                name: "MaritalStatusClassifications");

            migrationBuilder.DropTable(
                name: "MilitaryClassification");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "Privileges");

            migrationBuilder.DropTable(
                name: "Punishments");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Recruitments");

            migrationBuilder.DropTable(
                name: "Religions");

            migrationBuilder.DropTable(
                name: "Retirements");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Staffings");

            migrationBuilder.DropTable(
                name: "ThankAndAppreciations");

            migrationBuilder.DropTable(
                name: "CreditedServiceClassifications");

            migrationBuilder.DropTable(
                name: "EvaluationClassifications");

            migrationBuilder.DropTable(
                name: "DeputyClassifications");

            migrationBuilder.DropTable(
                name: "ResponsibleClassifications");

            migrationBuilder.DropTable(
                name: "LeaveClassifications");

            migrationBuilder.DropTable(
                name: "PrivilegeClassifications");

            migrationBuilder.DropTable(
                name: "PunishmentTypes");

            migrationBuilder.DropTable(
                name: "StaffClassifications");

            migrationBuilder.DropTable(
                name: "DismissClassifications");

            migrationBuilder.DropTable(
                name: "ShiftClasifications");

            migrationBuilder.DropTable(
                name: "JobTitles");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "ThankClassifications");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "JobTitleClassifications");

            migrationBuilder.DropTable(
                name: "LevelClassifications");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "RankOthers");

            migrationBuilder.DropTable(
                name: "LocationClassifications");
        }
    }
}
