using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewBackend2.Migrations
{
    public partial class AddConfirmationEmailToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isEmailConfirmed",
                table: "User",
                type: "bit",
                maxLength: 100,
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isEmailConfirmed",
                table: "User");
        }
    }
}
