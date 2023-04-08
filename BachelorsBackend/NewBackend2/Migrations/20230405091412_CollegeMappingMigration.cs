using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewBackend2.Migrations
{
    public partial class CollegeMappingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentPosition",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "College",
                columns: table => new
                {
                    CollegeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_College", x => x.CollegeId);
                });

            migrationBuilder.CreateTable(
                name: "DoctorCollegeMapping",
                columns: table => new
                {
                    DoctorCollegeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    CollegeId = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    startYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudyProgram = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_DoctorCollegeMapping_CollegeId",
                table: "DoctorCollegeMapping",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorCollegeMapping_DoctorId",
                table: "DoctorCollegeMapping",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorCollegeMapping");

            migrationBuilder.DropTable(
                name: "College");

            migrationBuilder.DropColumn(
                name: "CurrentPosition",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Doctor");
        }
    }
}
