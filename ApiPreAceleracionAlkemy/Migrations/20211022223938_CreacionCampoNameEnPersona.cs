using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiPreAceleracionAlkemy.Migrations
{
    public partial class CreacionCampoNameEnPersona : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Personas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Personas");
        }
    }
}
