using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Treatment",
                table: "PatientFiles",
                newName: "DescriptionDiagnosticCode");

            migrationBuilder.RenameColumn(
                name: "NumberOfTreatments",
                table: "PatientFiles",
                newName: "DiagnosticCode");

            migrationBuilder.AddColumn<int>(
                name: "PatientFileId",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TreatmentPlanId",
                table: "PatientFiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TreatmentPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfTreatmentsPerWeek = table.Column<int>(type: "int", nullable: false),
                    DurationTreatment = table.Column<int>(type: "int", nullable: false),
                    DiagnosticCode = table.Column<int>(type: "int", nullable: false),
                    DescriptionDiagnosticCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentPlan", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_PatientFileId",
                table: "Treatments",
                column: "PatientFileId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_TreatmentPlanId",
                table: "PatientFiles",
                column: "TreatmentPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientFiles_TreatmentPlan_TreatmentPlanId",
                table: "PatientFiles",
                column: "TreatmentPlanId",
                principalTable: "TreatmentPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_PatientFiles_PatientFileId",
                table: "Treatments",
                column: "PatientFileId",
                principalTable: "PatientFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientFiles_TreatmentPlan_TreatmentPlanId",
                table: "PatientFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_PatientFiles_PatientFileId",
                table: "Treatments");

            migrationBuilder.DropTable(
                name: "TreatmentPlan");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_PatientFileId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_PatientFiles_TreatmentPlanId",
                table: "PatientFiles");

            migrationBuilder.DropColumn(
                name: "PatientFileId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "TreatmentPlanId",
                table: "PatientFiles");

            migrationBuilder.RenameColumn(
                name: "DiagnosticCode",
                table: "PatientFiles",
                newName: "NumberOfTreatments");

            migrationBuilder.RenameColumn(
                name: "DescriptionDiagnosticCode",
                table: "PatientFiles",
                newName: "Treatment");
        }
    }
}
