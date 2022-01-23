using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangedAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Physiotherapists_PhysiotherapistId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Students_StudentId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PhysiotherapistId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_StudentId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PhysiotherapistId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "PersonEmail",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonEmail",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "PhysiotherapistId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PhysiotherapistId",
                table: "Appointments",
                column: "PhysiotherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_StudentId",
                table: "Appointments",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Physiotherapists_PhysiotherapistId",
                table: "Appointments",
                column: "PhysiotherapistId",
                principalTable: "Physiotherapists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Students_StudentId",
                table: "Appointments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
