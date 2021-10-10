using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class changedImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Patients");

            migrationBuilder.AddColumn<byte[]>(
                name: "Img",
                table: "Patients",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
