using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewBackend2.Migrations
{
    public partial class ModifyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diagnostic");

            migrationBuilder.CreateTable(
                name: "Diagnosis",
                columns: table => new
                {
                    DiagnosticId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    DiseaseName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SymptomList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DoctorSpecialization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosis", x => x.DiagnosticId);
                    table.ForeignKey(
                        name: "FK_Diagnosis_Disease_DiseaseName",
                        column: x => x.DiseaseName,
                        principalTable: "Disease",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_Diagnosis_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_DiseaseName",
                table: "Diagnosis",
                column: "DiseaseName");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_UserId",
                table: "Diagnosis",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diagnosis");

            migrationBuilder.CreateTable(
                name: "Diagnostic",
                columns: table => new
                {
                    DiagnosticId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseaseName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    DoctorSpecialization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DoctorTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SymptomList = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnostic", x => x.DiagnosticId);
                    table.ForeignKey(
                        name: "FK_Diagnostic_Disease_DiseaseName",
                        column: x => x.DiseaseName,
                        principalTable: "Disease",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_Diagnostic_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diagnostic_DiseaseName",
                table: "Diagnostic",
                column: "DiseaseName");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnostic_UserId",
                table: "Diagnostic",
                column: "UserId");
        }
    }
}
