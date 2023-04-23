using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewBackend2.Migrations
{
    public partial class SubscriptionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Send",
                table: "Emails");

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    SubscriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.SubscriptionId);
                    table.ForeignKey(
                        name: "FK_Subscription_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_UserId",
                table: "Subscription",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.AddColumn<bool>(
                name: "Send",
                table: "Emails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
