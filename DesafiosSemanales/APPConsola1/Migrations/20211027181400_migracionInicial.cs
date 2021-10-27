using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APPConsola1.Migrations
{
    public partial class migracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Publicaciones");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "Publicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "Publicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "Publicaciones",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "Publicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "Publicaciones",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Publicaciones",
                table: "Usuarios",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[] { 1, "correo@ejemplo.com", "Santiago", "Nueva1234" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UsuarioId",
                schema: "Publicaciones",
                table: "Comments",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UsuarioId",
                schema: "Publicaciones",
                table: "Posts",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments",
                schema: "Publicaciones");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "Publicaciones");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "Publicaciones");
        }
    }
}
