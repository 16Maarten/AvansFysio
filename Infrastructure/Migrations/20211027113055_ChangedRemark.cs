using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangedRemark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Remarks_Students_studentId",
                table: "Remarks");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Students_studentId",
                table: "Treatments");

            migrationBuilder.RenameColumn(
                name: "studentId",
                table: "Treatments",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Treatments_studentId",
                table: "Treatments",
                newName: "IX_Treatments_StudentId");

            migrationBuilder.RenameColumn(
                name: "studentId",
                table: "Remarks",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Remarks_studentId",
                table: "Remarks",
                newName: "IX_Remarks_StudentId");

            migrationBuilder.AddColumn<DateTime>(
                name: "RemarkDate",
                table: "Remarks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Remarks_Students_StudentId",
                table: "Remarks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Students_StudentId",
                table: "Treatments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Remarks_Students_StudentId",
                table: "Remarks");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Students_StudentId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "RemarkDate",
                table: "Remarks");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Treatments",
                newName: "studentId");

            migrationBuilder.RenameIndex(
                name: "IX_Treatments_StudentId",
                table: "Treatments",
                newName: "IX_Treatments_studentId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Remarks",
                newName: "studentId");

            migrationBuilder.RenameIndex(
                name: "IX_Remarks_StudentId",
                table: "Remarks",
                newName: "IX_Remarks_studentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Remarks_Students_studentId",
                table: "Remarks",
                column: "studentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Students_studentId",
                table: "Treatments",
                column: "studentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
