using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewBackend2.Migrations
{
    public partial class UpdateDiagnosticTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiagnosticEntitySymptomEntity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_DiagnosticEntitySymptomEntity_SymptomName",
                table: "DiagnosticEntitySymptomEntity",
                column: "SymptomName");
        }
    }
}
