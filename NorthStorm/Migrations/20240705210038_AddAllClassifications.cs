using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthStorm.Migrations
{
    /// <inheritdoc />
    public partial class AddAllClassifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Religiones_ReligionId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Religiones");

            migrationBuilder.AddColumn<string>(
                name: "BaghdadSymbol",
                table: "JobTitleClassifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KirkukSymbol",
                table: "JobTitleClassifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BaghdadSymbol",
                table: "Genders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KirkukSymbol",
                table: "Genders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Religions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religions", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Religions_ReligionId",
                table: "Employees",
                column: "ReligionId",
                principalTable: "Religions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Religions_ReligionId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Religions");

            migrationBuilder.DropColumn(
                name: "BaghdadSymbol",
                table: "JobTitleClassifications");

            migrationBuilder.DropColumn(
                name: "KirkukSymbol",
                table: "JobTitleClassifications");

            migrationBuilder.DropColumn(
                name: "BaghdadSymbol",
                table: "Genders");

            migrationBuilder.DropColumn(
                name: "KirkukSymbol",
                table: "Genders");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Religiones_ReligionId",
                table: "Employees",
                column: "ReligionId",
                principalTable: "Religiones",
                principalColumn: "Id");
        }
    }
}
