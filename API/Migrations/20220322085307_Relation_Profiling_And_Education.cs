using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Relation_Profiling_And_Education : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GPA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profilings_EducationId",
                table: "Profilings",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profilings_Educations_EducationId",
                table: "Profilings",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profilings_Educations_EducationId",
                table: "Profilings");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Profilings_EducationId",
                table: "Profilings");
        }
    }
}
