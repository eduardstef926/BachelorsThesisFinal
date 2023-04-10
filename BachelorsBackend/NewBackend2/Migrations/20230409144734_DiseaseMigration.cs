using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewBackend2.Migrations
{
    public partial class DiseaseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSymptomMapping");

            migrationBuilder.RenameColumn(
                name: "Symptom",
                table: "Symptom",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "Disease",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disease", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Diagnostic",
                columns: table => new
                {
                    DiagnosticId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    DiseaseName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SymptomList = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnostic", x => x.DiagnosticId);
                    table.ForeignKey(
                        name: "FK_Diagnostic_Disease_DiseaseName",
                        column: x => x.DiseaseName,
                        principalTable: "Disease",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diagnostic_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticEntitySymptomEntity",
                columns: table => new
                {
                    DiagnosticsDiagnosticId = table.Column<int>(type: "int", nullable: false),
                    SymptomName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticEntitySymptomEntity", x => new { x.DiagnosticsDiagnosticId, x.SymptomName });
                    table.ForeignKey(
                        name: "FK_DiagnosticEntitySymptomEntity_Diagnostic_DiagnosticsDiagnosticId",
                        column: x => x.DiagnosticsDiagnosticId,
                        principalTable: "Diagnostic",
                        principalColumn: "DiagnosticId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiagnosticEntitySymptomEntity_Symptom_SymptomName",
                        column: x => x.SymptomName,
                        principalTable: "Symptom",
                        principalColumn: "Name",
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

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticEntitySymptomEntity_SymptomName",
                table: "DiagnosticEntitySymptomEntity",
                column: "SymptomName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiagnosticEntitySymptomEntity");

            migrationBuilder.DropTable(
                name: "Diagnostic");

            migrationBuilder.DropTable(
                name: "Disease");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Symptom",
                newName: "Symptom");

            migrationBuilder.CreateTable(
                name: "UserSymptomMapping",
                columns: table => new
                {
                    UserSymptomMappingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SymptomName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSymptomMapping", x => x.UserSymptomMappingId);
                    table.ForeignKey(
                        name: "FK_UserSymptomMapping_Symptom_SymptomName",
                        column: x => x.SymptomName,
                        principalTable: "Symptom",
                        principalColumn: "Symptom",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSymptomMapping_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSymptomMapping_SymptomName",
                table: "UserSymptomMapping",
                column: "SymptomName");

            migrationBuilder.CreateIndex(
                name: "IX_UserSymptomMapping_UserId",
                table: "UserSymptomMapping",
                column: "UserId");
        }
    }
}
