using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewBackend2.Migrations
{
    public partial class RenameMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorCollegeMapping");

            migrationBuilder.DropIndex(
                name: "IX_Review_DoctorId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_UserId",
                table: "Review");

            migrationBuilder.CreateTable(
                name: "Degree",
                columns: table => new
                {
                    DegreeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    CollegeId = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    StartYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudyField = table.Column<int>(type: "int", nullable: false),
                    StudyProgram = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degree", x => x.DegreeId);
                    table.ForeignKey(
                        name: "FK_Degree_College_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "College",
                        principalColumn: "CollegeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Degree_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_DoctorId",
                table: "Review",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Degree_CollegeId",
                table: "Degree",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Degree_DoctorId",
                table: "Degree",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Degree");

            migrationBuilder.DropIndex(
                name: "IX_Review_DoctorId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_UserId",
                table: "Review");

            migrationBuilder.CreateTable(
                name: "DoctorCollegeMapping",
                columns: table => new
                {
                    DoctorCollegeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollegeId = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    DoctorId = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    StudyProgram = table.Column<int>(type: "int", nullable: false),
                    endYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    startYear = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorCollegeMapping", x => x.DoctorCollegeId);
                    table.ForeignKey(
                        name: "FK_DoctorCollegeMapping_College_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "College",
                        principalColumn: "CollegeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorCollegeMapping_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_DoctorId",
                table: "Review",
                column: "DoctorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorCollegeMapping_CollegeId",
                table: "DoctorCollegeMapping",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorCollegeMapping_DoctorId",
                table: "DoctorCollegeMapping",
                column: "DoctorId");
        }
    }
}
