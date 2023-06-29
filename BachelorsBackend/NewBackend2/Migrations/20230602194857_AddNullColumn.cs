using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewBackend2.Migrations
{
    public partial class AddNullColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnostic_Disease_DiseaseName",
                table: "Diagnostic");

            migrationBuilder.AlterColumn<string>(
                name: "DiseaseName",
                table: "Diagnostic",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnostic_Disease_DiseaseName",
                table: "Diagnostic",
                column: "DiseaseName",
                principalTable: "Disease",
                principalColumn: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnostic_Disease_DiseaseName",
                table: "Diagnostic");

            migrationBuilder.AlterColumn<string>(
                name: "DiseaseName",
                table: "Diagnostic",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnostic_Disease_DiseaseName",
                table: "Diagnostic",
                column: "DiseaseName",
                principalTable: "Disease",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
