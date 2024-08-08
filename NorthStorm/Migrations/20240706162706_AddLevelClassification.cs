using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthStorm.Migrations
{
    /// <inheritdoc />
    public partial class AddLevelClassification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassificationId",
                table: "Levels",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Levels_ClassificationId",
                table: "Levels",
                column: "ClassificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_LevelClassifications_ClassificationId",
                table: "Levels",
                column: "ClassificationId",
                principalTable: "LevelClassifications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_LevelClassifications_ClassificationId",
                table: "Levels");

            migrationBuilder.DropTable(
                name: "LevelClassifications");

            migrationBuilder.DropIndex(
                name: "IX_Levels_ClassificationId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "ClassificationId",
                table: "Levels");
        }
    }
}
