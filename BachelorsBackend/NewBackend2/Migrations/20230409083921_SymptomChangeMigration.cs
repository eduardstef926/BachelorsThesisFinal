using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewBackend2.Migrations
{
    public partial class SymptomChangeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Symptom_User_UserId",
                table: "Symptom");

            migrationBuilder.DropIndex(
                name: "IX_Symptom_UserId",
                table: "Symptom");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_DoctorId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_UserId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Symptom");

            migrationBuilder.CreateTable(
                name: "UserSymptomMapping",
                columns: table => new
                {
                    UserSymptomMappingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    SymptomName = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                name: "IX_Appointment_DoctorId",
                table: "Appointment",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_UserId",
                table: "Appointment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSymptomMapping_SymptomName",
                table: "UserSymptomMapping",
                column: "SymptomName");

            migrationBuilder.CreateIndex(
                name: "IX_UserSymptomMapping_UserId",
                table: "UserSymptomMapping",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSymptomMapping");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_DoctorId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_UserId",
                table: "Appointment");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Symptom",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Symptom_UserId",
                table: "Symptom",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_DoctorId",
                table: "Appointment",
                column: "DoctorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_UserId",
                table: "Appointment",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Symptom_User_UserId",
                table: "Symptom",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
