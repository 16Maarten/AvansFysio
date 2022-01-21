using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class changedPresenceAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartWednesday",
                table: "Presences",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTuesday",
                table: "Presences",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartThursday",
                table: "Presences",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartMonday",
                table: "Presences",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartFriday",
                table: "Presences",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndWednesday",
                table: "Presences",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTuesday",
                table: "Presences",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndThursday",
                table: "Presences",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndMonday",
                table: "Presences",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndFriday",
                table: "Presences",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Presences",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndFriday", "EndMonday", "EndThursday", "EndTuesday", "EndWednesday", "StartFriday", "StartMonday", "StartThursday", "StartTuesday", "StartWednesday" },
                values: new object[] { new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartWednesday",
                table: "Presences",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTuesday",
                table: "Presences",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartThursday",
                table: "Presences",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartMonday",
                table: "Presences",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartFriday",
                table: "Presences",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndWednesday",
                table: "Presences",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTuesday",
                table: "Presences",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndThursday",
                table: "Presences",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndMonday",
                table: "Presences",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndFriday",
                table: "Presences",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.UpdateData(
                table: "Presences",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndFriday", "EndMonday", "EndThursday", "EndTuesday", "EndWednesday", "StartFriday", "StartMonday", "StartThursday", "StartTuesday", "StartWednesday" },
                values: new object[] { new DateTime(1, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
