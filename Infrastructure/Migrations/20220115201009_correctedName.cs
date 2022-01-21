using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class correctedName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTeusday",
                table: "Presences",
                newName: "StartTuesday");

            migrationBuilder.RenameColumn(
                name: "EndTeusday",
                table: "Presences",
                newName: "EndTuesday");

            migrationBuilder.InsertData(
                table: "Physiotherapists",
                columns: new[] { "Id", "BIGNumber", "Email", "IdentificationNumber", "Name", "PhoneNumber", "PresenceId" },
                values: new object[] { 8, 123456789, "Peter@fysio-avans.nl", 5165842, "Peter", "061234567", null });

            migrationBuilder.InsertData(
                table: "Presences",
                columns: new[] { "Id", "EndFriday", "EndMonday", "EndThursday", "EndTuesday", "EndWednesday", "StartFriday", "StartMonday", "StartThursday", "StartTuesday", "StartWednesday" },
                values: new object[] { 1, new DateTime(1, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Physiotherapists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Presences",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "StartTuesday",
                table: "Presences",
                newName: "StartTeusday");

            migrationBuilder.RenameColumn(
                name: "EndTuesday",
                table: "Presences",
                newName: "EndTeusday");
        }
    }
}
