using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PresenceId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PresenceId",
                table: "Physiotherapists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Presences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartMonday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndMonday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTeusday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTeusday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartWednesday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndWednesday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartThursday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndThursday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartFriday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndFriday = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presences", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_PresenceId",
                table: "Students",
                column: "PresenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Physiotherapists_PresenceId",
                table: "Physiotherapists",
                column: "PresenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Physiotherapists_Presences_PresenceId",
                table: "Physiotherapists",
                column: "PresenceId",
                principalTable: "Presences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Presences_PresenceId",
                table: "Students",
                column: "PresenceId",
                principalTable: "Presences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Physiotherapists_Presences_PresenceId",
                table: "Physiotherapists");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Presences_PresenceId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Presences");

            migrationBuilder.DropIndex(
                name: "IX_Students_PresenceId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Physiotherapists_PresenceId",
                table: "Physiotherapists");

            migrationBuilder.DropColumn(
                name: "PresenceId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PresenceId",
                table: "Physiotherapists");
        }
    }
}
