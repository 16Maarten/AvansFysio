using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Physiotherapists",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "Maarten@gmail.com", "Maarten" });

            migrationBuilder.InsertData(
                table: "Presences",
                columns: new[] { "Id", "EndFriday", "EndMonday", "EndThursday", "EndTuesday", "EndWednesday", "StartFriday", "StartMonday", "StartThursday", "StartTuesday", "StartWednesday" },
                values: new object[] { 2, new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Email", "IdentificationNumber", "Name", "PhoneNumber", "PresenceId" },
                values: new object[] { 1, "Melvin@gmail.com", 5165842, "Melvin", "06435128745", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Presences",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Physiotherapists",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "Peter@fysio-avans.nl", "Peter" });
        }
    }
}
