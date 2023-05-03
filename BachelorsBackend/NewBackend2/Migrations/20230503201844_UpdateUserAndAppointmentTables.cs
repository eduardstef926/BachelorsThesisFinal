using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewBackend2.Migrations
{
    public partial class UpdateUserAndAppointmentTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Doctor_DoctorId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_User_UserId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_DoctorId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Cookies");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cookies");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Review",
                newName: "AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_UserId",
                table: "Review",
                newName: "IX_Review_AppointmentId");

            migrationBuilder.AddColumn<int>(
                name: "ConfirmationCode",
                table: "User",
                type: "int",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DoctorEntityDoctorId",
                table: "Review",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserEntityUserId",
                table: "Review",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Cookies",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsReviewed",
                table: "Appointment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Review_DoctorEntityDoctorId",
                table: "Review",
                column: "DoctorEntityDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserEntityUserId",
                table: "Review",
                column: "UserEntityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cookies_UserId",
                table: "Cookies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cookies_User_UserId",
                table: "Cookies",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Appointment_AppointmentId",
                table: "Review",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Doctor_DoctorEntityDoctorId",
                table: "Review",
                column: "DoctorEntityDoctorId",
                principalTable: "Doctor",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_User_UserEntityUserId",
                table: "Review",
                column: "UserEntityUserId",
                principalTable: "User",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cookies_User_UserId",
                table: "Cookies");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Appointment_AppointmentId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Doctor_DoctorEntityDoctorId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_User_UserEntityUserId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_DoctorEntityDoctorId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_UserEntityUserId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Cookies_UserId",
                table: "Cookies");

            migrationBuilder.DropColumn(
                name: "ConfirmationCode",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DoctorEntityDoctorId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "UserEntityUserId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cookies");

            migrationBuilder.DropColumn(
                name: "IsReviewed",
                table: "Appointment");

            migrationBuilder.RenameColumn(
                name: "AppointmentId",
                table: "Review",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_AppointmentId",
                table: "Review",
                newName: "IX_Review_UserId");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Review",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "Cookies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cookies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Review_DoctorId",
                table: "Review",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Doctor_DoctorId",
                table: "Review",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_User_UserId",
                table: "Review",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
