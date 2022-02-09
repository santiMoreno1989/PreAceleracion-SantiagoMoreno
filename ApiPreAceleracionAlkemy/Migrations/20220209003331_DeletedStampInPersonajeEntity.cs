using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiPreAceleracionAlkemy.Migrations
{
    public partial class DeletedStampInPersonajeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Personajes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedStamp",
                table: "Personajes",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Personajes");

            migrationBuilder.DropColumn(
                name: "DeletedStamp",
                table: "Personajes");
        }
    }
}
