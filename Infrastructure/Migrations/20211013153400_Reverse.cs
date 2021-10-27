using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Reverse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientFiles_IPerson_IntakePersonId",
                table: "PatientFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientFiles_IPerson_PatientId",
                table: "PatientFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientFiles_IPerson_PhysiotherapistId",
                table: "PatientFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Remarks_IPerson_PersonId",
                table: "Remarks");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_IPerson_TherapistId",
                table: "Treatments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IPerson",
                table: "IPerson");

            migrationBuilder.DropColumn(
                name: "BIGNumber",
                table: "IPerson");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "IPerson");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "IPerson");

            migrationBuilder.DropColumn(
                name: "EmployeeNumber",
                table: "IPerson");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "IPerson");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "IPerson");

            migrationBuilder.DropColumn(
                name: "Physiotherapist",
                table: "IPerson");

            migrationBuilder.DropColumn(
                name: "Physiotherapist_PhoneNumber",
                table: "IPerson");

            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "IPerson");

            migrationBuilder.RenameTable(
                name: "IPerson",
                newName: "Students");

            migrationBuilder.RenameColumn(
                name: "TherapistId",
                table: "Treatments",
                newName: "studentId");

            migrationBuilder.RenameIndex(
                name: "IX_Treatments_TherapistId",
                table: "Treatments",
                newName: "IX_Treatments_studentId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Remarks",
                newName: "studentId");

            migrationBuilder.RenameIndex(
                name: "IX_Remarks_PersonId",
                table: "Remarks",
                newName: "IX_Remarks_studentId");

            migrationBuilder.RenameColumn(
                name: "Student",
                table: "PatientFiles",
                newName: "IsStudent");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "PatientFiles",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "IntakePersonId",
                table: "PatientFiles",
                newName: "PatientNumber");

            migrationBuilder.RenameIndex(
                name: "IX_PatientFiles_PatientId",
                table: "PatientFiles",
                newName: "IX_PatientFiles_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientFiles_IntakePersonId",
                table: "PatientFiles",
                newName: "IX_PatientFiles_PatientNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhysiotherapistId",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Remarks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhysiotherapistId",
                table: "Remarks",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Treatment",
                table: "PatientFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Discription",
                table: "PatientFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdentificationNumber",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentificationNumber = table.Column<int>(type: "int", nullable: false),
                    Img = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Student = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientNumber);
                });

            migrationBuilder.CreateTable(
                name: "Physiotherapists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentificationNumber = table.Column<int>(type: "int", nullable: false),
                    BIGNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Physiotherapists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_PhysiotherapistId",
                table: "Treatments",
                column: "PhysiotherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_PhysiotherapistId",
                table: "Remarks",
                column: "PhysiotherapistId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientFiles_Patients_PatientNumber",
                table: "PatientFiles",
                column: "PatientNumber",
                principalTable: "Patients",
                principalColumn: "PatientNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientFiles_Physiotherapists_PhysiotherapistId",
                table: "PatientFiles",
                column: "PhysiotherapistId",
                principalTable: "Physiotherapists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientFiles_Students_StudentId",
                table: "PatientFiles",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Remarks_Physiotherapists_PhysiotherapistId",
                table: "Remarks",
                column: "PhysiotherapistId",
                principalTable: "Physiotherapists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Remarks_Students_studentId",
                table: "Remarks",
                column: "studentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Physiotherapists_PhysiotherapistId",
                table: "Treatments",
                column: "PhysiotherapistId",
                principalTable: "Physiotherapists",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientFiles_Patients_PatientNumber",
                table: "PatientFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientFiles_Physiotherapists_PhysiotherapistId",
                table: "PatientFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientFiles_Students_StudentId",
                table: "PatientFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Remarks_Physiotherapists_PhysiotherapistId",
                table: "Remarks");

            migrationBuilder.DropForeignKey(
                name: "FK_Remarks_Students_studentId",
                table: "Remarks");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Physiotherapists_PhysiotherapistId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Students_studentId",
                table: "Treatments");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Physiotherapists");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_PhysiotherapistId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Remarks_PhysiotherapistId",
                table: "Remarks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PhysiotherapistId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "PhysiotherapistId",
                table: "Remarks");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "IPerson");

            migrationBuilder.RenameColumn(
                name: "studentId",
                table: "Treatments",
                newName: "TherapistId");

            migrationBuilder.RenameIndex(
                name: "IX_Treatments_studentId",
                table: "Treatments",
                newName: "IX_Treatments_TherapistId");

            migrationBuilder.RenameColumn(
                name: "studentId",
                table: "Remarks",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Remarks_studentId",
                table: "Remarks",
                newName: "IX_Remarks_PersonId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "PatientFiles",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "PatientNumber",
                table: "PatientFiles",
                newName: "IntakePersonId");

            migrationBuilder.RenameColumn(
                name: "IsStudent",
                table: "PatientFiles",
                newName: "Student");

            migrationBuilder.RenameIndex(
                name: "IX_PatientFiles_StudentId",
                table: "PatientFiles",
                newName: "IX_PatientFiles_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientFiles_PatientNumber",
                table: "PatientFiles",
                newName: "IX_PatientFiles_IntakePersonId");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Remarks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Treatment",
                table: "PatientFiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Discription",
                table: "PatientFiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "IPerson",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "IPerson",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "IdentificationNumber",
                table: "IPerson",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "IPerson",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "BIGNumber",
                table: "IPerson",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "IPerson",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "IPerson",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeNumber",
                table: "IPerson",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "IPerson",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Img",
                table: "IPerson",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Physiotherapist",
                table: "IPerson",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Physiotherapist_PhoneNumber",
                table: "IPerson",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentNumber",
                table: "IPerson",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IPerson",
                table: "IPerson",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientFiles_IPerson_IntakePersonId",
                table: "PatientFiles",
                column: "IntakePersonId",
                principalTable: "IPerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientFiles_IPerson_PatientId",
                table: "PatientFiles",
                column: "PatientId",
                principalTable: "IPerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientFiles_IPerson_PhysiotherapistId",
                table: "PatientFiles",
                column: "PhysiotherapistId",
                principalTable: "IPerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Remarks_IPerson_PersonId",
                table: "Remarks",
                column: "PersonId",
                principalTable: "IPerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_IPerson_TherapistId",
                table: "Treatments",
                column: "TherapistId",
                principalTable: "IPerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
