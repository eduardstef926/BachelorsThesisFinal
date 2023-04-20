using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewBackend2.Migrations
{
    public partial class EmploymentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPosition",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Doctor");

            migrationBuilder.CreateTable(
                name: "Hospital",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospital", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Employment",
                columns: table => new
                {
                    EmploymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    CurrentPosition = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConsultPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeekDay = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", maxLength: 100, nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employment", x => x.EmploymentId);
                    table.ForeignKey(
                        name: "FK_Employment_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employment_Hospital_HospitalName",
                        column: x => x.HospitalName,
                        principalTable: "Hospital",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employment_DoctorId",
                table: "Employment",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Employment_HospitalName",
                table: "Employment",
                column: "HospitalName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employment");

            migrationBuilder.DropTable(
                name: "Hospital");

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

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Doctor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
