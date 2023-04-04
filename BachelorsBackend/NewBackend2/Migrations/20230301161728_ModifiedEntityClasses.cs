using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewBackend2.Migrations
{
    public partial class ModifiedEntityClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Doctor_DoctorName",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_User_UserName",
                table: "Appointment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_DoctorName",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_UserName",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Appointment");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Doctor",
                newName: "LastName");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "User",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Password",
                table: "User",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                table: "User",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Doctor",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Password",
                table: "Doctor",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                table: "Doctor",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Appointment",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Appointment",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor",
                column: "DoctorId");

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
                name: "FK_Appointment_Doctor_DoctorId",
                table: "Appointment",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_User_UserId",
                table: "Appointment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Doctor_DoctorId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_User_UserId",
                table: "Appointment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_DoctorId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_UserId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Appointment");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "User",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Doctor",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "Appointment",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Appointment",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_DoctorName",
                table: "Appointment",
                column: "DoctorName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_UserName",
                table: "Appointment",
                column: "UserName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Doctor_DoctorName",
                table: "Appointment",
                column: "DoctorName",
                principalTable: "Doctor",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_User_UserName",
                table: "Appointment",
                column: "UserName",
                principalTable: "User",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
