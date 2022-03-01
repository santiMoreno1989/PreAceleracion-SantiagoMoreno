using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiPreAceleracionAlkemy.Migrations
{
    public partial class DeletedInPeliculaEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Peliculas",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Peliculas");
        }
    }
}
