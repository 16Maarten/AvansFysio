using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AbstractionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientFiles_Patients_PatientNumber",
                table: "PatientFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientFiles_Physiotherapists_PhysiotherapistId",
                table: "PatientFiles");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Physiotherapists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "IPerson");

            migrationBuilder.RenameColumn(
                name: "PatientNumber",
                table: "PatientFiles",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientFiles_PatientNumber",
                table: "PatientFiles",
                newName: "IX_PatientFiles_PatientId");

            migrationBuilder.AddColumn<int>(
                name: "TherapistId",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Remarks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IntakePersonId",
                table: "PatientFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdentificationNumber",
                table: "IPerson",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_TherapistId",
                table: "Treatments",
                column: "TherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_PersonId",
                table: "Remarks",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_IntakePersonId",
                table: "PatientFiles",
                column: "IntakePersonId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Treatments_TherapistId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Remarks_PersonId",
                table: "Remarks");

            migrationBuilder.DropIndex(
                name: "IX_PatientFiles_IntakePersonId",
                table: "PatientFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IPerson",
                table: "IPerson");

            migrationBuilder.DropColumn(
                name: "TherapistId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Remarks");

            migrationBuilder.DropColumn(
                name: "IntakePersonId",
                table: "PatientFiles");

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
                name: "PatientId",
                table: "PatientFiles",
                newName: "PatientNumber");

            migrationBuilder.RenameIndex(
                name: "IX_PatientFiles_PatientId",
                table: "PatientFiles",
                newName: "IX_PatientFiles_PatientNumber");

            migrationBuilder.AlterColumn<int>(
                name: "IdentificationNumber",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
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
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationNumber = table.Column<int>(type: "int", nullable: false),
                    Img = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Physiotherapist = table.Column<bool>(type: "bit", nullable: false)
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
                    BIGNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Physiotherapists", x => x.Id);
                });

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
        }
    }
}
